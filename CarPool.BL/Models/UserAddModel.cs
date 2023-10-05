using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Models
{
    public class UserAddModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public string Email {  get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
