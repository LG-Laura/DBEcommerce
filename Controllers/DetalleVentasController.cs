// Controllers/DetallesOrdenController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class DetallesOrdenController : ControllerBase
{
    private readonly backendContext _context;

    public DetallesOrdenController(backendContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetDetallesVenta()
    {
        return await _context.DetallesVenta.Include(d => d.Producto).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleVenta>> GetDetalleVenta(int id)
    {
        var detalleVenta = await _context.DetallesVenta.Include(d => d.Producto).FirstOrDefaultAsync(d => d.IdDetalleVenta == id);
        if (detalleVenta == null)
        {
            return NotFound();
        }

        return detalleVenta;
    }

    [HttpPost]
    public async Task<ActionResult<DetalleVenta>> CreateDetalleVenta([FromBody] DetalleVenta detalleVenta)
    {
        _context.DetallesVenta.Add(detalleVenta);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDetalleVenta), new { id = detalleVenta.IdDetalleVenta }, detalleVenta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDetalleOrden(int id, [FromBody] DetalleVenta detalleVenta)
    {
        if (id != detalleVenta.IdDetalleVenta)
        {
            return BadRequest();
        }

        _context.Entry(detalleVenta).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.DetallesVenta.Any(e => e.IdDetalleVenta == id))
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
    public async Task<IActionResult> DeleteDetalleVenta(int id)
    {
        var detalleVenta = await _context.DetallesVenta.FindAsync(id);
        if (detalleVenta == null)
        {
            return NotFound();
        }

        _context.DetallesVenta.Remove(detalleVenta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

