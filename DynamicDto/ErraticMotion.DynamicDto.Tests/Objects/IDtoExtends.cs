// <copyright file="IDtoExtends.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    /// <summary>
    /// Interface <c>IDtoExtends</c>.
    /// </summary>
    /// <seealso cref="IDtoBase" />
    public interface IDtoExtends : IDtoBase
    {
        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>The card number.</value>
        long CardNumber { get; set; }

        /// <summary>
        /// Supers the method.
        /// </summary>
        void SuperMethod();
    }
}