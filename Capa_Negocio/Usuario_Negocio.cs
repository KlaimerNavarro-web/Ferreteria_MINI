using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;
using Konscious.Security.Cryptography;

namespace Capa_Negocio
{
    public class Usuario_Negocio
    {

        private readonly Usuario_Datos _usuarioDatos;
        public Usuario_Negocio(string cadenaConexion) => _usuarioDatos = new Usuario_Datos(cadenaConexion);

        // private readonly Usuario_Datos objcd_usuario = new Usuario_Datos();

        public List<Usuario> Listar()
        {
            return _usuarioDatos.Listar(); // retornando la lista que se encuentra en la capa datos
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            //creacion de validaciones 
            Mensaje = string.Empty;

            if (obj.nombre == "")
            {
                Mensaje += "Es necesario el nombres del Usuario\n";

            }

            if (obj.direccion == "")
            {
                Mensaje += "Es necesario la direccion del Usuario\n";

            }

            if (obj.correo == "")
            {
                Mensaje += "Es necesario el correo del Usuario\n";

            }

            if (obj.contraseña == "")
            {
                Mensaje += "Es necesario la contraseña del Usuario\n";

            }

            if (Mensaje != string.Empty) //si nuestro mensaje es diferente  va ser igual a 0 pero si no es dieferente o igual su valor llama al procedimiento
            {
                return 0;
            }
            else
            {
                return _usuarioDatos.Registrar(obj, out Mensaje); // retornando la lista que se encuentra en la capa datos
            }


        }

        public bool Editar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.nombre == "")
            {
                Mensaje += "Es necesario el nombres del Usuario\n";

            }

            if (obj.direccion == "")
            {
                Mensaje += "Es necesario la direccion del Usuario\n";

            }

            if (obj.correo == "")
            {
                Mensaje += "Es necesario el correo del Usuario\n";

            }

            if (obj.contraseña == "")
            {
                Mensaje += "Es necesario la contraseña del Usuario\n";

            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return _usuarioDatos.Editar(obj, out Mensaje); // retornando la lista que se encuentra en la capa datos
            }



        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return _usuarioDatos.Eliminar(obj, out Mensaje); // retornando la lista que se encuentra en la capa datos
        }

        public Usuario? Login(string correo, string contraseña)
        {
            var inicioRequest = new InicioRequest { correo = correo, contraseña = contraseña };
            var usuario = _usuarioDatos.Login(inicioRequest);
            if (usuario == null) return null;

            // Verificar contraseña
            if (VerificarClave(contraseña, usuario.contraseña))
                return usuario;

            return usuario;
        }

        private byte[] GenerarSalt(int tamaño = 16)
        {
            byte[] salt = new byte[tamaño];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private string HashearClave(string contraseña, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(contraseña))
            {
                Salt = salt,
                DegreeOfParallelism = 2,
                Iterations = 4,
                MemorySize = 1024
            };

            var hashBytes = argon2.GetBytes(16);
            return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hashBytes);
        }

        private bool VerificarClave(string contraseña, string hash)
        {
            var partes = hash.Split('.');
            if (partes.Length != 2) return false;

            var salt = Convert.FromBase64String(partes[0]);
            var hashOriginal = partes[1];
            var hashNuevo = HashearClave(contraseña, salt).Split('.')[1];

            return hashNuevo == hashOriginal;
        }
    }
}

