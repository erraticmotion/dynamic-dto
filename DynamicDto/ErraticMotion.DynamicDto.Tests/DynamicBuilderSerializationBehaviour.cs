// <copyright file="DynamicBuilderSerializationBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using FluentAssertions;
    using NUnit.Framework;

    /// <summary>
    /// Tests the Dynamic Builder Serialization <c>behaviour</c>.
    /// </summary>
    /// <seealso cref="DynamicBuilderSerializationBase" />
    [TestFixture]
    public class DynamicBuilderSerializationBehaviour
        : DynamicBuilderSerializationBase
    {
        /// <summary>
        /// Verifies the result.
        /// </summary>
        [Test]
        public void VerifyResult()
        {
            this.Result.GetType().Name.Should().Contain("___IDto");
        }
    }
}
