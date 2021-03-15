//-----------------------------------------------------------------------
// <copyright file="WordCountRequestModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the Word Count Request Model
    /// </summary>
    public class WordCountRequestModel
    {
        /// <summary>
        /// Gets or sets the name of the batch.
        /// </summary>
        /// <value>
        /// The name of the batch.
        /// </value>
        [JsonProperty(PropertyName = "batchName")]
        public string BatchName { get; set; }

        /// <summary>
        /// Gets or sets the target identifier.
        /// </summary>
        /// <value>
        /// The target identifier.
        /// </value>
        [JsonProperty(PropertyName = "targetId")]
        public long? TargetId { get; set; }

        /// <summary>
        /// Gets or sets the target language.
        /// </summary>
        /// <value>
        /// The target language.
        /// </value>
        [JsonProperty(PropertyName = "targetLanguage")]
        public string TargetLanguage { get; set; }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        [JsonProperty(PropertyName = "documentId")]
        public long? DocumentId { get; set; }
    }
}
