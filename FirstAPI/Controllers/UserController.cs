using Microsoft.AspNetCore.Mvc;
using FirstAPI.Models;
using FirstAPI.Services;
using System;
using System.Threading.Tasks;
using FirstAPI.Dao;


namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //https://localhost:8082/api/user/register

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDAO userDAO)
        {
            try
            {
                // Gọi phương thức Register trong IUserService để xử lý đăng ký người dùng
                User? user = await _userService.Register(userDAO);

                // Trả về dữ liệu người dùng đã đăng ký thành công
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
