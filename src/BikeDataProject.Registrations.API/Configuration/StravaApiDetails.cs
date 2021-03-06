using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BikeDataProject.Registrations.API.Configuration
{
    /// <summary>
    /// Class to hold the secrets needed to connect with the Strava API.
    /// </summary>
    public class StravaApiDetails
    {
        /// <summary>
        /// Constructor to instantiate the class given the clientId, clientSecret and authEndPoint.
        /// </summary>
        /// <param name="clientId">The strava clientId of the user.</param>
        /// <param name="clientSecret">The strava clientSecret of the user.</param>
        /// <param name="authEndPoint">The authentication endpoint from Strava.</param>
        /// <param name="redirectionUri">The redirection URI.</param>
        public StravaApiDetails(string clientId, string clientSecret, string authEndPoint, string redirectionUri)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.AuthEndPoint = authEndPoint;
            this.RedirectionUri = redirectionUri;
        }

        /// <summary>
        /// Constructor to instatiate the class given a configuration file.
        /// </summary>
        /// <param name="configuration">The configuration file containing the secrets needed.</param>
        /// <returns></returns>
        public static StravaApiDetails FromConfiguration(IConfiguration configuration)
        {
            var clientId = File.ReadAllText(configuration[$"{Program.EnvVarPrefix}STRAVA_CLIENT_ID"]);
            var clientSecret = File.ReadAllText(configuration[$"{Program.EnvVarPrefix}STRAVA_CLIENT_SECRET"]);
            var authEndPoint = configuration[$"{Program.EnvVarPrefix}STRAVA_AUTH_END_POINT"];
            var redirectionUri = configuration[$"{Program.EnvVarPrefix}REDIRECTION_URI"];

            if (string.IsNullOrWhiteSpace(clientId))
            {
                Log.Fatal("clientId is not set");
                throw new Exception("Environment variable not set");
            }

            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                Log.Fatal("clientSecret is not set");
                throw new Exception("Environment variable not set");
            }

            if (string.IsNullOrWhiteSpace(authEndPoint))
            {
                Log.Fatal("auth endpoint is not set");
                throw new Exception("Environment variable not set");
            }

            if (string.IsNullOrWhiteSpace(redirectionUri))
            {
                Log.Fatal("redirection is not set");
                throw new Exception("Environment variable not set");
            }

            return new StravaApiDetails(clientId, clientSecret, authEndPoint, redirectionUri);
        }
        
        /// <summary>
        /// The strava clientId of the user.
        /// </summary>
        /// <value></value>
        public string ClientId { get; }

        /// <summary>
        /// The strava clientSecret of the user.
        /// </summary>
        /// <value></value>
        public string ClientSecret { get; }

        /// <summary>
        /// The authentication endpoint from Strava.
        /// </summary>
        /// <value></value>
        public string AuthEndPoint { get; }

        /// <summary>
        /// The redirection URI at the end of the Strava registration.
        /// </summary>
        /// <value></value>
        public string RedirectionUri {get;}
    }
}