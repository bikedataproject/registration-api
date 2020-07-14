using System;
using System.Text.Json.Serialization;

namespace BikeDataProject.API.Models
{
    public class StravaRegistrationResponse
    {
        [JsonPropertyName("access_token")]
        public String AccessToken { get; set; }
        [JsonPropertyName("refresh_token")]
        public String RefreshToken { get; set; }
        [JsonPropertyName("expires_at")]
        public Int32 ExpiresAt { get; set; }
        [JsonPropertyName("expires_in")]
        public Int32 ExpiresIn { get; set; }
        [JsonPropertyName("athlete")]
        public StravaAthleteDetails Athlete { get; set; }
    }

    public class StravaAthleteDetails
    {
        [JsonPropertyName("id")]
        public Int32 Id { get; set; }
    }
}