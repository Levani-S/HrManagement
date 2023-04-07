using HRManagement.CustomExceptions;
using HRManagement.Data.Repositories.RepositoryInterfaces;
using HRManagement.Models;
using HRManagement.ValidateData;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrManagementDbContext _context;
        public EmployeeRepository(HrManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeModel>> GetAllEmployee()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task<List<EmployeeModel>> GetEmployeesByFirstNameOrLastName(string firstName, string lastName)
        {
            List<EmployeeModel> employees = await _context.Employees.Where(employee => employee.FirstName == firstName || employee.LastName == lastName).ToListAsync();

            return employees;
        }
        public async Task<EmployeeModel> AddEmployee(EmployeeModel newEmployee)
        {
            ValidateOnNull<EmployeeModel>.ValidateDataOnNull(newEmployee);

            if (_context.Employees.Any(x => x.IdentifyNumber == newEmployee.IdentifyNumber))
            {
                throw new AlreadyExistsException("User with that ID Number already exists");
            }
            if (_context.Employees.Any(x => x.Email == newEmployee.Email))
            {
                throw new AlreadyExistsException("User with that E-Mail already exists");
            }
            EmployeeModel employee = new EmployeeModel()
            {
                IdentifyNumber = newEmployee.IdentifyNumber,
                Email = newEmployee.Email,
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                GenderId = newEmployee.GenderId,
                BirthDate = newEmployee.BirthDate,
                Position = newEmployee.Position,
                Status = newEmployee.Status,
                ReleaseDate = newEmployee.ReleaseDate,
                PhoneNumber = newEmployee.PhoneNumber,
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<EmployeeModel> EditEmployee(EmployeeModel employee, Guid id)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("Incorrect Edit!");
            }
            var employeeToUpdate = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (_context.Employees.Any(x => x.IdentifyNumber == employee.IdentifyNumber))
            {
                throw new CouldNotUpdateUserException("User with that ID Number already exists");
            }
            if (_context.Employees.Any(x => x.Email == employee.Email))
            {
                throw new CouldNotUpdateUserException("User with that E-Mail already exists");
            }

            employeeToUpdate.IdentifyNumber = employee.IdentifyNumber;
            employeeToUpdate.Email = employee.Email;
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.GenderId = employee.GenderId;
            employeeToUpdate.BirthDate = employee.BirthDate;
            employeeToUpdate.Position = employee.Position;
            employeeToUpdate.Status = employee.Status;
            employeeToUpdate.ReleaseDate = employee.ReleaseDate;
            employeeToUpdate.PhoneNumber = employee.PhoneNumber;


            await _context.SaveChangesAsync();

            return employeeToUpdate;
        }
        public async Task<EmployeeModel> DeleteEmployee(Guid id)
        {
            EmployeeModel? findEmployee = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (findEmployee == null)
            {
                throw new DoesNotExistsException("Employee not found to delete");
            }
            _context.Employees.Remove(findEmployee);
            await _context.SaveChangesAsync();

            return findEmployee;
        }
    }
}
