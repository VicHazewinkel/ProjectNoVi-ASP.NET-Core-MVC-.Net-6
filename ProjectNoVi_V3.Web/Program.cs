using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectNoVi_V3.Web.Areas.Identity.Data;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Data;
using ProjectNoVi_V3.Web.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProjectNoVi_V3_ContextConnection") ?? throw new InvalidOperationException("Connection string 'ProjectNoVi_V3_ContextConnection' not found.");

builder.Services.AddDbContext<ProjectNoVi_V3_Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ProjectNoVi_V3_Context>();

//own services
//builder.Services.AddSingleton<IProductService, ProductsService>();
//builder.Services.AddTransient<IProductService, ProductsService>();
builder.Services.AddScoped<IProductService, ProductsService>();
builder.Services.AddScoped<IBrandService, BrandssService>();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    Seeder seeder = new Seeder(services);
    seeder.Seed();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
