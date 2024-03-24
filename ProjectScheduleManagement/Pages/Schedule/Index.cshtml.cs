using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.DTO;
using ProjectScheduleManagement.DTO.File;
using ProjectScheduleManagement.Mapping;
using ProjectScheduleManagement.Models;
using ProjectScheduleManagement.Service;

namespace ProjectScheduleManagement.Pages.Schedule
{
    public class IndexModel : PageModel
    {
        private readonly ProjectScheduleManagement.Models.ScheduleManagementContext _context;
        [BindProperty]
        public CSVModelDTO CSVData { get; set; }
        [BindProperty]
        public int ScheduleId { get; set; }
        public Models.Schedule Schedule { get; set; }
        public List<RoomDTO> Rooms { get; set; }
        public List<Models.Subject> Subjects { get; set; }
        public List<Models.Teacher> Teachers { get; set; }
        public List<Models.GrClass> Classes { get; set; }
        public List<Models.Slot> Slots { get; set; }
        public IndexModel(ProjectScheduleManagement.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

        public List<Models.Schedule> schedules { get; set; } = default!;

        public void OnGet(int id)
        {
            if (_context.Schedules != null)
            {
                schedules = _context.Schedules.Include(s => s.Class).Include(s => s.Subject).Include(s => s.Teacher).Include(s => s.Slot).Include(s => s.Room).ThenInclude(r => r.Building).ToList();
            }
 
        }

      
        public void OnPost()
        {
            ScheduleService deleteService = new ScheduleService(_context);
            deleteService.DeleteToDB(ScheduleId);
            Response.Redirect("/Schedule/Index");
        }
    }
}
