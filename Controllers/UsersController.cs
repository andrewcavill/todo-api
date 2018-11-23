using AutoMapper;
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
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<UserVm>> GetAll()
        {
            var users = _userService.GetAll();
            return _mapper.Map<List<UserVm>>(users);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserVm> GetUserById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            return _mapper.Map<UserVm>(user);
        }

        [HttpPost]
        public IActionResult CreateUser(UserCreateVm userCreateVm)
        {
            var user = _mapper.Map<User>(userCreateVm);
            _userService.Create(user);
            return CreatedAtRoute("GetUserById", new { id = user.Id }, user.Id);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserCreateVm userCreateVm)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            _mapper.Map(userCreateVm, user);
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