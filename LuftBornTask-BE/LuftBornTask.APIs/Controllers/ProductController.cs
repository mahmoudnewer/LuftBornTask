    using LuftBornTask.APIs.Mapping;
    using LuftBornTask.APIs.ViewModels;
    using LuftBornTask.APIs.ViewModels.Product;
    using LuftBornTask.Application.DTOs;
    using LuftBornTask.Application.Interfaces.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    namespace LuftBornTask.APIs.Controllers
    {
        [Authorize]
        [Route("api/[controller]")]
        [ApiController]
        public class ProductController : ControllerBase
        {
            private readonly IProductService _productService;
            public ProductController(IProductService productService)
            {
                _productService = productService;
            }


            [HttpGet("GetAll")]
            public async Task<IActionResult> GetAll()
            {
                try 
                {
                    var result = await _productService.GetAllProductsAsync();
                    var resultVM = result.Select(p => p.ToResponseVM()).ToList();
                    return Ok(new ApiResponseVM<List<ProductResponseVM>>()
                        {
                            Success = true,
                            Data = resultVM,
                            StatusCode = 200
                        });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ApiResponseVM<string>()
                    {
                        StatusCode = 500,
                        Errors = new List<string> { ex.Message },
                        Success = false,
                        Data = null
                    });
                }

            }

            //GET: api/<ProductController>
            [HttpGet("GetAllPaged")]
            public async Task<IActionResult> Get([FromQuery] ProductFilterDto filter)
            {
                try 
                { 
                    var result = await _productService.GetFilteredAndPagedProductsAsync(filter);
                    var response = result.ToPaginatedApiResponseVM();
                    return Ok(response); 
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ApiResponseVM<string>()
                    {
                        StatusCode = 500,
                        Errors = new List<string> { ex.Message },
                        Success = false,
                        Data = null
                    });
                }
            }

            // GET api/<ProductController>/5
            [HttpGet("{id}")]
            public async Task<IActionResult> Get(Guid id)
            {
                try 
                {
                    var product = await _productService.GetProductByIdAsync(id);
                    var productVM = product.ToResponseVM();
                    return Ok(new ApiResponseVM<ProductResponseVM>
                    {
                        Success = true,
                        StatusCode = 200,
                        Data = productVM
                    });

            }
                catch (KeyNotFoundException ex) 
                {
                    return NotFound(new ApiResponseVM<string>()
                    {
                        StatusCode = 404,
                        Errors = new List<string> { "Product Not found" },
                        Success = false,
                        Data = null
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500,new ApiResponseVM<string>()
                    {
                        StatusCode = 500,
                        Errors = new List<string> { ex.Message },
                        Success = false,
                        Data = null
                    });
                }
            }

            // POST api/<ProductController>
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] ProductRequestVM productVM)
            {
                try 
                {
                    var productDto = productVM.ToDto();
                    await _productService.AddProductAsync(productDto);
                    return Ok(new ApiResponseVM<string>()
                    {
                        Success = true,
                        StatusCode = 200,
                        Data = "Product created successfully"
                    }
                    );
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new ApiResponseVM<string>()
                    {
                        StatusCode = 400,
                        Errors = new List<string> { ex.Message },
                        Success = false,
                        Data = null
                    });
                }
                catch (Exception ex) 
                {
                    return StatusCode(500, new ApiResponseVM<string>()
                    {
                        StatusCode = 500,
                        Errors = new List<string> { ex.Message },
                        Success = false,
                        Data = null
                    });
                }

            }

            // PUT api/<ProductController>/5
            [HttpPut("{id}")]
            public async Task<IActionResult> Put([FromBody] ProductRequestVM productVM, Guid id)
            {
                try
                { 
                    var productDto = productVM.ToDto();
                    var UpdatedProduct = await _productService.UpdateProductAsync(id, productDto);
                    var UpdatedProductVM = UpdatedProduct.ToApiResponseVM();
                    return Ok(UpdatedProductVM);
                }
                catch (KeyNotFoundException ex) 
                {
                    return NotFound(new ApiResponseVM<string>()
                    {
                        StatusCode = 404,
                        Errors = new List<string> { "Product to be updated Not found" },
                        Success = false,
                        Data = null
                    });
                }
                catch (InvalidOperationException ex) 
                {
                    return BadRequest(new ApiResponseVM<string>()
                    {
                        StatusCode = 400,
                        Errors = new List<string> { ex.Message },
                        Success = false,
                        Data = null
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new ApiResponseVM<string>()
                    {
                        StatusCode = 500,
                        Errors = new List<string> { ex.Message },
                        Success = false,
                        Data = null
                    });
                }
            }

            // DELETE api/<ProductController>/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                try 
                {
                    await _productService.DeleteProductAsync(id);
                    return Ok(new ApiResponseVM<string>()
                    {
                        Success = true,
                        StatusCode = 200,
                        Data = "Product deleted successfully"
                    });
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new ApiResponseVM<string>()
                    {
                        StatusCode = 404,
                        Errors = new List<string> { "Product to be deleted Not found" },
                        Success = false,
                        Data = null
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500,new ApiResponseVM<string>()
                    {
                        StatusCode = 500,
                        Errors = new List<string> { ex.Message },
                        Success = false,
                        Data = null
                    });
                }
            }
        }
    }
