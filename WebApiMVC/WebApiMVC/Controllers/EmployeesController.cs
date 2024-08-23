using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiMVC.Models;

namespace WebApiMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return View(employees);
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                return View(new List<Crud>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return View(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(new Crud());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Crud crud)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployeeAsync(crud);
                return RedirectToAction(nameof(Index));
            }
            return View(crud);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return View(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(new Crud());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Crud crud)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.UpdateEmployeeAsync(crud);
                return RedirectToAction(nameof(Index));
            }
            return View(crud);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return View(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(new Crud());
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
