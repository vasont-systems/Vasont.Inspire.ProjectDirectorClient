//-----------------------------------------------------------------------
// <copyright file="MetaDataModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the Meta Data Model that is used when sending submissions.
    /// </summary>
    public class MetaDataModel
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}
