// <copyright file="DynamicBuilderVerifyExtensions.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System.Reflection;
    using System.Runtime.Serialization;

    using FluentAssertions;
    using NUnit.Framework;

    /// <summary>
    /// Contains assert methods for the dynamic builder result object.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal static class DynamicBuilderVerifyExtensions
    {
        /// <summary>
        /// Verifies the contract. Checks that the type supports  is
        /// decorated with the <see cref="DataContractAttribute"/> and
        /// that all properties are decorated with the <see cref="DataMemberAttribute"/>
        /// </summary>
        /// <param name="sut">The Software Under Test.</param>
        /// <param name="expectedDataMembers">
        /// The number of properties expected to have been decorated
        /// with the <see cref="DataMemberAttribute"/>.
        /// </param>
        internal static void VerifyContract(this object sut, int expectedDataMembers)
        {
            sut.IsDecoratedWithDataContract();
            sut.GetType().GetProperties().VerifyDataMemberContract(expectedDataMembers);
        }

        /// <summary>
        /// Determines whether the SUT has been decorated with data contract.
        /// </summary>
        /// <param name="sut">The SUT.</param>
        internal static void IsDecoratedWithDataContract(this object sut)
        {
            var atts = sut.GetType().GetCustomAttributes(typeof(DataContractAttribute), false);
            atts.Length.Should().Be(1, "Object is not decorated with the DataContract attribute");
        }

        /// <summary>
        /// Verifies the data member contract.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="expectedDataMembers">The expected data members.</param>
        internal static void VerifyDataMemberContract(this PropertyInfo[] values, int expectedDataMembers)
        {
            var count = 0;
            foreach (var value in values)
            {
                var atts = value.GetCustomAttributes(true);
                var found = false;
                foreach (var att in atts)
                {
                    if (att is DataMemberAttribute)
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    count++;
                }
            }

            Assert.True(
                count == expectedDataMembers, 
                $"Number of DataMemberAttributes {count} does not match expected {expectedDataMembers}");
        }
    }
}