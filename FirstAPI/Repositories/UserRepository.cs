using FirstAPI.Dao;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient; // Import MySQL Connector
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace FirstAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookingDoctorContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(BookingDoctorContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<User?> Create(UserDAO user)
        {
            try
            {
                User? newUser = new User();

                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.Email = user.Email;
                newUser.PasswordHash = newUser.PasswordHash = HashPassword(user.PasswordHash);
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

        public async Task<string> Login(LoginDAO login)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

                if (user != null && VerifyPassword(login.PasswordHash, user.PasswordHash))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? "");
                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        }),
                        Expires = DateTime.UtcNow.AddDays(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    };
                    var token = tokenHandler.CreateToken(tokenDescription);
                    var jwtToken = tokenHandler.WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    throw new ArgumentException("Wrong email or password");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine(ex.Message);
                throw;
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
