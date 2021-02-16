//-----------------------------------------------------------------------
// <copyright file="CreateSubmissionRequestModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// This enumeration is used for the Submission Claim Scope
    /// </summary>
    public enum SubmissionClaimScope
    {
        /// <summary>
        /// The language
        /// </summary>
        LANGUAGE,

        /// <summary>
        /// The batch
        /// </summary>
        BATCH,

        /// <summary>
        /// The file
        /// </summary>
        FILE
    }

    /// <summary>
    /// This class represents the Create Submission Request Model
    /// </summary>
    public class CreateSubmissionRequestModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSubmissionRequestModel"/> class.
        /// </summary>
        public CreateSubmissionRequestModel()
        {
            this.CustomAttributes = new Dictionary<string, string>();
            this.MetaData = new Dictionary<string, string>();
            this.Owners = new List<long>();
            this.ClaimScope = SubmissionClaimScope.BATCH;
        }

        /// <summary>
        /// Gets or sets the claim scope.
        /// </summary>
        /// <value>
        /// The claim scope.
        /// </value>
        public SubmissionClaimScope ClaimScope { get; set; }

        /// <summary>
        /// Gets or sets the custom attributes.
        /// </summary>
        /// <value>
        /// The custom attributes.
        /// </value>
        public Dictionary<string, string> CustomAttributes { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The date by when the submission has to be completed expressed in Unix Timestamp in milliseconds
        /// </value>
        public long DueDate { get; set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>
        /// The instructions.
        /// </value>
        public string Instructions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is favorite.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is favorite; otherwise, <c>false</c>.
        /// </value>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>
        /// The meta data.
        /// </value>
        public Dictionary<string, string> MetaData { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the owners.
        /// </summary>
        /// <value>
        /// The owners.
        /// </value>
        public List<long> Owners { get; set; }

        /// <summary>
        /// Gets or sets the pa job number.
        /// </summary>
        /// <value>
        /// The pa job number.
        /// </value>
        public string PaJobNumber { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public long ProjectId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [quote enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [quote enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool QuoteEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [request quote].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [request quote]; otherwise, <c>false</c>.
        /// </value>
        public bool RequestQuote { get; set; }

        /// <summary>
        /// Gets or sets the source language.
        /// </summary>
        /// <value>
        /// The source language.
        /// </value>
        public string SourceLanguage { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>
        /// The template identifier.
        /// </value>
        public long TemplateId { get; set; }
    }
}
