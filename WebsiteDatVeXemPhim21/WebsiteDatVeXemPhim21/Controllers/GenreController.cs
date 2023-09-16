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
    public class GenreController : ControllerBase
    {
       
            CinemaDBContext da = new CinemaDBContext();
            [HttpGet("get all genres")]
            public IActionResult GetAllGenres()
            {
                var ds = da.Genres.ToList();
                return Ok(ds);
            }

            [HttpGet("get genres by ID")]
            public IActionResult GetGenreById(int id)
            {
                var ds = da.Genres.FirstOrDefault(s => s.GenreId == id);
                return Ok(ds);
            }

            [HttpPost("add new genre")]
            public void AddGenre([FromBody] Genre genre)
            {
                using (var tran = da.Database.BeginTransaction())
                {
                    try
                    {
                        da.Genres.Add(genre);
                        da.SaveChanges();

                        tran.Commit();
                    }
                    catch (Exception)
                    {

                        tran.Rollback();
                    }
                }
            }

            [HttpPut("edit a genre")]
            public void EditGenre([FromBody] Genre genre)
            {
                using (var tran = da.Database.BeginTransaction())
                {
                    try
                    {
                        Genre g = da.Genres.FirstOrDefault(s => s.GenreId == genre.GenreId);
                        g.GenreName = genre.GenreName;
                        g.Disciption = genre.Disciption;

                        da.Genres.Add(g);
                        da.SaveChanges();

                        tran.Commit();
                    }
                    catch (Exception)
                    {

                        tran.Rollback();
                    }
                }
            }

            [HttpDelete("delete a genre")]
            public void DeleteGenre(int id)
            {
                using (var tran = da.Database.BeginTransaction())
                {
                    try
                    {
                        Genre g = da.Genres.FirstOrDefault(s => s.GenreId == id);

                        da.Genres.Remove(g);
                        da.SaveChanges();

                        tran.Commit();
                    }
                    catch (Exception)
                    {

                        tran.Rollback();
                    }
                }
            }
        }
    
}
