using melkikerapostgrescrud.Data;
using melkikerapostgrescrud.Entities;
using Microsoft.EntityFrameworkCore;

namespace melkikerapostgrescrud.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly ApplicationDbContext _context; // Database context for interacting with the database

        public UserRepository(ApplicationDbContext context)
        {
            _context = context; // Injecting database context via constructor
        }

        // Retrieves all products from the database
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            // Converts the Products table into a list and returns it asynchronously
            return await _context.Users.ToListAsync();
        }

        // Retrieves a product by its ID
        public async Task<User> GetByIdAsync(int id)
        {
            // Uses FindAsync to search for a product by its primary key (ID)
            return await _context.Users.FindAsync(id);
        }

        // Adds a new product to the database
        public async Task AddAsync(User user)
        {
            // Adds the product entity to the database context
            await _context.Users.AddAsync(user);

            // Saves the changes to the database asynchronously
            await _context.SaveChangesAsync();
        }

        // Updates an existing product in the database
        public async Task UpdateAsync(User user)
        {
            // Marks the product entity as updated in the database context
            _context.Users.Update(user);

            // Saves the updated product data to the database asynchronously
            await _context.SaveChangesAsync();
        }

        // Deletes a product by its ID
        public async Task DeleteAsync(int id)
        {
            // Finds the product in the database using the provided ID
            var product = await _context.Users.FindAsync(id);

            // If the product exists, remove it from the database
            if (product != null)
            {
                _context.Users.Remove(product);

                // Saves the changes to the database asynchronously
                await _context.SaveChangesAsync();
            }
        }
    }
}
