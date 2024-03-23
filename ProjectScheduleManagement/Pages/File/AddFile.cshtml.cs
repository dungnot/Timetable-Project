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
               
                if (!fileUpload.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    
                    ViewData["Messages"] = "File format is not correct, please import csv file.";
                    ViewData["MessageType"] = "alert-danger";
                    return Page();
                }

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
                ViewData["MessageType"] = messages.Any(msg => msg.Contains("successfully")) ? "alert-success" : "alert-danger";
            }
            else
            {
                ViewData["Messages"] = "Please select the file to upload.";
                ViewData["MessageType"] = "alert-danger";
                return Page();
            }
                return Page();
            }
  
    }
}
