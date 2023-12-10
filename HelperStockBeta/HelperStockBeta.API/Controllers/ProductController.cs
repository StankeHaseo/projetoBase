using System.Threading.Tasks;
using HelperStockBeta.Application.DTOs;
using HelperStockBeta.Application.Interfaces;
using HelperStockBeta.Application.Services;
using Microsoft.AspNetCore.Mvc;
using HelperStockBeta.API;

namespace HelperStockBeta.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        
        private  IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products = await _productService.GetProduct();
            return Ok(products);
        }

        [HttpGet("{id:int?}", Name = "GetProduct")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDTO)
        {
            await _productService.Add(productDTO);
            return CreatedAtAction(nameof(GetProductById), new { id = productDTO.Id });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            await _productService.Update(productDTO);
            return Ok();
        }

        [HttpDelete("{id:int?}", Name = "RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(int? id)
        {
            await _productService.Remove(id);
            return Ok();
        }
    }
}
