using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.DTO;
using ProjectScheduleManagement.Models;

namespace ProjectScheduleManagement.Mapping
{
    public class RoomMapper
    {
        private readonly ScheduleManagementContext _context;
        public RoomMapper(ScheduleManagementContext context)
        {
            _context = context;
        }

        public List<RoomDTO> ToRoomDTO()
        {
            List<Room> rooms = _context.Rooms.Include(r => r.Building).ToList();
            List<RoomDTO> results = new List<RoomDTO>();
            foreach (Room room in rooms)
            {
                results.Add(new RoomDTO
                {
                    room = $"{room.Building.Code}-{room.Code}"
                });
            }

            return results;
        }
    }
}
