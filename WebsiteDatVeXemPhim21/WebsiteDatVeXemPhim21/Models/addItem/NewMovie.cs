using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteDatVeXemPhim21.Models
{
    public class NewMovie
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public string Disciption { get; set; }
    }
}
