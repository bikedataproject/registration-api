using System;

namespace BikeDataProject.API.Domain
{
    public class UserContribution
    {
        public Guid UserContributionId { get; set; }

        public Guid UserId { get; set; }

        public int ContributionId { get; set; }

    }
}