using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ContentManagement.Models;

namespace ContentManagement.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration used to retrieve JWT settings.</param>
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Generates a JWT token for the specified user with claims including username and role.
    /// </summary>
    /// <param name="user">The user for whom the JWT token is generated.</param>
    /// <returns>A string representing the generated JWT token.</returns>
    public string GenerateJwtToken(User user)
    {
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Role))
        {
            throw new InvalidOperationException("User's username and role must not be null.");
        }
        // Define the claims for the JWT token, including username and role
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),  // Include the username in the claims
            new Claim(ClaimTypes.Role, user.Role)      // Include the role from the user object
        };

        // Retrieve the secret key from the configuration and create a symmetric security key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"] ?? "defaultsecret`1234567890-=~!@#$%^&*()_+{}|:<>?][;/.,"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the JWT token with the specified claims, issuer, audience, and expiration time
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),  // Set token expiration to 1 hour
            signingCredentials: creds
        );

        // Return the token as a string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
