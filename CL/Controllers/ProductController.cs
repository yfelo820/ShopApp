using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Services;

namespace CL.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        [HttpGet]        
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound($"Product Not Found Product id: {id}");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            return Ok(await _productService.AddProduct(product));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            return Ok(await _productService.UpdateProduct(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterProductsByName(string name)
        {
            return Ok(await _productService.FiltersProductsByName(name));
        }
    }
}
