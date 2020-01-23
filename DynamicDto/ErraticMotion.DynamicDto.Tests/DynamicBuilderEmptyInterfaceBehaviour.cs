// <copyright file="DynamicBuilderEmptyInterfaceBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using DynamicDto.Objects;
    using NUnit.Framework;

    /// <summary>
    /// Expected <c>behaviour</c> when the DTO has no members.
    /// </summary>
    [TestFixture]
    public class DynamicBuilderEmptyInterfaceBehaviour
        : DynamicBuilderBehaviourBase<IDtoEmpty>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderEmptyInterfaceBehaviour"/> class.
        /// </summary>
        public DynamicBuilderEmptyInterfaceBehaviour()
            : base(0)
        {
        }

        /// <inheritdoc />
        protected override IDtoEmpty GetResult()
        {
            return Dynamic.Builder.Create<IDtoEmpty>();
        }
    }
}
