using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class ShowTime
    {
        public ShowTime()
        {
            CinemaMovieShowTimes = new HashSet<CinemaMovieShowTime>();
        }

        public int ShowTimeId { get; set; }
        public DateTime ShowDate { get; set; }
        public TimeSpan ShowTime1 { get; set; }

        public virtual ICollection<CinemaMovieShowTime> CinemaMovieShowTimes { get; set; }
    }
}
