using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Models
{
    public class RideAddModel
    {
        public DateTime StartTime { get; set; }
        public int DurationInMinute { get; set; }
        public String StartLocation { get; set; }
        public String EndLocation { get; set; }
        public int UsedCarId { get; set; }
        public int DriverId { get; set; }
    }
}
