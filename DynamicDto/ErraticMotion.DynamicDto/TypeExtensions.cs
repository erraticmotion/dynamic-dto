// <copyright file="TypeExtensions.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Contains extension methods for the <see cref="Type"/> type.
    /// </summary>
    internal static class TypeExtensions
    {
        internal static IEnumerable<T> GetAll<T>(this Type item, Func<Type, IEnumerable<T>> selector)
        {
            foreach (var m in selector(item))
            {
                yield return m;
            }

            foreach (var m in item.GetInterfaces().SelectMany(selector))
            {
                yield return m;
            }
        }
    }
}
