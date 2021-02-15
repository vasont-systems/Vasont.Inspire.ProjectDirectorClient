//-----------------------------------------------------------------------
// <copyright file="ProjectModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the full Project Director Project Model
    /// </summary>
    public class ProjectModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectModel"/> class.
        /// </summary>
        public ProjectModel()
        {
            this.ProjectLanguageDirections = new List<ProjectLanguageDirectionModel>();
            this.WorkflowDefinitions = new List<WorkflowDefinitionModel>();
            this.FileFormatProfiles = new List<FileFormatProfileModel>();
        }

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
        /// Gets or sets the short code.
        /// </summary>
        /// <value>
        /// The short code.
        /// </value>
        public string ShortCode { get; set; }

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
        /// Gets or sets a value indicating whether [automatic start submission].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic start submission]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoStartSubmission { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [quote enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [quote enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool QuoteEnabled { get; set; }

        /// <summary>
        /// Gets or sets the enabled.
        /// </summary>
        /// <value>
        /// The enabled.
        /// </value>
        public long Enabled { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the quote scope.
        /// </summary>
        /// <value>
        /// The quote scope.
        /// </value>
        public string QuoteScope { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic quote].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic quote]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoQuote { get; set; }

        /// <summary>
        /// Gets or sets the project language directions.
        /// </summary>
        /// <value>
        /// The project language directions.
        /// </value>
        public List<ProjectLanguageDirectionModel> ProjectLanguageDirections { get; set; }

        /// <summary>
        /// Gets or sets the workflow definitions.
        /// </summary>
        /// <value>
        /// The workflow definitions.
        /// </value>
        public List<WorkflowDefinitionModel> WorkflowDefinitions { get; set; }

        /// <summary>
        /// Gets or sets the file format profiles.
        /// </summary>
        /// <value>
        /// The file format profiles.
        /// </value>
        public List<FileFormatProfileModel> FileFormatProfiles { get; set; }
    }
}
