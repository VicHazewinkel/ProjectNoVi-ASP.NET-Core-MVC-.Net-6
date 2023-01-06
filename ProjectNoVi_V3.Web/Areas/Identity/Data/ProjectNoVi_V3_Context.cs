using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectNoVi_V3.Models;

namespace ProjectNoVi_V3.Web.Areas.Identity.Data;

public class ProjectNoVi_V3_Context : IdentityDbContext<ApplicationUser>
{
    public ProjectNoVi_V3_Context(DbContextOptions<ProjectNoVi_V3_Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Brand> Brand { get; set; }

    public DbSet<Product> Product { get; set; }
}
