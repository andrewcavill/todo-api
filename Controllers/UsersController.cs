using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TodoContext _context;

        public UsersController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<UserVm>> GetAllUsers()
        {
            var userVms = new List<UserVm>();

            foreach(var user in _context.Users)
            {
                userVms.Add(new UserVm{
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                });
            }

            return userVms;
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserVm> GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return new UserVm
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        [HttpPost]
        public IActionResult CreateUser(UserCreateVm userCreateVm)
        {
            int nextId = _context.Users.Max(x => (int)x.Id) + 1;

            var user = new User {
                Id = nextId,
                Email = userCreateVm.Email,
                Name = userCreateVm.Name
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserCreateVm userCreateVm)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = userCreateVm.Email;
            user.Name = userCreateVm.Name;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}