namespace Aplicacion_web_2025_V1.DTOs
{
    public class PromedioProductoClienteDto
    {
        public string Cliente { get; set; } = "";
        public string ClienteNombre { get; set; } = "";
        public string Producto { get; set; } = "";
        public string ProductoNombre { get; set; } = "";
        public decimal[] Semanas { get; set; } = Array.Empty<decimal>();
        public decimal Promedio { get; set; }
    }
}