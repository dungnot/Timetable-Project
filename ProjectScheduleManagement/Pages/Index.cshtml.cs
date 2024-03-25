using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectScheduleManagement.Models;
using ProjectScheduleManagement.Service;
using System.Xml;

namespace ProjectScheduleManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ScheduleManagementContext _context;
        private readonly ValidationService _validationService;
        public string[][] ScheduleData { get; set; }
        public List<Models.Schedule> Schedules { get; set; }

        [BindProperty]
        public List<GrClass> GrClasses { get; set; }

        [BindProperty]
        public string ClassCodeSelected { get; set; }

        [BindProperty]
        public Models.Schedule[,] Data { get; set; } = new Models.Schedule[4, 7];

        public IndexModel(ILogger<IndexModel> logger, ScheduleManagementContext context, ValidationService validationService)
        {
            _logger = logger;
            _context = context;
            _validationService = validationService;
        }

        public async Task OnGetAsync(string? ClassCodeSelected)
        {
            try
            {
                await GetDataAsync();
                //if(ClassCodeSelected == null)
                //{
                //    ClassCodeSelected = _context.GrClasses.FirstOrDefault().Code;
                //}
                List<Models.Schedule> schedules = ClassCodeSelected == null ? _context.Schedules.Include(s => s.Slot).Include(s => s.Teacher).Include(s => s.Subject).Include(s => s.Class).Include(s => s.Room).ThenInclude(r => r.Building).ToList()
                                                    : _context.Schedules.Include(s => s.Slot).Include(s => s.Teacher).Include(s => s.Subject).Include(s => s.Class).Include(s => s.Room).ThenInclude(r => r.Building).Where(x => x.Class.Code == ClassCodeSelected).ToList();
                foreach (var item in schedules)
                {
                    if (item.Slot.SlotName[0] == 'A')
                    {
                        int daySlot1 = Int32.Parse(item.Slot.SlotName[1].ToString());
                        int daySlot2 = Int32.Parse(item.Slot.SlotName[2].ToString());
                        Data[0, daySlot1 - 2] = item;
                        Data[1, daySlot2 - 2] = item;
                    }
                    else if (item.Slot.SlotName[0] == 'P')
                    {
                        int daySlot1 = Int32.Parse(item.Slot.SlotName[1].ToString());
                        int daySlot2 = Int32.Parse(item.Slot.SlotName[2].ToString());
                        Data[2, daySlot1 - 2] = item;
                        Data[3, daySlot2 - 2] = item;
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task GetDataAsync()
        {
            try
            {
                GrClasses = await _context.GrClasses.ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}

