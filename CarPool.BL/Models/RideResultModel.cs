using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Models
{
    public class RideResultModel
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationInMinute { get; set; }
        public String StartLocation { get; set; }
        public String EndLocation { get; set; }
        public List<UserDetailModel> Passengers { get; set; }
        public CarDetailModel UsedCar { get; set; }
        public UserDetailModel Driver { get; set; }
    }
}
