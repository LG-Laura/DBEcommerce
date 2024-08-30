// Models/Usuario.cs
public class Usuario
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IdRol { get; set; } // Relación con la tabla Roles
    public Rol Rol { get; set; }  // Navegación
}

