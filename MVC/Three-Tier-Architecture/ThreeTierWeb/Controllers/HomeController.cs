using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThreeTier.Entities.ViewModels;
using ThreeTier.Repositories.Repository.Interface;
using ThreeTierWeb.Models;

namespace ThreeTierWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmployeeRepository _employeeRepository;
    public HomeController(ILogger<HomeController> logger, IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }
    [Route("", Name = "Default")]
    [Route("home/employee-list", Name = "EmployeeList")]
    public async Task<IActionResult> Index()
    {
        var employeeList = await _employeeRepository.GetAllEmployeesAync();
        return View(employeeList);
    }


    [Route("home/create-employee", Name = "CreateEmployee")]
    public IActionResult CreateEmployee()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("home/create-employee")]
    public async Task<IActionResult> CreateEmployee(EmployeeModel employee)
    {
        if(ModelState.IsValid)
        {
            int id = await _employeeRepository.AddEmployeeAsync(employee);
            TempData["Success"] = "Employee Added SuccessFully!!";
            return RedirectToAction("Index");
        }
        TempData["Failed"] = "Failed during adding employee !!!";
        return View();
    }

    //Get
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id <= 0) {
            TempData["Failed"] = "No Employee Found with given data !!!";
            return RedirectToAction("Index");
        }

        var model = await _employeeRepository.GetEmployeeByIdAsync(id);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmployeeModel model)
    {
        if (ModelState.IsValid)
        {
            var employee = await _employeeRepository.EditEmployee(model);
            TempData["Success"] = "Employee details edited successfully!!";
            return RedirectToAction("Index");
        }
        TempData["Failed"] = "Employee edit details failed !!!";
        return View(model);
    }

    //Get
    public IActionResult Delete(int? id)
    {
        if (id == null || id <= 0)
        {
            TempData["Failed"] = "No Employee Found with given data !!!";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
