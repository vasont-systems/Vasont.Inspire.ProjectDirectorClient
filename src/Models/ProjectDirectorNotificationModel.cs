//-----------------------------------------------------------------------
// <copyright file="ProjectDirectorNotificationModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the Project Director Notification Model
    /// </summary>
    public class ProjectDirectorNotificationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDirectorNotificationModel"/> class.
        /// </summary>
        public ProjectDirectorNotificationModel()
        {
            this.DocumentIds = new List<long>();
            this.TargetIds = new List<ProjectDirectorNotificationTargetModel>();
        }

        /// <summary>
        /// Gets or sets the event code.
        /// </summary>
        /// <value>
        /// The event code.
        /// </value>
        public string EventCode { get; set; }

        /// <summary>
        /// Gets or sets the submission identifier.
        /// </summary>
        /// <value>
        /// The submission identifier.
        /// </value>
        public long SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the epoch value representing the event time.
        /// </summary>
        /// <value>
        /// The epoch value representing the event time.
        /// </value>
        public long? EventTime { get; set; }

        /// <summary>
        /// Gets or sets the document ids.
        /// </summary>
        /// <value>
        /// The document ids.
        /// </value>
        public List<long> DocumentIds { get; set; }

        /// <summary>
        /// Gets or sets the target ids.
        /// </summary>
        /// <value>
        /// The target ids.
        /// </value>
        public List<ProjectDirectorNotificationTargetModel> TargetIds { get; set; }
    }
}
