using System.Text.Json.Serialization;

namespace ContentManagement.Models
{
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the username of the user. Used for authentication purposes.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user. Used for authentication purposes.
        /// </summary>
        public string? Password { get; set; }
    }
}
