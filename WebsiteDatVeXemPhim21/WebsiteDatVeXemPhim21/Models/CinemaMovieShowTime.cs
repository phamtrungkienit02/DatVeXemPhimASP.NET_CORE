using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class CinemaMovieShowTime
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public int ShowTimeId { get; set; }

        public virtual Cinema Cinema { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ShowTime ShowTime { get; set; }
    }
}
