
using LuftBornTask.Application.DTOs;

namespace LuftBornTask.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();

        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<PaginatedResponseDto<ProductDto>> GetFilteredAndPagedProductsAsync(ProductFilterDto productFilterDto);
        Task<ProductDto> AddProductAsync(ProductDto productDto);
        Task<bool> DeleteProductAsync(Guid id);
        Task<ProductDto> UpdateProductAsync(Guid id,ProductDto modifiedProduct);

    }
}
