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
        /// Method for creating a hash from an inputstring.
        /// This method uses the SHA256 algorithm to hash the inputstring.
        /// It gives the output as a byte array.
        /// </summary>
        /// <param name="inputString">The input string</param>
        /// <returns>hashed inputstring as a byte array</returns>
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        
        /// <summary>
        /// Method for creating a hash from an inputstring.
        /// This method uses the SHA256 algorithm to hash the inputstring.
        /// It gives the output as a string.
        /// </summary>
        /// <param name="inputString">The input string</param>
        /// <returns>hashed inputstring as a string</returns>
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

    }
}