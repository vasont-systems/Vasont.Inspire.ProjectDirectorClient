

using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Vasont.Inspire.ProjectDirector.Client.Settings;

namespace Vasont.Inspire.ProjectDirector.Client.Authentication
{
    public static class AuthenticationExtensions
    {
        public static async Task<bool> AuthenticateAsync(this ProjectDirectorClient projectDirectorClient, CancellationToken cancellationToken = default)
        {
            if (projectDirectorClient.configuration == null)
            {
                throw new NoNullAllowedException($"{nameof(projectDirectorClient.configuration)} cannot be null.");
            }

            // Check to see if an AuthorizationToken is not stored or expired
            if (string.IsNullOrWhiteSpace(projectDirectorClient.AuthorizationToken.Token) ||
                DateTime.UtcNow >= projectDirectorClient.AuthorizationToken.TokenExpiration)
            {
                HttpResponseMessage response = null;

                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, projectDirectorClient.configuration.TokenUrl + "/oauth/token");
                    var basicAuthHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{projectDirectorClient.configuration.ClientId}:{projectDirectorClient.configuration.ClientSecret}"));

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", basicAuthHeader);
                    request.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true };
                    request.Content = new StringContent($"grant_type=password&username={projectDirectorClient.configuration.UserName}&password={projectDirectorClient.configuration.Password}&scope={projectDirectorClient.configuration.ClientScopes}");
                    request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded");

                    response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
                }

                if (response == null && !response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    projectDirectorClient.Messages.Add($"Get Token fail with{Environment.NewLine}{errorContent}");
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var token = JObject.Parse(result)["access_token"].ToString();

                    projectDirectorClient.Messages.Add($"GetToken SUCCESS client id: {projectDirectorClient.configuration.ClientId} username: {projectDirectorClient.configuration.UserName} scopes: {projectDirectorClient.configuration.ClientScopes}{Environment.NewLine}{token}");

                    var expirationSeconds = int.Parse(JObject.Parse(result)["expires_in"].ToString());
                    var expirationDate = DateTime.UtcNow.AddSeconds(expirationSeconds);

                    projectDirectorClient.AuthorizationToken = (token, expirationDate);
                }
            }

            return !string.IsNullOrWhiteSpace(projectDirectorClient.AuthorizationToken.Token) &&
                DateTime.UtcNow <= projectDirectorClient.AuthorizationToken.TokenExpiration;
        }
    }
}
