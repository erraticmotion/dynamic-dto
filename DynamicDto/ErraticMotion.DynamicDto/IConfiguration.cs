// <copyright file="IConfiguration.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    /// <summary>
    /// Represents the runtime configuration of the dynamic type builder.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the assembly name
        /// </summary>
        string DynamicAssemblyName { get; }

        /// <summary>
        /// Gets the namespace
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Gets the dynamic type prefix
        /// </summary>
        string DynamicTypePrefix { get; }
    }
}