//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TestProjectDirectorClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using MimeTypes;
    using Newtonsoft.Json;
    using Vasont.Inspire.Core.Extensions;
    using Vasont.Inspire.Core.Utility;
    using Vasont.Inspire.Models.Common;
    using Vasont.Inspire.ProjectDirectorClient;
    using Vasont.Inspire.ProjectDirectorClient.Extensions;
    using Vasont.Inspire.ProjectDirectorClient.Models;
    using Vasont.Inspire.ProjectDirectorClient.Settings;

    /// <summary>
    /// This is the main entry point of the Test Project Director Client Console Application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            string resourceUri = CommandLine.Parameters.ContainsKey("resourceUrl") ? CommandLine.Parameters["resourceUrl"].ConvertToString() : string.Empty;
            string authorityUri = CommandLine.Parameters.ContainsKey("authorityUri") ? CommandLine.Parameters["authorityUri"].ConvertToString() : string.Empty;
            string routePrefix = CommandLine.Parameters.ContainsKey("routePrefix") ? CommandLine.Parameters["routePrefix"].ConvertToString() : string.Empty;
           
            string userName = CommandLine.Parameters.ContainsKey("userName") ? CommandLine.Parameters["userName"].ConvertToString() : string.Empty;
            string password = CommandLine.Parameters.ContainsKey("password") ? CommandLine.Parameters["password"].ConvertToString() : string.Empty;

            string clientSecret = CommandLine.Parameters.ContainsKey("clientSecret") ? CommandLine.Parameters["clientSecret"].ConvertToString() : string.Empty;
            string clientId = CommandLine.Parameters.ContainsKey("clientId") ? CommandLine.Parameters["clientId"].ConvertToString() : string.Empty;

            string grantType = CommandLine.Parameters.ContainsKey("grantType") ? CommandLine.Parameters["grantType"].ConvertToString() : string.Empty;
            string clientScopes = CommandLine.Parameters.ContainsKey("clientScopes") ? CommandLine.Parameters["clientScopes"].ConvertToString() : string.Empty;
            string userAgent = "Inspire/2021.1";

            string action = CommandLine.Parameters.ContainsKey("action") ? CommandLine.Parameters["action"].ConvertToString() : string.Empty;

            long submissionId = CommandLine.Parameters.ContainsKey("subId") ? CommandLine.Parameters["subId"].ToLong() : 0;
            string idsCsv = CommandLine.Parameters.ContainsKey("ids") ? CommandLine.Parameters["ids"].ConvertToString() : string.Empty;
            string folderPath = CommandLine.Parameters.ContainsKey("folderPath") ? CommandLine.Parameters["folderPath"].ConvertToString() : System.Reflection.Assembly.GetExecutingAssembly().Location;

            Console.WriteLine("GlobalLink Vasont Inspire Project Director Rest Api Client Test.");

            try
            {
                ProjectDirectorConfigurationModel authenticationSettings = new ProjectDirectorConfigurationModel
                {
                    ResourceUri = new Uri(resourceUri),
                    AuthorityUri = new Uri(authorityUri),
                    RoutePrefix = routePrefix,
                    UserName = userName,
                    Password = password,
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    GrantType = grantType,
                    TargetResourceScopes = clientScopes.Split(","),
                    UserAgent = userAgent,
                    AuthenticationMethod = ClientAuthenticationMethods.ResourceOwnerPassword,
                    IncludeBasicAuthenticationHeader = true
                };

                using (var projectDirectorClient = new ProjectDirectorClient(authenticationSettings))
                {
                    if (AsyncHelper.RunSync(() => projectDirectorClient.AuthenticateAsync()))
                    {
                        // Authentication successful
                        Console.WriteLine($"Successfully Authenticated. Messages:{Environment.NewLine}");

                        switch (action.ToLower().Trim())
                        {
                            case "getprojects":
                                {
                                    // Get a list of Available Projects from Project Director
                                    var projects = AsyncHelper.RunSync(() => projectDirectorClient.RetrieveProjectsAsync());

                                    if (projects != null)
                                    {
                                        Console.WriteLine($"Found {projects.Count} Projects for this user");

                                        projects.ForEach(project =>
                                        {
                                            // Output the project details
                                            Console.WriteLine(JsonConvert.SerializeObject(project));
                                        });
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to retrieve Projects for this user");
                                    }

                                    break;
                                }

                            case "createsubmission":
                                {
                                    SubmissionResponseModel responseModel = new SubmissionResponseModel();

                                    var projects = AsyncHelper.RunSync(() => projectDirectorClient.RetrieveProjectsAsync());

                                    if (projects != null)
                                    {
                                        long? projectAClientId = null;
                                        Console.WriteLine($"Found {projects.Count} Projects for this user");

                                        // Since we're testing against an internal PD instance we should grab a PaProjectId
                                        var projectAClients = AsyncHelper.RunSync(() => projectDirectorClient.RetrieveProjectAClientsAsync());
                                        
                                        if (projectAClients != null && projectAClients.Any())
                                        {
                                            // Grab the first for testing
                                            projectAClientId = projectAClients.FirstOrDefault().PaClientId;
                                            Console.WriteLine($"Found {projectAClients.Count} paClients for this user and using the first id: {projectAClientId}");
                                        }
                                        
                                        // Need ProjectId, LanguageCode, and WorkflowId
                                        var project = projects.FirstOrDefault();
                                        var languageInfoModels = new List<TargetLanguageInformationModel>();
                                        var batchInfoModels = new List<BatchInformationModel>();
                                        var projectInfo = AsyncHelper.RunSync(() => projectDirectorClient.FindProjectByIdAsync(project.ProjectId));
                                        responseModel.ProjectResponse = projectInfo;

                                        // Get the first Language to use
                                        var languageDirection = projectInfo.ProjectLanguageDirections.FirstOrDefault();

                                        // Get the file format from project
                                        var fileFormatName = projectInfo.FileFormatProfiles.FirstOrDefault().Name;

                                        languageInfoModels.Add(new TargetLanguageInformationModel
                                            {
                                                TargetLanguage = languageDirection.TargetLanguage
                                            });

                                        // Add a batch for the parsable files
                                        var parsableBatchInformationModel = new BatchInformationModel
                                        {
                                            Name = "Batch1",
                                            TargetFormat = TargetFormat.TXML,
                                            WorkflowId = projectInfo.DefaultWorkflowDefinitionId,
                                            TargetLanguageInfos = languageInfoModels
                                        };

                                        batchInfoModels.Add(parsableBatchInformationModel);

                                        /*
                                        // If there are non-parsable Files to be sent with the submisison then add a separate batch
                                        var nonParsableBatchInformationModel = new BatchInformationModel
                                        {
                                            Name = "NPBatch",
                                            TargetFormat = TargetFormat.NON_PARSABLE,
                                            WorkflowId = projectInfo.DefaultWorkflowDefinitionId,
                                            TargetLanguageInfos = languageInfoModels
                                        };

                                        batchInfoModels.Add(nonParsableBatchInformationModel);

                                        // If there are reference files to be sent with the submission then add a separate batch
                                        var referenceBatchInformationModel = new BatchInformationModel
                                        {
                                            Name = "RefBatch",
                                            TargetFormat = TargetFormat.NON_PARSABLE,
                                            WorkflowId = projectInfo.DefaultWorkflowDefinitionId,
                                            TargetLanguageInfos = languageInfoModels
                                        };

                                        batchInfoModels.Add(referenceBatchInformationModel);
                                        */

                                        // Add Webhook info to MetaData
                                        List<MetaDataModel> metaData = new List<MetaDataModel>();
                                        metaData.Add(new MetaDataModel { Key = "_webhookURL", Value = "https://webhook.site/bd9c7e40-338e-4442-9832-0829b969bc00" });

                                        string[] webHookScopes = { WebHookScope.SubmissionCompleted.ToEnumMemberAttrValue(), WebHookScope.TargetCompleted.ToEnumMemberAttrValue(), WebHookScope.SubmissionCancelled.ToEnumMemberAttrValue(), WebHookScope.TargetCancelled.ToEnumMemberAttrValue() };
                                        metaData.Add(new MetaDataModel { Key = "_webhookScope", Value = string.Join(",", webHookScopes) });

                                        metaData.Add(new MetaDataModel { Key = "_webhookBasicAuthUser", Value = userName });
                                        metaData.Add(new MetaDataModel { Key = "_webhookBasicAuthPass", Value = password });

                                        List<CustomAttributeModel> customAttributes = new List<CustomAttributeModel>();
                                        customAttributes.Add(new CustomAttributeModel { Name = "Test Custom Attribute", Value = "Some value." });

                                        var requestModel = new CreateSubmissionRequestModel
                                        {
                                            Name = "Testing Inspire PD Rest Api Integration",
                                            DueDate = new DateTimeOffset(DateTime.UtcNow.AddDays(5)).ToUnixTimeMilliseconds(),
                                            SourceLanguage = languageDirection.SourceLanguage,
                                            ProjectId = projectInfo.ProjectId,
                                            BatchInfos = batchInfoModels,
                                            ClaimScope = SubmissionClaimScope.LANGUAGE,
                                            PaClientId = projectAClientId,
                                            MetaData = metaData,
                                            CustomAttributes = customAttributes
                                        };

                                        string requestModelValue = JsonConvert.SerializeObject(requestModel);
                                        Console.WriteLine($"Calling Create Submission with the following request: {Environment.NewLine} {requestModelValue}");

                                        var createSubmissionResponseModel = AsyncHelper.RunSync(() => projectDirectorClient.CreateSubmissionAsync(requestModel));
                                        responseModel.CreateSubmissionResponse = createSubmissionResponseModel;

                                        if (createSubmissionResponseModel != null && createSubmissionResponseModel is CreateSubmissionResponseModel && createSubmissionResponseModel.SubmissionId > 0)
                                        {
                                            // Success
                                            Console.WriteLine($"Successfully created empty submission with id: {createSubmissionResponseModel.SubmissionId}.");

                                            // Upload Files to submission
                                            string parsableFilePath = @"C:\Users\sduffy\Downloads\LanguageManagement.xml";
                                            FileInfo parsableFileInfo = new FileInfo(parsableFilePath);
                                            string mimeType = MimeTypeMap.GetMimeType(parsableFileInfo.Extension);

                                            var parsableFileUploadRequestModel = new FileUploadRequestModel
                                            {
                                                SubmissionId = createSubmissionResponseModel.SubmissionId,
                                                FilePath = parsableFilePath,
                                                BatchName = parsableBatchInformationModel.Name,
                                                FileFormatName = fileFormatName,
                                                ContentType = mimeType,
                                                ExtractArchive = false,
                                                NonParsable = false,
                                                ReferenceFile = false
                                            };
                                            string parsableFileUploadRequestModelValue = JsonConvert.SerializeObject(parsableFileUploadRequestModel);
                                            Console.WriteLine($"Calling Upload File with the following request: {Environment.NewLine} {parsableFileUploadRequestModelValue}");

                                            var parsableFileUploadResponseModel = AsyncHelper.RunSync(() => projectDirectorClient.UploadFileAsync(parsableFileUploadRequestModel));
                                            responseModel.FileUploadResponses.Add(parsableFileUploadResponseModel);

                                            if (parsableFileUploadResponseModel != null && parsableFileUploadResponseModel.ProcessId != null)
                                            {
                                                Console.WriteLine($"Uploaded parsable file \"{parsableFilePath}\" for submission with id: {createSubmissionResponseModel.SubmissionId}.");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Failed to upload file \"{parsableFilePath}\" for submission with id: {createSubmissionResponseModel.SubmissionId}.");

                                                WriteLastErrorMessages(projectDirectorClient);
                                            }

                                            // Save Submission
                                            var saveSubmissionRequest = new SaveSubmissionRequestModel { AutoStart = true };
                                            string saveSubmissionRequestValue = JsonConvert.SerializeObject(saveSubmissionRequest);
                                            Console.WriteLine($"Calling Save Submission with the following request: {Environment.NewLine} {saveSubmissionRequestValue}");
                                            
                                            var saveSubmissionResponse = AsyncHelper.RunSync(() => projectDirectorClient.SaveSubmissionAsync(createSubmissionResponseModel.SubmissionId, saveSubmissionRequest));
                                            responseModel.SaveSubmissionResponse = saveSubmissionResponse;

                                            if (saveSubmissionResponse != null)
                                            {
                                                Console.WriteLine($"Saved submission with id: {createSubmissionResponseModel.SubmissionId}.{Environment.NewLine}Message: {saveSubmissionResponse.Message} {Environment.NewLine} Linked Submission Ids: {string.Join(',', saveSubmissionResponse.LinkedSubmissionIds)} {Environment.NewLine} Started Submission Ids: {string.Join(',', saveSubmissionResponse.StartedSubmissionIds)} {Environment.NewLine}");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Failed to save submission with id: {createSubmissionResponseModel.SubmissionId}.");

                                                WriteLastErrorMessages(projectDirectorClient);
                                            }

                                            // Get full word-count
                                            var wordCountRequestModel = new WordCountRequestModel();
                                            string wordCountRequestModelValue = JsonConvert.SerializeObject(wordCountRequestModel);
                                            Console.WriteLine($"Calling Retrieve Word Count with the following request: {Environment.NewLine} {wordCountRequestModelValue}");
                                            
                                            var fullWordCountResponseModel = AsyncHelper.RunSync(() => projectDirectorClient.RetrieveWordCountAsync(createSubmissionResponseModel.SubmissionId, wordCountRequestModel));

                                            if (fullWordCountResponseModel != null)
                                            {
                                                Console.WriteLine($"Full word count: {fullWordCountResponseModel.TotalWordCount} for submission with id: {createSubmissionResponseModel.SubmissionId}.");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Failed to get full word count for submission with id: {createSubmissionResponseModel.SubmissionId}.");

                                                WriteLastErrorMessages(projectDirectorClient);
                                            }

                                            string responseModelValue = JsonConvert.SerializeObject(responseModel);
                                            Console.WriteLine($"Submission Response Model: {responseModelValue}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to create empty submission.");

                                            WriteLastErrorMessages(projectDirectorClient);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to retrieve Projects for this user");
                                    }

                                    break;
                                }

                            case "retrievecompletedtargets":
                                {
                                    List<long> submissionIds = idsCsv.Split(",").Select(s => s.ToLong()).ToList();

                                    if (submissionIds.Any())
                                    {
                                        Console.WriteLine($"Requesting completed targets for the following submission ids: {string.Join(",", submissionIds)}.");

                                        var completedTargets = AsyncHelper.RunSync(() => projectDirectorClient.RetrieveCompletedTargetsAsync(submissionIds));

                                        if (completedTargets != null)
                                        {
                                            Console.WriteLine(JsonConvert.SerializeObject(completedTargets));
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to retrieve completed targets.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No submission Ids to request completed targets.");
                                    }

                                    break;
                                }

                            case "requestdownloadtargets":
                                {
                                    List<long> targetIds = idsCsv.Split(",").Select(s => s.ToLong()).ToList();

                                    if (targetIds.Any())
                                    {
                                        Console.WriteLine($"Requesting target downloads for the following target ids: {string.Join(",", targetIds)}.");

                                        var downloadResponse = AsyncHelper.RunSync(() => projectDirectorClient.RequestDownloadTargetsAsync(submissionId, targetIds));

                                        if (downloadResponse != null)
                                        {
                                            int retries = 0;

                                            while (!downloadResponse.ProcessingFinished && retries < 10)
                                            {
                                                // Continually get status until finished
                                                downloadResponse = AsyncHelper.RunSync(() => projectDirectorClient.RequestDownloadStatusAsync(submissionId, downloadResponse.DownloadId));

                                                if (downloadResponse != null && !downloadResponse.ProcessingFinished)
                                                {
                                                    retries++;
                                                    int numberOfSeconds = 60 + Math.Pow(retries.ConvertToString().ToLong(), 4).ConvertToString().ToInt();
                                                    Console.WriteLine($"Download Processing not finished. Will retry in {numberOfSeconds} seconds.");

                                                    Thread.Sleep(numberOfSeconds * 1000);
                                                }
                                                else
                                                {
                                                    // Processing is finished so Break out of the while loop
                                                    break;
                                                }
                                            }

                                            Console.WriteLine(JsonConvert.SerializeObject(downloadResponse));

                                            if (downloadResponse.ProcessingFinished)
                                            {
                                                // Download the file(s)
                                                var downloadBytes = AsyncHelper.RunSync(() => projectDirectorClient.DownloadFileAsync(downloadResponse.DownloadId));

                                                if (downloadBytes != null && downloadBytes.Length > 0)
                                                {
                                                    string submissionPath = Path.Combine(folderPath, submissionId.ConvertToString());

                                                    if (!Directory.Exists(submissionPath))
                                                    {
                                                        Directory.CreateDirectory(submissionPath);
                                                    }

                                                    string downloadFilePath = Path.Combine(submissionPath, submissionId.ConvertToString().AppendSuffix(".zip"));

                                                    // store the file to the local folder
                                                    File.WriteAllBytes(downloadFilePath, downloadBytes);

                                                    if (File.Exists(downloadFilePath))
                                                    {
                                                        Console.WriteLine($"File downloaded and saved to \"{downloadFilePath}\"");

                                                        // Mark the download as complete
                                                        MarkTargetsDeliveredModel markTargetsDeliveredModel = new MarkTargetsDeliveredModel
                                                        {
                                                             TargetIds = targetIds
                                                        };

                                                        bool markedComplete = AsyncHelper.RunSync(() => projectDirectorClient.MarkTargetsDeliveredAsync(submissionId, markTargetsDeliveredModel));

                                                        Console.WriteLine($"Marked Targets \"{idsCsv}\" delivered: {markedComplete}.");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to retrieve completed targets.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No target Ids to request target downloads.");
                                    }

                                    break;
                                }
                        }
                    }
                    else
                    {
                        // Could not Authenticate
                        Console.WriteLine($"Could not Authenticate. Messages:{Environment.NewLine}");

                        projectDirectorClient.LastErrorResponse.Messages.ForEach(error =>
                        {
                            Console.WriteLine($"{error.Message} {Environment.NewLine}");
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Exception was thrown. {Environment.NewLine} Error: {ex.Message} {Environment.NewLine} Stack Trace: {ex.StackTrace} {Environment.NewLine}");
            }

            // wait
            Console.WriteLine("Press any key to close the console.");
            Console.ReadKey();
        }

        /// <summary>
        /// Writes the last error messages to the console.
        /// </summary>
        /// <param name="projectDirectorClient">The project director client.</param>
        private static void WriteLastErrorMessages(ProjectDirectorClient projectDirectorClient)
        {
            if (projectDirectorClient.LastErrorResponse != null)
            {
                projectDirectorClient.LastErrorResponse.Messages.ForEach(message =>
                {
                    Console.WriteLine($"{message.ErrorType} Message: {message.Message} {Environment.NewLine}");
                });
            }
        }
    }
}
