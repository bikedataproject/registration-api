using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BikeDataProject.Registrations.API.Configuration
{
    public class StravaApiDetails
    {
        public StravaApiDetails(string clientId, string clientSecret, string authEndPoint)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.AuthEndPoint = authEndPoint;
        }

        public static StravaApiDetails FromConfiguration(IConfiguration configuration)
        {
            var clientId = configuration[$"{Program.EnvVarPrefix}STRAVA_CLIENT_ID"];
            var clientSecret = configuration[$"{Program.EnvVarPrefix}STRAVA_CLIENT_SECRET"];
            var authEndPoint = configuration[$"{Program.EnvVarPrefix}STRAVA_AUTH_END_POINT"];

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

            return new StravaApiDetails(clientId, clientSecret, authEndPoint);
        }
        
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string AuthEndPoint { get; }
    }
}