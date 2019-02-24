using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.IServices
{
    public interface IUserService
    {
        List<User> GetAll();

        User Get(int id);

        List<User> GetByEmail(string email);

        void Create(User user);

        void Update(User user);

        void Delete(User user);
    }
}