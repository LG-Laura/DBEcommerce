using Microsoft.EntityFrameworkCore;

public class backendContext : DbContext
{
    public backendContext(DbContextOptions<backendContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetalleVentas { get; set; }
}

