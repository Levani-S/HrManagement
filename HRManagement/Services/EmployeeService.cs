using HRManagement.Data.Repositories.RepositoryInterfaces;
using HRManagement.Data.UnitOfWork;
using HRManagement.Models;
using HRManagement.Services.ServiceInterfaces;

namespace HRManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeesRepository;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _employeesRepository = _unitOfWork.Employees;
        }
        public  Task<List<EmployeeModel>> GetAllEmployee()
        {
            return _employeesRepository.GetAllEmployee();
        }
        public  Task<EmployeeModel> AddEmployee(EmployeeModel newEmployee)
        {
            return _employeesRepository.AddEmployee(newEmployee);
        }
        public Task<EmployeeModel> EditEmployee(EmployeeModel employee, Guid id)
        {
            return _employeesRepository.EditEmployee(employee,id);
        }
        public  Task<EmployeeModel> DeleteEmployee(Guid id)
        {
            return _employeesRepository.DeleteEmployee(id);
        }
    }
}
