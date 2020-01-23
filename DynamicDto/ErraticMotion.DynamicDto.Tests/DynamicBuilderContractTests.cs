// <copyright file="DynamicBuilderContractTests.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;

    using NUnit.Framework;

    /// <summary>
    /// Dynamic Builder Contract Tests.
    /// </summary>
    [TestFixture]
    public class DynamicBuilderContractTests
    {
        /// <summary>
        /// Tests the error on class.
        /// </summary>
        [Test]
        public void TestErrorOnClass()
        {
            Assert.Throws<ArgumentException>(
                () => Dynamic.Builder.Create<DynamicBuilderContractTests>());
        }
    }
}