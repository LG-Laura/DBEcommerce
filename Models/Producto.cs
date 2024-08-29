// Models/Producto.cs
public class Producto
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int IdCategoria { get; set; }
    public decimal Precio { get; set; }
    public decimal PrecioOferta { get; set; }
    public int Cantidad { get; set; }
    public string Imagen { get; set; }
    public DateTime FechaCreacion { get; set; }
    public Categoria Categoria { get; set; } // Navegación
}

