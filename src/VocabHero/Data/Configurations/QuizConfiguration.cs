using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VocabHero.Domain.Entities;

namespace VocabHero.Data.Configurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q=>q.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany(u=>u.Quizzes)
                .HasForeignKey(q => q.UserId)
                .IsRequired();

            builder.HasMany(q => q.Words)
                .WithOne()
                .HasForeignKey(w => w.QuizId);
        }
    }
}
