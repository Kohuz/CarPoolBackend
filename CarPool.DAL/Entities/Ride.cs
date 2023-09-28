using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.DAL.Entities
{
    public class Ride : Entity
    {
        public DateTime StartTime { get; set; }
        public int DurationInMinute { get; set; }
        public String StartLocation { get; set; }
        public String EndLocation { get; set; }
        public List<User> Passengers { get; set; }
        public Car UsedCar { get; set; }
        public int UsedCarId { get; set; }
        public User Driver { get; set; }
        public int DriverId { get; set; }

    }
}
