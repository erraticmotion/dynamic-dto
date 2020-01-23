// <copyright file="DataTransferObject.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    using System;

    public class DataTransferObject : IDto
    {
        public int CardInfoId { get; set; }
        public long CardNumber { get; set; }
        public bool IsValid { get; set; }
        public byte CardIssueLevel { get; set; }
        public DateTime CardUpdated { get; set; }
        public string CardNumberDisplay { get; set; }
    }
}