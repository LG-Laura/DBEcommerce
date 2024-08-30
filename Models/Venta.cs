// Models/Venta.cs
public class Venta
{
    public int IdVenta { get; set; }
    public DateTime FechaCreacion { get; set; }
    public decimal Total { get; set; }
    public int IdUsuario { get; set; } // Foreign Key
    public Usuario Usuario { get; set; } // Navigation property
    public List<DetalleVenta> Detalles { get; set; }
}

