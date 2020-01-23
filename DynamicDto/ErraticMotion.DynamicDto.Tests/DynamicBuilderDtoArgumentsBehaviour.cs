// <copyright file="DynamicBuilderDtoArgumentsBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;

    using DynamicDto.Objects;
    using NUnit.Framework;

    /// <summary>
    /// Test the Dynamic Builder DTO Arguments <c>behaviour</c>.
    /// </summary>
    [TestFixture]
    public class DynamicBuilderDtoArgumentsBehaviour
        : DynamicBuilderBehaviourBase<IDtoArguments>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderDtoArgumentsBehaviour"/> class.
        /// </summary>
        public DynamicBuilderDtoArgumentsBehaviour()
            : base(0)
        {
        }

        /// <summary>
        /// Tests the with methods arguments.
        /// </summary>
        [Test]
        public void TestWithMethodsArguments()
        {
            Assert.DoesNotThrow(() => this.Result.TestMethod(1, 2, false, DateTime.Now, new DataTransferObject()));
        }

        /// <inheritdoc />
        protected override IDtoArguments GetResult()
        {
            return Dynamic.Builder.Create<IDtoArguments>();
        }
    }
}