//-------------------------------------------------------------
// <copyright file="Program.cs" company="Vasont Systems">
// Copyright (c) Vasont Systems. All rights reserved.
// </copyright>
//-------------------------------------------------------------
namespace TestProjectDirectorClient
{
    using System;
    using Vasont.Inspire.Core.Extensions;
    using Vasont.Inspire.Core.Utility;
    using Vasont.Inspire.ProjectDirector.Client;
    using Vasont.Inspire.ProjectDirector.Client.Settings;

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
            string userName = CommandLine.Parameters.ContainsKey("userName") ? CommandLine.Parameters["userName"].ConvertToString() : string.Empty;
            string password = CommandLine.Parameters.ContainsKey("password") ? CommandLine.Parameters["password"].ConvertToString() : string.Empty;

            string clientSecret = CommandLine.Parameters.ContainsKey("clientSecret") ? CommandLine.Parameters["clientSecret"].ConvertToString() : string.Empty;
            string clientId = CommandLine.Parameters.ContainsKey("clientId") ? CommandLine.Parameters["clientId"].ConvertToString() : string.Empty;

            string grantType = CommandLine.Parameters.ContainsKey("grantType") ? CommandLine.Parameters["grantType"].ConvertToString() : string.Empty;
            string clientScopes = CommandLine.Parameters.ContainsKey("clientScopes") ? CommandLine.Parameters["clientScopes"].ConvertToString() : string.Empty;
            string userAgent = "GlobalLink Vasont Inspire Project Director Rest Api Client";

            Console.WriteLine("GlobalLink Vasont Inspire Project Director Rest Api Client Test.");

            try
            {
                ProjectDirectorConfigurationModel authenticationSettings = new ProjectDirectorConfigurationModel
                {
                    ResourceUri = new Uri(resourceUri),
                    AuthorityUri = new Uri(authorityUri),
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

                        // Get a list of Available Projects from Project Director
                        var test = AsyncHelper.RunSync(() => projectDirectorClient.RetrieveProjectsAsync());
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
                Console.WriteLine($"An Exception was thrown. {Environment.NewLine} Error: {ex.Message} {Environment.NewLine} Stack Trace: {ex.StackTrace} Environment.NewLine");
            }

            // wait
            Console.WriteLine("Press any key to close the console.");
            Console.ReadKey();
        }
    }
}
