//-----------------------------------------------------------------------
// <copyright file="TargetLanguageInformationModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the Target Language Information Model.
    /// </summary>
    public class TargetLanguageInformationModel
    {
        /// <summary>
        /// Gets or sets the target language.
        /// </summary>
        /// <value>
        /// The target language.
        /// </value>
        [JsonProperty(PropertyName = "targetLanguage")]
        public string TargetLanguage { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [JsonProperty(PropertyName = "dueDate")]
        public long? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the tm profile identifier.
        /// </summary>
        /// <value>
        /// The tm profile identifier.
        /// </value>
        [JsonProperty(PropertyName = "tmProfileId")]
        public long? TmProfileId { get; set; }
    }
}
