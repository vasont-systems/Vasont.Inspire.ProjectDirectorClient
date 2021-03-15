//-----------------------------------------------------------------------
// <copyright file="CreateSubmissionRequestModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// This enumeration is used for the Submission Claim Scope
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))] 
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
    /// This enumeration is used to define the scopes of the project director webhook calls
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WebHookScope
    {
        /// <summary>
        /// Receive a webhook when the submission is completed
        /// </summary>
        [EnumMember(Value = "submission.completed")]
        SubmissionCompleted,

        /// <summary>
        /// Receive a webhook if the submission is canceled
        /// </summary>
        [EnumMember(Value = "submission.cancelled")]
        SubmissionCancelled,

        /// <summary>
        /// Receive a webhook when any of the targets in the submission is completed
        /// </summary>
        [EnumMember(Value = "target.completed")]
        TargetCompleted,

        /// <summary>
        /// Receive a webhook if any of the targets in the submission is canceled
        /// </summary>
        [EnumMember(Value = "target.cancelled")]
        TargetCancelled,

        /// <summary>
        /// Receive a webhook if, after completion, any of the targets in the submission is reopened/updated
        /// </summary>
        [EnumMember(Value = "target.reopened")]
        TargetReopened
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
            this.ClaimScope = SubmissionClaimScope.BATCH;
        }

        /// <summary>
        /// Gets or sets the claim scope.
        /// </summary>
        /// <value>
        /// The claim scope.
        /// </value>
        [JsonProperty(PropertyName = "claimScope")]
        public SubmissionClaimScope ClaimScope { get; set; }

        /// <summary>
        /// Gets or sets the custom attributes.
        /// </summary>
        /// <value>
        /// The custom attributes.
        /// </value>
        [JsonProperty(PropertyName = "customAttributes")]
        public List<CustomAttributeModel> CustomAttributes { get; set; }

        /// <summary>
        /// Gets or sets the Batch Information Models.
        /// </summary>
        /// <value>
        /// The Batch Information Models.
        /// </value>
        [JsonProperty(PropertyName = "batchInfos")]
        public List<BatchInformationModel> BatchInfos { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The date by when the submission has to be completed expressed in Unix Timestamp in milliseconds
        /// </value>
        [JsonProperty(PropertyName = "dueDate")]
        public long DueDate { get; set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>
        /// The instructions.
        /// </value>
        [JsonProperty(PropertyName = "instructions")]
        public string Instructions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is favorite.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is favorite; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "isFavorite")]
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>
        /// The meta data.
        /// </value>
        [JsonProperty(PropertyName = "metadata")]
        public List<MetaDataModel> MetaData { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the owners.
        /// </summary>
        /// <value>
        /// The owners.
        /// </value>
        [JsonProperty(PropertyName = "owners")]
        public List<long> Owners { get; set; }

        /// <summary>
        /// Gets or sets the pa client identifier.
        /// </summary>
        /// <value>
        /// The pa client identifier.
        /// </value>
        [JsonProperty(PropertyName = "paClientId")]
        public long? PaClientId { get; set; }

        /// <summary>
        /// Gets or sets the pa job number.
        /// </summary>
        /// <value>
        /// The pa job number.
        /// </value>
        [JsonProperty(PropertyName = "paJobNumber")]
        public string PaJobNumber { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        [JsonProperty(PropertyName = "projectId")]
        public long ProjectId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [quote enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [quote enabled]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "quoteEnabled")]
        public bool? QuoteEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [request quote].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [request quote]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "requestQuote")]
        public bool? RequestQuote { get; set; }

        /// <summary>
        /// Gets or sets the source language.
        /// </summary>
        /// <value>
        /// The source language.
        /// </value>
        [JsonProperty(PropertyName = "sourceLanguage")]
        public string SourceLanguage { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>
        /// The template identifier.
        /// </value>
        [JsonProperty(PropertyName = "templateId")]
        public long? TemplateId { get; set; }
    }
}
