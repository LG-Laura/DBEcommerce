// Models/DetalleVenta.cs
public class DetalleVenta
{
    public int IdDetalleVenta { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public int IdProducto { get; set; } // Foreign Key
    public Producto Producto { get; set; } // Navigation property
    public int IdVenta { get; set; } // Foreign Key
    public Venta Venta { get; set; } // Navigation property
}

