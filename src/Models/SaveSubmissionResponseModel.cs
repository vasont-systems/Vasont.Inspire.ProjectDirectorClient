//-----------------------------------------------------------------------
// <copyright file="SaveSubmissionResponseModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the Save Submission Response Model
    /// </summary>
    public class SaveSubmissionResponseModel
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the linked submission ids.
        /// </summary>
        /// <value>
        /// The linked submission ids.
        /// </value>
        public List<long> LinkedSubmissionIds { get; set; }

        /// <summary>
        /// Gets or sets the started submission ids.
        /// </summary>
        /// <value>
        /// The started submission ids.
        /// </value>
        public List<long> StartedSubmissionIds { get; set; }
    }
}
