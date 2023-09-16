using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class Cinema
    {
        public Cinema()
        {
            CinemaMovieShowTimes = new HashSet<CinemaMovieShowTime>();
            Seats = new HashSet<Seat>();
        }

        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string Address { get; set; }
        public byte[] ImageData { get; set; }

        public virtual ICollection<CinemaMovieShowTime> CinemaMovieShowTimes { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
