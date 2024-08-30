// Controllers/RolesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly backendContext _context;

    public RolesController(backendContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
    {
        return await _context.Roles.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Rol>> GetRol(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol == null)
        {
            return NotFound();
        }

        return rol;
    }

    [HttpPost]
    public async Task<ActionResult<Rol>> CreateRol([FromBody] Rol rol)
    {
        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRol), new { id = rol.IdRol }, rol);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRol(int id, [FromBody] Rol rol)
    {
        if (id != rol.IdRol)
        {
            return BadRequest();
        }

        _context.Entry(rol).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Roles.Any(e => e.IdRol == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRol(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol == null)
        {
            return NotFound();
        }

        _context.Roles.Remove(rol);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

