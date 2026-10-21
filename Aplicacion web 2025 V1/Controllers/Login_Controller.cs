using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Capa_Entidad;
using Capa_Datos;
using Capa_Negocio;
using Microsoft.AspNetCore.Identity.Data;

namespace API_Ferreteria.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class Login_Controller : ControllerBase
    {
        private readonly Usuario_Negocio _usuarioNegocio;
        private readonly IConfiguration _config;

        public Login_Controller(IConfiguration config)
        {
            _config = config;

            // Conexión a SQL
            _usuarioNegocio = new Usuario_Negocio(
                config.GetConnectionString("DefaultConnection")
            );
        }

       
        // IniciarSesion
    

        [HttpPost("IniciarSesion")]
        public IActionResult IniciarSesion([FromBody] Capa_Entidad.InicioRequest request)
        {
            if (string.Equals(request.correo, "string", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(request.contraseña, "string", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Datos de Login no proporcionados, por favor llene todos los campos");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (request == null || string.IsNullOrWhiteSpace(request.correo) || string.IsNullOrWhiteSpace(request.contraseña))
                return BadRequest("correo y contraseña son requeridos.");

            var usuario = _usuarioNegocio.Login(request.correo, request.contraseña);
            if (usuario == null) return Unauthorized(new { error = "Credenciales inválidas." });

            var token = GenerarToken(usuario);
            return Ok(new { token });
        }
        
        
        
        
        
        
        /* [HttpPost("IniciarSesion")]
         public IActionResult IniciarSesion([FromBody] InicioRequest request)
         {
             if (request == null ||
                 string.IsNullOrWhiteSpace(request.correo) ||
                 string.IsNullOrWhiteSpace(request.contraseña))
             {
                 return BadRequest("Debe proporcionar correo y contraseña.");
             }

             var usuario = _usuarioNegocio.Login(request.correo, request.contraseña);

             if (usuario == null)
                 return Unauthorized(new { error = "Credenciales inválidas." });

             string token = GenerarToken(usuario);

             return Ok(new
             {
                 mensaje = "Login exitoso",
                 token,
                 usuario
             });
         }*/


        // =============================
        // MÉTODO PARA GENERAR TOKEN JWT
        // =============================
        private string GenerarToken(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            // Claims (información dentro del token)
            var claims = new[]
            {
                new Claim("id_usuario", usuario.id_usuario.ToString()),
                new Claim("nombre", usuario.nombre ?? "Usuario"),
                new Claim("contraseña", usuario.contraseña ?? "Usuario"),
                new Claim("correo", usuario.correo ?? "Usuario"),
                new Claim("id_rol", usuario.oRol.ToString())
            };

            // Obtener claves desde appsettings.json
            var keyString = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("No está configurada la llave JWT (Jwt:Key)");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear token
            var jwtToken = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"] ?? "JosueEstrada",
                audience: _config["Jwt:Audience"] ?? "JosueUsuarios",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            // Convertir a string
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}

/*using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Capa_Entidad;


namespace API_Ferreteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login_Controller : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public Login_Controller(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        // ============================
        //  POST: api/login
        // ============================
        [HttpPost]
        public IActionResult Login([FromBody] Usuario userLogin)
        {
            Usuario? usuarioValido = ValidarUsuario(userLogin.correo, userLogin.contraseña, userLogin.oRol);

            if (usuarioValido == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            string token = GenerarToken(usuarioValido);

            return Ok(new
            {
                mensaje = "Login exitoso",
                token = token,
                usuario = usuarioValido
            });
        }


        // ====================================
        //  VALIDAR USUARIO EN SQL SERVER
        // ====================================
        private Usuario? ValidarUsuario(string correo, string password, int oRol)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string query = @"SELECT id_usuario, nombre, direccion, correo, contraseña, estado, fecha_registro, oRol
                                 FROM Usuario
                                 WHERE correo = @correo AND contraseña = @password";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@id_rol", oRol);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new Usuario
                    {
                        id_usuario = Convert.ToInt32(dr["id_usuario"]),
                        nombre = dr["nombre"].ToString(),
                        direccion = dr["direccion"].ToString(),
                        correo = dr["correo"].ToString(),
                        contraseña = dr["contraseña"].ToString(),
                        estado = Convert.ToBoolean(dr["estado"]),
                        fecha_registro = dr["fecha_registro"].ToString(),
                        oRol = Convert.ToInt32(dr["oRol"])
                    };
                }
            }

            return null;
        }


        // ============================
        //  GENERAR TOKEN JWT
        // ============================
        private string GenerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JMEC:Key"])
            );

            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id_usuario", usuario.id_usuario.ToString()),
                new Claim("nombre", usuario.nombre),
                new Claim("correo", usuario.correo),
                new Claim("rol", usuario.oRol.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["JMEC:Issuer"],
                audience: _config["JMEC:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}*/
