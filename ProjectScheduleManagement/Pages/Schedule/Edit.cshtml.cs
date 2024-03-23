using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.Models;

namespace ProjectScheduleManagement.Pages.Schedule
{
    public class EditModel : PageModel
    {
        private readonly ScheduleManagementContext _context;

        public EditModel(ScheduleManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Schedule Schedule { get; set; }
        public List<Models.Room> Rooms { get; set; }
        public List<Models.Subject> Subjects { get; set; }
        public List<Models.Teacher> Teachers { get; set; }
        public List<Models.GrClass> Classes { get; set; }
        public List<Models.Slot> Slots { get; set; }

        public void OnGet(int id)
        {
            GetData(id);
        }

        private void GetData(int id)
        {
            Schedule = _context.Schedules
                .Include(s => s.Class)
                .Include(s => s.Room)
                .Include(s => s.Teacher)
                .Include(s => s.Subject)
                .Include(s => s.Slot)
                .FirstOrDefault(s => s.Id == id);

            Rooms = _context.Rooms.ToList();
            Subjects = _context.Subjects.ToList();
            Teachers = _context.Teachers.ToList();
            Classes = _context.GrClasses.ToList();
            Slots = _context.Slots.ToList();
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Schedule);
                    _context.SaveChanges();
                    ViewData["Messages"] = "Saved successfully!";
                }
                catch (DbUpdateException ex)
                {
                    ViewData["Messages"] = ex.Message;
                }
            }
            else
            {
                ViewData["Messages"] = "Please fix the errors and try again.";
            }
        }

    }
}

