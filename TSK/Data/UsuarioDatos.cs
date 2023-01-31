using TSK.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace TSK.Data
{
    public class UsuarioDatos
    {
        public List<Usuario> ListaUsuario()
        {   var _usuario = new List<Usuario>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("SP_Usuario_listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {   while (dr.Read())
                    {   _usuario.Add(new Usuario()
                        {
                            idUsuario = Convert.ToInt32(dr["ID_USR"]),
                            idPos = Convert.ToInt32(dr["ID_POS"]),
                            Nombre = dr["NOMBRE"].ToString(),
                            UserName = dr["USUARIO"].ToString(),
                            Clave = dr["CONTRASENA"].ToString(),
                            Posiciones = new string[] { dr["NOMBRE_POS"].ToString() }
                        });
                    }
                }
            }
            return _usuario;

        }

        public Usuario ValidarUsuario(string _correo, string _clave)
        {
            return ListaUsuario().Where(item => item.UserName == _correo && item.Clave == _clave).FirstOrDefault();

        }
    }


}

