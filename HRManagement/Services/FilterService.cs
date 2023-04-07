using HRManagement.Data.Repositories;
using HRManagement.Data.Repositories.RepositoryInterfaces;
using HRManagement.Data.UnitOfWork;
using HRManagement.Filters;
using HRManagement.Models;
using HRManagement.Services.ServiceInterfaces;
using HRManagement.ValidateData;

namespace HRManagement.Services
{
    public class FilterService : IFilterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterRepository _filterRepository;

        public FilterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _filterRepository = _unitOfWork.Filters;
        }
        public async Task<List<EmployeeModel>> FilterEmployees(List<EmployeeFilter> employeeFilters)
        {
            employeeFilters = ValidateFilter.ValidateEmployeeFilters(employeeFilters);
            return await _filterRepository.FilterEmployees(employeeFilters);
        }
    }
}
