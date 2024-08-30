// Models/Producto.cs
public class Producto
{
    public int IdProducto { get; set; }
    public string NombreProducto { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public decimal PrecioOferta { get; set; }
    public int Stock { get; set; }
    public string Imagen { get; set; }
    public DateTime FechaDeCreacion { get; set; }
    public DateTime? FechaDeBaja { get; set; } // Puede ser nulo si no está dado de baja
    public int IdCategoria { get; set; } // Foreign Key
    public Categoria Categoria { get; set; } // Navigation property
}

