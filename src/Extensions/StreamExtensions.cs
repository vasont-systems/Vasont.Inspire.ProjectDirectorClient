//-----------------------------------------------------------------------
// <copyright file="StreamExtensions.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Extensions
{
    using System;
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
        /// <exception cref="ArgumentException">Exception thrown if file \"{formModel.FilePath}\" was not found while Writing Multi-part Form Data to the Form Data Stream.</exception>
        public static void WriteMultiPartFormData(this Stream formDataStream, FileUploadRequestModel formModel, string boundary = "inspireBoundary", Encoding encoding = null)
        {
            if (!File.Exists(formModel.FilePath))
            { 
                throw new ArgumentException($"File \"{formModel.FilePath}\" was not found while Writing Multi-part Form Data to the Form Data Stream.");
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] lineBreakBytes = encoding.GetBytes("\r\n");

            string nameContent =
                string.Format(
                    "----{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n",
                    boundary,
                    "file",
                    Path.GetFileName(formModel.FilePath),
                    formModel.ContentType);

            formDataStream.Write(encoding.GetBytes(nameContent), 0, encoding.GetByteCount(nameContent));
            formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

            if (File.Exists(formModel.FilePath))
            {
                var fileData = File.ReadAllBytes(formModel.FilePath);

                // Write the file data directly to the Stream, rather than serializing it to a string.
                formDataStream.Write(fileData, 0, fileData.Length);
                formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
            }

            string batchNameContent =
                string.Format(
                    "----{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\nContent-Type: text/plain\r\n\r\n{2}",
                    boundary,
                    "batchName",
                    formModel.BatchName);

            formDataStream.Write(encoding.GetBytes(batchNameContent), 0, encoding.GetByteCount(batchNameContent));
            formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

            // Only add the File Format Name if the file is Parsable
            if (!formModel.NonParsable && !formModel.ReferenceFile)
            {
                string fileFormatNameContent =
                    string.Format(
                        "----{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\nContent-Type: text/plain\r\n\r\n{2}",
                        boundary,
                        "fileFormatName",
                        formModel.FileFormatName);

                formDataStream.Write(encoding.GetBytes(fileFormatNameContent), 0, encoding.GetByteCount(fileFormatNameContent));
                formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
            }

            if (formModel.ReferenceFile)
            {
                if (string.IsNullOrWhiteSpace(formModel.ReferenceFileTargetLanguageLevel))
                {
                    string submissionLevelContent = string.Format(
                            "----{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\nContent-Type: text/plain\r\n\r\n{2}",
                            boundary,
                            "submissionLevel",
                            true);

                    formDataStream.Write(encoding.GetBytes(submissionLevelContent), 0, encoding.GetByteCount(submissionLevelContent));
                    formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
                }
                else
                {
                    string targetLanguageLevelContent = string.Format(
                            "----{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\nContent-Type: text/plain\r\n\r\n{2}",
                            boundary,
                            "targetLanguageLevel",
                            formModel.ReferenceFileTargetLanguageLevel);

                    formDataStream.Write(encoding.GetBytes(targetLanguageLevelContent), 0, encoding.GetByteCount(targetLanguageLevelContent));
                    formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);
                }
            }

            string extractArchvieContent =
                string.Format(
                    "----{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\nContent-Type: text/plain\r\n\r\n{2}",
                    boundary,
                    "extractArchive",
                    formModel.ExtractArchive.ToString().ToLower());

            formDataStream.Write(encoding.GetBytes(extractArchvieContent), 0, encoding.GetByteCount(extractArchvieContent));
            formDataStream.Write(lineBreakBytes, 0, lineBreakBytes.Length);

            // Add the end of the request.
            string footerContent = string.Format("----" + boundary + "--\r\n");

            formDataStream.Write(encoding.GetBytes(footerContent), 0, encoding.GetByteCount(footerContent));
        }

        /// <summary>
        /// Reads all bytes.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>Returns a byte array containing the contents of the stream.</returns>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            byte[] returnBytes = { };

            if (stream is MemoryStream)
            {
                returnBytes = ((MemoryStream)stream).ToArray();
            }

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                returnBytes = memoryStream.ToArray();
            }

            return returnBytes;
        }
    }
}
