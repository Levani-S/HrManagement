using HRManagement.Filters.Enums;
using HRManagement.Filters;
using HRManagement.Models;
using Microsoft.EntityFrameworkCore;
using HRManagement.ValidateData;
using HRManagement.Data.Repositories.RepositoryInterfaces;

namespace HRManagement.Data.Repositories
{
    public class FilterRepository : IFilterRepository
    {
        private readonly HrManagementDbContext _context;
        public FilterRepository(HrManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeModel>> FilterEmployees(List<EmployeeFilter> employeeFilters)
        {
            employeeFilters = ValidateFilter.ValidateEmployeeFilters(employeeFilters);

            var employees = _context.Employees.AsQueryable();

            foreach (var filter in employeeFilters)
            {
                if (filter.PropertyType == FilteringEmployee.FirstName)
                {
                    employees = ApplyFirstNameFilters(employees, filter);
                }
                else if (filter.PropertyType == FilteringEmployee.LastName)
                {
                    employees = ApplyLastNameFilters(employees, filter);
                }
            }
            return await employees.ToListAsync();
        }
        private IQueryable<EmployeeModel> ApplyFirstNameFilters(IQueryable<EmployeeModel> employees, EmployeeFilter employeeFilter)
        {
            return employees.Where(employee => employee.FirstName.Contains(employeeFilter.PropertyValue));
        }

        private IQueryable<EmployeeModel> ApplyLastNameFilters(IQueryable<EmployeeModel> employees, EmployeeFilter employeeFilter)
        {
            return employees.Where(employee => employee.LastName.Contains(employeeFilter.PropertyValue));
        }
    }
}
