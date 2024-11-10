using Microsoft.AspNetCore.Identity;
using VocabHero.Domain.Abstraction;

namespace VocabHero.Domain.Entities
{
    public class User : IdentityUser<int>, IEntity<int>
    {
        public ICollection<Word> Words { get; set; } = new List<Word>();
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }

        public int? StatisticId { get; set; }
        public Statistic Statistic { get; set; } = new Statistic();
    }
}
