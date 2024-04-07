namespace Employee.Management.system.Responses;

public class GetEmployeesResponse : BaseResponse
{
    public List<Employee>? Employees { get; set; }
}
