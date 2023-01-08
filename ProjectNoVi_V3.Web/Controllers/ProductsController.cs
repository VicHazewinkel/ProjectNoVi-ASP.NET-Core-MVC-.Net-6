using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.Areas.Identity.Data;
using ProjectNoVi_V3.Web.Mapper;
using ProjectNoVi_V3.Web.Services;
using ProjectNoVi_V3.Web.ViewModels;

namespace ProjectNoVi_V3.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ProjectNoVi_V3_Context _context;

        public ProductsController(IProductService productService, IBrandService brandService)
        {
            _productService = productService;
            _brandService = brandService;
            //_context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //1. ophalen van products uit DB
            var productsList = _productService.GetAll();
            productsList.Add(productsList[0]);

            //get all distinct brand Ids out of products
            List<int> brandIds= new List<int>();
            brandIds = productsList.Select(p => p.MerkId).Distinct().ToList();

            var brands = _brandService.GetByIds(brandIds);

            //2. omvormen van products naar viewmodels
            List<ProductViewModel> viewModelList = new List<ProductViewModel>();
            foreach (var product in productsList)
            {
                var brand = brands.FirstOrDefault(x => x.Id == product.MerkId);

                var viewModel = ProductMapper.Map(product, brand);
                
                viewModelList.Add(viewModel); 
            }

            //3. doorgeven van viewmodels naar view
            return View(viewModelList);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,MerkId,Collectie,Kleur,Materiaal,Prijs,Correctie")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,MerkId,Collectie,Kleur,Materiaal,Prijs,Correctie")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ProjectNoVi_V3_Context.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
