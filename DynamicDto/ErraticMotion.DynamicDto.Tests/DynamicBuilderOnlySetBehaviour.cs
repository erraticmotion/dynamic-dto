// <copyright file="DynamicBuilderOnlySetBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using DynamicDto.Objects;

    using NUnit.Framework;

    /// <summary>
    /// Test the Dynamic Builder with only Set <c>behaviour</c>.
    /// </summary>
    public class DynamicBuilderOnlySetBehaviour
        : DynamicBuilderBehaviourBase<IDtoWithOnlySet>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderOnlySetBehaviour"/> class.
        /// </summary>
        public DynamicBuilderOnlySetBehaviour()
            : base(0)
        {
        }

        /// <inheritdoc />
        protected override IDtoWithOnlySet GetResult()
        {
            return Dynamic.Builder.Create<IDtoWithOnlySet>();
        }

        /// <summary>
        /// Tests the can set value.
        /// </summary>
        [Test]
        public void TestCanSetValue()
        {
            this.Result.OnlySet = 18;
        }
    }
}
