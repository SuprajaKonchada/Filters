using Microsoft.AspNetCore.Mvc;
using ContentManagement.Services;
using ContentManagement.Models;
using ContentManagement.Data;

namespace ContentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly AuthService _authService;

    public AccountController(ApplicationDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    /// <summary>
    /// Registers a new user and assigns a default role if none is provided.
    /// </summary>
    /// <param name="user">The user object containing registration details.</param>
    /// <returns>Success or error message depending on registration result.</returns>
    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        // Check if role is null or empty, and assign default
        if (string.IsNullOrEmpty(user.Role))
        {
            user.Role = "User";  // Default role
        }

        if (_context.Users.Any(u => u.Username == user.Username))
            return BadRequest("Username already exists.");

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User registered successfully.");
    }

    /// <summary>
    /// Authenticates a user and generates a JWT token if the credentials are valid.
    /// </summary>
    /// <param name="user">The user object containing login details.</param>
    /// <returns>A JWT token if authentication is successful.</returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);
        if (existingUser == null)
            return Unauthorized("Invalid credentials.");

        // Generate the JWT token with the role
        var token = _authService.GenerateJwtToken(existingUser);

        return Ok(new { Token = token });
    }
}
