// <copyright file="DynamicBuilderSerializationBase.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;
    using System.IO;

    using DynamicDto.Objects;

    using Moq;

    using Newtonsoft.Json;

    using NUnit.Framework;

    /// <summary>
    /// Acts as a base class for the Dynamic Builder Serialization type tests.
    /// </summary>
    [TestFixture]
    public abstract class DynamicBuilderSerializationBase
    {
        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>The object.</value>
        protected string Obj { get; private set; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        protected IDto Result { get; private set; }

        /// <summary>
        /// Gets the SUT.
        /// </summary>
        /// <value>The SUT.</value>
        protected IDynamicBuilder Sut { get; private set; }

        /// <summary>
        /// Sets up.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            var mock = Mock.Of<IDto>(
                x =>
                    x.CardInfoId == 10 &&
                    x.CardIssueLevel == 0xDE &&
                    x.CardNumberDisplay == "1234567890" &&
                    x.CardNumber == 1234567890 &&
                    x.CardUpdated == new DateTime(2014, 7, 4));

            this.Sut = Dynamic.Builder;
            this.Result = this.Sut.Create<IDto>(mock);
            this.Obj = JsonConvert.SerializeObject(this.Result);
        }
    }
}