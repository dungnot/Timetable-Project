using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.Models;

namespace ProjectScheduleManagement.Pages.Schedule
{
    public class IndexModel : PageModel
    {
        private readonly ProjectScheduleManagement.Models.ScheduleManagementContext _context;

        public IndexModel(ProjectScheduleManagement.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

        public IList<GrClass> GrClass { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.GrClasses != null)
            {
                GrClass = await _context.GrClasses.ToListAsync();
            }
        }
    }
}