
using LuftBornTask.Application.Interfaces.UnitOfWork;
using LuftBornTask.Infrastructure.Context;

namespace LuftBornTask.Infrastructure.Implementation.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
