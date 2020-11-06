//-----------------------------------------------------------------------
// <copyright file="ProjectDirectorClient.cs" company="GlobalLink Vasont">
// Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Vasont.Inspire.ProjectDirector.Client
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Vasont.Inspire.ProjectDirector.Client.Models;
    using Vasont.Inspire.ProjectDirector.Client.Settings;

    /// <summary>
    /// This class represents the Project Director Client used to communicate with the Rest Api
    /// </summary>
    /// <seealso cref="Vasont.Inspire.ProjectDirector.Client.BaseClient" />
    public class ProjectDirectorClient : BaseClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDirectorClient"/> class.
        /// </summary>
        /// <param name="authenticationSettings">The authentication settings.</param>
        public ProjectDirectorClient(ProjectDirectorConfigurationModel authenticationSettings)
            : base(authenticationSettings)
        {
        }

        /// <summary>
        /// Retrieves the projects from Project Director.
        /// </summary>
        /// <param name="cancellationToken">Contains the cancellation token.</param>
        /// <returns>Returns a <see cref="List{ProjectDirectorProjectModel}"/> objects.</returns>
        public async Task<List<ProjectDirectorProjectModel>> RetrieveProjectsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            List<ProjectDirectorProjectModel> returnValue = null;

            if (!this.HasAuthenticated)
            {
                await this.AuthenticateAsync(string.Empty, cancellationToken)
                    .ConfigureAwait(false);
            }

            if (this.HasAuthenticated)
            {
                var request = this.CreateRequest($"/rest/v0/projects");
                returnValue = this.RequestContent<List<ProjectDirectorProjectModel>>(request);
            }

            return returnValue;
        }
    }
}
