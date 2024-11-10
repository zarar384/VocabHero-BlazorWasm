using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VocabHero.Domain.Entities;

namespace VocabHero.Data.Configurations
{
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w=>w.Text)
                .HasMaxLength(33)
                .IsRequired();

            builder.Property(w => w.Definition).HasMaxLength(255);
            builder.HasOne<User>()
                .WithMany(u=>u.Words)
                .HasForeignKey(w => w.UserId);
        }
    }
}