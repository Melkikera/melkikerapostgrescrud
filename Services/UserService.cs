using melkikerapostgrescrud.DTOs;
using melkikerapostgrescrud.Entities;
using melkikerapostgrescrud.Repositories;

namespace melkikerapostgrescrud.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; // Repository instance for database operations

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository; // Injecting the repository via constructor
        }

        // Retrieves all Users, converts them to DTOs, and returns the list
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var Users = await _userRepository.GetAllAsync(); // Fetch all Users from repository

            // Convert each User entity into a UserResponseDto and return the list
            return Users.Select(p => new UserResponseDto
            {
                Id = p.Id,
                Username=p.Username,
                Name = p.Name,
                Email = p.Email
            });
        }

        // Retrieves a User by ID and converts it to a DTO
        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            var User = await _userRepository.GetByIdAsync(id); // Fetch User by ID

            // If the User is not found, throw an exception
            if (User == null)
                throw new KeyNotFoundException("User not found");

            // Convert entity to DTO and return it
            return new UserResponseDto
            {
                Id = User.Id,
                Username = User.Username,
                Name = User.Name,
                Email = User.Email
            };
        }

        // Adds a new User using a request DTO
        public async Task AddUserAsync(UserRequestDto UserDto)
        {
            // Convert DTO to entity
            var user = new User
            {
                Name = UserDto.Name,
                Username = UserDto.Username,
                Email = UserDto.Email,
                Password=UserDto.Password
            };

            // Add the new User to the database
            await _userRepository.AddAsync(user);
        }

        // Updates an existing User with new data
        public async Task UpdateUserAsync(int id, UserRequestDto UserDto)
        {
            var User = await _userRepository.GetByIdAsync(id); // Fetch the User by ID

            // If the User does not exist, throw an exception
            if (User == null)
                throw new KeyNotFoundException("User not found");

            // Update User fields with new values from DTO
            User.Username = UserDto.Username;
            User.Name = UserDto.Name;
            User.Email = UserDto.Email;
            User.Password = UserDto.Password;

            // Save the updated User in the database
            await _userRepository.UpdateAsync(User);
        }

        // Deletes a User by ID
        public async Task DeleteUserAsync(int id)
        {
            var User = await _userRepository.GetByIdAsync(id); // Fetch the User by ID

            // If the User does not exist, throw an exception
            if (User == null)
                throw new KeyNotFoundException("User not found");

            // Delete the User from the database
            await _userRepository.DeleteAsync(id);
        }
    }
}
