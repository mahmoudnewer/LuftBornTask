
using LuftBornTask.Domain.Entities;
using System.Linq.Expressions;

namespace LuftBornTask.Application.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<List<Product>> GetAllAsync();

        Task AddAsync(Product project);
        Product Update(Product project);
        void Delete(Product product);
        Task<(List<Product> products, int totalCount)> GetFilteredAdPagedAsync(
                Expression<Func<Product, bool>>? predicate,int pageNumber,int pageSize);
        Task<bool> AnyAsync(Expression<Func<Product, bool>> predicate);

    }
}
