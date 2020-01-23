// <copyright file="DynamicBuilderDeserializationBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using DynamicDto.Objects;
    using FluentAssertions;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// Tests the Dynamic Builder deserialization <c>behaviour</c>.
    /// </summary>
    /// <seealso cref="DynamicBuilderSerializationBase" />
    [TestFixture]
    public class DynamicBuilderDeserializationBehaviour
        : DynamicBuilderSerializationBase
    {
        /// <summary>
        /// Verifies the result.
        /// </summary>
        [Test]
        public void VerifyResult()
        {
            var r = (IDto)JsonConvert.DeserializeObject<DataTransferObject>(this.Obj);
            r.CardInfoId.Should().Be(this.Result.CardInfoId);
            r.CardIssueLevel.Should().Be(this.Result.CardIssueLevel);
            r.CardNumber.Should().Be(this.Result.CardNumber);
            r.CardNumberDisplay.Should().Be(this.Result.CardNumberDisplay);
            r.CardUpdated.Should().Be(this.Result.CardUpdated);
            r.IsValid.Should().Be(this.Result.IsValid);
        }
    }
}