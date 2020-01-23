// <copyright file="DynamicBuilderGetSetBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;

    using DynamicDto.Objects;

    using FluentAssertions;
    using NUnit.Framework;

    /// <summary>
    /// Expected <c>behaviour</c> when the DTO has 7 read/write properties.
    /// </summary>
    [TestFixture]
    public class DynamicBuilderGetSetBehaviour
        : DynamicBuilderBehaviourBase<IDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderGetSetBehaviour"/> class.
        /// </summary>
        public DynamicBuilderGetSetBehaviour()
            : base(6)
        {
        }

        /// <summary>
        /// Should assign card information identifier value.
        /// </summary>
        [Test]
        public void ShouldAssignCardInfoIdValue()
        {
            this.Result.CardInfoId = int.MinValue;
            this.Result.CardInfoId.Should().Be(int.MinValue);
            this.Result.CardInfoId = int.MaxValue;
            this.Result.CardInfoId.Should().Be(int.MaxValue);
        }

        /// <summary>
        /// Should assign card number value.
        /// </summary>
        [Test]
        public void ShouldAssignCardNumberValue()
        {
            this.Result.CardNumber = long.MinValue;
            this.Result.CardNumber.Should().Be(long.MinValue);
            this.Result.CardNumber = long.MaxValue;
            this.Result.CardNumber.Should().Be(long.MaxValue);
        }

        /// <summary>
        /// Should assign card issue level value.
        /// </summary>
        [Test]
        public void ShouldAssignCardIssueLevelValue()
        {
            this.Result.CardIssueLevel = byte.MinValue;
            this.Result.CardIssueLevel.Should().Be(byte.MinValue);
            this.Result.CardIssueLevel = byte.MaxValue;
            this.Result.CardIssueLevel.Should().Be(byte.MaxValue);
        }

        /// <summary>
        /// Should assign be is valid value.
        /// </summary>
        [Test]
        public void ShouldAssignBeIsValidValue()
        {
            this.Result.IsValid = false;
            this.Result.IsValid.Should().BeFalse();
            this.Result.IsValid = true;
            this.Result.IsValid.Should().BeTrue();
        }

        /// <summary>
        /// Should assign card updated value.
        /// </summary>
        [Test]
        public void ShouldAssignCardUpdatedValue()
        {
            this.Result.CardUpdated = DateTime.MinValue;
            this.Result.CardUpdated.Should().Be(DateTime.MinValue);
            this.Result.CardUpdated = DateTime.MaxValue;
            this.Result.CardUpdated.Should().Be(DateTime.MaxValue);
        }

        /// <summary>
        /// Should assign card number display value.
        /// </summary>
        [Test]
        public void ShouldAssignCardNumberDisplayValue()
        {
            this.Result.CardNumberDisplay = "This is the lows";
            this.Result.CardNumberDisplay.Should().Be("This is the lows");
            this.Result.CardNumberDisplay = "This is the highs";
            this.Result.CardNumberDisplay.Should().Be("This is the highs");
        }

        /// <inheritdoc />
        protected override IDto GetResult()
        {
            return Dynamic.Builder.Create<IDto>();
        }
    }
}
