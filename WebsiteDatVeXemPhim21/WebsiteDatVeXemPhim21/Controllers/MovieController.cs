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
    public class MovieController : ControllerBase
    {
        CinemaDBContext da = new CinemaDBContext();
        [HttpGet("get all movies")]
        public IActionResult GetAllMovies()
        {
            var ds = da.Movies.ToList();
            return Ok(ds);
        }

        [HttpGet("get movie by ID")]
        public IActionResult GetMovieById(int id)
        {
            var ds = da.Movies.FirstOrDefault(s => s.MovieId == id);
            return Ok(ds);
        }

        [HttpPost("add new movie")]
        public void AddMovie([FromBody] NewMovie movie)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {

                    Movie m = new Movie();
                    m.MovieName = movie.MovieName;
                    m.Year = movie.Year;
                    m.Price = movie.Price;
                    m.Disciption = movie.Disciption;

                    da.Movies.Add(m);
                    da.SaveChanges();

                    tran.Commit();
                }
                catch (Exception)
                {

                    tran.Rollback();
                }
            }
        }

        [HttpPut("edit a movie")]
        public void EditMovie([FromBody] NewMovie movie)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    Movie m = da.Movies.FirstOrDefault(s => s.MovieId == movie.MovieId);
                    m.MovieName = movie.MovieName;
                    m.Year = movie.Year;
                    m.Price = movie.Price;
                    m.Disciption = movie.Disciption;

                    da.Movies.Add(m);
                    da.SaveChanges();

                    tran.Commit();
                }
                catch (Exception)
                {

                    tran.Rollback();
                }
            }
        }

        [HttpDelete("delete a movie")]
        public void DeleteMovie(int id)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    Movie m = da.Movies.FirstOrDefault(s => s.MovieId == id);

                    da.Movies.Remove(m);
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
        private object SearchMovies(SearchMovieReq searchMovieReq)
        {
            //Lay ds sp theo keyword
            var movies = da.Movies.Where(x => x.MovieName.Contains(searchMovieReq.Keyword));
            // xu li phan trang
            var offset = (searchMovieReq.Page - 1) * searchMovieReq.Size;
            var total = movies.Count();
            int totalPage = (total % searchMovieReq.Size) == 0 ? (int)(total / searchMovieReq.Size) :
                (int)(1 + (total / searchMovieReq.Size));

            //skip=> dua vao trang 2 thi hien thi tu trang 2 bo qua trang 1
            var data = movies.OrderBy(x => x.MovieId).Skip(offset).Take(searchMovieReq.Size).ToList();

            var res = new
            {
                Data = data,
                //so phim
                TotalRecord = total,
                //so trang
                TotalPages = totalPage,
                Page = searchMovieReq.Page,
                Size = searchMovieReq.Size,
            };
            return res;

        }

        [HttpPost("search movies")]
        public IActionResult SearchMovie([FromBody] SearchMovieReq searchMovieReq )
        {
            var ds = SearchMovies(searchMovieReq);
            return Ok(ds);
        }
    }
}
