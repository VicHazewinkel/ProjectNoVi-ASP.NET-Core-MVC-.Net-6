using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.Areas.Identity.Data;

namespace ProjectNoVi_V3.Web.Services;

public interface IBrandService
{
    public List<Brand> GetAll();
    public List<Brand> GetByIds(List<int> ids);
}

public class BrandsService : IBrandService
{
    private readonly ProjectNoVi_V3_Context _context;

    public BrandsService(ProjectNoVi_V3_Context context)
    {
        _context = context;
    }

    public List<Brand> GetAll()
    {
        List<Brand> brands = new List<Brand>();

        var dbBrands = _context.Brand.Where(x => x.Id > 0);  // linq
        
        brands.AddRange(dbBrands);
        return brands;
    }

    public List<Brand> GetByIds(List<int> ids)
    {
        List<Brand> brands = new List<Brand>();

        foreach (int id in ids)
        {
            brands.Add(_context.Brand.FirstOrDefault(b => b.Id == id));
        }

        return brands;
    }
}
