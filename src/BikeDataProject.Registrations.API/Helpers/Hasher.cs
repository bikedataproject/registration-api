using System.Security.Cryptography;
using System.Text;

namespace BikeDataProject.Registrations.API.Helpers
{
    /// <summary>
    /// Class that contains methods related to hashing.
    /// </summary>
    public class Hasher
    {
        /// <summary>
        /// Method for creating a hash from an inputstring
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns>hashed inputstring</returns>
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        
        /// <summary>
        /// Method for creating a hash from an inputstring
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns>hashed inputstring</returns>
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

    }
}