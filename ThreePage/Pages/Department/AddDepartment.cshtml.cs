using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Three.Models;
using Three.Services;

namespace ThreePage.Pages.Department
{
    public class AddDepartmentModel : PageModel
    {
        private readonly IDepartmentService _departmentService;

        [BindProperty]
        public Three.Models.Department Department { get; set; }
       

        public AddDepartmentModel(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        //public void OnGet()
        //{
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _departmentService.Add(Department);
            return RedirectToPage("/Index");
        }

    }
}
