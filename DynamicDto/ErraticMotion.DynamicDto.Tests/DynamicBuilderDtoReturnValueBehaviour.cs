// <copyright file="DynamicBuilderDtoReturnValueBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using DynamicDto.Objects;

    using FluentAssertions;
    using NUnit.Framework;

    /// <summary>
    /// Test the Dynamic Builder DTO Arguments <c>behaviour</c>.
    /// </summary>
    [TestFixture]
    public class DynamicBuilderDtoReturnValueBehaviour
        : DynamicBuilderBehaviourBase<IDtoReturnValuesObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderDtoReturnValueBehaviour"/> class.
        /// </summary>
        public DynamicBuilderDtoReturnValueBehaviour()
            : base(0)
        {
        }

        /// <summary>
        /// Tests the with methods return values object.
        /// </summary>
        [Test]
        public void TestWithMethodsReturnValuesObject()
        {
            var result = this.Result.Create();
            result.Should().BeNull();
        }

        /// <inheritdoc />
        protected override IDtoReturnValuesObject GetResult()
        {
            return Dynamic.Builder.Create<IDtoReturnValuesObject>();
        }
    }
}