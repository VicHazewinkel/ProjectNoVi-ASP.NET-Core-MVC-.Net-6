using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.Areas.Identity.Data;

namespace ProjectNoVi_V3.Web.Services;

public interface IProductService
{
    public List<Product> GetAll(); 
}

public class ProductsService : IProductService
{
    private readonly ProjectNoVi_V3_Context _context;

    public ProductsService(ProjectNoVi_V3_Context context)
    {
        _context = context;
    }

    public List<Product> GetAll()
    {
        List<Product> products = new List<Product>();
        
        var dbProducts = _context.Product.Where(x => x.Id > 0);  // linq

        products.AddRange(dbProducts);
        //products = dbProducts.ToList();

        return products;
    }
}
