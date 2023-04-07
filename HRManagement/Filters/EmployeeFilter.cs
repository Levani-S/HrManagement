using HRManagement.Filters.Enums;

namespace HRManagement.Filters
{
    public class EmployeeFilter
    {
        public FilteringEmployee PropertyType { get; set; }
        public string PropertyValue { get; set; }
    }
}
