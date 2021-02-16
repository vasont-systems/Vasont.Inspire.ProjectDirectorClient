//-----------------------------------------------------------------------
// <copyright file="ProjectLanguageDirectionModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    /// <summary>
    /// This class represents the Project Director Project Language Direction Model
    /// </summary>
    public class ProjectLanguageDirectionModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProjectLanguageDirectionModel"/> is default.
        /// </summary>
        /// <value>
        ///   <c>true</c> if default; otherwise, <c>false</c>.
        /// </value>
        public bool Default { get; set; }

        /// <summary>
        /// Gets or sets the target language.
        /// </summary>
        /// <value>
        /// The target language.
        /// </value>
        public string TargetLanguage { get; set; }

        /// <summary>
        /// Gets or sets the source language.
        /// </summary>
        /// <value>
        /// The source language.
        /// </value>
        public string SourceLanguage { get; set; }

        /// <summary>
        /// Gets or sets the display name of the target language.
        /// </summary>
        /// <value>
        /// The display name of the target language.
        /// </value>
        public string TargetLanguageDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the source language.
        /// </summary>
        /// <value>
        /// The display name of the source language.
        /// </value>
        public string SourceLanguageDisplayName { get; set; }
    }
}
