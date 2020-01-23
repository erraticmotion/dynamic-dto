// <copyright file="IDtoArguments.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    using System;

    /// <summary>
    /// Interface <c>IDtoArguments</c>.
    /// </summary>
    public interface IDtoArguments
    {
        /// <summary>
        /// Tests the method.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="l">The l.</param>
        /// <param name="b">if set to <c>true</c> [b].</param>
        /// <param name="d">The d.</param>
        /// <param name="u">The u.</param>
        void TestMethod(int i, long l, bool b, DateTime d, DataTransferObject u);
    }
}