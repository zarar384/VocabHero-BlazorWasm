using VocabHero.Domain.Abstraction;

namespace VocabHero.Domain.Entities
{
    public class Word : Entity<int>
    {
        public string Text { get; set; }
        public string? Definition { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int? QuizId { get; set; }
        public Quiz Quiz { get; set; } = new Quiz();
    }
}
