using Microsoft.EntityFrameworkCore;
using VocabHero.Domain.Entities;

namespace VocabHero.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = await GetByIdAsync(user.Id);

            if (existingUser is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            //existingUser.Email = user.Email;
            //existingUser.UserName = user.UserName;
            //existingUser.PhoneNumber = user.PhoneNumber;
            //existingUser.Words = user.Words;
            //existingUser.Quizzes = user.Quizzes;
            //existingUser.PasswordHash = user.PasswordHash;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
    }
}
