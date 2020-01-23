// <copyright file="DynamicBuilderGetSetValuesUsingGenericCtorBehaviour.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using DynamicDto.Objects;

    using FluentAssertions;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests generic types and subclasses of generic types.
    /// </summary>
    [TestFixture]
    public class DynamicBuilderGetSetValuesUsingGenericCtorBehaviour
    {
        /// <summary>
        /// The generic DTO
        /// </summary>
        private IGenericDto<int> genericDto;

        /// <summary>
        /// The generic subclass DTO
        /// </summary>
        private IGenericDtoExtends<byte> genericSubclassDto;

        /// <summary>
        /// The dynamic builder
        /// </summary>
        private IDynamicBuilder dynamicBuilder;

        /// <summary>
        /// Sets up.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            this.genericDto = Mock.Of<IGenericDto<int>>(
                x => x.GenericType == 45);

            this.genericSubclassDto = Mock.Of<IGenericDtoExtends<byte>>(
                x => x.GenericType == 0x01 && x.Value == 0xff);

            this.dynamicBuilder = Dynamic.Builder;
        }

        /// <summary>
        /// Create with object should be.
        /// </summary>
        [Test]
        public void CreateWithObjectShouldBe()
        {
            var sut = this.dynamicBuilder.Create<IGenericDto<int>>(this.genericDto);
            sut.VerifyContract(1);
            sut.GenericType.Should().Be(45);
        }

        /// <summary>
        /// Create with subclass object should be.
        /// </summary>
        [Test]
        public void CreateWithSubclassObjectShouldBe()
        {
            var sut = this.dynamicBuilder.Create<IGenericDtoExtends<byte>>(this.genericSubclassDto);
            sut.VerifyContract(2);
            sut.GenericType.Should().Be(0x01);
            sut.Value.Should().Be(0xff);
        }

        /// <summary>
        /// Create without object should be.
        /// </summary>
        [Test]
        public void CreateWithoutObjectShouldBe()
        {
            var sut = this.dynamicBuilder.Create<IGenericDto<int>>();
            sut.VerifyContract(1);
            sut.GenericType = 54;
            sut.GenericType.Should().Be(54);
        }

        /// <summary>
        /// Creates the subclass without object should be.
        /// </summary>
        [Test]
        public void CreateSubclassWithoutObjectShouldBe()
        {
            var sut = this.dynamicBuilder.Create<IGenericDtoExtends<int>>();
            sut.VerifyContract(2);
            sut.GenericType = 54;
            sut.GenericType.Should().Be(54);
            sut.Value = 0xcd;
            sut.Value.Should().Be(0xcd);
        }
    }
}
