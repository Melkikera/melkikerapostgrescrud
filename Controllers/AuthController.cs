using melkikerapostgrescrud.Data;
using melkikerapostgrescrud.Entities;
using melkikerapostgrescrud.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Threading.Tasks;

[ApiController]
[Route("api/login")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == login.Email);

        if (user == null)
        {
            return Unauthorized("Nom d'utilisateur incorrect.");
        }

        string decryptedPassword = "";
        byte[] encrypted = Convert.FromBase64String(user.Password); // 256 bits
        // Decrypt the bytes to a string.
        decryptedPassword = AesEncryption.DecryptStringFromBytes_Aes(user.Password,encrypted, Constantes.salt);

        if (decryptedPassword != login.Password) // Pour des raisons de sécurité, hasher le mot de passe !
        {
            return Unauthorized("Mot de passe incorrect.");
        }

        // Ici, on pourrait générer un JWT ou stocker une information de session
        return Ok(new LoginOkModel
        {
            Message = "Authentification réussie",
            Loggeduser = user.Name
        });
    }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginOkModel
{
    public string Message { get; set; }
    public string Loggeduser { get; set; }
}