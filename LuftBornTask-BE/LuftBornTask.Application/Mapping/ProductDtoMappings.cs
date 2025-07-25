
using LuftBornTask.Application.DTOs;
using LuftBornTask.Domain.Entities;

namespace LuftBornTask.Application.Mapping
{
    public static class ProductDtoMappings
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description
            };
        }

        public static Product ToEntity(this ProductDto dto)
        {
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Quantity = dto.Quantity,
                Description = dto.Description
            };
        }
    }
}
