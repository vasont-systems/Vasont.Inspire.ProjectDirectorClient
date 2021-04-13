//-----------------------------------------------------------------------
// <copyright file="ProjectDirectorConfigurationModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Settings
{
    using System.Collections.Generic;
    using Vasont.Inspire.ProjectDirectorClient.Models;

    /// <summary>
    /// This class represents the configuration model used for the Project Director Rest Api integration.
    /// </summary>
    /// <seealso cref="Vasont.Inspire.ProjectDirectorClient.Settings.BaseConfigurationModel" />
    /// <seealso cref="Vasont.Inspire.ProjectDirector.Client.Settings.BaseConfigurationModel" />
    public class ProjectDirectorConfigurationModel : BaseConfigurationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDirectorConfigurationModel"/> class.
        /// </summary>
        public ProjectDirectorConfigurationModel()
        {
            this.WebHookScopes = new List<string>();
        }

        /// <summary>
        /// Gets or sets the pa client identifier.
        /// </summary>
        /// <value>
        /// The pa client identifier.
        /// </value>
        public long PaClientId { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public long ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the name of the file format.
        /// </summary>
        /// <value>
        /// The name of the file format.
        /// </value>
        public string FileFormatName { get; set; }

        /// <summary>
        /// Gets or sets the workflow identifier.
        /// </summary>
        /// <value>
        /// The workflow identifier.
        /// </value>
        public long WorkflowId { get; set; }

        /// <summary>
        /// Gets or sets the web hook URL.
        /// </summary>
        /// <value>
        /// The web hook URL.
        /// </value>
        public string WebHookUrl { get; set; }

        /// <summary>
        /// Gets or sets the basic authentication user identifier.
        /// </summary>
        /// <value>
        /// The basic authentication user identifier.
        /// </value>
        public string BasicAuthUserId { get; set; }

        /// <summary>
        /// Gets or sets the basic authentication secret.
        /// </summary>
        /// <value>
        /// The basic authentication secret.
        /// </value>
        public string BasicAuthSecret { get; set; }

        /// <summary>
        /// Gets or sets the web hook scopes.
        /// </summary>
        /// <value>
        /// The web hook scopes.
        /// </value>
        public List<string> WebHookScopes { get; set; }

        /// <summary>
        /// Gets or sets the submission claim scope.
        /// </summary>
        /// <value>
        /// The submission claim scope.
        /// </value>
        public SubmissionClaimScope SubmissionClaimScope { get; set; } = SubmissionClaimScope.LANGUAGE;

        /// <summary>
        /// Gets or sets a value indicating whether [submission automatic start].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [submission automatic start]; otherwise, <c>false</c>.
        /// </value>
        public bool SubmissionAutoStart { get; set; } = true;

        /// <summary>
        /// Gets or sets the parsable file search regular expression.
        /// </summary>
        /// <value>
        /// The parsable file search regular expression.
        /// </value>
        public string ParsableFileSearchRegularExpression { get; set; }

        /// <summary>
        /// Gets or sets the non parsable file search regular expression.
        /// </summary>
        /// <value>
        /// The non parsable file search regular expression.
        /// </value>
        public string NonParsableFileSearchRegularExpression { get; set; }
    }
}
