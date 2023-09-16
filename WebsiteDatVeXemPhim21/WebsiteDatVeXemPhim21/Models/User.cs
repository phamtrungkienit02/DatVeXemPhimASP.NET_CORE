using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class User
    {
        public User()
        {
            Ratings = new HashSet<Rating>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Active { get; set; }
        public string UserRole { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
