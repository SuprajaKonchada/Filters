using System.Text.Json.Serialization;

namespace ContentManagement.Models;

public class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the user. This property is ignored during JSON serialization.
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user. Used for authentication purposes.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the user. Used for authentication purposes.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the role of the user. Default value is "User".
    /// </summary>
    public string Role { get; set; } = "User";
}
