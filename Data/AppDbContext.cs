using Microsoft.EntityFrameworkCore;
using BackEnd_student.Models;

namespace BackEnd_student.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
}