// <copyright file="DynamicBuilderDtoMethodsBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using DynamicDto.Objects;
    using NUnit.Framework;

    /// <summary>
    /// Test the Dynamic Builder DTO Method <c>behaviour</c>.
    /// </summary>
    [TestFixture]
    public class DynamicBuilderDtoMethodsBehaviour
        : DynamicBuilderBehaviourBase<IDtoMethods>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderDtoMethodsBehaviour"/> class.
        /// </summary>
        public DynamicBuilderDtoMethodsBehaviour()
            : base(0)
        {
        }

        /// <summary>
        /// Tests the with methods return values primitive.
        /// </summary>
        [Test]
        public void TestWithMethodsReturnValuesPrimitive()
        {
            Assert.DoesNotThrow(() => this.Result.IsValid());
            Assert.DoesNotThrow(() => this.Result.CardUpdated());
            Assert.DoesNotThrow(() => this.Result.CardInfoID());
            Assert.DoesNotThrow(() => this.Result.CardNumber());
            Assert.DoesNotThrow(() => this.Result.CardNumberDisplay());
        }

        /// <inheritdoc />
        protected override IDtoMethods GetResult()
        {
            return Dynamic.Builder.Create<IDtoMethods>();
        }
    }
}