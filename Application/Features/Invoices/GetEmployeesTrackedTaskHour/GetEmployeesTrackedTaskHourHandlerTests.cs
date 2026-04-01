using Application.ExternalDeps.EmployeesApi;
using Application.ExternalDeps.TimeApi;
using Moq;
using Xunit;

namespace Application.Features.Invoices.GetEmployeesTrackedTaskHour;

[UnitTest]
public class GetEmployeesTrackedTaskHoursHandlerTests
{
    [Fact]
    public async Task GetEmployeesTrackedTaskHoursHandler_ShouldReturnCorrectResultWithEmployeesTrackedTaskHours()
    {
        var employee = new EmployeeDto
        {
            Id = 1,
            FullName = "Test Test Test",
            TenantId = 1
        };

        var employeesApiMock = new Mock<IEmployeesApi>();

        employeesApiMock
            .Setup(x => x.GetAllEmployeesAsync())
            .ReturnsAsync(new EmployeesResponse
            {
                Employees = new List<EmployeeDto>
                {
                    employee
                }
            });

        var timeEmployeeTrackedTaskHour = new TimeEmployeesTrackedTaskHourDto
        {
            EmployeeId = employee.Id,
            TrackedHours = 40,
        };

        var timeApiMock = new Mock<ITimeApi>();

        timeApiMock
            .Setup(x => x.GetEmployeesTrackedTaskHoursAsync(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new TimeGetEmployeesTrackedTaskHoursResponse
            {
                EmployeesTrackedTaskHours = new List<TimeEmployeesTrackedTaskHourDto>
                {
                    timeEmployeeTrackedTaskHour
                }
            });

        var getEmployeesTrackedTaskHoursHandler = new GetEmployeesTrackedTaskHoursHandler(
            employeesApiMock.Object,
            timeApiMock.Object
        );

        var result = await getEmployeesTrackedTaskHoursHandler.HandleAsync(1, 1, 1999);

        Assert.Equal(employee.Id, result.EmployeesTrackedTaskHours[0].EmployeeId);
        Assert.Equal(employee.FullName, result.EmployeesTrackedTaskHours[0].FullName);
        Assert.Equal(timeEmployeeTrackedTaskHour.TrackedHours, result.EmployeesTrackedTaskHours[0].TrackedHours);
    }
}
