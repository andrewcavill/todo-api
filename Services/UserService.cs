using System;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using TodoApi.IServices;

namespace TodoApi.Services
{
    public class UserService : IUserService
    {
        private readonly TodoContext _context;

        public UserService(TodoContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public List<User> GetByEmail(string email)
        {
            return _context.Users.Where(u => 
                string.Equals(u.Email, email, StringComparison.Ordinal)).ToList();
        }

        public void Create(User user)
        {
            int nextId = _context.Users.Max(x => (int)x.Id) + 1;
            user.Id = nextId;

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}