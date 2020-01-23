// <copyright file="IDtoBase.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    /// <summary>
    /// Interface <c>IDtoBase</c>.
    /// </summary>
    public interface IDtoBase
    {
        /// <summary>
        /// Gets or sets the card information identifier.
        /// </summary>
        /// <value>The card information identifier.</value>
        int CardInfoId { get; set; }

        /// <summary>
        /// Bases the method.
        /// </summary>
        void BaseMethod();
    }
}