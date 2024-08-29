// Models/Venta.cs
public class Venta
{
    public int IdVenta { get; set; }
    public int IdUsuario { get; set; }
    public decimal Total { get; set; }
    public DateTime FechaCreacion { get; set; }
    public Usuario Usuario { get; set; } // Navegación
}

