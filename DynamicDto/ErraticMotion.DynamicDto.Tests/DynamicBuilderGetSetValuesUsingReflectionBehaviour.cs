// <copyright file="DynamicBuilderGetSetValuesUsingReflectionBehaviour.cs" company="Erratic Motion Ltd">
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
    /// Test the Dynamic Builder Get and Set Values Using Reflection <c>behaviour</c>.
    /// </summary>
    [TestFixture]
    public class DynamicBuilderGetSetValuesUsingReflectionBehaviour
        : DynamicBuilderBehaviourBase<IDto>
    {
        /// <summary>
        /// The value
        /// </summary>
        private IDto value;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderGetSetValuesUsingReflectionBehaviour"/> class.
        /// </summary>
        public DynamicBuilderGetSetValuesUsingReflectionBehaviour()
            : base(6)
        {
        }

        /// <summary>
        /// Card information should be.
        /// </summary>
        [Test]
        public void CardInfoShouldBe()
        {
            this.Result.CardInfoId.Should().Be(this.value.CardInfoId);
        }

        /// <summary>
        /// Card issue level should be.
        /// </summary>
        [Test]
        public void CardIssueLevelShouldBe()
        {
            this.Result.CardIssueLevel.Should().Be(this.value.CardIssueLevel);
        }

        /// <summary>
        /// Card number should be.
        /// </summary>
        [Test]
        public void CardNumberShouldBe()
        {
            this.Result.CardNumber.Should().Be(this.value.CardNumber);
        }

        /// <summary>
        /// Card number display should be.
        /// </summary>
        [Test]
        public void CardNumberDisplayShouldBe()
        {
            this.Result.CardNumberDisplay.Should().Be(this.value.CardNumberDisplay);
        }

        /// <summary>
        /// Card updated should be.
        /// </summary>
        [Test]
        public void CardUpdatedShouldBe()
        {
            this.Result.CardUpdated.Should().Be(this.value.CardUpdated);
        }

        /// <summary>
        /// Determines whether the is valid should be.
        /// </summary>
        [Test]
        public void IsValidShouldBe()
        {
            this.Result.IsValid.Should().Be(this.value.IsValid);
        }

        /// <summary>
        /// Determines whether is serializable should be.
        /// </summary>
        [Test]
        public void IsSerializableShouldBe()
        {
            this.Result.GetType().IsSerializable.Should().BeTrue();
        }

        /// <summary>
        /// Determines whether is sealed should be.
        /// </summary>
        [Test]
        public void IsSealedShouldBe()
        {
            this.Result.GetType().IsSealed.Should().BeTrue();
        }

        /// <summary>
        /// Sanity check the on object that was proxied.
        /// </summary>
        [Test]
        public void SanityOnObjThatWasProxied()
        {
            this.value.GetType().IsSerializable.Should().BeFalse();
            this.value.GetType().IsSealed.Should().BeFalse();
        }

        /// <inheritdoc />
        protected override IDto GetResult()
        {
            this.value = new DataTransferObject
                             {
                                 CardInfoId = 12,
                                 CardNumber = 11223344,
                                 IsValid = true,
                                 CardIssueLevel = 0xFF,
                                 CardUpdated = new DateTime(2014, 7, 4),
                                 CardNumberDisplay = "card number",
                             };

            var result = Dynamic.Builder.Create<IDto>();
            foreach (var valueProperty in this.value.GetType().GetProperties())
            {
                var v = valueProperty.GetValue(this.value, new object[0]);
                var sutProperty = result.GetType().GetProperty(valueProperty.Name);

                // ReSharper disable once PossibleNullReferenceException
                sutProperty.SetValue(result, v, new object[0]);
            }

            return result;
        }
    }
}
