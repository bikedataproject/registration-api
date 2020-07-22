using System;
using System.Runtime.Serialization;

namespace BikeDataProject.Registrations.API.Models
{
    /// <summary>
    /// Contains the attributes for the UserInfo class.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// The IMEI number of the user.
        /// </summary>
        [DataMember(Name = "imei")]
        public string Imei {get; set;}
        /// <summary>
        /// The UserIdentifier of the user.
        /// </summary>
        /// <value></value>
        [DataMember(Name = "userIdentifier")]
        public Guid? UserIdentifier {get; set;}
    }
}