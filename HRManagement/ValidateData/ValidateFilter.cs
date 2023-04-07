using HRManagement.Filters;

namespace HRManagement.ValidateData
{
    public static class ValidateFilter
    {
        public static List<EmployeeFilter> ValidateEmployeeFilters(List<EmployeeFilter> employeeFilters)
        {
            if (employeeFilters == null)
            {
                return new List<EmployeeFilter>();
            }

            foreach (var filter in employeeFilters)
            {
                filter.PropertyValue ??= "";
            }
            return employeeFilters;
        }
    }
}
