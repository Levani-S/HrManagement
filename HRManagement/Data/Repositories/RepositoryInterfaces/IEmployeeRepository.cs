using HRManagement.Models;

namespace HRManagement.Data.Repositories.RepositoryInterfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetAllEmployee();
        Task<EmployeeModel?> GetEmployeeById(Guid id);
        Task<EmployeeModel> AddEmployee(EmployeeModel newEmployee);
        Task<EmployeeModel> EditEmployee(EmployeeModel employee, Guid id);
        Task<EmployeeModel> DeleteEmployee(Guid id);
    }
}
