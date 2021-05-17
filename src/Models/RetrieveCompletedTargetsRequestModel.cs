//-----------------------------------------------------------------------
// <copyright file="RetrieveCompletedTargetsRequestModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the Model used to Retrieve Completed Targets.
    /// </summary>
    public class RetrieveCompletedTargetsRequestModel
    {
        /// <summary>
        /// Gets or sets the translatable names.
        /// </summary>
        /// <value>
        /// The translatable names.
        /// </value>
        [JsonProperty(PropertyName = "translatableNames")]
        public List<TranslatableNameModel> TranslatableNames { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the target language.
        /// </summary>
        /// <value>
        /// The target language.
        /// </value>
        [JsonProperty(PropertyName = "targetLanguage")]
        public string TargetLanguage { get; set; }

        /// <summary>
        /// Gets or sets the target identifier.
        /// </summary>
        /// <value>
        /// The target identifier.
        /// </value>
        [JsonProperty(PropertyName = "targetId")]
        public long TargetId { get; set; }

        /// <summary>
        /// Gets or sets the submission identifier.
        /// </summary>
        /// <value>
        /// The submission identifier.
        /// </value>
        [JsonProperty(PropertyName = "submissionId")]
        public long SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        [JsonProperty(PropertyName = "jobId")]
        public long JobId { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [JsonProperty(PropertyName = "dueDate")]
        public long DueDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the document.
        /// </summary>
        /// <value>
        /// The name of the document.
        /// </value>
        [JsonProperty(PropertyName = "documentName")]
        public string DocumentName { get; set; }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        [JsonProperty(PropertyName = "documentId")]
        public long DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the file format.
        /// </summary>
        /// <value>
        /// The name of the file format.
        /// </value>
        [JsonProperty(PropertyName = "fileFormatName")]
        public string FileFormatName { get; set; }

        /// <summary>
        /// Gets or sets the source language.
        /// </summary>
        /// <value>
        /// The source language.
        /// </value>
        [JsonProperty(PropertyName = "sourceLanguage")]
        public string SourceLanguage { get; set; }

        /// <summary>
        /// Gets or sets the date completed.
        /// </summary>
        /// <value>
        /// The date completed.
        /// </value>
        [JsonProperty(PropertyName = "dateCompleted")]
        public long DateCompleted { get; set; }

        /// <summary>
        /// Gets or sets the file format identifier.
        /// </summary>
        /// <value>
        /// The file format identifier.
        /// </value>
        [JsonProperty(PropertyName = "fileFormatId")]
        public long FileFormatId { get; set; }

        /// <summary>
        /// Gets or sets the current phase.
        /// </summary>
        /// <value>
        /// The current phase.
        /// </value>
        [JsonProperty(PropertyName = "currentPhase")]
        public string CurrentPhase { get; set; }
    }
}
