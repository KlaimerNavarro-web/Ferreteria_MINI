using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_Datos
{


    public class Usuario_Datos
    {

        private readonly string _cadenaConexion;
        private static List<Usuario> usuarios = new List<Usuario>();

        public Usuario_Datos(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        // Obtener todos
        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return usuarios;
        }

        // Obtener por Id
        public Usuario? ObtenerUsuarioPorId(int id)
        {
            return usuarios.FirstOrDefault(u => u.id_usuario == id);
        }




        //Metodo que permite listar Todo los usuario que esta en la BD
        // Este metodo tiene que ser llamada por la capa negocio
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            //conectar ala BD por using
            using (SqlConnection oconexion = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    //consulta hacia los campo en la tabla Usuario
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.id_usuario,u.nombre,u.direccion,u.correo,u.contraseña,u.estado, r.id_rol, r.descripcion from Usuario u");
                    query.AppendLine("inner join Rol r on r.id_rol = u.id_rol");


                    // ejecutando los comando
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open(); // abriendo la cadena de conexion

                    using (SqlDataReader dr = cmd.ExecuteReader()) //para leer el resultado de nuestro comando 
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()//cada regitro lo alamacene en lista
                            {
                                id_usuario = Convert.ToInt32(dr["id_usuario"]),
                                nombre = dr["nombre"].ToString(),
                                direccion = dr["direccion"].ToString(),
                                correo = dr["correo"].ToString(),
                                contraseña = dr["contraseña"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                //  oRol = new Rol() { id_rol = Convert.ToInt32(dr["id_rol"]) , descripcion = dr["descripcion"].ToString() }

                            });

                        }


                    }
                }
                catch (Exception ex)
                {

                    lista = new List<Usuario>();// si hay un error sea una lista basia que no tenga elmentos y se pone como nueva

                }

            }

            return lista;
        }


        // Mandamos llamar al procedimiento almacenado SP_RESGISTRAR_USUARIO con sus parametros de entrada y salida 
        public int Registrar(Usuario obj, out string Mensaje)
        {
            int id_usuario_generado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_cadenaConexion))
                {



                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_USUARIO", oconexion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("contraseña", obj.contraseña);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.AddWithValue("id_rol", obj.oRol);
                    cmd.Parameters.Add("id_usuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    id_usuario_generado = Convert.ToInt32(cmd.Parameters["id_usuarioResultado"].Value);
                    Mensaje = "Agregado correctamente";
                    //Mensaje = cmd.Parameters[Mensaje="Registrado correctamente"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                id_usuario_generado = 0;
                Mensaje = ex.Message;

            }

            return id_usuario_generado;

        }



        //Mandamos a llamar el procedimiento almacenado SP_EDITAR_USUARIO con sus parametros de entrada y salida 

        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool repuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_cadenaConexion))
                {



                    SqlCommand cmd = new SqlCommand("SP_EDITAR_USUARIO", oconexion);
                    cmd.Parameters.AddWithValue("id_usuario", obj.id_usuario);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("contraseña", obj.contraseña);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.AddWithValue("id_rol", obj.oRol);
                    cmd.Parameters.Add("Repuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    repuesta = Convert.ToBoolean(cmd.Parameters["Repuesta"].Value);
                    Mensaje = "Actualizado correctamente";
                    // Mensaje = cmd.Parameters[Mensaje="Actualizado correctamente"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                repuesta = false;
                Mensaje = ex.Message;

            }

            return repuesta;

        }

        //Mandamos a llamar el procedimiento almacenado SP_ELIMINAR_USUARIO con sus parametros de entrada y salida

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            bool repuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_cadenaConexion))
                {




                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_USUARIO", oconexion);
                    cmd.Parameters.AddWithValue("id_usuario", obj.id_usuario);
                    cmd.Parameters.Add("Repuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    repuesta = Convert.ToBoolean(cmd.Parameters["Repuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                repuesta = false;
                Mensaje = ex.Message;

            }

            return repuesta;

        }

        public Usuario? Login(InicioRequest correo)
        {
            using (SqlConnection conn = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InicioUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@correo", correo.correo);
                    
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                id_usuario = Convert.ToInt32(reader["id_usuario"]),
                                contraseña = reader["contraseña"].ToString()!,
                                nombre = reader["nombre"].ToString()!,
                                direccion = reader["direccion"].ToString()!,
                                correo = reader["correo"].ToString()!,
                                estado = Convert.ToBoolean(reader["estado"]),
                                oRol = Convert.ToInt32(reader["id_rol"])
                                

                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}









