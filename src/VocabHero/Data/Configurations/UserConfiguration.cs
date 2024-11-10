using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VocabHero.Domain.Entities;

namespace VocabHero.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u=>u.Id);
            builder.HasOne(u => u.Statistic)
                .WithOne(s => s.User)
                .HasForeignKey<Statistic>(s => s.UserId);

            builder.HasMany(u=>u.Words)
                .WithOne()
                .HasForeignKey(w => w.UserId);

            builder.HasMany(u => u.Quizzes)
               .WithOne()
               .HasForeignKey(q => q.UserId);
        }
    }
}
