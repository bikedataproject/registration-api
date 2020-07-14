using System;
namespace BikeDataProject.API.Domain
{
    public class Contribution
    {
        public Guid ContributionId { get; set; } = Guid.NewGuid();

        public string UserAgent { get; set; }

        public int Distance { get; set; }

        public DateTime TimeStampStart { get; set; }

        public DateTime TimeStampStop { get; set; }

        public int Duration { get; set; } //in seconds

        public byte[] PointsGeom { get; set; }

        public DateTime[] PointsTime { get; set; }


    }
}