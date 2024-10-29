using VocabHero.Domain.Abstraction;

namespace VocabHero.Domain.Entities
{
    public class Statistics : Entity<int>
    {
        public int TotalWordsLearned { get; set; }
        public int TotalQuizzesTaken { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
