using System;
using System.Collections.Generic;
using System.Text;

namespace DroneDelivery.Models
{
    class Schedule
    {
        public Drone drone { get; set; }
        public List<List<Location>> locations { get; set; }
    }
}
