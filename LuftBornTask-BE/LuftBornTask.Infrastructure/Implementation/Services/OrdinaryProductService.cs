
using LuftBornTask.Application.DTOs;
using LuftBornTask.Application.Interfaces.Repository;
using LuftBornTask.Application.Interfaces.Services;
using LuftBornTask.Application.Interfaces.UnitOfWork;
using LuftBornTask.Application.Mapping;
using LuftBornTask.Domain.Entities;
using System.Linq.Expressions;

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
            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new InvalidOperationException("Product name cannot be empty.");

            if (productDto.Price <= 0)
                throw new InvalidOperationException("Product price must be greater than 0.");

            if (productDto.Quantity <= 0)
                throw new InvalidOperationException("Product quantity must be greater than 0.");

            bool isNameTaken = await _productRepository.AnyAsync(
                p => p.Name.ToLower() == productDto.Name.ToLower());

            if (isNameTaken)
                throw new InvalidOperationException($"A product with the name '{productDto.Name}' already exists.");

            var productEntity = productDto.ToEntity();
            productEntity.CreatedAt = DateTimeOffset.UtcNow;

            await _productRepository.AddAsync(productEntity);
            await _unitOfWork.SaveChangesAsync();

            return productEntity.ToDto(); 
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<PaginatedResponseDto<ProductDto>> GetFilteredAndPagedProductsAsync(ProductFilterDto productFilterDto)
        {
            Expression<Func<Product, bool>> predicate = product =>
                (string.IsNullOrEmpty(productFilterDto.Name) || product.Name.ToLower().Contains(productFilterDto.Name.ToLower())) &&
                (!productFilterDto.Price.HasValue || product.Price == productFilterDto.Price);

            var (products, totalCount) = await _productRepository.GetFilteredAdPagedAsync(
                predicate,
                productFilterDto.pageNumber,
                productFilterDto.pageSize);

            var dtoList = products.Select(p => p.ToDto()).ToList();

            return new PaginatedResponseDto<ProductDto>
            {
                Items = dtoList,
                TotalCount = totalCount,
                PageNumber = productFilterDto.pageNumber,
                PageSize = productFilterDto.pageSize
            };
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
                    throw new InvalidOperationException($"A product with the name '{modifiedProduct.Name}' already exists.");
                dpProduct.Name = modifiedProduct.Name;
            }
            else 
            {
                throw new InvalidOperationException("Product name cannot be empty.");
            }
            if (modifiedProduct.Price > 0)
            {
                dpProduct.Price = modifiedProduct.Price;
            }
            else
            {
                throw new InvalidOperationException("Product price cannot be 0 or less.");
            }
            if (modifiedProduct.Quantity > 0)
            {
                dpProduct.Quantity = modifiedProduct.Quantity;
            }
            else 
            {
                throw new InvalidOperationException("Product Quantity cannot be 0 or less.");
            }
            dpProduct.UpdatedAt = DateTimeOffset.UtcNow;
            await _unitOfWork.SaveChangesAsync();
            return modifiedProduct;

        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p=>p.ToDto()).ToList();
        }
    }
}
