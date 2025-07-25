using LuftBornTask.APIs.Mapping;
using LuftBornTask.APIs.ViewModels;
using LuftBornTask.Application.DTOs;
using LuftBornTask.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LuftBornTask.APIs.Controllers
{
    [Authorize]
    //[Route("api/[controller]")]
    [Route("secure")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetAllProductsAsync();
            var resultVM = result.Select(p => p.ToResponseVM());
            return Ok(resultVM);
        }

        // GET: api/<ProductController>
        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] ProductFilterDto filter)
        //{
        //    var result = await _productService.GetFilteredAndPagedProductsAsync(filter);
        //    var resultVM = result.Select(p => p.ToResponseVM());
        //    return Ok(resultVM);
        //}

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            var productVM = product.ToResponseVM();
            return Ok(productVM);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductRequestVM productVM)
        {
            var productDto = productVM.ToDto();
            await _productService.AddProductAsync(productDto);
            return Ok(new { message = "product created" });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] ProductRequestVM productVM, Guid id)
        {
            var productDto = productVM.ToDto();
            var UpdatedProduct = await _productService.UpdateProductAsync(id,productDto);
            var UpdatedProductVM = UpdatedProduct.ToResponseVM();
            return Ok(UpdatedProductVM);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok(new { message = "Product deleted" });
        }
    }
}
