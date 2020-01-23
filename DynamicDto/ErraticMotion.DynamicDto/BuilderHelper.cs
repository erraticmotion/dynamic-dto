// <copyright file="BuilderHelper.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.Serialization;

    /// <summary>
    /// Got Helper in its class name, so don't know what responsibilities it has.
    /// Needs looking at again and sorting.
    /// </summary>
    /// <typeparam name="T">The dynamic type to build.</typeparam>
    internal class BuilderHelper<T>
        where T : class
    {
        /// <summary>
        /// The builder namespace
        /// </summary>
        private readonly string builderNamespace;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderHelper{T}" /> class.
        /// </summary>
        /// <param name="dynamicTypePrefix">The dynamic type prefix.</param>
        /// <param name="builderNamespace">The builder namespace.</param>
        public BuilderHelper(string dynamicTypePrefix, string builderNamespace)
        {
            this.TypeName = this.GetTypeName(dynamicTypePrefix); // typeName;
            this.builderNamespace = builderNamespace;
        }

        /// <summary>
        /// Gets the data contract builder.
        /// </summary>
        /// <value>The data contract builder.</value>
        public CustomAttributeBuilder DataContractBuilder
        {
            get
            {
                var dataContractCtor = typeof(DataContractAttribute).GetConstructor(new Type[0]);
                Debug.Assert(dataContractCtor != null, "data contract constructor info was null");

                var nameSpace = typeof(DataContractAttribute).GetProperty("Namespace");
                return new CustomAttributeBuilder(
                    dataContractCtor,
                    new object[0],
                    new[] { nameSpace },
                    new object[] { this.builderNamespace });
            }
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <value>The name of the type.</value>
        public string TypeName { get; }

        /// <summary>
        /// Gets the type of the base.
        /// </summary>
        /// <value>The type of the base.</value>
        public Type BaseType => typeof(object);

        /// <summary>
        /// Gets the base type constructor information.
        /// </summary>
        /// <value>The base type constructor information.</value>
        public ConstructorInfo BaseTypeConstructorInfo
        {
            get
            {
                var baseConstructorInfo = this.BaseType.GetConstructor(Type.EmptyTypes);
                Debug.Assert(baseConstructorInfo != null, "base constructor info was null");
                return baseConstructorInfo;
            }
        }

        private string GetTypeName(string dynamicTypePrefix)
        {
            var type = typeof(T);
            var result = dynamicTypePrefix + type.Name;
            if (type.IsGenericType)
            {
                result = type.GetGenericArguments().Aggregate(
                    result,
                    (current, g) => current + ("<" + g.Name + ">"));
            }

            return result;
        }
    }
}