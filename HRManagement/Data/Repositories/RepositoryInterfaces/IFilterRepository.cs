using HRManagement.Filters;
using HRManagement.Models;

namespace HRManagement.Data.Repositories.RepositoryInterfaces
{
    public interface IFilterRepository
    {
        Task<List<EmployeeModel>> FilterEmployees(List<EmployeeFilter> employeeFilters);
    }
}
