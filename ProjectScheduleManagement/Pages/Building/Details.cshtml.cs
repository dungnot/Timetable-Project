﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.Models;

namespace ProjectScheduleManagement.Pages.Building
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectScheduleManagement.Models.ScheduleManagementContext _context;

        public DetailsModel(ProjectScheduleManagement.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

      public Models.Building Building { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Buildings == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings.FirstOrDefaultAsync(m => m.Id == id);
            if (building == null)
            {
                return NotFound();
            }
            else 
            {
                Building = building;
            }
            return Page();
        }
    }
}
