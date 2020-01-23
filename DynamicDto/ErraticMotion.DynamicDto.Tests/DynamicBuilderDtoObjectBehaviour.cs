// <copyright file="DynamicBuilderDtoObjectBehaviour.cs" company="Erratic Motion Ltd">
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
    public class DynamicBuilderDtoObjectBehaviour
        : DynamicBuilderBehaviourBase<IDtoWithObjects>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderDtoObjectBehaviour"/> class.
        /// </summary>
        public DynamicBuilderDtoObjectBehaviour()
            : base(2)
        {
        }

        /// <summary>
        /// Tests with objects.
        /// </summary>
        [Test]
        public void TestWithObjects()
        {
            this.Result.SubInterface = this.Result;
            this.Result.SubObject = new DataTransferObject { CardInfoId = 12 };

            this.Result.SubInterface.Should().Be(this.Result);
        }

        /// <inheritdoc />
        protected override IDtoWithObjects GetResult()
        {
            return Dynamic.Builder.Create<IDtoWithObjects>();
        }
    }
}