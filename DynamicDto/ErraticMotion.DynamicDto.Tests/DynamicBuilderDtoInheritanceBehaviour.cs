// <copyright file="DynamicBuilderDtoInheritanceBehaviour.cs" company="Erratic Motion Ltd">
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
    public class DynamicBuilderDtoInheritanceBehaviour
        : DynamicBuilderBehaviourBase<IDtoExtends>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderDtoInheritanceBehaviour"/> class.
        /// </summary>
        public DynamicBuilderDtoInheritanceBehaviour()
            : base(2)
        {
        }

        /// <summary>
        /// Tests the inheritance.
        /// </summary>
        [Test]
        public void TestInheritance()
        {
            Assert.DoesNotThrow(this.Result.BaseMethod);
            Assert.DoesNotThrow(this.Result.SuperMethod);

            this.Result.CardInfoId = 4;
            this.Result.CardNumber = -333;

            this.Result.CardInfoId.Should().Be(4);
            this.Result.CardNumber.Should().Be(-333);
        }

        /// <inheritdoc />
        protected override IDtoExtends GetResult()
        {
            return Dynamic.Builder.Create<IDtoExtends>();
        }
    }
}