//-----------------------------------------------------------------------
// <copyright file="RetrievalRequestModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the Project Director Retrieval Request Model that will be stored with the Translation Job record.
    /// </summary>
    public class RetrievalRequestModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetrievalRequestModel"/> class.
        /// </summary>
        public RetrievalRequestModel()
        {
            this.TargetIds = new List<long>();
            this.MarkTargetsDelivered = new MarkTargetsDeliveredModel();
            this.Messages = new List<string>();
            this.RequestDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        /// <value>
        /// The request date.
        /// </value>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the submission identifier.
        /// </summary>
        /// <value>
        /// The submission identifier.
        /// </value>
        public long SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the target ids.
        /// </summary>
        /// <value>
        /// The target ids.
        /// </value>
        public List<long> TargetIds { get; set; }

        /// <summary>
        /// Gets or sets the download identifier.
        /// </summary>
        /// <value>
        /// The download identifier.
        /// </value>
        public Guid DownloadId { get; set; }

        /// <summary>
        /// Gets or sets the mark targets delivered.
        /// </summary>
        /// <value>
        /// The mark targets delivered.
        /// </value>
        public MarkTargetsDeliveredModel MarkTargetsDelivered { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public List<string> Messages { get; set; }
    }
}
