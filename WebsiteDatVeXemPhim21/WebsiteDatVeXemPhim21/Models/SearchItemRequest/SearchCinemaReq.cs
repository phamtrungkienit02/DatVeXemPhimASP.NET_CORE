using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteDatVeXemPhim21.Models
{
    public class SearchCinemaReq
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string Keyword { get; set; }
    }
}
