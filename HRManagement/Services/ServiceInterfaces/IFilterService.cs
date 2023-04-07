using HRManagement.Filters;
using HRManagement.Models;

namespace HRManagement.Services.ServiceInterfaces
{
    public interface IFilterService
    {
        Task<List<EmployeeModel>> FilterEmployees(List<EmployeeFilter> employeeFilters);
    }
}
