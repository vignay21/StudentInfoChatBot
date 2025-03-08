using Microsoft.EntityFrameworkCore;
using StudentInfoChatBot.Models;

namespace StudentInfoChatBot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<StudentQuery> StudentQueries { get; set; }


        public DbSet<AdminUser> AdminUsers { get; set; }
    }
}
