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
        private ImportFileService _importFileService;
        public AddFileModel(FileUploadService fileUploadService, ScheduleManagementContext context, ImportFileService importFile)
        {
            _fileUploadService = fileUploadService;
            _context = context;
            _importFileService = importFile;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(IFormFile fileUpload)
        {

            if (fileUpload != null && fileUpload.Length > 0)
            {
               
                string folder = "Uploads";
                string filePath = _fileUploadService.UploadFile(fileUpload, folder);
                List<CSVModelDTO> records = _fileUploadService.ReadDataScheduleFromFile(filePath);
                List<string> messages = new List<string>();
                for (int i = 0; i < records.Count(); i++)
                {
                    string message = _importFileService.AddDataToDatabase(records[i]);
                    messages.Add(message);
                }

                ViewData["Messages"] = messages;
            }
                return RedirectToPage("/Index");
            }
  
    }
}
