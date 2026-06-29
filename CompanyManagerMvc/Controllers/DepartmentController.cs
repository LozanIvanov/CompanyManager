using CompanyManager.Application.Services;
using CompanyManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyManagerMvc.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentService service;
        public DepartmentController(DepartmentService service)
        {
            this.service = service;
        }
        public async Task <IActionResult> Index(string? searchText)
        {
            List<Department> departments;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                departments = await service.GetAllDepartmentAsync();
              
            }
            else
            {
                departments=await service.SearchByNameAsync(searchText);
                  
            }
            ViewBag.SearchText = searchText;
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            await service.AddDepartmentAsync(department);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task <IActionResult>Edit(int id)
        {
            var department=await service.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            await service.UpdateDepartmentAsync(department);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteDepartmentAsync(id);

            return RedirectToAction("Index");
        }

    }
}
