
using LuftBornTask.Application.DTOs;
using LuftBornTask.Application.Interfaces.Repository;
using LuftBornTask.Application.Interfaces.Services;
using LuftBornTask.Application.Interfaces.UnitOfWork;
using LuftBornTask.Application.Mapping;

namespace LuftBornTask.Infrastructure.Implementation.Services
{
    public class OrdinaryProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrdinaryProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> AddProductAsync(ProductDto productDto)
        {
            var ProductEntity = productDto.ToEntity();
            await _productRepository.AddAsync(ProductEntity);
            await _unitOfWork.SaveChangesAsync();
            return productDto;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var result =  _productRepository.Delete(product);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            else 
            {
                throw new Exception("Error happened when deleting the product");
            }
            return true;
        }

        public Task<List<ProductDto>> GetFilteredAndPagedProductsAsync(ProductFilterDto productFilterDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            return product.ToDto();
        }

        public async Task<ProductDto> UpdateProductAsync(Guid id, ProductDto modifiedProduct)
        {
            var dpProduct = await _productRepository.GetByIdAsync(id);
            if (dpProduct == null) throw new KeyNotFoundException("product not found.");
            if (!string.IsNullOrWhiteSpace(modifiedProduct.Name))
            {
                var isNameTaken = await _productRepository.AnyAsync(p => p.Name.ToLower() == modifiedProduct.Name.ToLower() && p.Id != id);
                if (isNameTaken)
                    throw new InvalidOperationException($"A project with the name '{modifiedProduct.Name}' already exists.");
                dpProduct.Name = modifiedProduct.Name;
            }
            if (modifiedProduct.Price > 0)
            {
                dpProduct.Price = modifiedProduct.Price;
            }
            if (modifiedProduct.Quantity > 0)
            {
                dpProduct.Price = modifiedProduct.Price;
            }
            return modifiedProduct;

        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if (products == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            return products.Select(p=>p.ToDto()).ToList();
        }
    }
}
