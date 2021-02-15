//-----------------------------------------------------------------------
// <copyright file="CreateSubmissionResponseModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the Create Submission Response Model
    /// </summary>
    public class CreateSubmissionResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSubmissionResponseModel"/> class.
        /// </summary>
        public CreateSubmissionResponseModel()
        {
            this.Messages = new List<string>();
        }

        /// <summary>
        /// Gets or sets the submission identifier.
        /// </summary>
        /// <value>
        /// The submission identifier.
        /// </value>
        public long SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public List<string> Messages { get; set; }
    }
}
