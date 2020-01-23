// <copyright file="DynamicBuilderOnlyGetBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;

    using DynamicDto.Objects;

    using FluentAssertions;
    using Moq;

    using Newtonsoft.Json;

    using NUnit.Framework;

    /// <summary>
    /// Test the Dynamic Builder with only Get <c>behaviour</c>.
    /// </summary>
    public class DynamicBuilderOnlyGetBehaviour
        : DynamicBuilderBehaviourBase<IDtoWithOnlyGet>
    {
        /// <summary>
        /// The object
        /// </summary>
        private IDtoWithOnlyGet obj;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderOnlyGetBehaviour"/> class.
        /// </summary>
        public DynamicBuilderOnlyGetBehaviour()
            : base(1)
        {
        }

        /// <summary>
        /// Should get default value.
        /// </summary>
        [Test]
        public void ShouldGetDefaultValue()
        {
            var i = this.Result.OnlyGet;
            i.Should().Be(12);
        }

        /// <inheritdoc />
        protected override IDtoWithOnlyGet GetResult()
        {
            this.obj = Mock.Of<IDtoWithOnlyGet>(x => x.OnlyGet == 12);
            return Dynamic.Builder.Create<IDtoWithOnlyGet>(this.obj);
        }
    }
}
