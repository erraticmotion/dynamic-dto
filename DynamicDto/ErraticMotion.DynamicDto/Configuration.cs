// <copyright file="Configuration.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    /// <summary>
    /// Class Configuration.
    /// </summary>
    internal class Configuration : IConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.DynamicAssemblyName = "EM.Dynamic.Builder.Emit";
            this.Namespace = "http://em.dynamic.builder/";
            this.DynamicTypePrefix = "EM.Dynamic.Builder___";
        }

        /// <inheritdoc />
        public string DynamicAssemblyName { get; }

        /// <inheritdoc />
        public string Namespace { get; }

        /// <inheritdoc />
        public string DynamicTypePrefix { get; }
    }
}