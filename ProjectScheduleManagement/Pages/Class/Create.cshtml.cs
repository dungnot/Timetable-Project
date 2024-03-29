﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectScheduleManagement.Models;

namespace ProjectScheduleManagement.Pages.Class
{
    public class CreateModel : PageModel
    {
        private readonly ProjectScheduleManagement.Models.ScheduleManagementContext _context;

        public CreateModel(ProjectScheduleManagement.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public GrClass GrClass { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.GrClasses == null || GrClass == null)
            {
                return Page();
            }

            _context.GrClasses.Add(GrClass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
