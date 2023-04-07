using AutoMapper;
using HRManagement.Data.Repositories;
using HRManagement.Data.Repositories.RepositoryInterfaces;

namespace HRManagement.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HrManagementDbContext _context;
        public IEmployeeRepository Employees { get; private set; }
        public IFilterRepository Filters { get; private set; }

        public UnitOfWork(HrManagementDbContext context, IMapper mapper)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Filters = new FilterRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
