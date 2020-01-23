// <copyright file="DynamicBuilder.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    /// <summary>
    /// Contains the IL instructions to build a DTO type. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="IDynamicBuilder" />
    internal class DynamicBuilder : IDynamicBuilder
    {
        /// <summary>
        /// The sink implementations
        /// </summary>
        private static readonly Dictionary<Type, Type> SinkImplementations = new Dictionary<Type, Type>();

        /// <summary>
        /// The assembly builder
        /// </summary>
        private readonly AssemblyBuilder assemblyBuilder;

        /// <summary>
        /// The module builder
        /// </summary>
        private readonly ModuleBuilder moduleBuilder;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilder" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public DynamicBuilder(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            var appDomain = AppDomain.CurrentDomain;
            var assemblyName = new AssemblyName
            {
                Name = this.configuration.DynamicAssemblyName,
                Version = new Version(0, 0, 1, 0),
                CultureInfo = CultureInfo.InvariantCulture,
                Flags = AssemblyNameFlags.None,
            };

            // Does not save the assembly automatically, still need to
            // call Save on the assembly builder, but don't do it for
            // production as you can't add further types to a dynamic
            // assembly after it's been saved.
            this.assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            this.moduleBuilder = this.assemblyBuilder.DefineDynamicModule(this.configuration.DynamicAssemblyName, this.configuration.DynamicAssemblyName + ".dll");
        }

        /// <inheritdoc />
        public T Create<T>()
            where T : class
        {
            return (T)Activator.CreateInstance(this.GetType<T>());
        }

        /// <inheritdoc />
        public T Create<T>(object item)
            where T : class
        {
            return (T)Activator.CreateInstance(this.GetType<T>(), item);
        }

        /// <inheritdoc />
        public IEnumerable<Type> KnownTypes()
        {
            return SinkImplementations.Values;
        }

        private Type GetType<T>()
            where T : class
        {
            var type = typeof(T);
            if (!type.IsInterface)
            {
                throw new ArgumentException("Type is not an Interface");
            }

            var options = new BuilderHelper<T>(this.configuration.DynamicTypePrefix, this.configuration.Namespace);
            return this.Create(options);
        }

        private Type Create<T>(BuilderHelper<T> options)
            where T : class
        {
            var type = typeof(T);
            if (!SinkImplementations.ContainsKey(type))
            {
                this.CreateTypeFor(type, options, SinkImplementations);
            }

            return SinkImplementations[type];
        }

        private void CreateTypeFor<T>(Type type, BuilderHelper<T> options, IDictionary<Type, Type> storage)
            where T : class
        {
            const TypeAttributes typeAtts = TypeAttributes.Class |
                                            TypeAttributes.Public |
                                            TypeAttributes.Serializable |
                                            TypeAttributes.Sealed |
                                            TypeAttributes.BeforeFieldInit;

            var typeBuilder = this.moduleBuilder.DefineType(
                options.TypeName,
                typeAtts,
                options.BaseType);

            typeBuilder.AddInterfaceImplementation(type);
            typeBuilder.SetCustomAttribute(options.DataContractBuilder);

            var baseConstructorInfo = options.BaseTypeConstructorInfo;

            // Default empty constructor
            var defaultCtorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                Type.EmptyTypes);

            var ilDefaultCtorGenerator = defaultCtorBuilder.GetILGenerator();
            ilDefaultCtorGenerator.Emit(OpCodes.Ldarg_0);
            ilDefaultCtorGenerator.Emit(OpCodes.Call, baseConstructorInfo);
            ilDefaultCtorGenerator.Emit(OpCodes.Nop);
            ilDefaultCtorGenerator.Emit(OpCodes.Nop);
            ilDefaultCtorGenerator.Emit(OpCodes.Nop);
            ilDefaultCtorGenerator.Emit(OpCodes.Ret);

            // Constructor that takes in the interface type
            var objCtorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                new[] { type });

            var ilObjCtorGenerator = objCtorBuilder.GetILGenerator();
            ilObjCtorGenerator.Emit(OpCodes.Ldarg_0);
            ilObjCtorGenerator.Emit(OpCodes.Call, defaultCtorBuilder);
            ilObjCtorGenerator.Emit(OpCodes.Nop);
            ilObjCtorGenerator.Emit(OpCodes.Nop);

            var compilerGeneratedCtor = typeof(CompilerGeneratedAttribute).GetConstructor(new Type[0]);
            Debug.Assert(compilerGeneratedCtor != null);

            var compilerGeneratedAttribute = new CustomAttributeBuilder(compilerGeneratedCtor, new object[0]);

            var dataMemberCtor = typeof(DataMemberAttribute).GetConstructor(new Type[0]);
            Debug.Assert(dataMemberCtor != null);

            const MethodAttributes getSetAtts =
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig;

            const MethodAttributes getSetExplicitAtts =
                getSetAtts |
                MethodAttributes.NewSlot |
                MethodAttributes.Virtual |
                MethodAttributes.Final;

            void EmitSet(ILGenerator g, FieldBuilder f)
            {
                g.Emit(OpCodes.Ldarg_0);
                g.Emit(OpCodes.Ldarg_1);
                g.Emit(OpCodes.Stfld, f);
                g.Emit(OpCodes.Ret);
            }

            void EmitGet(ILGenerator g, FieldBuilder f)
            {
                g.Emit(OpCodes.Ldarg_0);
                g.Emit(OpCodes.Ldfld, f);
                g.Emit(OpCodes.Ret);
            }

            MethodBuilder GetGetBuilder(string n, MethodAttributes a, Type t) => typeBuilder.DefineMethod(n, a, t, Type.EmptyTypes);

            MethodBuilder GetSetBuilder(string n, MethodAttributes a, Type t) => typeBuilder.DefineMethod(n, a, typeof(void), new[] { t });

            FieldBuilder GetBackingField(string n, Type a) => typeBuilder.DefineField("<" + n + ">k__BackingField", a, FieldAttributes.Private);

            var methods = new List<MethodInfo>(type.GetAll(x => x.GetMethods()));
            var properties = new List<PropertyInfo>(type.GetAll(x => x.GetProperties()));
            foreach (var pi in properties)
            {
                var piName = pi.Name;
                var propertyType = pi.PropertyType;

                var hasSet = false;
                var hasGet = false;

                var propertyBuilder = typeBuilder.DefineProperty(
                    piName,
                    PropertyAttributes.HasDefault,
                    CallingConventions.HasThis,
                    propertyType,
                    null);

                var field = GetBackingField(piName, propertyType);
                field.SetCustomAttribute(compilerGeneratedAttribute);

                var getMethod = pi.GetGetMethod();
                if (getMethod != null)
                {
                    hasGet = true;

                    var getBuilder = GetGetBuilder(getMethod.Name, getSetAtts, propertyType);
                    EmitGet(getBuilder.GetILGenerator(), field);
                    propertyBuilder.SetGetMethod(getBuilder);

                    var getExplicitBuilder = GetGetBuilder(
                        type.Name + "." + getMethod.Name,
                        getSetExplicitAtts,
                        propertyType);
                    EmitGet(getExplicitBuilder.GetILGenerator(), field);
                    typeBuilder.DefineMethodOverride(getExplicitBuilder, getMethod);
                }

                var setMethod = pi.GetSetMethod();
                string setMethodName;
                if (setMethod != null)
                {
                    hasSet = true;
                    setMethodName = setMethod.Name;
                }
                else
                {
                    // Must have a get method otherwise it wouldn't be a property.
                    setMethodName = pi.GetGetMethod().Name.Replace("set_", "get_");
                }

                var setBuilder = GetSetBuilder(setMethodName, getSetAtts, propertyType);
                EmitSet(setBuilder.GetILGenerator(), field);
                propertyBuilder.SetSetMethod(setBuilder);

                // Keep to the interface contract, only add a set to the interface implementation
                if (hasSet)
                {
                    var setExplicitBuilder = GetSetBuilder(
                        type.Name + "." + setMethod.Name,
                        getSetExplicitAtts,
                        propertyType);
                    EmitSet(setExplicitBuilder.GetILGenerator(), field);
                    typeBuilder.DefineMethodOverride(setExplicitBuilder, setMethod);
                }

                if (hasGet)
                {
                    // property for serialization, apply the DataMember attribute
                    // to the property.
                    propertyBuilder.SetCustomAttribute(
                        new CustomAttributeBuilder(dataMemberCtor, new object[0]));

                    // Emit property set in the ctor that takes an instance of the
                    // interface that this type supports.
                    ilObjCtorGenerator.Emit(OpCodes.Nop);
                    ilObjCtorGenerator.Emit(OpCodes.Ldarg_0);
                    ilObjCtorGenerator.Emit(OpCodes.Ldarg_1);
                    ilObjCtorGenerator.Emit(OpCodes.Callvirt, pi.GetGetMethod());
                    ilObjCtorGenerator.Emit(OpCodes.Call, propertyBuilder.GetSetMethod());
                }

                if (hasGet)
                {
                    methods.Remove(getMethod);
                }

                if (hasSet)
                {
                    methods.Remove(setMethod);
                }
            }

            // Complete the ctor that takes the interface type as an argument.
            ilObjCtorGenerator.Emit(OpCodes.Nop);
            ilObjCtorGenerator.Emit(OpCodes.Nop);
            ilObjCtorGenerator.Emit(OpCodes.Ret);

            // Methods are no-ops; if there is a 
            // return value, return a default value or null
            foreach (var methodInfo in methods)
            {
                var returnType = methodInfo.ReturnType;
                var methodBuilder = typeBuilder.DefineMethod(
                    methodInfo.Name,
                    MethodAttributes.Public | MethodAttributes.Virtual,
                    returnType,
                    methodInfo.GetParameters().Select(parameterInfo => parameterInfo.ParameterType).ToArray());

                ilDefaultCtorGenerator = methodBuilder.GetILGenerator();

                if (returnType != typeof(void))
                {
                    var localBuilder = ilDefaultCtorGenerator.DeclareLocal(returnType);
                    ilDefaultCtorGenerator.Emit(OpCodes.Ldloc, localBuilder);
                }

                ilDefaultCtorGenerator.Emit(OpCodes.Ret);
                typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
            }

            // Define override ToString() for diagnostic purposes
            var sB = typeBuilder.DefineMethod(
                "ToString", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(string), Type.EmptyTypes);

            var sG = sB.GetILGenerator();
            sG.Emit(OpCodes.Nop);

