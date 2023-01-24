using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.Areas.Identity.Data;
using ProjectNoVi_V3.Web.Mapper;
using ProjectNoVi_V3.Web.ViewModels;

namespace ProjectNoVi_V3.Web.Services;

public interface IProductService
{
    public List<Product> GetAll();

    /// <summary>
    /// Adds or updates a product in the database
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public int Upsert(Product product);

    public ProductViewModel GetViewModelById(int id);
    public Product GetProdcutyId(int id);

    /// <summary>
    /// returns true if deleted
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool DeleteById(int id);
}

public class ProductsService : IProductService
{
    private readonly ProjectNoVi_V3_Context _context;
   

    public ProductsService(ProjectNoVi_V3_Context context)
    {
        _context = context;
    }

    public bool DeleteById(int id)
    {
        var product = this.GetProdcutyId(id);

        if (product == null)
        {
            return false;
        }

        try
        {
            //this.DeleteById(product.Id);
            _context.Product.Remove(product);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            return false;
        }

        //await _context.SaveChangesAsync();

        return true;
    }

    public List<Product> GetAll()
    {
        List<Product> products = new List<Product>();

        var dbProducts = _context.Product.Where(x => x.Id > 0);  // linq

        products.AddRange(dbProducts);
        //products = dbProducts.ToList();

        return products;
    }

    public Product GetProdcutyId(int id)
    {
        var dbProduct = _context.Product.AsNoTracking().FirstOrDefault(x => x.Id == id);

        if (dbProduct == null)
        {
            return null;
        }

        return dbProduct;
    }

    public ProductViewModel GetViewModelById(int id)
    {
        ProductViewModel product = new ProductViewModel();

        var dbProduct = _context.Product.FirstOrDefault(x => x.Id == id); 

        if (dbProduct == null) 
        {
            return null; 
        }

        Brand dbBrand = _context.Brand.FirstOrDefault(x => x.Id == dbProduct.MerkId);

        if (dbProduct == null)
        {
            return null;
        }

        var productViewModel = ProductMapper.Map(dbProduct, dbBrand);

        return productViewModel;
    }

    /// <summary>
    /// Adds or updates a product in the database
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public int Upsert(Product product)
    {
        //toevoegen als ID wordt terug gevonden in de DB

        //var brandName = allBrands.FirstOrDefault(x => x.Id == product.MerkId).MerkName;
        var dbProduct = _context.Product.AsNoTracking().FirstOrDefault(x => x.Id == product.Id);
        try
        {
            if (dbProduct == null)
            {
                _context.Product.Add(product);
                return _context.SaveChanges();
            }
            else
            {
                _context.Product.Update(product);
                int updatedEntities = _context.SaveChanges();
                return updatedEntities;
            }
        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex);
            return -1;
        }        
    }


}
