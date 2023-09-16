using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteDatVeXemPhim21.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public double Rating1 { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
