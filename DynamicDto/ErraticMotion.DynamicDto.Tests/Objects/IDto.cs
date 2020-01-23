// <copyright file="IDto.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    using System;

    /// <summary>
    /// Interface <c>IDto</c>
    /// </summary>
    public interface IDto
    {
        /// <summary>
        /// Gets or sets the card information identifier.
        /// </summary>
        /// <value>The card information identifier.</value>
        int CardInfoId { get; set; }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>The card number.</value>
        long CardNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the card issue level.
        /// </summary>
        /// <value>The card issue level.</value>
        byte CardIssueLevel { get; set; }

        /// <summary>
        /// Gets or sets the card updated.
        /// </summary>
        /// <value>The card updated.</value>
        DateTime CardUpdated { get; set; }

        /// <summary>
        /// Gets or sets the card number display.
        /// </summary>
        /// <value>The card number display.</value>
        string CardNumberDisplay { get; set; }
    }
}