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

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }

        private int _totalItem, _pageSize, _startIndex;
        public int TotalPage { get; set; }

        public IndexModel(ProjectScheduleManagement.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

        public List<Models.Schedule> schedules { get; set; } = default!;

        public void OnGet()
        {
           schedules = _context.Schedules.OrderByDescending(s => s.Id).Include(s => s.Class).Include(s => s.Subject).Include(s => s.Teacher).Include(s => s.Slot).Include(s => s.Room).ThenInclude(r => r.Building).ToList();
            Paging();
        }

        public void Paging()
        {
            if (PageIndex < 1) PageIndex = 1;
            _totalItem = schedules.Count();
            _pageSize = 5;
            TotalPage = (int)Math.Ceiling((double)_totalItem / _pageSize);

            if (TotalPage > 0)
            {
                if (PageIndex > TotalPage) PageIndex = TotalPage;
                schedules = schedules.Skip((PageIndex - 1) * _pageSize)
                    .Take(_pageSize)
                    .ToList();
            }
        }

        public void OnPostDelete()
        {
            ScheduleService deleteService = new ScheduleService(_context);
            deleteService.DeleteToDB(ScheduleId);
            schedules = _context.Schedules.OrderByDescending(s => s.Id).Include(s => s.Class).Include(s => s.Subject).Include(s => s.Teacher).Include(s => s.Slot).Include(s => s.Room).ThenInclude(r => r.Building).ToList();
            Paging();
            Response.Redirect("/Schedule/Index");
        }
    }
}
