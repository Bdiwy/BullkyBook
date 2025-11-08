using BullkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<Category> Catagories { get; set; }
        
    }
}