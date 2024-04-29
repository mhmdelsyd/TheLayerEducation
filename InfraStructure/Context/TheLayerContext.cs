using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheLayer.Core.Models.Entities;
using TheLayer.Core.Models.Identities;
using TheLayer.InfraStructure.Helpers;
using TheLayer.InfraStructure.Helpers.Seed;

namespace TheLayer.InfraStructure.Context;

public class TheLayerContext : IdentityDbContext<Consumer>
{
    public TheLayerContext(DbContextOptions<TheLayerContext> options)
        : base(options)
    {

    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.SeedRoles();
        builder.SeedCourses();
        builder.EditTable();
    }


}
