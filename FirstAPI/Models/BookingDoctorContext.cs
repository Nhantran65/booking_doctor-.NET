using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;

public class BookingDoctorContext : DbContext
{
    public BookingDoctorContext(DbContextOptions<BookingDoctorContext> options)
        : base(options)
    {
        Users = Set<User>(); // Khởi tạo Users trong hàm tạo
    }

    // DbSet cho các đối tượng trong cơ sở dữ liệu
    public DbSet<User> Users { get; set; } // Khởi tạo hoặc làm cho Users có thể là null
}
