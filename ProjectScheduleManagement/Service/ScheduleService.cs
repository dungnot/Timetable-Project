using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.DTO.File;
using ProjectScheduleManagement.Models;

namespace ProjectScheduleManagement.Service
{
    public class ScheduleService
    {
        private ScheduleManagementContext _context;
        public Schedule schedule { get; set; }

        public ScheduleService(ScheduleManagementContext context)
        {
            _context = context;
        }

        public Schedule GetScheduleFromCSVData(CSVModelDTO data)
        {
            string[] roomParts = data.Room.Split('-');
            string buildingCode = roomParts[0];
            string roomCode = roomParts[1];
            Schedule schedule = new Schedule();
            schedule.TeacherId = _context.Teachers.FirstOrDefault(t => t.Code == data.Teacher).Id;

            schedule.RoomId = _context.Rooms.Include(r => r.Building)
                                             .FirstOrDefault(r => r.Building.Code == buildingCode && r.Code == roomCode).Id;

            schedule.SubjectId = _context.Subjects.FirstOrDefault(s => s.Code == data.Subject).Id;

            schedule.ClassId = _context.GrClasses.FirstOrDefault(c => c.Code == data.Class).Id;
            schedule.SlotId = _context.Slots.FirstOrDefault(s => s.SlotName == data.TimeSlot).Id;
            return schedule;
        }

        public void DeleteToDB(int id)
        {
            var schedule = _context.Schedules.FirstOrDefault(s => s.Id == id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                _context.SaveChanges();
            }
        }

        public string AddDataToDB(CSVModelDTO data)
        {
            int messageId = IsExist(data);
            if (messageId == 0)
            {

                Schedule newschedule = GetScheduleFromCSVData(data);
                string message = IsValid(newschedule, 0);
                if (message != "")
                {
                    return message;
                }
                _context.Schedules.Add(newschedule);
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

        public string EditDataToDatabase(CSVModelDTO data, int id)
        {
            int messageId = IsExist(data);
            if (messageId == 0)
            {

                Schedule newschedule = GetScheduleFromCSVData(data);
                string message = IsValid(newschedule, id);
                if (message != "")
                {
                    return message;
                }
                var oldschedule = _context.Schedules.FirstOrDefault(s => s.Id == id);
                oldschedule.ClassId = newschedule.ClassId;
                oldschedule.RoomId = newschedule.RoomId;
                oldschedule.SubjectId = newschedule.SubjectId;
                oldschedule.TeacherId = newschedule.TeacherId;
                oldschedule.SlotId = newschedule.SlotId;
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

        public string IsValid(Schedule schedule, int ScheduleId)
        {
            ValidationService validationService = new ValidationService(_context);
            return validationService.ValidateSchedule(schedule, ScheduleId);
        }

    }
}
