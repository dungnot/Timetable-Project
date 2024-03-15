using CsvHelper;
using ProjectScheduleManagement.DTO.File;
using ProjectScheduleManagement.Mapping;
using System.Globalization;

namespace ProjectScheduleManagement.Service
{
    public class FileUploadService
    {
        private IWebHostEnvironment _environment;
        public FileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;

        }
        public string UploadFile(IFormFile fileUpload, string folder)
        {
            string pathDirectory = Path.Combine(_environment.ContentRootPath, folder);

            string pathDirectoryDebug = Path.Combine(Directory.GetCurrentDirectory(), folder);
            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }




            string fileNameSave = Path.GetFileNameWithoutExtension(fileUpload.FileName) + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss") + Path.GetExtension(fileUpload.FileName);


            var file = Path.Combine(pathDirectory, fileNameSave);

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                fileUpload.CopyTo(fileStream);
            }
            return file;
        }
        public List<CSVModelDTO> ReadDataScheduleFromFile(string path)
        {
            List<CSVModelDTO> listSchedule = new List<CSVModelDTO>();
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {

                        csv.Context.RegisterClassMap<CSVModelMapping>();
                        listSchedule = csv.GetRecords<CSVModelDTO>().ToList();
                    }
                }
                return listSchedule;
            }
            else
            {
                return null;
            }

        }

        public List<CSVModelDTO> ReadDataScheduleFromFile(Stream stream)
        {
            List<CSVModelDTO> listSchedule = new List<CSVModelDTO>();

            using (StreamReader reader = new StreamReader(stream))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {

                    csv.Context.RegisterClassMap<CSVModelMapping>();
                    listSchedule = csv.GetRecords<CSVModelDTO>().ToList();
                }
            }
            return listSchedule;

        }
    }
}
