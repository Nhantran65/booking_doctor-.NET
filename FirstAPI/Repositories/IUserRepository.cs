using FirstAPI.Dao;
using FirstAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);

        Task<User?> GetByFirstName(string FirstName);

        Task<User?> GetByEmail(string email);

        Task<User?> Create(UserDAO user);

        Task<string> Login(LoginDAO login);

    }
}
