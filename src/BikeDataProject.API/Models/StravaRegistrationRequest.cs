using System;
using System.Collections.Generic;

namespace BikeDataProject.API.Models
{
    public class StravaRegistrationRequest
    {
        public String ClientId { get; set; }
        public String ClientSecret { get; set; }
        public String Code { get; set; }
        public String GrantType { get; set; } = "authorization_code";
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
