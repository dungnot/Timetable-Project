﻿using System;
using System.Collections.Generic;

namespace ProjectScheduleManagement.Models
{
    public partial class Room
    {
        public Room()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public int? BuildingId { get; set; }

        public virtual Building? Building { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
