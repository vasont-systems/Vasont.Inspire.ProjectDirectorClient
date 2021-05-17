//-----------------------------------------------------------------------
// <copyright file="SubmissionResponseModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the Submission Response Model
    /// </summary>
    public class SubmissionResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubmissionResponseModel"/> class.
        /// </summary>
        public SubmissionResponseModel()
        {
            this.ResponseDate = DateTime.UtcNow;
            this.FileUploadResponses = new List<FileUploadResponseModel>();
        }

        /// <summary>
        /// Gets or sets the response date.
        /// </summary>
        /// <value>
        /// The response date.
        /// </value>
        public DateTime ResponseDate { get; set; }

        /// <summary>
        /// Gets or sets the create submission response.
        /// </summary>
        /// <value>
        /// The create submission response.
        /// </value>
        public CreateSubmissionResponseModel CreateSubmissionResponse { get; set; }

        /// <summary>
        /// Gets or sets the project response.
        /// </summary>
        /// <value>
        /// The project response.
        /// </value>
        public ProjectModel FindProjectByIdResponse { get; set; }

        /// <summary>
        /// Gets or sets the find project a clients by identifier response.
        /// </summary>
        /// <value>
        /// The find project a clients by identifier response.
        /// </value>
        public ProjectAClientModel FindProjectAClientsByIdResponse { get; set; }

        /// <summary>
        /// Gets or sets the file upload responses.
        /// </summary>
        /// <value>
        /// The file upload responses.
        /// </value>
        public List<FileUploadResponseModel> FileUploadResponses { get; set; }

        /// <summary>
        /// Gets or sets the save submission response.
        /// </summary>
        /// <value>
        /// The save submission response.
        /// </value>
        public SaveSubmissionResponseModel SaveSubmissionResponse { get; set; }
    }
}
