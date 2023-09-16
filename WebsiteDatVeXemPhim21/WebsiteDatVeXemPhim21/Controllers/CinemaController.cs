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
    public class CinemaController : ControllerBase
    {
        CinemaDBContext da = new CinemaDBContext();
        [HttpGet("get all cinemas")]
        public IActionResult GetAllCinemas()
        {
            var ds = da.Cinemas.ToList();
            return Ok(ds);
        }

        [HttpGet("get cinema by ID")]
        public IActionResult GetCinemaById(int id)
        {
            var ds = da.Cinemas.FirstOrDefault(s => s.CinemaId == id);
            return Ok(ds);
        }

        [HttpPost("add new cinema")]
        public void AddCinema([FromBody] NewCinema cinema)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    Cinema c = new Cinema();
                    c.CinemaName = cinema.CinemaName;
                    c.Address = cinema.Address;

                    da.Cinemas.Add(c);
                    da.SaveChanges();

                    tran.Commit();
                }
                catch (Exception)
                {

                    tran.Rollback();
                } 
            }
        }

        [HttpPut("edit a cinema")]
        public void EditCinema([FromBody] NewCinema cinema)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    Cinema c = da.Cinemas.FirstOrDefault(s => s.CinemaId == cinema.CinemaId);
                    c.CinemaName = cinema.CinemaName;
                    c.Address = cinema.Address;

                    da.Cinemas.Update(c);
                    da.SaveChanges();

                    tran.Commit();
                }
                catch (Exception)
                {

                    tran.Rollback();
                }
            }
        }

        [HttpDelete("delete a cinema")]
        public void DeleteCinema(int id)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    Cinema c = da.Cinemas.FirstOrDefault(s => s.CinemaId == id);

                    da.Cinemas.Remove(c);
                    da.SaveChanges();

                    tran.Commit();
                }
                catch (Exception)
                {

                    tran.Rollback();
                }
            }
        }

        //xử lý phân trang
        private object SearchCinemas(SearchCinemaReq searchCinemaReq)
        {
            //Lay ds sp theo keyword
            var cinemas = da.Cinemas.Where(x => x.CinemaName.Contains(searchCinemaReq.Keyword));
            // xu li phan trang
            var offset = (searchCinemaReq.Page - 1) * searchCinemaReq.Size;
            var total = cinemas.Count();
            int totalPage = (total % searchCinemaReq.Size) == 0 ? (int)(total / searchCinemaReq.Size) :
                (int)(1 + (total / searchCinemaReq.Size));

            //skip=> dua vao trang 2 thi hien thi tu trang 2 bo qua trang 1
            var data = cinemas.OrderBy(x => x.CinemaId).Skip(offset).Take(searchCinemaReq.Size).ToList();

            var res = new
            {
                Data = data,
                //so phim
                TotalRecord = total,
                //so trang
                TotalPages = totalPage,
                Page = searchCinemaReq.Page,
                Size = searchCinemaReq.Size,
            };
            return res;

        }

        //xử lý tim kiem theo ten rap co phân trang
        private object SearchCinemaAddress(SearchCinemaReq searchCinemaReq)
        {
            //Lay ds sp theo keyword
            var cinemas = da.Cinemas.Where(x => x.Address.Contains(searchCinemaReq.Keyword));
            // xu li phan trang
            var offset = (searchCinemaReq.Page - 1) * searchCinemaReq.Size;
            var total = cinemas.Count();
            int totalPage = (total % searchCinemaReq.Size) == 0 ? (int)(total / searchCinemaReq.Size) :
                (int)(1 + (total / searchCinemaReq.Size));

            //skip=> dua vao trang 2 thi hien thi tu trang 2 bo qua trang 1
            var data = cinemas.OrderBy(x => x.CinemaId).Skip(offset).Take(searchCinemaReq.Size).ToList();

            var res = new
            {
                Data = data,
                //so phim
                TotalRecord = total,
                //so trang
                TotalPages = totalPage,
                Page = searchCinemaReq.Page,
                Size = searchCinemaReq.Size,
            };
            return res;

        }

        [HttpPost("search cinemas by name")]
        public IActionResult SearchCinemaByName([FromBody] SearchCinemaReq searchCinemaReq)
        {
            var ds = SearchCinemas(searchCinemaReq);
            return Ok(ds);
        }

        [HttpPost("search cinemas by address")]
        public IActionResult SearchCinemaByAddress([FromBody] SearchCinemaReq searchCinemaReq)
        {
            var ds = SearchCinemaAddress(searchCinemaReq);
            return Ok(ds);
        }

    }
}
