using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class Movie
    {
        public Movie()
        {
            CinemaMovieShowTimes = new HashSet<CinemaMovieShowTime>();
            Ratings = new HashSet<Rating>();
        }

        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public string Disciption { get; set; }
        public byte[] ImageData { get; set; }

        public virtual ICollection<CinemaMovieShowTime> CinemaMovieShowTimes { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
