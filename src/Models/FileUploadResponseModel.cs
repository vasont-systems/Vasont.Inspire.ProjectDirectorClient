//-----------------------------------------------------------------------
// <copyright file="FileUploadResponseModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System;

    /// <summary>
    /// This class represents the File Upload Response Model
    /// </summary>
    public class FileUploadResponseModel
    {
        /// <summary>
        /// Gets or sets the process identifier.
        /// </summary>
        /// <value>
        /// The process identifier.
        /// </value>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// Gets or sets the submission identifier.
        /// </summary>
        /// <value>
        /// The submission identifier.
        /// </value>
        public long SubmissionId { get; set; }
    }
}
