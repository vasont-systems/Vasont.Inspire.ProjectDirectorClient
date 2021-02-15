//-----------------------------------------------------------------------
// <copyright file="StreamExtensions.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Extensions
{
    using System.IO;
    using System.Text;
    using Vasont.Inspire.ProjectDirectorClient.Models;

    /// <summary>
    /// This class contains static extensions for the Stream object.
    /// </summary>
    internal static class StreamExtensions
    {
        /// <summary>
        /// This extension method is used to write a multi-part form post to a specified stream.
        /// </summary>
        /// <param name="formDataStream">Contains the form data stream to write the output to.</param>
        /// <param name="formModel">Contains the form model object to serialize to the output.</param>
        /// <param name="boundary">Contains the boundary name for the multi-part data.</param>
        /// <param name="encoding">Contains an optional encoding for string data. By default, encoding is UTF8.</param>
        public static void WriteMultiPartFormData(this Stream formDataStream, FileUploadRequestModel formModel, string boundary = "inspireBoundary", Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] lineBreakBytes = encoding.GetBytes("\r\n");

            byte[] nameContentBytes = encoding.GetBytes(
                string.Format(
                    "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\nfilename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                    boundary,
                    "file",
                    formModel.FilePath,
                    formModel.ContentType));

            formDataStream.Write(nameContentBytes, 0, nameContentBytes.Length);
            formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

            if (File.Exists(formModel.FilePath))
            { 
                byte[] fileData = File.ReadAllBytes(formModel.FilePath);

                // Write the file data directly to the Stream, rather than serializing it to a string.
                formDataStream.Write(fileData, 0, fileData.Length);
                formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
            }

            byte[] batchNameContentBytes = encoding.GetBytes(
                string.Format(
                    "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    "batchName",
                    formModel.BatchName));

            formDataStream.Write(batchNameContentBytes, 0, batchNameContentBytes.Length);
            formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

            // Only add the File Format Name if the file is Parsable
            if (!formModel.NonParsable && !formModel.ReferenceFile)
            {
                byte[] fileFormatNameContentBytes = encoding.GetBytes(
                    string.Format(
                        "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        "fileFormatName",
                        formModel.BatchName));

                formDataStream.Write(fileFormatNameContentBytes, 0, fileFormatNameContentBytes.Length);
                formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
            }

            if (formModel.ReferenceFile)
            {
                if (string.IsNullOrWhiteSpace(formModel.ReferenceFileTargetLanguageLevel))
                {
                    byte[] submissionLevelContentBytes = encoding.GetBytes(
                        string.Format(
                            "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                            boundary,
                            "submissionLevel",
                            true));

                    formDataStream.Write(submissionLevelContentBytes, 0, submissionLevelContentBytes.Length);
                    formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
                }
                else
                {
                    byte[] targetLanguageLevelContentBytes = encoding.GetBytes(
                        string.Format(
                            "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                            boundary,
                            "targetLanguageLevel",
                            formModel.ReferenceFileTargetLanguageLevel));

                    formDataStream.Write(targetLanguageLevelContentBytes, 0, targetLanguageLevelContentBytes.Length);
                    formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
                }
            }

            byte[] extractArchiveContentBytes = encoding.GetBytes(
                string.Format(
                    "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    "extractArchive",
                    formModel.ExtractArchive));

            formDataStream.Write(extractArchiveContentBytes, 0, extractArchiveContentBytes.Length);
            formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

            // Add the end of the request.  Start with a newline
            byte[] footerBytes = encoding.GetBytes("\r\n--" + boundary + "--\r\n");
            formDataStream.Write(footerBytes, 0, footerBytes.Length);
        }
    }
}
