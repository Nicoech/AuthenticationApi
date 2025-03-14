using AuthenticationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //User entity
        modelBuilder
            .Entity<User>()
            .Property(u => u.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");
        modelBuilder
            .Entity<User>()
            .Property(u => u.Username)
            .HasMaxLength(100);
        modelBuilder
            .Entity<User>()
            .Property(u => u.Password)
            .HasMaxLength(50);
        modelBuilder
            .Entity<User>()
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "User_Roles", // Table name
                j => j.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRoles_Roles"), // Foreign Key to Role
                j => j.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRoles_Users"), // Foreign Key to User
                j =>
                {
                    j.Property("UserId").HasColumnName("User_Id");
                    j.Property("RoleId").HasColumnName("Role_Id");
                    j.ToTable("User_Roles");
                });
    }
}