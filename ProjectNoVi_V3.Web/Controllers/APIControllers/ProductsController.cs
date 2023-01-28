using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ProjectNoVi_V3.Enums;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.Controllers.APIControllers.DTOs;
using ProjectNoVi_V3.Web.Migrations;
using ProjectNoVi_V3.Web.Services;
using System.Net;

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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            //zoeken op basis van id
            var result = _productService.DeleteById(id);
            // var result = true;
            if(result)
            {
                return new OkObjectResult("ok");
            }

            return new BadRequestObjectResult("Not Ok");
            
        }

        [HttpPut]
        public async Task<IActionResult> Update ([FromQuery]int id, [FromBody]ProductDto productDto)
        {
            if(id == null && id == 0) 
            {
                return new BadRequestObjectResult("id is required for update");
            }

            // string omzetten naar Enum 
            bool result = Enum.TryParse(productDto.type, out ProductType productType);

            if (result == false)
            {
                return new BadRequestObjectResult("Could not parse type into enum.");
            }

            var brands = _brandsService.GetAll();
            var brand = brands.FirstOrDefault(x => x.MerkName == productDto.brandName);

            if (brand == null)
            {
                var response =  new BadRequestObjectResult("Could not find brand based on brandname");
                response.StatusCode = (int) HttpStatusCode.NotFound;
                return response;
            }

            var product = new Product()
            {
                Id = id,
                Type = productType,
                ProductMerk = brand,
                Collectie = productDto.collection,
                Kleur = productDto.color,
                Materiaal = productDto.material,
                Prijs = productDto.price,
                Correctie = productDto.correction,
                MerkId = brand.Id,
            };

            var updateResult = _productService.UpdateById(product); 
            if(updateResult) 
            {
                return new OkObjectResult("ok"); 
            }
            return new BadRequestObjectResult("Not Ok"); 
        }
    }
}
