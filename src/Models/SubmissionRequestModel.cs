//-----------------------------------------------------------------------
// <copyright file="SubmissionRequestModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This class represents the Project Director Submission Request Model that will be stored with the Translation Job record.
    /// </summary>
    public class SubmissionRequestModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubmissionRequestModel"/> class.
        /// </summary>
        public SubmissionRequestModel()
        {
            this.Messages = new List<string>();
            this.RequestDate = DateTime.UtcNow;
            this.CreateSubmissionRequest = new CreateSubmissionRequestModel();
            this.FileUploadRequests = new List<FileUploadRequestModel>();
            this.SaveSubmissionRequest = new SaveSubmissionRequestModel();
            this.WordCountRequest = new WordCountRequestModel();
        }

        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        /// <value>
        /// The request date.
        /// </value>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public List<string> Messages { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public long ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the submission identifier.
        /// </summary>
        /// <value>
        /// The submission identifier.
        /// </value>
        public long SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the create submission request.
        /// </summary>
        /// <value>
        /// The create submission request.
        /// </value>
        public CreateSubmissionRequestModel CreateSubmissionRequest { get; set; }

        /// <summary>
        /// Gets or sets the file upload requests.
        /// </summary>
        /// <value>
        /// The file upload requests.
        /// </value>
        public List<FileUploadRequestModel> FileUploadRequests { get; set; }

        /// <summary>
        /// Gets or sets the save submission request.
        /// </summary>
        /// <value>
        /// The save submission request.
        /// </value>
        public SaveSubmissionRequestModel SaveSubmissionRequest { get; set; }

        /// <summary>
        /// Gets or sets the word count request.
        /// </summary>
        /// <value>
        /// The word count request.
        /// </value>
        public WordCountRequestModel WordCountRequest { get; set; }
    }
}
