using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VocabHero.Domain.Entities;

namespace VocabHero.Data.Configurations
{
    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            builder.HasKey(s=>s.Id);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(w => w.UserId);
        }
    }
}
