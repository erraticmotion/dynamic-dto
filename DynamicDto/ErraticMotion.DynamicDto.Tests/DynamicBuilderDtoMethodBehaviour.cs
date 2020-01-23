// <copyright file="DynamicBuilderDtoMethodBehaviour.cs" company="Erratic Motion Ltd">
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
    public class DynamicBuilderDtoMethodBehaviour
        : DynamicBuilderBehaviourBase<IDtoMethod>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderDtoMethodBehaviour"/> class.
        /// </summary>
        public DynamicBuilderDtoMethodBehaviour()
            : base(0)
        {
        }

        /// <summary>
        /// Should not throw generated method.
        /// </summary>
        [Test]
        public void ShouldNotThrowGeneratedMethod()
        {
            Assert.DoesNotThrow(this.Result.Foo);
        }

        /// <inheritdoc />
        protected override IDtoMethod GetResult()
        {
            return Dynamic.Builder.Create<IDtoMethod>();
        }
    }
}