using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using TodoApi.IServices;
using TodoApi.ViewModels;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<UserVm>> GetAll()
        {
            var userVms = new List<UserVm>();
            foreach(var user in _userService.GetAll())
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
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
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
            var user = new User {
                Email = userCreateVm.Email,
                Name = userCreateVm.Name
            };
            _userService.Create(user);
            return CreatedAtRoute("GetUserById", new { id = user.Id }, user.Id);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserCreateVm userCreateVm)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            user.Email = userCreateVm.Email;
            user.Name = userCreateVm.Name;
            _userService.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            _userService.Delete(user);
            return NoContent();
        }
    }
}