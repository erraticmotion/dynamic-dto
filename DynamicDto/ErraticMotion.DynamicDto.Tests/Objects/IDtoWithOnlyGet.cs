// <copyright file="IDtoWithOnlyGet.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    /// <summary>
    /// Interface <c>IDtoWithOnlyGet</c>.
    /// </summary>
    public interface IDtoWithOnlyGet
    {
        /// <summary>
        /// Gets the only get.
        /// </summary>
        /// <value>The only get.</value>
        int OnlyGet { get; }
    }
}