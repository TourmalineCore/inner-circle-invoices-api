using Application.ExternalDeps.EmployeesApi;
using static Application.ExternalDeps.EmployeesApi.EmployeesResponse;

namespace Application.Features.Invoices.GetEmployeesEntriesByProjectAndPeriod
{
    public class EmployeeMapper
    {
        public const string NotFoundEmployeeFullName = "Not Found";

        public static EmployeeDto? MapToEmployeeDto(long? employeeId, EmployeesResponse employeesResponse)
        {
            return employeeId == null
                ? null
                : new EmployeeDto
                {
                    Id = employeeId.Value,
                    FullName = employeesResponse
                        .Employees
                        .SingleOrDefault(y => y.Id == employeeId.Value)
                        ?.FullName ?? NotFoundEmployeeFullName
                };
        }
    }
}
