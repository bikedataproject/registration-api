using System;
using Newtonsoft.Json;

namespace BikeDataProject.Registrations.API.Models
{
    /// <summary>
    /// Model of the Response of the Strava registration API.
    /// </summary>
    public class StravaRegistrationResponse
    {
        /// <summary>
        /// The acces token for the user.
        /// </summary>
        /// <value></value>
        [JsonProperty("access_token")]
        public String AccessToken { get; set; }

        /// <summary>
        /// The refresh token for the user.
        /// </summary>
        /// <value></value>
        [JsonProperty("refresh_token")]
        public String RefreshToken { get; set; }

        /// <summary>
        /// The number of seconds since the epoch when the provided access token will expire.
        /// </summary>
        /// <value></value>
        [JsonProperty("expires_at")]
        public int ExpiresAt { get; set; }

        /// <summary>
        /// Seconds until the access token will expire.
        /// </summary>
        /// <value></value>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Athlete details from strava, currently only contains the id.
        /// </summary>
        /// <value></value>
        [JsonProperty("athlete")]
        public StravaAthleteDetails Athlete { get; set; }
    }

    /// <summary>
    /// Class that contains the Athlete details gained from Strava.
    /// </summary>
    public class StravaAthleteDetails
    {
        /// <summary>
        /// The userId inside of Strava.
        /// </summary>
        /// <value></value>
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}