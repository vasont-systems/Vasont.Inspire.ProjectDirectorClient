//-----------------------------------------------------------------------
// <copyright file="ProjectMicroModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    /// <summary>
    /// This class represents the Project Model used with the Project Director Rest Api
    /// </summary>
    public class ProjectMicroModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProjectMicroModel"/> is enabled.
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
        /// Gets or sets the "aquotescope" property.
        /// </summary>
        /// <value>
        /// The "aquotescope" property.
        /// </value>
        public string AquoteScope { get; set; }

        /// <summary>
        /// Gets or sets the "aautoquote" property.
        /// </summary>
        /// <value>
        /// The "aautoquote" property.
        /// </value>
        public string AautoQuote { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public long ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public long OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [quote enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [quote enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool QuoteEnabled { get; set; }

        /// <summary>
        /// Gets or sets the cost scope identifier.
        /// </summary>
        /// <value>
        /// The cost scope identifier.
        /// </value>
        public long CostScopeId { get; set; }

        /// <summary>
        /// Gets or sets the default workflow definition identifier.
        /// </summary>
        /// <value>
        /// The default workflow definition identifier.
        /// </value>
        public long DefaultWorkflowDefinitionId { get; set; }

        /// <summary>
        /// Gets or sets the short code.
        /// </summary>
        /// <value>
        /// The short code.
        /// </value>
        public string ShortCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic start submission].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic start submission]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoStartSubmission { get; set; }
    }
}
