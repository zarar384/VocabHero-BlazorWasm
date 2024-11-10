using VocabHero.Domain.Abstraction;

namespace VocabHero.Domain.Entities
{
    public class Statistic : Entity<int>
    {
        public int TotalWordsLearned { get; set; }
        public int TotalQuizzesTaken { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
