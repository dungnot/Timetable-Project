using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectScheduleManagement.DTO.File;
using ProjectScheduleManagement.Models;
using ProjectScheduleManagement.Service;

namespace ProjectScheduleManagement.Pages.File
{
    public class AddFileModel : PageModel
    {
        private readonly FileUploadService _fileUploadService;
        private readonly ScheduleManagementContext _context;

        public AddFileModel(FileUploadService fileUploadService, ScheduleManagementContext context)
        {
            _fileUploadService = fileUploadService;
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(IFormFile fileUpload)
        {
            if (fileUpload != null && fileUpload.Length > 0)
            {
                // Specify the folder where you want to save the file
                string folder = "Uploads";

                // Upload the file using the service method
                string filePath = _fileUploadService.UploadFile(fileUpload, folder);

                // Read data from CSV file
                List<CSVModelDTO> csvData = _fileUploadService.ReadDataScheduleFromFile(filePath);

                foreach (var item in csvData)
                {
           
                    GrClass grClass = _context.GrClasses.FirstOrDefault(c => c.Code == item.Class);
                    if (grClass == null)
                    {
                        grClass = new GrClass { Code = item.Class };
                        _context.GrClasses.Add(grClass);
                    }

                    
                    Models.Subject subject = _context.Subjects.FirstOrDefault(s => s.Code == item.Subject);
                    if (subject == null)
                    {
                        subject = new Models.Subject { Code = item.Subject };
                        _context.Subjects.Add(subject);
                    }

                   
                    Models.Room room = _context.Rooms.FirstOrDefault(r => r.Code == item.Room);
                    if (room == null)
                    {
                        room = new Models.Room { Code = item.Room };
                        _context.Rooms.Add(room);
                    }

                   
                    Models.Teacher teacher = _context.Teachers.FirstOrDefault(t => t.Code == item.Teacher);
                    if (teacher == null)
                    {
                        teacher = new Models.Teacher { Code = item.Teacher };
                        _context.Teachers.Add(teacher);
                    }

                   
                    Models.Slot slot = _context.Slots.FirstOrDefault(s => s.SlotName == item.TimeSlot);
                    if (slot == null)
                    {
                        slot = new Models.Slot { SlotName = item.TimeSlot };
                        _context.Slots.Add(slot);
                    }

                    await _context.SaveChangesAsync(); 


                    Schedule schedule = new Schedule
                    {
                        ClassId = grClass.Id,
                        SubjectId = subject.Id,
                        RoomId = room.Id,
                        TeacherId = teacher.Id,
                        SlotId = slot.Id,
                        Date = DateTime.Now // You may need to adjust this based on your CSV data
                    };

                    _context.Schedules.Add(schedule);
                    await _context.SaveChangesAsync();
                }

      
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError("fileUpload", "Please select a file to upload.");
                return Page();
            }
        }
    }
}
