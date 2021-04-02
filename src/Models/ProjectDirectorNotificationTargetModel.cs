//-----------------------------------------------------------------------
// <copyright file="ProjectDirectorNotificationTargetModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the Project Director Notification Target Model
    /// </summary>
    public class ProjectDirectorNotificationTargetModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDirectorNotificationTargetModel"/> class.
        /// </summary>
        public ProjectDirectorNotificationTargetModel()
        {
            this.MetaData = new List<MetaDataModel>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the epoch value representing the event time.
        /// </summary>
        /// <value>
        /// The epoch value representing the event time.
        /// </value>
        public long? EventTime { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>
        /// The meta data.
        /// </value>
        public List<MetaDataModel> MetaData { get; set; }
    }
}
