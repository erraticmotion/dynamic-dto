// <copyright file="Dynamic.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto
{
    using System;

    /// <summary>
    /// Ambient context. Entry point that gives access to
    /// the synchronization send and receive channels.
    /// </summary>
    public static class Dynamic
    {
        /// <summary>
        /// Lazy load the context factory
        /// </summary>
        private static readonly Lazy<IDynamicBuilder> ContextFactory = new Lazy<IDynamicBuilder>(() => new DynamicBuilder(Configuration));

        /// <summary>
        /// Initializes static members of the <see cref="Dynamic"/> class.
        /// </summary>
        static Dynamic()
        {
            Configuration = new Configuration();
        }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        public static IDynamicBuilder Builder => ContextFactory.Value;
    }
}
