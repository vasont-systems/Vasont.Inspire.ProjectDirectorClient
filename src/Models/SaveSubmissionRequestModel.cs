//-----------------------------------------------------------------------
// <copyright file="SaveSubmissionRequestModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the Save Submission Request Model.
    /// </summary>
    public class SaveSubmissionRequestModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [automatic start].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic start]; otherwise, <c>false</c>.
        /// </value>        
        [JsonProperty(PropertyName = "autoStart")]
        public bool AutoStart { get; set; }
    }
}
