using CarPool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.DAL.Entities
{
    public class Car : IEntity
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public int NumberOfSeats { get; set; }
        public ApplicationUser Owner { get; set; }
        public Guid OwnerId { get; set; }
    }
}
