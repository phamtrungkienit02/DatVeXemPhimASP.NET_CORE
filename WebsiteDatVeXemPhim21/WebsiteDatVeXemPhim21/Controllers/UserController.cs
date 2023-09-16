using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class UserController : ControllerBase
    {
        CinemaDBContext da = new CinemaDBContext();
        [HttpGet("get all user")]
        public IActionResult GetAllUser()
        {
            var ds = da.Users.Select(
                u => new { u.UserId, u.FirstName, u.LastName, u.UserName, u.Email, u.PhoneNumber})
                .ToList();
             return Ok(ds);
            
        }

        [HttpGet("get user by ID")]
        public IActionResult GetUserById(int id)
        {
            var ds = da.Users.Where(u => u.UserId == id)
                            .Select(u =>  new { u.UserId, u.FirstName, u.LastName, u.UserName, u.Email, u.PhoneNumber })
                            .FirstOrDefault();
            return Ok(ds);
        }

        [HttpPost("add new user")]
        public void AddUser([FromBody] NewUser user)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {

                    User u = new User();
                    u.FirstName = user.FirstName;
                    u.LastName = user.LastName;
        
                    u.Email = user.Email;
                    u.PhoneNumber = user.PhoneNumber;
                    u.Email = user.Email;

                    da.Users.Add(u);
                    da.SaveChanges();

                    tran.Commit();
                }
                catch (Exception)
                {

                    tran.Rollback();
                }
            }
        }

        [HttpPut("edit a user")]
        public void EditUser([FromBody] NewUser user)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    User u = da.Users.FirstOrDefault(s => s.UserId == user.UserId);
                    u.FirstName = user.FirstName;
                    u.LastName = user.LastName;
            
                    u.Email = user.Email;
                    u.PhoneNumber = user.PhoneNumber;
                    u.Email = user.Email;

                    da.Users.Add(u);
                    da.SaveChanges();

                    tran.Commit();
                }
                catch (Exception)
                {

                    tran.Rollback();
                }
            }
        }

        [HttpDelete("delete a user")]
        public void DeleteUser(int id)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    User u = da.Users.FirstOrDefault(s => s.UserId == id);

                    da.Users.Remove(u);
                    da.SaveChanges();

                    tran.Commit();
                }
                catch (Exception)
                {

                    tran.Rollback();
                }
            }
        }

        private object SearchUsers(SearchUserReq searchUserReq)
        {
            // tim từ khóa  lấy  dss
            var users = da.Users.Where(x => x.LastName.Contains(searchUserReq.Keyword));
            // xu li phân trang
            var offset = (searchUserReq.Page - 1) * searchUserReq.Size;
            var total = users.Count();
            int totalPage = (total % searchUserReq.Size) == 0 ? (int)(total / searchUserReq.Size) :
                (int)(1 + (total / searchUserReq.Size));

            var data = users.OrderBy(x => x.UserId).Skip(offset).Take(searchUserReq.Size).ToList();

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPage,
                Page = searchUserReq.Page,
                Size = searchUserReq.Size,
            };
            return res;

        }

        [HttpPost("Search  users")]
        public IActionResult SearchUser([FromBody] SearchUserReq searchUserReq)
        {

            var ds = SearchUsers(searchUserReq);
            return Ok(ds);
        }


        [HttpPost("Check user by username")]
        public Boolean CheckUsername(String username)
        {
            var user = da.Users.FirstOrDefault(s => s.UserName == username);
            if (user != null)
            {
                // Nếu tìm thấy username, trả về true
                return true;
            }
            else
            {
                // Nếu không tìm thấy username, trả về false
                return false;
            }
        }

        //[HttpPost("Login")]
        //public IActionResult Login([FromBody] SearchUserReq searchUserReq)
        //{

        //    var ds = SearchUsers(searchUserReq);
        //    return Ok(ds);
        //}
    }
}
