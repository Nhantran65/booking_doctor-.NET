using FirstAPI.Dao;
using FirstAPI.Models;

namespace FirstAPI.Services
{
    public interface IUserService
    {
        Task<User?> Register(UserDAO user);
        // JWT string
        Task<string> Login(LoginDAO login);
    }
}
