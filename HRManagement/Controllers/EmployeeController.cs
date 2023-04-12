using HRManagement.CustomExceptions;
using HRManagement.Filters;
using HRManagement.Models;
using HRManagement.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeesService;
        private readonly IFilterService _filtersService;
        public EmployeeController(IEmployeeService employeeService, IFilterService filtersService)
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
        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            return new OkObjectResult(await _employeesService.GetEmployeeById(id));
        }
        [HttpPost]
        [Route("Filter")]
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
        public async Task<IActionResult> EditEmployee([FromBody] EmployeeModel employee, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedEmployee = await _employeesService.EditEmployee(employee, id);
                return Ok(updatedEmployee);
            }
            catch (CouldNotUpdateUserException ex)
            {
                return BadRequest(new { error = "" + ex.Message });
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            return new OkObjectResult(await _employeesService.DeleteEmployee(id));
        }
    }
}
