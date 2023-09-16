using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public int SeatId { get; set; }
        public int ShowTimeId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }

        public virtual Cinema Cinema { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Order Order { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual CinemaMovieShowTime ShowTime { get; set; }
    }
}
