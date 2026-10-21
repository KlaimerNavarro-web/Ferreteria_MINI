using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capa_Entidad;
using Capa_Datos;
using Capa_Negocio; 
using Konscious.Security.Cryptography;
using System.Security.Cryptography; // Nuevo: Para el RandomNumberGenerator
using System.Text.RegularExpressions; // Nuevo: Para la validación del correo


namespace Aplicacion_web_2025_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Usuario_Controllers : ControllerBase
    {
        private readonly Usuario_Negocio _usuarioNegocio;
        // Constantes para el Hashing (Mejora la seguridad y mantenibilidad)
        private const int SaltSize = 16;
        private const int HashSize = 16;
        private const int Iterations = 4;
        private const int MemorySize = 1024;
        private const int DegreeOfParallelism = 2;

        public Usuario_Controllers(IConfiguration configuration)
        {
            // Omitimos la comprobación de null en la cadena de conexión ya que es responsabilidad de la configuración.
            string cadenaConexion = configuration.GetConnectionString("DefaultConnection")!;
            _usuarioNegocio = new Usuario_Negocio(cadenaConexion);
        }

        // --- MÉTODOS PÚBLICOS DEL API ---

        // ✅ POST: api/Usuario - Registrar
        [HttpPost]
        public IActionResult RegistrarUsuario([FromBody] Usuario usuario1)

        {
            if (usuario1 == null)
                return BadRequest(new { mensaje = "Datos del usuario son requeridos." });

            // ⚠ Validación de Formato de Correo Electrónico (CRÍTICO: No estaba)
            if (!ValidarEmail(usuario1.correo))
            {
                return BadRequest(new { mensaje = "El formato del correo electrónico es inválido." });
            }

            // 🔐 Validamos y Encriptamos contraseña
            /* if (!string.IsNullOrWhiteSpace(usuario1.contraseña))
                 return BadRequest(new { mensaje = "La contraseña es obligatoria." });

             usuario1.contraseña = HashearClave(usuario1.contraseña);*/



            int idGenerado = _usuarioNegocio.Registrar(usuario1, out string mensaje);

            if (idGenerado > 0)
                // 201 Created: Respuesta más semántica para una creación exitosa
                return CreatedAtAction(nameof(GetUsuarioById), new { id = idGenerado }, new { mensaje, id_usuario = idGenerado });
            else
                return BadRequest(new { mensaje });
        }

        // ✅ PUT: api/Usuario/{id} - Editar
        [HttpPut("{id}")]
        public IActionResult EditarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || id != usuario.id_usuario)
                return BadRequest(new { 
                    mensaje = "Datos inválidos o ID del cuerpo no coincide con el ID de la ruta." });

            // ⚠ Validación de Formato de Correo Electrónico
            if (!ValidarEmail(usuario.correo))
            {
                return BadRequest(new { mensaje = "El formato del correo electrónico es inválido." });
            }

          

            // 🔐 Si viene una contraseña nueva, la encriptamos.
            // Si la cadena está vacía/null, se asume que no se quiere cambiar la contraseña, 
            // y la capa de negocio DEBE preservar la contraseña anterior en la base de datos.
             if (!string.IsNullOrWhiteSpace(usuario.contraseña))
             {
                 usuario.contraseña = HashearClave(usuario.contraseña);
             }

            bool resultado = _usuarioNegocio.Editar(usuario, out string mensaje);

            if (resultado)
                // 204 No Content: Respuesta semántica para una actualización exitosa sin contenido de retorno
                return NoContent();
            else
                return BadRequest(new { mensaje });
        }

        // ... (otros métodos GET, DELETE se mantienen igual por ser correctos) ...

        // ✅ GET: api/Usuario
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            try
            {
                var lista = _usuarioNegocio.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener usuarios", error = ex.Message });
            }
        }

        // ✅ GET: api/Usuario/{id}
        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            try
            {
                // 💡 MEJORA: Es mejor tener un método ListarPorId en Capa_Negocio para evitar traer toda la lista
                var usuario = _usuarioNegocio.Listar().FirstOrDefault(u => u.id_usuario == id);
                if (usuario == null)
                    return NotFound(new { mensaje = "Usuario no encontrado" });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener usuario", error = ex.Message });
            }
        }

        // ✅ DELETE: api/Usuario/{id}
        [HttpDelete("{id}")]
      /*  public IActionResult EliminarUsuario(int id)
        {
            if (id <= 0)
                return BadRequest(new { mensaje = "ID inválido" });

            Usuario usuarios = new Usuario { id_usuario = id };
            bool resultado = _usuarioNegocio.Eliminar(usuarios, out string mensaje);

            if (resultado)
                return Ok(new { mensaje = "Usuario eliminado correctamente" });
            else
                return BadRequest(new { mensaje = "Error eliminar usuario", detalle = mensaje });
        }
      */
        // --- MÉTODOS PRIVADOS DE UTILIDAD ---

        // 🔐 MÉTODO PRIVADO: Hashear contraseña con Argon2id (Mejorado con constantes)

        private string HashearClave(string clave)
        {
            // Usamos RNGCryptoServiceProvider para asegurar que usamos el generador de números
            // aleatorios criptográficamente seguro y eliminamos el 'using var rng = ...'
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(clave))
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                Iterations = Iterations,
                MemorySize = MemorySize
            };

            var hashBytes = argon2.GetBytes(HashSize);
            // Formato: {Base64_Salt}.{Base64_Hash} para facilitar la verificación
            string hashFinal = $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hashBytes)}";

            return hashFinal;
        }

        // 📧 MÉTODO PRIVADO: Validación de formato de email (CRÍTICO: NO ESTABA)
        private bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            // Expresión regular estándar para la mayoría de los casos de email.
            // Es una simplificación, pero cubre los requisitos básicos de formato.
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
    

