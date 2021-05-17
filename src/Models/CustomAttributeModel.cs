//-----------------------------------------------------------------------
// <copyright file="CustomAttributeModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the Custom Attribute Model used when sending submissions.
    /// </summary>
    public class CustomAttributeModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

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
