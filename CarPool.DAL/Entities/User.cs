using CarPool.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CarPool.DAL.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public string Name {  get; set; }
        public string Surname { get; set; }
        public List<Car> OwnedCars { get; set; }
        public List<Ride> RidesTaking { get; set; }
        public List<Ride> RidesOffering { get; set; }

    }
}
