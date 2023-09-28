using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.DAL.Entities
{
    public class Car : Entity
    {
        public string RegistrationNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public int NumberOfSeats { get; set; }
        public User Owner { get; set; }
        public int OwnerId { get; set; }
    }
}
