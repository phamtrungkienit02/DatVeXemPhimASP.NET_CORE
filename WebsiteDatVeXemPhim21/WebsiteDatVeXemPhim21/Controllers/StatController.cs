using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteDatVeXemPhim21.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteDatVeXemPhim21.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        CinemaDBContext da = new CinemaDBContext();

        //Thong ke so luong don hang cua tung khach hang
        [HttpPost("Order by user")]
        public IActionResult calOrderByUser()
        {
            var ds = da.Orders.GroupBy(u => u.UserId).Select(s => new { s.Key, sldh = s.Count() });

            return Ok(ds);
        }
        //thong ke doanh so ban duoc theo nam nhap vao
        [HttpPost("cal total by year")]
        public IActionResult calTotalByYear(int year)
        {
            var ds = da.Orders.Where(s => s.BookingDate.Year == year)
                          .Join(da.OrderDetails, d => d.OrderId, o => o.OrderId, (d, o) =>
                           new { year = d.BookingDate.Year, tTien = o.Quantity * o.UnitPrice })
                          .GroupBy(s => s.year).Select(g => new { g.Key, total = g.Sum(s => s.tTien)});
            return Ok(ds);
        }
        //thong ke don hang duoc tung rap
        [HttpPost("total Order of Cinema by year")]
        public IActionResult calTotalOrderByYear(int year)
        {
            var ds = da.Orders.Where(o => o.BookingDate.Year == year)
                    .Join(da.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new { o, od })
                    .Join(da.Cinemas, od => od.od.CinemaId, c => c.CinemaId, (od, c) => new { CinemaName = c.CinemaName, Year = od.o.BookingDate.Year, Total = od.od.Quantity * od.od.UnitPrice })
                    .GroupBy(x => new { x.CinemaName, x.Year })
                    .Select(g => new { cinemaName = g.Key.CinemaName, year = g.Key.Year, totalOrders = g.Count(), TotalSales = g.Sum(x => x.Total) });
            return Ok(ds);
        }
        //thong ke doanh so  theo phim
        [HttpPost("total Order of Movie by year")]
        public IActionResult totalOrderByYear(int year)
        {
            var ds = da.Orders.Where(o => o.BookingDate.Year == year)
                   .Join(da.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new { o, od })
                   .Join(da.Movies, od => od.od.MovieId, c => c.MovieId, (od, c) => new { movieName = c.MovieName, Year = od.o.BookingDate.Year, Total = od.od.Quantity * od.od.UnitPrice })
                   .GroupBy(x => new { x.movieName, x.Year })
                   .Select(g => new { moviename = g.Key.movieName, year = g.Key.Year, totalOrders = g.Count(), TotalSales = g.Sum(x => x.Total) });
            return Ok(ds);
        }
        //thong ke diem danh gia cua tung phim
        [HttpGet("total Rating of Movie")]
        public IActionResult ratingMovie()
        {
            var ds = da.Ratings
                    .Join(da.Movies, r => r.MovieId, m => m.MovieId, (r, m) => new { MovieId = r.MovieId, MovieName = m.MovieName, Rate = r.Rating1 })
                     .GroupBy(rm => new { rm.MovieId, rm.MovieName })
                    .Select(g => new {g.Key.MovieId,g.Key.MovieName, AverageRating = Math.Round(g.Average(rm => rm.Rate), 2)});
            return Ok(ds);
        }
    }
}
