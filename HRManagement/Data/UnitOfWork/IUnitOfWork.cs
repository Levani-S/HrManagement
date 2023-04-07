using HRManagement.Data.Repositories.RepositoryInterfaces;

namespace HRManagement.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IFilterRepository Filters { get; }
    }
}
