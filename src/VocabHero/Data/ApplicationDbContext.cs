using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VocabHero.Domain.Entities;

namespace VocabHero.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
    }
}
