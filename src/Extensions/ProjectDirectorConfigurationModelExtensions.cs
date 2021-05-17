//-----------------------------------------------------------------------
// <copyright file="ProjectDirectorConfigurationModelExtensions.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Extensions
{
    using System.Collections.Generic;
    using Vasont.Inspire.ProjectDirectorClient.Models;
    using Vasont.Inspire.ProjectDirectorClient.Settings;

    /// <summary>
    /// This class represents the extensions methods for the <see cref="ProjectDirectorConfigurationModel"/>
    /// </summary>
    public static class ProjectDirectorConfigurationModelExtensions
    {
        /// <summary>
        /// This method retrieves list of all of the web hook scopes.
        /// </summary>
        /// <returns>
        /// Returns a list of all of the web hook scopes.
        /// </returns>
        public static List<string> AllWebHookScopes()
        {
            return new List<string> { WebHookScope.SubmissionCompleted.ToEnumMemberAttrValue(), WebHookScope.TargetCompleted.ToEnumMemberAttrValue(), WebHookScope.SubmissionCancelled.ToEnumMemberAttrValue(), WebHookScope.TargetCancelled.ToEnumMemberAttrValue(), WebHookScope.TargetReopened.ToEnumMemberAttrValue() };
        }

        /// <summary>
        /// Validates the configuration.
        /// </summary>
        /// <param name="model">Contains the <see cref="ProjectDirectorConfigurationModel"/> to be validated.</param>
        /// <returns>Returns true if the <see cref="ProjectDirectorConfigurationModel"/> is valid.</returns>
        public static bool ValidateConfiguration(this ProjectDirectorConfigurationModel model)
        {
            // ProjectId, FileFormatName, WorkflowId, ParsableFileSearchRegularExpression, ResourceUri, AuthorityUri is required
            return model.ProjectId > 0 &&
                !string.IsNullOrWhiteSpace(model.FileFormatName) &&
                model.WorkflowId > 0 &&
                !string.IsNullOrWhiteSpace(model.ParsableFileSearchRegularExpression) &&
                !string.IsNullOrWhiteSpace(model.NonParsableFileSearchRegularExpression) &&
                model.ResourceUri != null &&
                model.AuthorityUri != null;
        }
    }
}
