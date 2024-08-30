// Controllers/OrdenesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class VentasController : ControllerBase
{
    private readonly backendContext _context;

    public VentasController(backendContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
    {
        return await _context.Ventas.Include(o => o.Detalles).ThenInclude(d => d.Producto).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Venta>> GetOrden(int id)
    {
        var venta = await _context.Ventas.Include(o => o.Detalles).ThenInclude(d => d.Producto).FirstOrDefaultAsync(o => o.IdVenta == id);
        if (venta == null)
        {
            return NotFound();
        }

        return venta;
    }

    [HttpPost]
    public async Task<ActionResult<Venta>> CreateVenta([FromBody] Venta venta)
    {
        _context.Ventas.Add(venta);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrden), new { id = venta.IdVenta }, venta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVenta(int id, [FromBody] Venta orden)
    {
        if (id != orden.IdVenta)
        {
            return BadRequest();
        }

        _context.Entry(orden).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Ventas.Any(e => e.IdVenta == id))
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
    public async Task<IActionResult> DeleteVenta(int id)
    {
        var venta = await _context.Ventas.FindAsync(id);
        if (venta == null)
        {
            return NotFound();
        }

        _context.Ventas.Remove(venta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

