using FirstAPI.Dao;
using FirstAPI.Models;

namespace FirstAPI.Services
{
    public interface IUserService
    {
        Task<User?> Register(UserDAO user);
    }
}
