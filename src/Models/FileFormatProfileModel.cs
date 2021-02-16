//-----------------------------------------------------------------------
// <copyright file="FileFormatProfileModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the Project Director File Format Profile Model.
    /// </summary>
    public class FileFormatProfileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileFormatProfileModel"/> class.
        /// </summary>
        public FileFormatProfileModel()
        {
            this.MimeTypes = new List<string>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FileFormatProfileModel"/> is default.
        /// </summary>
        /// <value>
        ///   <c>true</c> if default; otherwise, <c>false</c>.
        /// </value>
        public bool Default { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FileFormatProfileModel"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [non parsable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [non parsable]; otherwise, <c>false</c>.
        /// </value>
        public bool NonParsable { get; set; }

        /// <summary>
        /// Gets or sets the target format.
        /// </summary>
        /// <value>
        /// The target format.
        /// </value>
        public string TargetFormat { get; set; }

        /// <summary>
        /// Gets or sets the MIME types.
        /// </summary>
        /// <value>
        /// The MIME types.
        /// </value>
        public List<string> MimeTypes { get; set; }

        /// <summary>
        /// Gets or sets the file format identifier.
        /// </summary>
        /// <value>
        /// The file format identifier.
        /// </value>
        public long FileFormatId { get; set; }

        /// <summary>
        /// Gets or sets the date updated.
        /// </summary>
        /// <value>
        /// The date updated.
        /// </value>
        public long DateUpdated { get; set; }
    }
}
