namespace BikeDataProject.Registrations.API.Models
{
    /// <summary>
    /// This class contains all the constants that we use throughout this project.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// User Agent filling the Provider field in the database.
        /// </summary>
        public const string WebStravaUserAgent = "web/Strava";

        /// <summary>
        /// Strava Status query parameter permitting for the website to show if the registration succeeded or not.
        /// </summary>
        public const string StravaStatusQueryParameter = "?stravaStatus=";

        /// <summary>
        /// Strava Status query parameter succes.
        /// </summary>
        public const string StravaStatusSuccess = StravaStatusQueryParameter + "success";

        /// <summary>
        /// Strava Status query parameter failed.
        /// </summary>
        public const string StravaStatusFailed = StravaStatusQueryParameter + "failed";
    }
}
