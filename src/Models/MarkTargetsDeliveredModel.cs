//-----------------------------------------------------------------------
// <copyright file="MarkTargetsDeliveredModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The class represents the model used to Mark Targets as Delivered.
    /// </summary>
    public class MarkTargetsDeliveredModel
    {
        /// <summary>
        /// Gets or sets the target ids.
        /// </summary>
        /// <value>
        /// The target ids.
        /// </value>
        [JsonProperty(PropertyName = "targetIds")]
        public List<long> TargetIds { get; set; }
    }
}
