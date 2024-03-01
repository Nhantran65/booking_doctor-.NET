using FirstAPI.Dao;
using FirstAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace FirstAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookingDoctorContext _context;

        public UserRepository(BookingDoctorContext context)
        {
            _context = context;
        }


        public async Task<User?> Create(UserDAO user)
        {
            try
            {
                User? newUser = new User();

                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.Email = user.Email;
                newUser.PasswordHash = newUser.PasswordHash = HashPassword(user.Password); ;
                newUser.ProfilePicture = user.ProfilePicture;
                newUser.Bio = user.Bio;

                // Thêm người dùng mới vào cơ sở dữ liệu
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return newUser;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private string? HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<User?> GetByEmail(string email) // Thay đổi kiểu trả về thành nullable
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByFirstName(string FirstName) // Thay đổi kiểu trả về thành nullable
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.FirstName == FirstName);
        }

        public async Task<User?> GetById(int id) // Thay đổi kiểu trả về thành nullable
        {
            return await _context.Users.FindAsync(id); 
        }


    }
}
