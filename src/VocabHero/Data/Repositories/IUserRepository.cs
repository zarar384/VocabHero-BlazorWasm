using VocabHero.Domain.Entities;

namespace VocabHero.Data.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUserNameAsync(string userName);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
