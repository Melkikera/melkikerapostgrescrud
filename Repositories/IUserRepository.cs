using melkikerapostgrescrud.Entities;

namespace melkikerapostgrescrud.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User product);
        Task DeleteAsync(int id);
    }
}
