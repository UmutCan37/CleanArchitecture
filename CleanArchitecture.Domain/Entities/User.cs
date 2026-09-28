using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    public User()
    {
        Id = Guid.NewGuid();
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set;}

}