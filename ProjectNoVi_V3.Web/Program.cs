using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectNoVi_V3.Web.Areas.Identity.Data;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Data;
using ProjectNoVi_V3.Web.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using ProjectNoVi_V3.Web.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using NETCore.MailKit.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProjectNoVi_V3_ContextConnection") ?? throw new InvalidOperationException("Connection string 'ProjectNoVi_V3_ContextConnection' not found.");

builder.Services.AddDbContext<ProjectNoVi_V3_Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ProjectNoVi_V3_Context>();

// AddLocalization

builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();


//own services

//builder.Services.AddSingleton<IProductService, ProductsService>();
//builder.Services.AddTransient<IProductService, ProductsService>();
builder.Services.AddScoped<IBrandService, BrandsService>();
builder.Services.AddScoped<IProductService, ProductsService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// MailKit Service
builder.Services.AddTransient<IEmailSender, MailKitService>();
builder.Services.Configure<MailKitOptions>(options =>
{
    options.Server = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
    options.Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
    options.Account = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"];
    options.Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"];
    options.SenderEmail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
    options.SenderName = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"];
    // Set it to TRUE to enable ssl or tls, FALSE otherwise
    options.Security = false;  // true zet ssl or tls aan
});



var app = builder.Build();

var supportedCultures = new[] { "en-US", "fr", "nl" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
       .AddSupportedCultures(supportedCultures)
       .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

Language.InitLanguages();

using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    Seeder seeder = new Seeder(services);
    await seeder.Seed();
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


// Lijst gebruikte Names and cultures 


//
//var supportedCultures = new[] { "en-US", "nl", "fr" };
//var localizationOptions = new
//RequestLocalizationOptions().SetDefaultCulture(Language.SupportedCultures[0])
//    .AddSupportedCultures(Language.SupportedCultures)
//    .AddSupportedUICultures(Language.SupportedCultures);

//app.UseRequestLocalization(localizationOptions);
//

//var supportedCultures = new[] { "en-US", "nl", "fr" };
//var localizationOptions = new 
//RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
//       .AddSupportedCultures(supportedCultures)
//       .AddSupportedUICultures(supportedCultures);

//app.UseRequestLocalization(localizationOptions);


app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
