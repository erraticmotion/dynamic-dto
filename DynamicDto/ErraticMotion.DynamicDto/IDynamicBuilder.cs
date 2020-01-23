// <copyright file="IDynamicBuilder.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the operations that support the dynamic creation of Data Transfer
    /// Objects at runtime
    /// </summary>
    public interface IDynamicBuilder
    {
        /// <summary>
        /// Creates a DTO instance based on <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// An interface that the DTO should support
        /// </typeparam>
        /// <returns>A DTO instance.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Designed to be generic")]
        T Create<T>() where T : class;

        /// <summary>
        /// Creates a DTO instance based on <typeparamref name="T"/> and populates
        /// the properties with the values supplied by the <paramref name="item"/>.
        /// </summary>
        /// <typeparam name="T">
        /// An interface that the DTO should support.
        /// </typeparam>
        /// <param name="item">
        /// The item whose values the DTO should be populated with.
        /// </param>
        /// <returns>A DTO instance.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Designed to be generic")]
        T Create<T>(object item) where T : class;

        /// <summary>
        /// Returns a collection of known types that have been dynamically created
        /// by this object.
        /// </summary>
        /// <returns>A collection of known types.</returns>
        IEnumerable<Type> KnownTypes();
    }
}
