//-----------------------------------------------------------------------
// <copyright file="BaseClient.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Cache;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using IdentityModel.Client;
    using Newtonsoft.Json;
    using Vasont.Inspire.Core.Errors;
    using Vasont.Inspire.Models.Common;
    using Vasont.Inspire.ProjectDirectorClient.Extensions;
    using Vasont.Inspire.ProjectDirectorClient.Properties;
    using Vasont.Inspire.ProjectDirectorClient.Settings;

    /// <summary>
    /// This class represents the Base Client
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class BaseClient : IDisposable
    {
        #region Private Constants
        /// <summary>
        /// The delegation grant type name
        /// </summary>
        private const string DelegationGrantTypeName = "delegation";

        /// <summary>
        /// The alpha numeric characters
        /// </summary>
        private const string AlphaNumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        #endregion Private Constants

        #region Private Fields

        /// <summary>
        /// The random generator object
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Contains a value indicating whether the class has been disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Contains a discovery result from the authority.
        /// </summary>
        private DiscoveryDocumentResponse discovery;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClient"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public BaseClient(ProjectDirectorConfigurationModel configuration)
        {
            this.Configuration = configuration;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the Project Director Client configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public ProjectDirectorConfigurationModel Configuration { get; set; }

        /// <summary>
        /// Gets or sets the authorization token.
        /// </summary>
        /// <value>
        /// The authorization token.
        /// </value>
        public (string Token, DateTime TokenExpiration) AuthorizationToken { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has authenticated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool HasAuthenticated => this.TokenResponse != null && !this.TokenResponse.IsError && !string.IsNullOrWhiteSpace(this.TokenResponse.AccessToken);

        /// <summary>
        /// Gets a value indicating whether the client has an error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        public bool HasError => this.LastErrorResponse != null || this.LastException != null;

        /// <summary>
        /// Gets or sets the token response.
        /// </summary>
        /// <value>
        /// The token response.
        /// </value>
        public TokenResponse TokenResponse { get; set; }

        /// <summary>
        /// Gets the value of the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken => this.TokenResponse?.AccessToken ?? string.Empty;

        /// <summary>
        /// Gets the last error response.
        /// </summary>
        /// <value>
        /// The last error response.
        /// </value>
        public ErrorResponseModel LastErrorResponse { get; private set; }

        /// <summary>
        /// Gets the last <see cref="Exception" /> handled within the client.
        /// </summary>
        /// <value>
        /// The last exception.
        /// </value>
        public Exception LastException { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Authenticates the asynchronous.
        /// </summary>
        /// <param name="scopes">The scopes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns [true] if authentication was successful.</returns>
        public async Task<bool> AuthenticateAsync(string scopes = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            bool authenticationSuccessful;

            // Check to see if this Client has already authenticated
            if (!this.HasAuthenticated)
            {
                string requestScopes = scopes + (!string.IsNullOrWhiteSpace(scopes) ? " " : string.Empty) + string.Join(" ", this.Configuration.TargetResourceScopes);

                if (this.Configuration.AuthenticationMethod == ClientAuthenticationMethods.Delegation && string.IsNullOrWhiteSpace(this.Configuration.DelegatedAccessToken))
                {
                    throw new ClientException(this.Configuration, Resources.AccessMethodRequiresTokenErrorText);
                }

                // if we're using discovery and need to call it...
                if (this.discovery == null && this.Configuration.UseDiscovery)
                {
                    // get the discovery document.
                    this.discovery = await this.RequestDiscoveryAsync(this.Configuration.AuthorityUri, cancellationToken);

                    if (this.discovery.IsError)
                    {
                        throw new ClientException(this.Configuration, this.discovery.Error);
                    }
                }

                switch (this.Configuration.AuthenticationMethod)
                {
                    case ClientAuthenticationMethods.Delegation:
                        this.TokenResponse = await this.RequestDelegationAsync(requestScopes, cancellationToken).ConfigureAwait(false);
                        break;

                    case ClientAuthenticationMethods.ClientCredentials:
                        this.TokenResponse = await this.RequestClientCredentialsAsync(requestScopes, cancellationToken).ConfigureAwait(false);
                        break;

                    case ClientAuthenticationMethods.ResourceOwnerPassword:
                        this.TokenResponse = await this.RequestResourceOwnerPasswordAsync(requestScopes, cancellationToken).ConfigureAwait(false);
                        break;
                }

                authenticationSuccessful = this.TokenResponse != null && !this.TokenResponse.IsError;

                // an error occurred...
                if (!authenticationSuccessful)
                {
                    string errorMessage = this.TokenResponse.Error + Environment.NewLine + this.TokenResponse.ErrorDescription;
                    this.LastErrorResponse = new ErrorResponseModel();
                    this.LastErrorResponse.Messages.Add(new ErrorModel
                    {
                        Message = errorMessage,
                        EventDate = DateTime.UtcNow
                    });

                    // bubble up an error response.
                    throw new ClientException(this.Configuration, errorMessage);
                }
            }
            else
            {
                // This client is already authenticated so return true
                authenticationSuccessful = true;
            }

            return authenticationSuccessful;
        }

        /// <summary>
        /// This method is used to easily create a new WebRequest object for the Web API.
        /// </summary>
        /// <param name="relativeUri">Contains the relative Uri path of the web request to make against the Web API.</param>
        /// <param name="method">Contains the HttpMethod request method object.</param>
        /// <param name="noCache">Contains a value indicating whether the URL shall contain a parameter preventing the server from returning cached content.</param>
        /// <param name="credentials">Contains optional credentials</param>
        /// <param name="contentType">Contains optional content type.</param>
        /// <returns>
        /// Returns a new WebRequest object to execute.
        /// </returns>
        public HttpWebRequest CreateRequest(string relativeUri, HttpMethod method = null, bool noCache = true, ICredentials credentials = null, string contentType = "application/json")
        {
            if (string.IsNullOrWhiteSpace(relativeUri))
            {
                throw new ArgumentNullException(nameof(relativeUri));
            }

            if (method == null)
            {
                method = HttpMethod.Get;
            }

            return this.CreateRequest(relativeUri, method.Method, noCache, credentials, contentType);
        }

        /// <summary>
        /// This method is used to easily create a new WebRequest object for the Web API.
        /// </summary>
        /// <param name="relativeUri">Contains the relative Uri path of the web request to make against the Web API.</param>
        /// <param name="method">Contains the request method as a string value.</param>
        /// <param name="noCache">Contains a value indicating whether the URL shall contain a parameter preventing the server from returning cached content.</param>
        /// <param name="credentials">Contains optional credentials</param>
        /// <param name="contentType">Contains optional content type.</param>
        /// <returns>
        /// Returns a new HttpWebRequest object to execute.
        /// </returns>
        public HttpWebRequest CreateRequest(string relativeUri, string method, bool noCache = true, ICredentials credentials = null, string contentType = "application/json")
        {
            // request /Token, on success, return and store token.
            var request = WebRequest.CreateHttp(new Uri($"{this.Configuration.ResourceUri}{relativeUri}"));
            request.Method = method;

            if (credentials == null)
            {
                credentials = CredentialCache.DefaultCredentials;
                request.UseDefaultCredentials = true;
            }

            request.Credentials = credentials;
            request.UserAgent = this.Configuration.UserAgent;
            request.ContentType = contentType;

            // Set a cache policy level for the "http:" and "https" schemes.
            if (noCache)
            {
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            }

            if (this.HasAuthenticated)
            {
                request.Headers.Add("Authorization", "Bearer " + this.AccessToken);
            }

            return request;
        }

        /// <summary>
        /// Executes a request and returns true if the request was successful.
        /// </summary>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <returns>Returns true if request was successful.</returns>
        public bool RequestNoContent(HttpWebRequest request)
        {
            bool requestSuccess = false;
            this.ResetErrors();

            try
            {
                // execute the request
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    requestSuccess = (int)response.StatusCode == 200;
                }
            }
            catch (WebException webEx)
            {
                this.LastException = webEx;
            }

            return requestSuccess;
        }

        /// <summary>
        /// Posts a model to the request and returns no content.
        /// </summary>
        /// <typeparam name="T">Contains the type of the object that is to be sent with the request.</typeparam>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <param name="requestBodyModel">Contains the object to serialize and submit with the request.</param>
        /// <returns>Returns true if request was successful.</returns>
        public bool RequestNoContent<T>(HttpWebRequest request, T requestBodyModel)
        {
            if (requestBodyModel == null)
            {
                throw new ArgumentNullException(nameof(requestBodyModel));
            }

            // check to ensure we're not trying to post data on a GET or other non-body request.
            if (request.Method != HttpMethod.Post.Method && request.Method != HttpMethod.Put.Method && request.Method != HttpMethod.Delete.Method)
            {
                throw new HttpRequestException(Resources.InvalidRequestTypeErrorText);
            }

            byte[] requestData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBodyModel));

            // write data out to the request stream
            using (var postStream = request.GetRequestStream())
            {
                postStream.Write(requestData, 0, requestData.Length);
            }

            return this.RequestNoContent(request);
        }

        /// <summary>
        /// This method is used to execute a web request and return the results of the request as a defined object type.
        /// </summary>
        /// <typeparam name="T">Contains the type of the object that is to be sent with the request.</typeparam>
        /// <typeparam name="TOut">Contains the type of object that is returned from the request.</typeparam>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <param name="requestBodyModel">Contains the object to serialize and submit with the request.</param>
        /// <returns>
        /// Returns the content of the request response as the specified object.
        /// </returns>
        public TOut RequestContent<T, TOut>(HttpWebRequest request, T requestBodyModel)
        {
            string content = this.RequestContent(request, requestBodyModel);

            return !string.IsNullOrWhiteSpace(content) && !this.HasError ? JsonConvert.DeserializeObject<TOut>(content) : default(TOut);
        }

        /// <summary>
        /// This method is used to execute a web request and return the results of the request as a defined object type.
        /// </summary>
        /// <typeparam name="TOut">Contains the type of object that is returned from the request.</typeparam>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <returns>
        /// Returns the content of the request response as the specified object.
        /// </returns>
        public TOut RequestContent<TOut>(HttpWebRequest request)
        {
            string content = this.RequestContent(request);

            return !string.IsNullOrWhiteSpace(content) && !this.HasError ? JsonConvert.DeserializeObject<TOut>(content) : default(TOut);
        }

        /// <summary>
        /// Requests the content stream.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Returns a <see cref="byte[]"/> representation of the requested content</returns>
        public byte[] RequestContentStream(HttpWebRequest request)
        {
            byte[] returnBytes = this.RequestStream(request);

            return returnBytes != null && returnBytes.Length > 0 && !this.HasError ? returnBytes : default(byte[]);
        }

        /// <summary>
        /// Requests the content stream.
        /// </summary>
        /// <typeparam name="T">Contains the type of the object that is to be sent with the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="requestBodyModel">The request body model.</param>
        /// <returns>Returns a <see cref="byte[]"/> representation of the requested content.</returns>
        public byte[] RequestContentStream<T>(HttpWebRequest request, T requestBodyModel)
        {
            byte[] returnBytes = this.RequestStream(request, requestBodyModel);

            return returnBytes != null && returnBytes.Length > 0 && !this.HasError ? returnBytes : default(byte[]);
        }

        /// <summary>
        /// This method is used to execute a web request and return the results of the request as a string.
        /// </summary>
        /// <typeparam name="T">Contains the type of the object that is to be sent with the request.</typeparam>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <param name="requestBodyModel">Contains the object to serialize and submit with the request.</param>
        /// <returns>
        /// Returns the content of the request response.
        /// </returns>
        public string RequestContent<T>(HttpWebRequest request, T requestBodyModel)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (requestBodyModel == null)
            {
                throw new ArgumentNullException(nameof(requestBodyModel));
            }

            // check to ensure we're not trying to post data on a GET or other non-body request.
            if (request.Method != HttpMethod.Post.Method && request.Method != HttpMethod.Put.Method && request.Method != HttpMethod.Delete.Method)
            {
                throw new HttpRequestException(Resources.InvalidRequestTypeErrorText);
            }

            byte[] requestData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBodyModel));

            // write data out to the request stream
            using (var postStream = request.GetRequestStream())
            {
                postStream.Write(requestData, 0, requestData.Length);
            }

            return this.RequestContent(request);
        }

        /// <summary>
        /// This method is used to execute a web request and return the results of the request as a string.
        /// </summary>
        /// <param name="request">Contains the HttpWebRequest to execute.</param>
        /// <returns>
        /// Returns the content of the request response.
        /// </returns>
        public string RequestContent(HttpWebRequest request)
        {
            string resultContent = string.Empty;
            this.ResetErrors();

            try
            {
                // execute the request
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            resultContent = new StreamReader(responseStream).ReadToEnd();

                            // if the status code was an error and there's content...
                            if ((int)response.StatusCode >= 400 && !string.IsNullOrWhiteSpace(resultContent))
                            {
                                // set the error model
                                this.LastErrorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(resultContent);
                            }
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                this.LastException = webEx;

                if (webEx.Response != null)
                {
                    using (var exceptionResponse = (HttpWebResponse)webEx.Response)
                    {
                        if (exceptionResponse != null)
                        {
                            using (var responseStream = exceptionResponse.GetResponseStream())
                            {
                                if (responseStream != null)
                                {
                                    resultContent = new StreamReader(responseStream).ReadToEnd();

                                    // if the status code was an error and there's content...
                                    if ((int)exceptionResponse.StatusCode >= 400 && !string.IsNullOrWhiteSpace(resultContent))
                                    {
                                        // set the error model
                                        var lastErrorResponse = new ErrorResponseModel();
                                        lastErrorResponse.Messages.Add(new ErrorModel
                                        {
                                            Message = resultContent,
                                            StackTrace = webEx.StackTrace,
                                            EventDate = DateTime.UtcNow,
                                            ErrorType = ErrorType.Critical,
                                            PropertyName = "HttpWebResponse"
                                        });

                                        this.LastErrorResponse = lastErrorResponse;
                                    }
                                }
                            }
                        }
                    }
                }

                if (this.LastErrorResponse == null)
                {
                    var lastErrorResponse = new ErrorResponseModel();
                    lastErrorResponse.Messages.Add(new ErrorModel
                    {
                        Message = webEx.Message,
                        StackTrace = webEx.StackTrace,
                        EventDate = DateTime.UtcNow,
                        ErrorType = ErrorType.Critical,
                        PropertyName = "HttpWebResponse"
                    });

                    this.LastErrorResponse = lastErrorResponse;
                }
            }

            return resultContent;
        }

        /// <summary>
        /// Requests the stream.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Returns the Byte Array of the Requested Stream.
        /// </returns>
        public byte[] RequestStream(HttpWebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            byte[] resultStream = { };
            this.ResetErrors();

            try
            {
                // execute the request
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            resultStream = responseStream.ReadAllBytes();

                            // if the status code was an error and there's content...
                            if ((int)response.StatusCode >= 400 && resultStream.Length >= 0)
                            {
                                var resultContent = new StreamReader(responseStream).ReadToEnd();

                                // set the error model
                                this.LastErrorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(resultContent);
                            }
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                this.LastException = webEx;

                if (webEx.Response != null)
                {
                    using (var exceptionResponse = (HttpWebResponse)webEx.Response)
                    {
                        if (exceptionResponse != null)
                        {
                            using (var responseStream = exceptionResponse.GetResponseStream())
                            {
                                if (responseStream != null)
                                {
                                    var resultContent = new StreamReader(responseStream).ReadToEnd();

                                    // if the status code was an error and there's content...
                                    if ((int)exceptionResponse.StatusCode >= 400 && !string.IsNullOrWhiteSpace(resultContent))
                                    {
                                        // set the error model
                                        var lastErrorResponse = new ErrorResponseModel();
                                        lastErrorResponse.Messages.Add(new ErrorModel
                                        {
                                            Message = resultContent,
                                            StackTrace = webEx.StackTrace,
                                            EventDate = DateTime.UtcNow,
                                            ErrorType = ErrorType.Critical,
                                            PropertyName = "HttpWebResponse"
                                        });

                                        this.LastErrorResponse = lastErrorResponse;
                                    }
                                }
                            }
                        }
                    }
                }

                if (this.LastErrorResponse == null)
                {
                    var lastErrorResponse = new ErrorResponseModel();
                    lastErrorResponse.Messages.Add(new ErrorModel
                    {
                        Message = webEx.Message,
                        StackTrace = webEx.StackTrace,
                        EventDate = DateTime.UtcNow,
                        ErrorType = ErrorType.Critical,
                        PropertyName = "HttpWebResponse"
                    });

                    this.LastErrorResponse = lastErrorResponse;
                }
            }

            return resultStream;
        }

        /// <summary>
        /// Requests the stream.
        /// </summary>
        /// <typeparam name="T">Contains the type of the object that is to be sent with the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="requestBodyModel">Contains the object to serialize and submit with the request.</param>
        /// <returns>
        /// Returns the Byte Array of the Requested Stream.
        /// </returns>
        public byte[] RequestStream<T>(HttpWebRequest request, T requestBodyModel)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (requestBodyModel == null)
            {
                throw new ArgumentNullException(nameof(requestBodyModel));
            }

            // check to ensure we're not trying to post data on a GET or other non-body request.
            if (request.Method != HttpMethod.Post.Method && request.Method != HttpMethod.Put.Method && request.Method != HttpMethod.Delete.Method)
            {
                throw new HttpRequestException(Resources.InvalidRequestTypeErrorText);
            }

            byte[] requestData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBodyModel));

            // write data out to the request stream
            using (var postStream = request.GetRequestStream())
            {
                postStream.Write(requestData, 0, requestData.Length);
            }

            return this.RequestStream(request);
        }
        #endregion Public Methods

        #region IDispose Methods

        /// <summary>
        /// This method is called upon disposal of the client class.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDispose Methods

        #region Protected Methods

        /// <summary>
        /// This method is called upon disposal of the client class.
        /// </summary>
        /// <param name="disposing">Contains a value indicating whether the class is currently being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                // if we're still logged in, be sure to log off.
                if (this.HasAuthenticated)
                {
                    this.ResetSession();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// This method is used to clear any previous error objects.
        /// </summary>
        protected void ResetErrors()
        {
            this.LastException = null;
        }

        /// <summary>
        /// This method is used to contact an authority discovery endpoint for information for interacting with the authority.
        /// </summary>
        /// <param name="authorityUri">Contains the authority URI to contact.</param>
        /// <param name="cancellationToken">Contains a cancellation token.</param>
        /// <returns>
        /// Returns a <see cref="DiscoveryDocumentResponse" /> object when successful.
        /// </returns>
        protected async Task<DiscoveryDocumentResponse> RequestDiscoveryAsync(Uri authorityUri, CancellationToken cancellationToken)
        {
            DiscoveryDocumentResponse result;

            using (DiscoveryDocumentRequest documentRequest = new DiscoveryDocumentRequest { Address = authorityUri.ToString() })
            using (HttpClient client = new HttpClient())
            {
                result = await client.GetDiscoveryDocumentAsync(documentRequest, cancellationToken)
                    .ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Requests the client credentials.
        /// </summary>
        /// <param name="scopes">The scopes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Returns a <see cref="TokenResponse" /> object.
        /// </returns>
        protected async Task<TokenResponse> RequestClientCredentialsAsync(string scopes, CancellationToken cancellationToken)
        {
            TokenResponse result;

            using (HttpClient client = new HttpClient())
            {
                if (this.Configuration.IncludeBasicAuthenticationHeader)
                {
                    string headerValue = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(this.Configuration.ClientId + ":" + this.Configuration.ClientSecret));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", headerValue);
                }

                var tokenRequest = new ClientCredentialsTokenRequest
                {
                    Address = this.Configuration.AuthorityUri.ToString(),
                    ClientId = this.Configuration.ClientId,
                    Scope = scopes,
                    ClientSecret = this.Configuration.ClientSecret
                };

                result = await client.RequestClientCredentialsTokenAsync(tokenRequest, cancellationToken)
                    .ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// This method is used to request a delegation token from the identity server that supports the custom grant type of "delegation".
        /// </summary>
        /// <param name="scopes">The scopes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Returns a new token response from the request.
        /// </returns>
        protected async Task<TokenResponse> RequestDelegationAsync(string scopes, CancellationToken cancellationToken)
        {
            TokenResponse result;

            using (HttpClient client = new HttpClient())
            {
                if (this.Configuration.IncludeBasicAuthenticationHeader)
                {
                    string headerValue = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(this.Configuration.ClientId + ":" + this.Configuration.ClientSecret));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", headerValue);
                }

                var tokenRequest = new TokenRequest
                {
                    Address = this.Configuration.AuthorityUri.ToString(),
                    GrantType = DelegationGrantTypeName,
                    ClientId = this.Configuration.ClientId,
                    ClientSecret = this.Configuration.ClientSecret,
                    Parameters =
                        {
                            { "scope", scopes },
                            { "token", this.Configuration.DelegatedAccessToken }
                        }
                };

                result = await client.RequestTokenAsync(tokenRequest, cancellationToken)
                    .ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Request the resource owner credentials.
        /// </summary>
        /// <param name="scopes">Contains the scopes.</param>
        /// <param name="cancellationToken">Contains a cancellation token.</param>
        /// <returns>
        /// Returns a <see cref="TokenResponse" /> object.
        /// </returns>
        protected async Task<TokenResponse> RequestResourceOwnerPasswordAsync(string scopes, CancellationToken cancellationToken)
        {
            TokenResponse result;

            using (HttpClient client = new HttpClient())
            {
                if (this.Configuration.IncludeBasicAuthenticationHeader)
                {
                    string headerValue = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(this.Configuration.ClientId + ":" + this.Configuration.ClientSecret));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", headerValue);
                }

                var config = new PasswordTokenRequest
                {
                    Address = this.Configuration.AuthorityUri.ToString(),
                    ClientId = this.Configuration.ClientId,
                    Scope = scopes,
                    ClientSecret = this.Configuration.ClientSecret,
                    UserName = this.Configuration.UserName,
                    Password = this.Configuration.Password,
                };

                result = await client.RequestPasswordTokenAsync(config, cancellationToken)
                    .ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        /// Retrieves the route prefix.
        /// </summary>
        /// <returns>Returns the value of Route Prefix including forward slash if one exists.</returns>
        protected string RetrieveRoutePrefix()
        {
            return !string.IsNullOrWhiteSpace(this.Configuration.RoutePrefix) ? $"/{this.Configuration.RoutePrefix}" : string.Empty;
        }

        /// <summary>
        /// Generates the random alpha numeric string.
        /// </summary>
        /// <param name="length">Contains the length of the Alpha Numeric string to generate.</param>
        /// <returns>Returns a string containing Random Alpha Numeric characters.</returns>
        protected string GenerateRandomAlphaNumeric(int length)
        {
            return new string(Enumerable.Range(1, length).Select(a => AlphaNumericCharacters[random.Next(AlphaNumericCharacters.Length)]).ToArray());
        }
        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// This method is used to reset session-related fields.
        /// </summary>
        private void ResetSession()
        {
            this.ResetErrors();
            this.AuthorizationToken = (string.Empty, DateTime.MinValue);
        }
        #endregion Private Methods
    }
}
