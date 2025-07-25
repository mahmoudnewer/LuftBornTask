
using LuftBornTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuftBornTask.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
