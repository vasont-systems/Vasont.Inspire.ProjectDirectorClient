//-----------------------------------------------------------------------
// <copyright file="WordCountResponseModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the Word Count Response Model
    /// </summary>
    public class WordCountResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WordCountResponseModel"/> class.
        /// </summary>
        public WordCountResponseModel()
        {
            this.CumulativeTmStatistics = new List<KeyValuePair<long, string>>();
            this.CanceledTmStatistics = new List<KeyValuePair<long, string>>();
        }

        /// <summary>
        /// Gets or sets the name of the submission.
        /// </summary>
        /// <value>
        /// The name of the submission.
        /// </value>
        public string SubmissionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the batch.
        /// </summary>
        /// <value>
        /// The name of the batch.
        /// </value>
        public string BatchName { get; set; }

        /// <summary>
        /// Gets or sets the target identifier.
        /// </summary>
        /// <value>
        /// The target identifier.
        /// </value>
        public long TargetId { get; set; }

        /// <summary>
        /// Gets or sets the name of the target.
        /// </summary>
        /// <value>
        /// The name of the target.
        /// </value>
        public string TargetName { get; set; }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public long DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the document.
        /// </summary>
        /// <value>
        /// The name of the document.
        /// </value>
        public string DocumentName { get; set; }

        /// <summary>
        /// Gets or sets the total word count.
        /// </summary>
        /// <value>
        /// The total word count.
        /// </value>
        public long TotalWordCount { get; set; }

        /// <summary>
        /// Gets or sets the target language.
        /// </summary>
        /// <value>
        /// The target language.
        /// </value>
        public string TargetLanguage { get; set; }

        /// <summary>
        /// Gets or sets the cumulative tm statistics.
        /// </summary>
        /// <value>
        /// The cumulative tm statistics.
        /// </value>
        public List<KeyValuePair<long, string>> CumulativeTmStatistics { get; set; }

        /// <summary>
        /// Gets or sets the canceled tm statistics.
        /// </summary>
        /// <value>
        /// The canceled tm statistics.
        /// </value>
        public List<KeyValuePair<long, string>> CanceledTmStatistics { get; set; }
    }
}
