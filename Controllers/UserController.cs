using melkikerapostgrescrud.DTOs;
using melkikerapostgrescrud.Services;
using melkikerapostgrescrud.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace melkikerapostgrescrud.Controllers
{
    [ApiController] // Specifies that this is an API controller
    [Route("api/users")] // Defines the route as 'api/user'
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; // Service instance for business logic

        public UserController(IUserService userService)
        {
            _userService = userService; // Injecting the service via constructor
        }

        // Handles HTTP GET request to fetch all Users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Users = await _userService.GetAllUsersAsync(); // Calls service to get all Users
            return Ok(Users); // Returns 200 OK response with User data
        }

        // Handles HTTP GET request to fetch a single User by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var User = await _userService.GetUserByIdAsync(id); // Calls service to fetch User by ID
                return Ok(User); // Returns 200 OK response if found
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Returns 404 Not Found if User does not exist
            }
        }

        // Handles HTTP POST request to add a new User
        [HttpPost]
        public async Task<IActionResult> Add(UserRequestDto userDto)
        {

            // Encrypt the string to an array of bytes.
            string ciphertext = AesEncryption.EncryptStringToBytes_Aes(userDto.Password, Constantes.salt);

            userDto.Password = ciphertext;
            

            await _userService.AddUserAsync(userDto); // Calls service to add a new User
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
            // Returns 201 Created response with location header pointing to the new User
        }

        // Handles HTTP PUT request to update an existing User
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserRequestDto userDto)
        {
            try
            {
                string ciphertext = AesEncryption.EncryptStringToBytes_Aes(userDto.Password, Constantes.salt);

                userDto.Password = ciphertext;

                await _userService.UpdateUserAsync(id, userDto); // Calls service to update User
                return NoContent(); // Returns 204 No Content response on success
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Returns 404 Not Found if User does not exist
            }
        }

        // Handles HTTP DELETE request to delete a User by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id); // Calls service to delete User
                return NoContent(); // Returns 204 No Content response on success
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Returns 404 Not Found if User does not exist
            }
        }
    }
}
