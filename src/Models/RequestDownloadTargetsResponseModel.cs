//-----------------------------------------------------------------------
// <copyright file="RequestDownloadTargetsResponseModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the Request Download Targets Response Model
    /// </summary>
    public class RequestDownloadTargetsResponseModel
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the download identifier.
        /// </summary>
        /// <value>
        /// The download identifier.
        /// </value>
        [JsonProperty(PropertyName = "downloadId")]
        public Guid DownloadId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [processing finished].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [processing finished]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "processingFinished")]
        public bool ProcessingFinished { get; set; }
    }
}
