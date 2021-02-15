//-----------------------------------------------------------------------
// <copyright file="FileUploadRequestModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    /// <summary>
    /// This class represents the File Upload Request Model
    /// </summary>
    public class FileUploadRequestModel
    {
        /// <summary>
        /// Gets or sets the submission identifier.
        /// </summary>
        /// <value>
        /// The submission identifier.
        /// </value>
        public long SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the batch.
        /// </summary>
        /// <value>
        /// The name of the batch.
        /// </value>
        public string BatchName { get; set; }

        /// <summary>
        /// Gets or sets the name of the file format.
        /// </summary>
        /// <value>
        /// The name of the file format.
        /// </value>
        public string FileFormatName { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [extract archive].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [extract archive]; otherwise, <c>false</c>.
        /// </value>
        public bool ExtractArchive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the File is [non parsable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if the File is [non parsable]; otherwise, <c>false</c>.
        /// </value>
        public bool NonParsable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the file is a [reference file].
        /// </summary>
        /// <value>
        ///   <c>true</c> if the file is a [reference file]; otherwise, <c>false</c>.
        /// </value>
        public bool ReferenceFile { get; set; }

        /// <summary>
        /// Gets or sets the reference file target language level.
        /// </summary>
        /// <value>
        /// The reference file target language level.
        /// </value>
        public string ReferenceFileTargetLanguageLevel { get; set; }
    }
}
