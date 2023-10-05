using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Models
{
    public class CarDetailModel
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
