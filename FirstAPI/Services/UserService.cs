using FirstAPI.Dao;
using FirstAPI.Models;
using FirstAPI.Repositories;
using Mysqlx;
using System.Security.Cryptography.X509Certificates;

namespace FirstAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> Register(UserDAO user)
        {

            var existingUserByEmail = await _repository.GetByEmail(user.Email??"");

            if (existingUserByEmail != null)
            {
                throw new ArgumentException("Email already exists");
            }

            //Thực hiện thêm mới user
            return await _repository.Create(user); // Sử dụng await để chờ hoàn thành phương thức Create
        }
    }
}
