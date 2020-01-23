// <copyright file="IDtoReturnValuesObject.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    /// <summary>
    /// Interface <c>IDtoReturnValuesObject</c>.
    /// </summary>
    public interface IDtoReturnValuesObject
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>A DynamicBuilderTests object.</returns>
        DynamicBuilderContractTests Create();
    }
}