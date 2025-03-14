namespace AuthenticationApi.Domain.Entities;

public class Role : BaseClass
{
    public required string Description { get; set; }
    public List<User> Users { get; set; }
}