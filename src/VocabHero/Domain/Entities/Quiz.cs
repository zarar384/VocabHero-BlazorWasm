using VocabHero.Domain.Abstraction;

namespace VocabHero.Domain.Entities
{
    public class Quiz: Entity<int>
    {
        public string Title { get; set; }
        public ICollection<Word> Words { get; set; } = new List<Word>();

        public int UserId { get; set; }
        public User User{ get; set; }
    }
}
