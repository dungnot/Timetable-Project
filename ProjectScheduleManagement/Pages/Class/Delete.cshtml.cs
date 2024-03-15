using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.Models;

namespace ProjectScheduleManagement.Pages.Class
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectScheduleManagement.Models.ScheduleManagementContext _context;

        public DeleteModel(ProjectScheduleManagement.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
      public GrClass GrClass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GrClasses == null)
            {
                return NotFound();
            }

            var grclass = await _context.GrClasses.FirstOrDefaultAsync(m => m.Id == id);

            if (grclass == null)
            {
                return NotFound();
            }
            else 
            {
                GrClass = grclass;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.GrClasses == null)
            {
                return NotFound();
            }
            var grclass = await _context.GrClasses.FindAsync(id);

            if (grclass != null)
            {
                GrClass = grclass;
                _context.GrClasses.Remove(GrClass);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
