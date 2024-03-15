using System;
using System.Collections.Generic;

namespace ProjectScheduleManagement.Models
{
    public partial class GrClass
    {
        public GrClass()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
