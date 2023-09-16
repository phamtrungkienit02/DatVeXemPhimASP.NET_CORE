using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
