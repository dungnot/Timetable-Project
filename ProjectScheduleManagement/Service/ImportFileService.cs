using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.DTO.File;
using ProjectScheduleManagement.Models;

namespace ProjectScheduleManagement.Service
{
    public class ImportFileService
    {
        private ScheduleManagementContext _context;

        public ImportFileService(ScheduleManagementContext context)
        {
            _context = context;
        }

        public int IsExist(CSVModelDTO data)
        {
            string[] roomParts = data.Room.Split('-');
            string buildingCode = roomParts[0];
            string roomCode = roomParts[1];
            if (_context.Teachers.FirstOrDefault(t => t.Code == data.Teacher) == null) return 1;
            if (_context.Subjects.FirstOrDefault(t => t.Code == data.Subject) == null) return 2;
            if (_context.GrClasses.FirstOrDefault(t => t.Code == data.Class) == null) return 3;
            if (_context.Buildings.FirstOrDefault(t => t.Code == buildingCode) == null) return 4;
            if (_context.Rooms.Include(t => t.Building).FirstOrDefault(t => t.Building.Code == buildingCode
            && t.Code == roomCode) == null) return 4;
            if (_context.Slots.FirstOrDefault(t => t.SlotName == data.TimeSlot) == null) return 5;

            return 0;
        }

        private Schedule GetDatafromCSVFile(CSVModelDTO data)
        {
            string[] roomParts = data.Room.Split('-');
            string buildingCode = roomParts[0];
            string roomCode = roomParts[1];

            Schedule Schedule = new Schedule();
           

            Schedule.TeacherId = _context.Teachers.FirstOrDefault(t => t.Code == data.Teacher).Id;
            Schedule.RoomId = _context.Rooms.Include(r => r.Building)
                                             .FirstOrDefault(r => r.Building.Code == buildingCode && r.Code == roomCode).Id;

            Schedule.SubjectId = _context.Subjects.FirstOrDefault(s => s.Code == data.Subject).Id;
            Schedule.ClassId = _context.GrClasses.FirstOrDefault(c => c.Code == data.Class).Id;
            Schedule.SlotId = _context.Slots.FirstOrDefault(s => s.SlotName == data.TimeSlot).Id;

            return Schedule;
           
        }


        public void SaveToDb(List<Schedule> schedules)
        {
            foreach (Schedule s in schedules)
            {
                _context.Add(s);
            }
            _context.SaveChanges();
        }

        public string AddDataToDatabase(CSVModelDTO data)
        {
            int messageId = IsExist(data);
            if (messageId == 0)
            {

                Schedule scheduleDTO = GetDatafromCSVFile(data);
                string message = IsValid(scheduleDTO);
                if (message != "")
                {
                    return message;
                }
                    _context.Schedules.Add(scheduleDTO);
                    _context.SaveChanges();

            }
            else
            {
                if (messageId == 1)
                {
                    return "Teacher code do not exists";
                }
                else if (messageId == 2)
                {
                    return "Subject code do not exists";
                }
                else if (messageId == 3)
                {
                    return "Class code do not exists";
                }
                else if (messageId == 4)
                {
                    return "Room code do not exists";
                }
                else if (messageId == 5)
                {
                    return "Wrong slot name!";
                }
            }


            return "Saved successfully!";
        }

        public string IsValid(Schedule schedule)
        {
            ValidationService validationService = new ValidationService(_context);
            return validationService.ValidateSchedule(schedule, 0);
        }

      

    }
}
