namespace AuthenticationApi.Domain.Entities;

public class User : BaseClass
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public List<Role> Roles { get; set; }
}