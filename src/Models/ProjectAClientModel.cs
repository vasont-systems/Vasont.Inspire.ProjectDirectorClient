//-----------------------------------------------------------------------
// <copyright file="ProjectAClientModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    /// <summary>
    /// This class represents the Project A Client Model
    /// </summary>
    public class ProjectAClientModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProjectAClientModel"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the pa client identifier.
        /// </summary>
        /// <value>
        /// The pa client identifier.
        /// </value>
        public long PaClientId { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public long OrganizationId { get; set; }
    }
}
