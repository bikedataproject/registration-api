using System;
using System.Collections.Generic;

namespace BikeDataProject.API.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public String Provider { get; set; }
        public String ProviderUser { get; set; }
        public String AccessToken { get; set; }
        public String RefreshToken { get; set; }
        public DateTime TokenCreationDate { get; set; } = DateTime.Now;
        public int ExpiresAt { get; set; }
        public int ExpiresIn { get; set; }
        public List<UserContribution> UserContributions { get; set; }
    }
}