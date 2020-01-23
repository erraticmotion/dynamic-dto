// <copyright file="IDtoWithObjects.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    /// <summary>
    /// Interface <c>IDtoWithObjects</c>.
    /// </summary>
    public interface IDtoWithObjects
    {
        /// <summary>
        /// Gets or sets the sub interface.
        /// </summary>
        /// <value>The sub interface.</value>
        IDtoWithObjects SubInterface { get; set; }

        /// <summary>
        /// Gets or sets the sub object.
        /// </summary>
        /// <value>The sub object.</value>
        DataTransferObject SubObject { get; set; }
    }
}