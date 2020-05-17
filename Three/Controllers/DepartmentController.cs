using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Three.Models;
using Three.Services;

namespace Three.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IOptions<ThreeOptions> _threeOption;

        public DepartmentController(IDepartmentService departmentService,IOptions<ThreeOptions> threeOption)
        {
            _departmentService = departmentService;
            _threeOption = threeOption;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Department Index";
            var departments = await _departmentService.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "Add Department";
            return View(new Department());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Add(model);
            }

            return RedirectToAction(nameof(Index));
        }



    }
}