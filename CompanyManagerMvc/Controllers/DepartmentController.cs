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
        public async Task <IActionResult> Index()
        {
            var departments=await service.GetAllDepartmentAsync();
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
