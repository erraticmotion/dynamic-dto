// <copyright file="DynamicBuilderGetSetValuesUsingCtorBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;

    using DynamicDto.Objects;

    using FluentAssertions;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Expected <c>behaviour</c> when the DTO has is constructed
    /// with an object that supports the interface.
    /// </summary>
    [TestFixture]
    public class DynamicBuilderGetSetValuesUsingCtorBehaviour
        : DynamicBuilderBehaviourBase<IDto>
    {
        /// <summary>
        /// The original item
        /// </summary>
        private IDto item;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderGetSetValuesUsingCtorBehaviour"/> class.
        /// </summary>
        public DynamicBuilderGetSetValuesUsingCtorBehaviour()
            : base(6)
        {
        }

        /// <summary>
        /// Assert that the expected results have occurred.
        /// </summary>
        [Test]
        public void CanSetPropertiesUsingCtor()
        {
            this.Result.CardInfoId.Should().Be(this.item.CardInfoId);
            this.Result.CardIssueLevel.Should().Be(this.item.CardIssueLevel);
            this.Result.CardNumber.Should().Be(this.item.CardNumber);
            this.Result.CardNumberDisplay.Should().Be(this.item.CardNumberDisplay);
            this.Result.CardUpdated.Should().Be(this.item.CardUpdated);
            this.Result.IsValid.Should().Be(this.item.IsValid);
        }

        /// <inheritdoc />
        protected override IDto GetResult()
        {
            this.item = Mock.Of<IDto>(x =>
                x.CardInfoId == 10 &&
                x.CardIssueLevel == 0xDE &&
                x.CardNumberDisplay == "1234567890" &&
                x.CardNumber == 1234567890 &&
                x.CardUpdated == new DateTime(2014, 7, 4));

            return Dynamic.Builder.Create<IDto>(this.item);
        }
    }
}
