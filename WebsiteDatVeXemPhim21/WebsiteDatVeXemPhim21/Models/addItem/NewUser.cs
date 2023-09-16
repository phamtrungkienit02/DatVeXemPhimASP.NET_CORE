using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteDatVeXemPhim21.Models
{
    public class NewUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string UserName { get; set; }
        //public string Pass { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public string UserRole { get; set; }
    }
}
