using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace NewsAPI.Entities;

public class AppRole : IdentityRole<Guid>
{

    public List<AppUserRole> UserRoles { get; set; } = [];

}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AppRoles
{
    Admin,
    Member,
    Moderator,
}

public static class AppPolicy
{
    public const string RequireAdminRole = "Admin";
    public const string RequireModeratorRole = "Member";

}


