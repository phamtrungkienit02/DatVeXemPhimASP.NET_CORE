using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class Seat
    {
        public int SeatId { get; set; }
        public int CinemaId { get; set; }
        public string SeatNumber { get; set; }

        public virtual Cinema Cinema { get; set; }
    }
}
