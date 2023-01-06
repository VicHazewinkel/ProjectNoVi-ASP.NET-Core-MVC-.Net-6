using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectNoVi_V3.Enums;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.Areas.Identity.Data;

namespace ProjectNoVi_V3.Data;

public class Seeder
{
    private readonly IServiceProvider serviceProvider;
    private readonly ProjectNoVi_V3_Context dbContext;

    public Seeder(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;

        dbContext = serviceProvider.GetService<ProjectNoVi_V3_Context>();

        if (dbContext == null || dbContext.Database.EnsureCreated())
        {
            throw new Exception("Could not instantiate database.");
        }
    }

    public void Seed()
    {
        this.AddBrands();
        this.AddProducts();
        this.AddUsers();
    }

    private void AddProducts()
    {
        //var currentbdata = dbContext.Product.Where(p => p.Id > 0).ToList();
        var isSeeded = dbContext.Product.Any(p => p.Id > 0);

        if (isSeeded) return;

        List<Product> listOfProducts = new();
        Product FirstProduct = new Product()
        {
            //Id = 0,
            MerkId = 1,
            Type = ProductType.RegularGlasses,
            Collectie = "Spirit",
            Kleur = "grijs",
            Materiaal = "Titanium",
            Prijs = 550,
        };
        Product SecondProduct = new Product()
        {
            //Id = 0,
            MerkId = 2,
            Type = ProductType.Sunglasses,
            Collectie = "Flower",
            Kleur = "Zwart",
            Materiaal = "Acetaat",
            Prijs = 320,
        };
        Product ThirdProduct = new Product()
        {
            //Id = 0,
            MerkId = 3,
            Type = ProductType.RegularGlasses,
            Collectie = "Pilot",
            Kleur = "Zwart",
            Materiaal = "Metaal",
            Prijs = 340,
        };
        Product FourthProduct = new Product()
        {
            //Id = 0,
            MerkId = 4,
            Type = ProductType.RegularGlasses,
            Collectie = "Marfona",
            Kleur = "Paars-Groen",
            Materiaal = "Titanium",
            Prijs = 560,
        };
        Product FifthProduct = new Product()
        {
            //Id = 0,
            MerkId = 5,
            Type = ProductType.Sunglasses,
            Collectie = "Panter",
            Kleur = "Goud",
            Materiaal = "Titanium",
            Prijs = 920,
        };
        Product SixthProduct = new Product()
        {
            //Id = 0,
            MerkId = 6,
            Type = ProductType.Dailylens,
            Collectie = "Dalies Total 1",
            Kleur = "Dag lens",
            Materiaal = "Silicone",
            Prijs = 30,
        };
        Product SeventhProduct = new Product()
        {
            //Id = 0,
            MerkId = 7,
            Type = ProductType.MonthyLens,
            Collectie = "Biofinity",
            Kleur = "Maand lens",
            Materiaal = "Silicone",
            Prijs = 27,
        };

        listOfProducts.Add(FirstProduct);
        listOfProducts.Add(SecondProduct);
        listOfProducts.Add(ThirdProduct);
        listOfProducts.Add(FourthProduct);
        listOfProducts.Add(FifthProduct);
        listOfProducts.Add(SixthProduct);
        listOfProducts.Add(SeventhProduct);

        dbContext.Product.AddRange(listOfProducts);
        dbContext.SaveChanges();
    }

    private void AddBrands()
    {
        var currentbdata = dbContext.Brand.Where(b => b.Id > 0).ToList();
        var isSeeded = dbContext.Brand.Any(b => b.Id > 0);

        if (isSeeded) return;

        List<Brand> listOfBrands = new();

        Brand firstMerk = new Brand()
        {
            Id = 0,
            MerkName = "Lindberg"
        };
        Brand SecondMerk = new Brand()
        {
            Id = 0,
            MerkName = "Gucci"
        };
        Brand ThirdMerk = new Brand()
        {
            Id = 0,
            MerkName = "Tom Ford"
        };
        Brand FourthMerk = new Brand()
        {
            Id = 0,
            MerkName = "Theo"
        };
        Brand FifthMerk = new Brand()
        {
            Id = 0,
            MerkName = "Cartier"
        };
        Brand SixthMerk = new Brand()
        {
            Id = 0,
            MerkName = "Alcon"          // Lens
        };
        Brand SeventhMerk = new Brand()
        {
            Id = 0,                     // Lens
            MerkName = "Coopervision"
        };

        listOfBrands.Add(firstMerk);
        listOfBrands.Add(SecondMerk);
        listOfBrands.Add(ThirdMerk);
        listOfBrands.Add(FourthMerk);
        listOfBrands.Add(FifthMerk);
        listOfBrands.Add(SixthMerk);
        listOfBrands.Add(SeventhMerk);

        dbContext.Brand.AddRange(listOfBrands);
        dbContext.SaveChanges();
    }
    private void AddUsers()
    {
        var isSeeded = dbContext.Users.Any();
        if (isSeeded) return;

        ApplicationUser FirstUser = new ApplicationUser()
        {
            //Id = "0",
            UserName = "User@gmail.com",
            Name = "User",
            Email = "User@gmail.com",
            EmailConfirmed = true,
            PasswordHash = "Abc@123",
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        ApplicationUser SecondUser = new ApplicationUser()
        {
            //Id = "0",
            UserName = "Optometrist@gmail.com",
            Name = "Optometrist",
            Email = "Optometrist@gmail.com",
            EmailConfirmed = true,
            PasswordHash = "Abc@123",
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        ApplicationUser ThirdUser = new ApplicationUser()
        {
            //Id = "0",
            UserName = "Admin@gmail.com",
            Name = "Admin",
            Email = "Admin@gmail.com",
            EmailConfirmed = true,
            PasswordHash = "Abc@123",
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        CreateUser(FirstUser, "Abc@123");
        CreateUser(SecondUser, "Abc@123");
        CreateUser(ThirdUser, "Abc@123");

    }
    private void CreateUser(ApplicationUser user, string psswrd)
    {
        if (!dbContext.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, psswrd);
            user.PasswordHash = hashed;
            var userStore = new UserStore<ApplicationUser>(dbContext);
            var result = userStore.CreateAsync(user).Result;
        }
    }
}

