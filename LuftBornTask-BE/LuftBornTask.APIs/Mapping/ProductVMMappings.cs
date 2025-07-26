using LuftBornTask.APIs.ViewModels;
using LuftBornTask.APIs.ViewModels.Product;
using LuftBornTask.Application.DTOs;

namespace LuftBornTask.APIs.Mapping
{
    public static class ProductVMMappings
    {
        public static ProductDto ToDto(this ProductRequestVM vm)
        {
            return new ProductDto
            {
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

        public static ApiResponseVM<ProductResponseVM> ToApiResponseVM(this ProductDto dto, int statusCode = 200 ,List<string> errors = null)
        {
            return new ApiResponseVM<ProductResponseVM>
            {   
                StatusCode = statusCode,
                Success = true,
                Errors = errors,
                Data = new ProductResponseVM
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    Description = dto.Description
                }
            };
        }

        public static ApiResponseVM<PaginatedResponseVM<ProductResponseVM>> ToPaginatedApiResponseVM(
    this PaginatedResponseDto<ProductDto> paginatedDto, int statusCode = 200, List<string>? errors = null)
        {
            var paginatedVm = new PaginatedResponseVM<ProductResponseVM>
            {
                TotalCount = paginatedDto.TotalCount,
                PageNumber = paginatedDto.PageNumber,
                PageSize = paginatedDto.PageSize,
                Items = paginatedDto.Items.Select(dto => new ProductResponseVM
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    Description = dto.Description
                }).ToList()
            };

            return new ApiResponseVM<PaginatedResponseVM<ProductResponseVM>>
            {
                StatusCode = statusCode,
                Success = true,
                Data = paginatedVm,
                Errors = errors
            };
        }

    }
}
