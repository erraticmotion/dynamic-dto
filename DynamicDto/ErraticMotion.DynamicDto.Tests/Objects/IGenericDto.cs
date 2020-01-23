// <copyright file="IGenericDto.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    public interface IGenericDto<T>
    {
        T GenericType { get; set; }
    }
}