using HRManagement.Filters;
using HRManagement.Models;
using HRManagement.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeesService;
        private readonly IFilterService _filtersService;
        public EmployeesController(IEmployeeService employeeService, IFilterService filtersService)
        {
            _employeesService = employeeService;
            _filtersService = filtersService;
        }
        [HttpGet]
        [Route("AllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            return new OkObjectResult(await _employeesService.GetAllEmployee());
        }
        [HttpPost("Filter")]
        public async Task<IActionResult> FilterEmployees([FromBody] List<EmployeeFilter> employeeFilters)
        {
            var result = await _filtersService.FilterEmployees(employeeFilters);
            return Ok(result);
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeModel newEmployee)
        {
            return new OkObjectResult(await _employeesService.AddEmployee(newEmployee));
        }
        [HttpPut]
        [Route("EditEmployee/{id}")]
        public async Task<IActionResult> EditEmployee([FromBody]EmployeeModel employee, Guid id)
        {
            return new OkObjectResult(await _employeesService.EditEmployee(employee,id));
        }
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            return new OkObjectResult(await _employeesService.DeleteEmployee(id));
        }
    }
}
