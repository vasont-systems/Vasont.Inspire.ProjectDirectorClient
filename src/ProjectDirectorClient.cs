//-----------------------------------------------------------------------
// <copyright file="ProjectDirectorClient.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Vasont.Inspire.ProjectDirectorClient.Extensions;
    using Vasont.Inspire.ProjectDirectorClient.Models;
    using Vasont.Inspire.ProjectDirectorClient.Settings;

    /// <summary>
    /// This class represents the Project Director Client used to communicate with the Rest Api
    /// </summary>
    /// <seealso cref="Vasont.Inspire.ProjectDirectorClient.BaseClient" />
    /// <seealso cref="Vasont.Inspire.ProjectDirector.Client.BaseClient" />
    public class ProjectDirectorClient : BaseClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDirectorClient" /> class.
        /// </summary>
        /// <param name="authenticationSettings">The authentication settings.</param>
        public ProjectDirectorClient(ProjectDirectorConfigurationModel authenticationSettings)
            : base(authenticationSettings)
        {
        }

        #region Projects

        /// <summary>
        /// Retrieves the projects from Project Director.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="cancellationToken">Contains the cancellation token.</param>
        /// <returns>
        /// Returns a <see cref="List{ProjectDirectorProjectModel}" /> objects.
        /// </returns>
        public async Task<List<ProjectMicroModel>> RetrieveProjectsAsync(long pageSize = 50, string orderBy = "projectName", CancellationToken cancellationToken = default(CancellationToken))
        {
            List<ProjectMicroModel> returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/projects?pageSize={pageSize}&orderBy={orderBy} ");
                returnValue = this.RequestContent<List<ProjectMicroModel>>(request);
            }

            return returnValue;
        }

        /// <summary>
        /// Finds the project by identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">Contains the cancellation token.</param>
        /// <returns>
        /// Returns a <see cref="ProjectMicroModel" /> object.
        /// </returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the projectId parameter is less than or equal to zero.</exception>
        public async Task<ProjectMicroModel> FindProjectByIdAsync(long projectId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            ProjectMicroModel returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/project/{projectId}");
                returnValue = this.RequestContent<ProjectMicroModel>(request);
            }

            return returnValue;
        }

        /// <summary>
        /// Finds the projects by name.
        /// </summary>
        /// <param name="projectNameFilter">The project name filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Returns a <see cref="List{ProjectDirectorProjectModel}" /> objects.
        /// </returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the projectNameFilter parameter is null or whitespace.</exception>
        public async Task<List<ProjectMicroModel>> FindProjectsByNameAsync(string projectNameFilter, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(projectNameFilter))
            {
                throw new ArgumentNullException(nameof(projectNameFilter));
            }

            List<ProjectMicroModel> returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/projects?projectName={projectNameFilter} ");
                returnValue = this.RequestContent<List<ProjectMicroModel>>(request);
            }

            return returnValue;
        }

        #endregion Projects

        #region ProjectA
        /// <summary>
        /// Retrieves the project a clients.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Returns a <see cref="List{ProjectAClientModel}}" /> objects.
        /// </returns>
        public async Task<List<ProjectAClientModel>> RetrieveProjectAClientsAsync(long pageSize = 50, string orderBy = "paClientId", CancellationToken cancellationToken = default(CancellationToken))
        {
            List<ProjectAClientModel> returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/paclients?pageSize={pageSize}&orderBy={orderBy} ");
                returnValue = this.RequestContent<List<ProjectAClientModel>>(request);
            }

            return returnValue;
        }

        /// <summary>
        /// Finds the project a clients by name.
        /// </summary>
        /// <param name="projectAClientName">Name of the project a client.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Returns a <see cref="List{ProjectAClientModel}}" /> objects.
        /// </returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the projectAClientName parameter is null or whitespace.</exception>
        public async Task<List<ProjectAClientModel>> FindProjectAClientsByNameAsync(string projectAClientName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(projectAClientName))
            {
                throw new ArgumentNullException(nameof(projectAClientName));
            }

            List<ProjectAClientModel> returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/paclients?projectAClientName={projectAClientName} ");
                returnValue = this.RequestContent<List<ProjectAClientModel>>(request);
            }

            return returnValue;
        }

        /// <summary>
        /// Retrieves the custom attributes.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a <see cref="List{CustomAttributeModel}"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the projectId parameter is less than or equal to zero.</exception>
        public async Task<List<CustomAttributeModel>> RetrieveCustomAttributesAsync(long projectId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            List<CustomAttributeModel> returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/project/{projectId}/customattributes");
                returnValue = this.RequestContent<List<CustomAttributeModel>>(request);
            }

            return returnValue;
        }

        /// <summary>
        /// Retrieves the project organization users.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a <see cref="List{OrganizationUserModel}"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the projectId parameter is less than or equal to zero.</exception>
        public async Task<List<OrganizationUserModel>> RetrieveProjectOrganizationUsersAsync(long projectId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (projectId <= 0)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            List<OrganizationUserModel> returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/project/{projectId}/users/org");
                returnValue = this.RequestContent<List<OrganizationUserModel>>(request);
            }

            return returnValue;
        }
        #endregion ProjectA

        #region Submission
        /// <summary>
        /// Creates the submission.
        /// </summary>
        /// <param name="requestModel">The request model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns the <see cref="CreateSubmissionResponseModel"/> object.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the requestModel parameter is null.</exception>
        public async Task<CreateSubmissionResponseModel> CreateSubmissionAsync(CreateSubmissionRequestModel requestModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (requestModel == null)
            {
                throw new ArgumentNullException(nameof(requestModel));
            }

            CreateSubmissionResponseModel returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/submissions/create", HttpMethod.Post);
                returnValue = this.RequestContent<CreateSubmissionRequestModel, CreateSubmissionResponseModel>(request, requestModel);
            }

            return returnValue;
        }

        /// <summary>
        /// Uploads the translatable file.
        /// </summary>
        /// <param name="requestModel">The request model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Returns the <see cref="FileUploadResponseModel" /> object.
        /// </returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the requestModel parameter is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown when file \"{requestModel.FilePath}\" not found.</exception>
        public async Task<FileUploadResponseModel> UploadFileAsync(FileUploadRequestModel requestModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (requestModel == null)
            {
                throw new ArgumentNullException(nameof(requestModel));
            }

            if (!File.Exists(requestModel.FilePath))
            {
                throw new ArgumentException($"File \"{requestModel.FilePath}\" not found.");
            }

            FileUploadResponseModel returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                string boundary = "InspireFormBoundary";

                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/submissions/{requestModel.SubmissionId}/upload/source", HttpMethod.Post, contentType: "multipart/form-data; boundary=" + boundary);

                // write data out to the request stream
                using (var postStream = request.GetRequestStream())
                {
                    postStream.WriteMultiPartFormData(requestModel, boundary);
                }

                returnValue = this.RequestContent<FileUploadRequestModel, FileUploadResponseModel>(request, requestModel);
            }

            return returnValue;
        }

        /// <summary>
        /// Uploads the reference file.
        /// </summary>
        /// <param name="requestModel">The request model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Returns the <see cref="FileUploadResponseModel" /> object.
        /// </returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the requestModel parameter is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown when file \"{requestModel.FilePath}\" not found.</exception>
        public async Task<FileUploadResponseModel> UploadReferenceFileAsync(FileUploadRequestModel requestModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (requestModel == null)
            {
                throw new ArgumentNullException(nameof(requestModel));
            }

            if (!File.Exists(requestModel.FilePath))
            {
                throw new ArgumentException($"File \"{requestModel.FilePath}\" not found.");
            }

            FileUploadResponseModel returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                string boundary = "InspireFormBoundary";

                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/submissions/{requestModel.SubmissionId}/upload/reference ", HttpMethod.Post, contentType: "multipart/form-data; boundary=" + boundary);

                // write data out to the request stream
                using (var postStream = request.GetRequestStream())
                {
                    postStream.WriteMultiPartFormData(requestModel, boundary);
                }

                returnValue = this.RequestContent<FileUploadRequestModel, FileUploadResponseModel>(request, requestModel);
            }

            return returnValue;
        }

        /// <summary>
        /// Retrieves the submission status.
        /// </summary>
        /// <param name="submissionId">The submission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns the <see cref="SubmissionStatusModel"/> object.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the submissionId parameter is less than or equal to zero.</exception>
        public async Task<SubmissionStatusModel> RetrieveSubmissionStatusAsync(long submissionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (submissionId <= 0)
            {
                throw new ArgumentNullException(nameof(submissionId));
            }

            SubmissionStatusModel returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/submissions/{submissionId}/status");
                returnValue = this.RequestContent<SubmissionStatusModel>(request);
            }

            return returnValue;
        }

        /// <summary>
        /// Saves the submission.
        /// </summary>
        /// <param name="submissionId">The submission identifier.</param>
        /// <param name="requestModel">The request model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Returns the <see cref="SaveSubmissionResponseModel" /> object.
        /// </returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the submissionId parameter is less than or equal to zero or requestModel is null.</exception>
        public async Task<SaveSubmissionResponseModel> SaveSubmissionAsync(long submissionId, SaveSubmissionRequestModel requestModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (submissionId <= 0)
            {
                throw new ArgumentNullException(nameof(submissionId));
            }

            if (requestModel == null)
            {
                throw new ArgumentNullException(nameof(requestModel));
            }

            SaveSubmissionResponseModel returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/submissions/{submissionId}/save", HttpMethod.Post);
                returnValue = this.RequestContent<SaveSubmissionRequestModel, SaveSubmissionResponseModel>(request, requestModel);
            }

            return returnValue;
        }

        /// <summary>
        /// Starts the submission.
        /// IMPORTANT NOTE: This step only applies to submissions that are on hold and analyzed. 
        /// If you don't want to move your submission to the Active folder, you can skip this step.
        /// If you set auto-start: false when you called POST /save and then analyzed your submission and now you want to move it to the Active folder, you can call this endpoint.
        /// Note that this is normally not necessary. In normal circumstances, if you want to analyze and start your submission, you call POST /save with autoStart: true or, if you want to just put it on hold, you call POST /save with autoStart: false and then choose whether to retrieve the wordcount or not. This endpoint is reserved for very specific scenarios.
        /// </summary>
        /// <param name="submissionId">The submission identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a <see cref="SubmissionStatusModel"/> object.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when the submissionId parameter is less than or equal to zero.</exception>
        public async Task<SubmissionStatusModel> StartSubmissionAsync(long submissionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (submissionId <= 0)
            {
                throw new ArgumentNullException(nameof(submissionId));
            }

            SubmissionStatusModel returnValue = null;

            if (await this.AuthenticateAsync(string.Empty, cancellationToken).ConfigureAwait(false))
            {
                var request = this.CreateRequest($"{this.Configuration.RoutePrefix}/submissions/{submissionId}/start", HttpMethod.Post);
                returnValue = this.RequestContent<SubmissionStatusModel>(request);
            }

            return returnValue;
        }
        #endregion Submission
    }
}
