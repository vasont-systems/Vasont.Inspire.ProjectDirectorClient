//-----------------------------------------------------------------------
// <copyright file="RetrievalResponseModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System;

    /// <summary>
    /// This class represents the Project Director Retrieval Response Model that will be stored with the Translation Job record.
    /// </summary>
    public class RetrievalResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetrievalResponseModel"/> class.
        /// </summary>
        public RetrievalResponseModel()
        {
            this.ResponseDate = DateTime.UtcNow;
            this.RequestDownloadTargetsResponse = new RequestDownloadTargetsResponseModel();
        }

        /// <summary>
        /// Gets or sets the response date.
        /// </summary>
        /// <value>
        /// The response date.
        /// </value>
        public DateTime ResponseDate { get; set; }

        /// <summary>
        /// Gets or sets the request download targets response.
        /// </summary>
        /// <value>
        /// The request download targets response.
        /// </value>
        public RequestDownloadTargetsResponseModel RequestDownloadTargetsResponse { get; set; }

        /// <summary>
        /// Gets or sets the length of the download bytes.
        /// </summary>
        /// <value>
        /// The length of the download bytes.
        /// </value>
        public long DownloadBytesLength { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [marked targets delivered].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [marked targets delivered]; otherwise, <c>false</c>.
        /// </value>
        public bool MarkedTargetsDelivered { get; set; }
    }
}
