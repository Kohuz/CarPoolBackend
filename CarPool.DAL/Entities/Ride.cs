using CarPool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.DAL.Entities
{
    public class Ride : IEntity
    {
        public DateTime StartTime { get; set; }
        public int DurationInMinute { get; set; }
        public String StartLocation { get; set; }
        public String EndLocation { get; set; }
        public List<ApplicationUser> Passengers { get; set; }
        public Car UsedCar { get; set; }
        public Guid UsedCarId { get; set; }
        public ApplicationUser Driver { get; set; }
        public Guid DriverId { get; set; }
        public Guid Id {get; set; }
    }
}
