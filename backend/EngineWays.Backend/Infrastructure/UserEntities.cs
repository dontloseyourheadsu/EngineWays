namespace EngineWays.Backend.Infrastructure;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public List<UserRole> Roles { get; set; } = new List<UserRole>();
}

public class UserRole
{
    public int Id { get; set; }
    public string RoleName { get; set; } = default!;
}