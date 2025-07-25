using LuftBornTask.APIs.ViewModels;
using LuftBornTask.Application.DTOs;

namespace LuftBornTask.APIs.Mapping
{
    public static class ProductVMMappings
    {
        public static ProductDto ToDto(this ProductRequestVM vm)
        {
            return new ProductDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Price = vm.Price,
                Quantity = vm.Quantity,
                Description = vm.Description
            };
        }

        public static ProductResponseVM ToResponseVM(this ProductDto dto)
        {
            return new ProductResponseVM
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
