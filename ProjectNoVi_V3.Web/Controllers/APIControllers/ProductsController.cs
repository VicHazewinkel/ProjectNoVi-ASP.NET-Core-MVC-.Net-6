using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ProjectNoVi_V3.Enums;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.Controllers.APIControllers.DTOs;
using ProjectNoVi_V3.Web.Migrations;
using ProjectNoVi_V3.Web.Services;

namespace ProjectNoVi_V3.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandsService;

        public ProductsController(IProductService productService, IBrandService brandService) 
        {
            _productService = productService;
            _brandsService = brandService;
            //_brandService = 
        }


    //    {
    //    "id": 6,
    //    "type": 2,
    //    "merkId": 6,
    //    "productMerk": null,
    //    "collectie": "Dalies Total 1",
    //    "kleur": "Dag lens",
    //    "materiaal": "Silicone",
    //    "prijs": 30,
    //    "correctie": 0
    //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allProducts =_productService.GetAll();
            //var allBrands = _brandsService.GetAll();
            List<Brand> allBrands = _brandsService.GetAll();

            List<ProductDto> productDtos = new List<ProductDto>();

            foreach (var product in allProducts)
            {
                var brandName = allBrands.FirstOrDefault(x => x.Id == product.MerkId).MerkName;

                ProductDto dto = new ProductDto()
                {
                    type = product.Type.ToString(),
                    brandName = brandName, 
                    collection = product.Collectie,
                    color = product.Kleur, 
                    material = product.Materiaal, 
                    price = product.Prijs, 
                    correction = product.Correctie, 
                };
                productDtos.Add(dto);
            }

            return new OkObjectResult(productDtos);
        }

        //
        //
        //
        
        [HttpPost]
        public async Task<IActionResult> Upsert(ProductDto productDto)
        {

            //Enum.TryParse("Active", out StatusEnum myStatus);

            bool result = Enum.TryParse(productDto.type, out ProductType productType);

            if(result == false) 
            { 
                return new BadRequestObjectResult("Could not parse type into enum.");
            }

            List<Brand> allBrands = _brandsService.GetAll();
            Brand brand = allBrands.FirstOrDefault(x => x.MerkName == productDto.brandName);

            Product product = new Product()
            {
                Type = productType,
                Collectie = productDto.collection,
                Kleur = productDto.color,
                Materiaal = productDto.material,
                Prijs = productDto.price,
                Correctie = productDto.correction,
                MerkId = brand.Id,
                ProductMerk = brand
            };

            var upsertResult = _productService.Upsert(product);

            if (upsertResult < 0)
            {
                return new BadRequestObjectResult("An error occurred while creating entity"); 
            } 

            return new OkObjectResult(upsertResult);
        }
    }
}
