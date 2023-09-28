using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.DAL.Entities
{
    public class User : Entity
    {
        public string Name {  get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<Car> OwnedCars { get; set; }
        public List<Ride> RidesTaking { get; set; }
        public List<Ride> RidesOffering { get; set; }
    }
}
