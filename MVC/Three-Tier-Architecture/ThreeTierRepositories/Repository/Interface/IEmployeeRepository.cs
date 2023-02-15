using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ThreeTier.Entities.ViewModels;

namespace ThreeTier.Repositories.Repository.Interface;
public interface IEmployeeRepository
{
    Task<List<EmployeeModel>> GetAllEmployeesAync();

    Task<int> AddEmployeeAsync( EmployeeModel model);

    Task<EmployeeModel> GetEmployeeByIdAsync(int? id);
    Task<int> EditEmployee(EmployeeModel model);
}
