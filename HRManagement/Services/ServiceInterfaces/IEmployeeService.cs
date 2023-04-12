using HRManagement.Models;

namespace HRManagement.Services.ServiceInterfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetAllEmployee();
        Task<EmployeeModel?> GetEmployeeById(Guid id);
        Task<EmployeeModel> AddEmployee(EmployeeModel newEmployee);
        Task<EmployeeModel> EditEmployee(EmployeeModel employee, Guid id);
        Task<EmployeeModel> DeleteEmployee(Guid id);
    }
}