// ReSharper disable AssignNullToNotNullAttribute
            sG.Emit(OpCodes.Newobj, typeof(System.Text.StringBuilder).GetConstructor(Type.EmptyTypes));

// ReSharper restore AssignNullToNotNullAttribute

            // Ldloc_0
            sG.Emit(OpCodes.Stloc_0, sG.DeclareLocal(typeof(System.Text.StringBuilder)));

            sG.Emit(OpCodes.Ldloc_0);
            sG.Emit(OpCodes.Ldstr, options.TypeName + " : " + type.FullName + " {");
            sG.Emit(OpCodes.Callvirt, typeof(System.Text.StringBuilder).GetMethod("AppendLine", new[] { typeof(string) }));
            sG.Emit(OpCodes.Pop);

            foreach (var p in from p in new List<PropertyInfo>(type.GetAll(x => x.GetProperties())) 
                              let getM = p.GetGetMethod()
                              where getM != null
                              where !getM.ReturnType.IsClass
                                && !getM.ReturnType.IsInterface
                              select p)
            {
                sG.Emit(OpCodes.Ldloc_0);
                if (p.PropertyType == typeof(byte))
                {
                    sG.Emit(OpCodes.Ldstr, "  " + p.Name + ": 0x{0:x2}\n");
                }
                else
                {
                    sG.Emit(OpCodes.Ldstr, "  " + p.Name + ": {0}\n");
                }

                sG.Emit(OpCodes.Ldarg_0);
                sG.Emit(OpCodes.Call, p.GetGetMethod());
                sG.Emit(OpCodes.Box, p.PropertyType);
                sG.Emit(OpCodes.Callvirt, typeof(System.Text.StringBuilder).GetMethod("AppendFormat", new[] { typeof(string), typeof(object) }));
                sG.Emit(OpCodes.Pop);
            }

            sG.Emit(OpCodes.Ldloc_0);
            sG.Emit(OpCodes.Ldstr, "} // " + options.TypeName);
            sG.Emit(OpCodes.Callvirt, typeof(System.Text.StringBuilder).GetMethod("AppendLine", new[] { typeof(string) }));
            sG.Emit(OpCodes.Pop);

            sG.Emit(OpCodes.Ldloc_0);
            sG.Emit(OpCodes.Callvirt, typeof(System.Text.StringBuilder).GetMethod("ToString", Type.EmptyTypes));
            sG.Emit(OpCodes.Ret);

            var createdType = typeBuilder.CreateType();
            storage[type] = createdType;
        }
    }
}
