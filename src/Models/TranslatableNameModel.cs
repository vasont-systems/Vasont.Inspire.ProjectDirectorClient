//-----------------------------------------------------------------------
// <copyright file="TranslatableNameModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the Translatable Name Model.
    /// </summary>
    public class TranslatableNameModel
    {
        /// <summary>
        /// Gets or sets the name of the target file.
        /// </summary>
        /// <value>
        /// The name of the target file.
        /// </value>
        [JsonProperty(PropertyName = "targetFileName")]
        public string TargetFileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the phase.
        /// </summary>
        /// <value>
        /// The name of the phase.
        /// </value>
        [JsonProperty(PropertyName = "phaseName")]
        public string PhaseName { get; set; }
    }
}
