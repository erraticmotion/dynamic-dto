// <copyright file="IDtoWithOnlySet.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    /// <summary>
    /// Interface <c>IDtoWithOnlySet</c>.
    /// </summary>
    public interface IDtoWithOnlySet
    {
        /// <summary>
        /// Sets the only set.
        /// </summary>
        /// <value>The only set.</value>
        int OnlySet { set; }
    }
}