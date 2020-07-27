using System;
using System.Collections.Generic;

namespace BikeDataProject.Registrations.API.Models
{
    /// <summary>
    /// Class containing all information needed to request the acces token and refresh token from Strava.
    /// </summary>
    public class StravaRegistrationRequest
    {
        /// <summary>
        /// The Client ID of the Strava user.
        /// </summary>
        /// <value></value>
        public String ClientId { get; set; }

        /// <summary>
        /// The client secret of the Strava user.
        /// </summary>
        /// <value></value>
        public String ClientSecret { get; set; }

        /// <summary>
        /// The token exchange code for Strava.
        /// </summary>
        /// <value></value>
        public String Code { get; set; }

        /// <summary>
        /// THe grant type for the request.
        /// Must always be "authorization_code".
        /// </summary>
        /// <value></value>
        public String GrantType { get; set; } = "authorization_code";

        /// <summary>
        /// Converts the structure to a JSON path dictionary.
        /// </summary>
        /// <returns>JSON path dictionary</returns>
        public Dictionary<String, String> ToKeyValue()
        {
            return new Dictionary<String, String>{
                {"client_id", this.ClientId},
                {"client_secret", this.ClientSecret},
                {"code", this.Code},
                {"grant_type", this.GrantType}
            };
        }
    }
}
