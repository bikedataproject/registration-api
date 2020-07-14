using System;
using System.Collections.Generic;

namespace BikeDataProject.API.Domain
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public String Provider { get; set; }
        public Int32 ProviderId { get; set; }
        public String AccessToken { get; set; }
        public String RefreshToken { get; set; }
        public DateTime TokenCreationDate { get; set; } = DateTime.Now;
        public Int32 ExpiresAt { get; set; }
        public Int32 ExpiresIn { get; set; }
        public List<UserContribution> UserContributions { get; set; }
    }
}