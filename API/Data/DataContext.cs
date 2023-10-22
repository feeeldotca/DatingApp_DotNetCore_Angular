using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        // Tables in Database only related to AppUser class, not class of DTOs
        // it contains username, Id, passwordHash, passwordSalt
        public DbSet<AppUser> Users { get; set; } 
    }
}