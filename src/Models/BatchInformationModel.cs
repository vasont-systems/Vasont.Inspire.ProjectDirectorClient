//-----------------------------------------------------------------------
// <copyright file="BatchInformationModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// This enumeration is used for the Target Format of the Batch
    /// </summary>
    public enum TargetFormat
    {
        /// <summary>
        /// The TXML Target Format
        /// </summary>
        TXML,

        /// <summary>
        /// The TXLF Target Format
        /// </summary>
        TXLF,

        /// <summary>
        /// The non parsable Target Format
        /// </summary>
        NON_PARSABLE
    }

    /// <summary>
    /// This class represents the Batch Information Model
    /// </summary>
    public class BatchInformationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchInformationModel"/> class.
        /// </summary>
        public BatchInformationModel()
        {
            this.TargetLanguageInfos = new List<TargetLanguageInformationModel>();
            this.TargetFormat = TargetFormat.NON_PARSABLE;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the target format.
        /// </summary>
        /// <value>
        /// The target format.
        /// </value>
        public TargetFormat TargetFormat { get; set; }

        /// <summary>
        /// Gets or sets the target language info.
        /// </summary>
        /// <value>
        /// The target language info.
        /// </value>
        public List<TargetLanguageInformationModel> TargetLanguageInfos { get; set; }

        /// <summary>
        /// Gets or sets the workflow identifier.
        /// </summary>
        /// <value>
        /// The workflow identifier.
        /// </value>
        public long WorkflowId { get; set; }
    }
}
