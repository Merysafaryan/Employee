using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Employee.Management.system.DTOs;
using Employee.Management.system.Responses;

namespace Employee.Management.system.Services
{
    public interface IEmployeeService
    {
        Task<GetEmployeesResponse> GetEmployees();
        Task<BaseResponse> AddEmployee(AddEmployeeForm form);
        Task<GetEmployeeResponse> GetEmployee(int id);
        Task<BaseResponse> DeleteEmployee(EmployeeManagement.DTOs.Employee employee);
        Task<BaseResponse> EditEmployee(EmployeeManagement.DTOs.Employee employee);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IDbContextFactory<DataContext> _factory;

        public EmployeeService(IDbContextFactory<DataContext> factory)
        {
            _factory = factory;
        }

        public async Task<BaseResponse> AddEmployee(AddEmployeeForm form)
        {
            var response = new BaseResponse();
            try
            {
                using (var context = _factory.CreateDbContext())
                {
                    context.Add(new EmployeeManagement.DTOs.Employee
                    {
                        Name = form.Name,
                        Position = form.Position,
                        Salary = form.Salary,
                        Type = form.Type,
                        ImgUrl = form.ImgUrl
                    });
                    var result = await context.SaveChangesAsync();
                    if (result == 1)
                    {
                        response.StatusCode = 200;
                        response.Message = "Employee added successfully";
                    }
                    else
                    {
                        response.StatusCode = 400;
                        response.Message = "Error occurred while adding employee";
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Error adding employee: " + ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> DeleteEmployee(EmployeeManagement.DTOs.Employee employee)
        {
            var response = new BaseResponse();
            try
            {
                using (var context = _factory.CreateDbContext())
                {
                    context.Remove(employee);
                    var result = await context.SaveChangesAsync();
                    if (result == 1)
                    {
                        response.StatusCode = 200;
                        response.Message = "Employee deleted successfully";
                    }
                    else
                    {
                        response.StatusCode = 400;
                        response.Message = "Error occurred while deleting employee";
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Error deleting employee: " + ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> EditEmployee(EmployeeManagement.DTOs.Employee employee)
        {
            var response = new BaseResponse();
            try
            {
                using (var context = _factory.CreateDbContext())
                {
                    context.Update(employee);
                    var result = await context.SaveChangesAsync();
                    if (result == 1)
                    {
                        response.StatusCode = 200;
                        response.Message = "Employee updated successfully";
                    }
                    else
                    {
                        response.StatusCode = 400;
                        response.Message = "Error occurred while updating employee";
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Error updating employee: " + ex.Message;
            }
            return response;
        }

        public async Task<GetEmployeeResponse> GetEmployee(int id)
        {
            var response = new GetEmployeeResponse();
            try
            {
                using (var context = _factory.CreateDbContext())
                {
                    var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                    if (employee != null)
                    {
                        response.StatusCode = 200;
                        response.Message = "Success";
                        response.Employee = employee;
                    }
                    else
                    {
                        response.StatusCode = 404;
                        response.Message = "Employee not found";
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Error retrieving employee: " + ex.Message;
            }
            return response;
        }

        public async Task<GetEmployeesResponse> GetEmployees()
        {
            var response = new GetEmployeesResponse();
            try
            {
                using (var context = _factory.CreateDbContext())
                {
                    var employees = await context.Employees.ToListAsync();
                    response.StatusCode = 200;
                    response.Message = "Success";
                    response.Employees = employees;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Error retrieving employees: " + ex.Message;
            }
            return response;
        }
    }
}
