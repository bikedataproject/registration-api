using System;
using Newtonsoft.Json;

namespace BikeDataProject.API.Models
{
    public class StravaRegistrationResponse
    {
        [JsonProperty("access_token")]
        public String AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public String RefreshToken { get; set; }
        [JsonProperty("expires_at")]
        public int ExpiresAt { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("athlete")]
        public StravaAthleteDetails Athlete { get; set; }
    }

    public class StravaAthleteDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}