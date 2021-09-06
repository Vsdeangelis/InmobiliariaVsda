using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioTipo
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InmobiliariaVsda;Trusted_Connection=True;MultipleActiveResultSets=true";
        public RepositorioTipo(){}
        public int Alta(int id, TipoInmueble tipo)
        {
            int res = -1;
            if (id == 0)
            {
                Insert(tipo);
            }
            else
            {
                res = Modificar(id, tipo);
            }
            return res;
        }
        public int Insert(TipoInmueble tipo)
        {
            int res = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Tipos (Tipo, Estado) 
                             VALUES (@tipo, @estado);
                             SELECT SCOPE_IDENTITY();";

                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    comm.Parameters.AddWithValue("@tipo", tipo.Tipo);
                    comm.Parameters.AddWithValue("@estado", 1);
                    conn.Open();
                    res = Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    tipo.IdTipo = res;
                }
            }
            return res;
        }
        public int Modificar(int id, TipoInmueble tipo)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Tipos SET Tipo=@tipo, Estado=@estado," +
                    $"WHERE IdTipo = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@tipo", tipo.Tipo);
                    command.Parameters.AddWithValue("@estado", 1);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public TipoInmueble ObtenerPorNombre(string nombre)
        {
            TipoInmueble tipo = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdTipo, Nombre, Estado FROM Tipos WHERE Tipo=@nombre;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        tipo = new TipoInmueble
                        {
                            IdTipo = (int)reader[nameof(TipoInmueble.IdTipo)],
                            Tipo = (string)reader[nameof(TipoInmueble.Tipo)],
                            Estado = (int)reader[nameof(TipoInmueble.Estado)]
                        };
                    }
                    connection.Close();
                }
            }
            return tipo;
        }
        public TipoInmueble ObtenerPorId(int id)
        {
            TipoInmueble tipo = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdTipo, Tipo, Estado FROM Tipos WHERE " +
                    $"IdTipo=@id ;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        tipo = new TipoInmueble
                        {
                            IdTipo = reader.GetInt32(0),
                            Tipo = (string)reader[nameof(TipoInmueble.Tipo)],
                            Estado = (int)reader[nameof(TipoInmueble.Estado)]
                        };
                    }
                    connection.Close();
                }
            }
            return tipo;
        }
        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Tipos SET Estado = 0 WHERE IdTipo= @id; ";
                ///string sql = $"DELETE FROM Propietarios WHERE IdPropietario = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public IList<TipoInmueble> ObtenerTipoInmuebles()
        {
            IList<TipoInmueble> res = new List<TipoInmueble>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT IdTipo, Tipo, Estado FROM Tipos 
                            WHERE Estado !=0;";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var tipo = new TipoInmueble
                        {
                            IdTipo = reader.GetInt32(0),
                            Tipo = (string)reader[nameof(TipoInmueble.Tipo)],
                            Estado = (int)reader[nameof(TipoInmueble.Estado)],
                        };
                        res.Add(tipo);
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }
}
