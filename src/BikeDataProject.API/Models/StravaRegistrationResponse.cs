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
    }
}