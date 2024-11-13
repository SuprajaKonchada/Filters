using Microsoft.EntityFrameworkCore;
using ContentManagement.Models;

namespace ContentManagement.Data;

public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <summary>
    /// Gets or sets the collection of users in the database.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the collection of content in the database.
    /// </summary>
    public DbSet<Content> Contents { get; set; }
}
