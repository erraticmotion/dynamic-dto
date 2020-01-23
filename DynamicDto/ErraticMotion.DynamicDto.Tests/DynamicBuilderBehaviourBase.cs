// <copyright file="DynamicBuilderBehaviourBase.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Newtonsoft.Json;

    using NUnit.Framework;

    /// <summary>
    /// Acts as a base class for dynamic builder <c>behaviour</c> tests.
    /// </summary>
    /// <typeparam name="T">The type of SUT</typeparam>
    [TestFixture]
    public abstract class DynamicBuilderBehaviourBase<T>
        where T : class
    {
        /// <summary>
        /// The expected number of properties on the dynamix type.
        /// </summary>
        private readonly int expectedProperties;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBuilderBehaviourBase{T}"/> class.
        /// </summary>
        /// <param name="expectedProperties">The expected properties.</param>
        protected DynamicBuilderBehaviourBase(int expectedProperties)
        {
            this.expectedProperties = expectedProperties;
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        protected T Result { get; set; }

        /// <summary>
        /// Sets up the initial condition for the test.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            this.Result = this.GetResult();
        }

        /// <summary>
        /// To the string does not throw.
        /// </summary>
        [Test]
        public void ShouldNotThrow()
        {
            var output = JsonConvert.SerializeObject(this.Result);
            Console.WriteLine(output);

            Assert.DoesNotThrow(() => Console.WriteLine(this.Result));
        }

        /// <summary>
        /// Should not be null.
        /// </summary>
        [Test]
        public void ShouldNotBeNull()
        {
            this.Result.Should().NotBeNull();
        }

        /// <summary>
        /// Should support required interface.
        /// </summary>
        [Test]
        public void ShouldSupportInterface()
        {
            this.Result.GetType().GetInterfaces()
                .FirstOrDefault(x => x == typeof(T))
                .Should().NotBeNull();
        }

        /// <summary>
        /// Assert that the expected results have occurred.
        /// </summary>
        [Test]
        public void ShouldVerifyContract()
        {
            this.Result.VerifyContract(this.expectedProperties);
        }

        /// <summary>
        /// Extension point for test sub-classes to provide the <see cref="Result"/>.
        /// </summary>
        /// <returns>The DTO result.</returns>
        protected abstract T GetResult();
    }
}