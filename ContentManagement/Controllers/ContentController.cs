using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContentManagement.Models;
using ContentManagement.Data;
using ContentManagement.Filters;

namespace ContentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(RoleBasedAuthorizationFilter))]
[ServiceFilter(typeof(ResultFilter))]
[ServiceFilter(typeof(ExceptionFilter))]
[ServiceFilter(typeof(LoggingFilter))]
public class ContentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ContentController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a list of all contents.
    /// </summary>
    /// <returns>A list of contents.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public IActionResult GetContents()
    {
        var contents = _context.Contents.ToList();
        return Ok(contents);
    }

    /// <summary>
    /// Creates a new content entry.
    /// </summary>
    /// <param name="content">The content object to create.</param>
    /// <returns>The created content object.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public IActionResult CreateContent([FromBody] Content content)
    {
        content.CreatedAt = DateTime.UtcNow;
        _context.Contents.Add(content);
        _context.SaveChanges();
        return Ok(content);
    }

    /// <summary>
    /// Edits an existing content by ID.
    /// </summary>
    /// <param name="id">The ID of the content to edit.</param>
    /// <param name="content">The content object containing updated details.</param>
    /// <returns>The updated content object.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult EditContent(int id, [FromBody] Content content)
    {
        var existingContent = _context.Contents.FirstOrDefault(c => c.Id == id);
        if (existingContent == null)
            return NotFound();

        existingContent.Title = content.Title;
        existingContent.Body = content.Body;
        existingContent.Category = content.Category;
        _context.SaveChanges();

        return Ok(existingContent);
    }

    /// <summary>
    /// Deletes an existing content by ID.
    /// </summary>
    /// <param name="id">The ID of the content to delete.</param>
    /// <returns>No content response.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteContent(int id)
    {
        var content = _context.Contents.FirstOrDefault(c => c.Id == id);
        if (content == null)
            return NotFound();

        _context.Contents.Remove(content);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// A test endpoint to trigger an exception.
    /// </summary>
    /// <returns>Throws an exception for testing purposes.</returns>
    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new InvalidOperationException("This is a test exception");
    }
}
