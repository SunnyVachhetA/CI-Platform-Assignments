using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeTier.Entities.DataModels;
using ThreeTier.Entities.ViewModels;
using ThreeTier.Repositories.Repository.Interface;

namespace ThreeTier.Repositories.Repository;
public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeDbContext _employeeDbContext;

    public EmployeeRepository(EmployeeDbContext employeeDbContext)
    {
        _employeeDbContext = employeeDbContext;
    }

    public async Task<int> AddEmployeeAsync(EmployeeModel model)
    {
        var employee = new Employee()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            Role = model.Role,
        };
        _employeeDbContext.Add(employee);
        await _employeeDbContext.SaveChangesAsync();
        return employee.Id;
    }

    public async Task<int> EditEmployee(EmployeeModel model)
    {
        var employee = new Employee()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            Role = model.Role
        };

        await _employeeDbContext.SaveChangesAsync();
        return (int) employee.Id;
    }

    public async Task<List<EmployeeModel>> GetAllEmployeesAync()
    {
        return await _employeeDbContext.Employees.Select(
                        employee =>
                                    new EmployeeModel()
                                    {
                                        Id = employee.Id,
                                        FirstName = employee.FirstName,
                                        Role = employee.Role,
                                    }
                    ).ToListAsync();
        
    }

    public async Task<EmployeeModel> GetEmployeeByIdAsync(int? id)
    {
        var employee =  await _employeeDbContext.Employees.FindAsync(id);

        if (employee == null) return null;
        else
        {
            var model = new EmployeeModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                Role = employee.Role
            };
            return model;
        }
        //return null;
    }
}
