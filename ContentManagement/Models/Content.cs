using System.Text.Json.Serialization;

namespace ContentManagement.Models;

public class Content
{
    /// <summary>
    /// Gets or sets the unique identifier for the content. This property is ignored during JSON serialization.
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the content.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the body content of the content.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// Gets or sets the category of the content.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp of the content. This property is ignored during JSON serialization.
    /// </summary>
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
}
