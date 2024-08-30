// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly backendContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(backendContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
    {
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Rol.NombreRol)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Email == registerModel.Email))
        {
            return BadRequest("El email ya está en uso.");
        }

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "User");
        if (role == null)
        {
            return BadRequest("Rol no encontrado.");
        }

        var user = new Usuario
        {
            Nombre = registerModel.Nombre,
            Apellido = registerModel.Apellido,
            Email = registerModel.Email,
            Password = registerModel.Password,
            Telefono = registerModel.Telefono,
            IdRol = role.IdRol
        };

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Register), new { id = user.IdUsuario }, user);
    }
}
