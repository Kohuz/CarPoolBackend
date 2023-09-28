using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Models
{
    public class CarAddModel
    {
        public string RegistrationNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public int NumberOfSeats { get; set; }
        public int OwnerId { get; set; }

    }
}
