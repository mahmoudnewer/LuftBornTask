using LuftBornTask.Application.Interfaces.Repository;
using LuftBornTask.Domain.Entities;
using LuftBornTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LuftBornTask.Infrastructure.Implementation.Repository
{
    public class ProductEFRepo : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductEFRepo(ApplicationContext context)
        {
            _context = context;

        }
        public async Task AddAsync(Product product)
        {
            await _context.AddAsync(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<(List<Product> products, int totalCount)> GetFilteredAdPagedAsync(Expression<Func<Product, bool>>? predicate, int pageNumber, int pageSize)
        {
            var query = _context.Products.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public Product Update(Product project)
        {
            _context.Products.Update(project);
            return project;
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.AnyAsync(predicate);
        }


    }
}
