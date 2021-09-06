using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioInmueble
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InmobiliariaVsda;Trusted_Connection=True;MultipleActiveResultSets=true";
        public RepositorioInmueble(){}
        public int Alta(int id, Inmueble inmueble)
        {
            int res = -1;
            if (id == 0)
            {
                Insert(inmueble);
            }
            else
            {
                res = Modificar(id, inmueble);
            }
            return res;
        }
        public int Insert(Inmueble inmueble)
        {
            int res = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Inmuebles (IdPropietario, Calle, 
                                Numero, Barrio, Manzana, Dpto, Piso, IdTipo, 
                                Uso, Descripcion, Ambientes, Precio, Disponibilidad,
                                Estado) VALUES (@idDuenio, @calle, @numero, @barrio,
                                @manzana, @dpto, @piso, @tipo, @uso, @descripcion,
                                @ambientes, @precio, @disponibilidad, @estado);
                                SELECT SCOPE_IDENTITY();";

                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    comm.Parameters.AddWithValue("@idDuenio", inmueble.IdPropietario);
                    comm.Parameters.AddWithValue("@calle",inmueble.Calle);
                    comm.Parameters.AddWithValue("@numero", inmueble.Numero);
                    comm.Parameters.AddWithValue("@barrio", inmueble.Barrio);
                    comm.Parameters.AddWithValue("@manzana", inmueble.Manzana);
                    comm.Parameters.AddWithValue("@dpto", inmueble.Dpto);
                    comm.Parameters.AddWithValue("@piso", inmueble.Piso);
                    comm.Parameters.AddWithValue("@tipo", inmueble.IdTipo);
                    comm.Parameters.AddWithValue("@uso", inmueble.Uso);
                    comm.Parameters.AddWithValue("@descripcion", inmueble.Descripcion);
                    comm.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
                    comm.Parameters.AddWithValue("@precio", inmueble.Precio);
                    comm.Parameters.AddWithValue("@disponibilidad", inmueble.Disponibilidad);
                    comm.Parameters.AddWithValue("@estado", 1);
                    conn.Open();
                    res = Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    inmueble.IdInmueble = res;
                }
            }
            return res;
        }
        public int Modificar(int id, Inmueble inmueble)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inmuebles SET IdPropietario= @idDuenio, Calle=@calle," +
                                $"Numero= @numero, Barrio= @barrio, Manzana = @manzana , " +
                                $"Dpto= @dpto, Piso= @piso, IdTipo= @idTipo, Uso=@uso, " +
                                $"Descripcion =@descripcion, Ambientes= @ambientes, " +
                                $"Precio= @precio, Disponibilidad= @disponibilidad, " +
                                $"Estado = @estado WHERE IdInmueble = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@idDuenio", inmueble.IdPropietario);
                    command.Parameters.AddWithValue("@calle", inmueble.Calle);
                    command.Parameters.AddWithValue("@numero", inmueble.Numero);
                    command.Parameters.AddWithValue("@barrio", inmueble.Barrio);
                    command.Parameters.AddWithValue("@manzana", inmueble.Manzana);
                    command.Parameters.AddWithValue("@dpto", inmueble.Dpto);
                    command.Parameters.AddWithValue("@piso", inmueble.Piso);
                    command.Parameters.AddWithValue("@idTipo", inmueble.IdTipo);
                    command.Parameters.AddWithValue("@uso", inmueble.Uso);
                    command.Parameters.AddWithValue("@descripcion", inmueble.Descripcion);
                    command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
                    command.Parameters.AddWithValue("@precio", inmueble.Precio);
                    command.Parameters.AddWithValue("@disponibilidad", inmueble.Disponibilidad);
                    command.Parameters.AddWithValue("@estado", 1);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public Inmueble ObtenerPorId(int id)
        {
            Inmueble inmueble= null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdInmueble, IdPropietario, Calle, Numero, " +
                    $"Barrio, Manzana, Dpto, Piso, IdTipo, Uso, Descripcion, " +
                    $"Ambientes, Precio, Disponibilidad, Estado FROM Inmuebles " +
                    $"WHERE IdInmueble=@id ;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        inmueble = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(0),
                            IdPropietario = (int)reader[nameof(Inmueble.IdPropietario)],
                            Calle = (string)reader[nameof(Inmueble.Calle)],
                            Numero = (string)reader[nameof(Inmueble.Numero)],
                            Barrio = (string)reader[nameof(Inmueble.Barrio)],
                            Manzana = (string)reader[nameof(Inmueble.Manzana)],
                            Dpto = (string)reader[nameof(Inmueble.Dpto)],
                            Piso = (string)reader[nameof(Inmueble.Piso)],
                            IdTipo = (int)reader[nameof(Inmueble.IdTipo)],
                            Uso = (string)reader[nameof(Inmueble.Uso)],
                            Descripcion = (string)reader[nameof(Inmueble.Descripcion)],
                            Ambientes= (string)reader[nameof(Inmueble.Ambientes)],
                            Precio= (decimal)reader[nameof(Inmueble.Precio)],
                            Disponibilidad=(string)reader[nameof(Inmueble.Disponibilidad)],
                            Estado = (int)reader[nameof(Inmueble.Estado)]
                        };
                    }
                    connection.Close();
                }
            }
            return inmueble;
        }
        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inmuebles SET Estado = 0 WHERE IdInmueble= @id; ";
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
        public IList<Inmueble> ObtenerInmuebles()
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT IdInmueble, IdPropietario, Calle, Numero,
                            Barrio, Manzana, Dpto, Piso, IdTipo, Uso, Descripcion, 
                            Ambientes, Precio, Disponibilidad, Estado FROM Inmuebles 
                            WHERE Estado !=0;";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var inmueble = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(0),
                            IdPropietario = (int)reader[nameof(Inmueble.IdPropietario)],
                            Calle = (string)reader[nameof(Inmueble.Calle)],
                            Numero = reader[nameof(Inmueble.Numero)].ToString(),
                            Barrio = (string)reader[nameof(Inmueble.Barrio)],
                            Manzana = (string)reader[nameof(Inmueble.Manzana)],
                            Dpto = (string)reader[nameof(Inmueble.Dpto)],
                            Piso = (string)reader[nameof(Inmueble.Piso)],
                            IdTipo= (int)reader[nameof(Inmueble.IdTipo)],
                            Uso= (string)reader[nameof(Inmueble.Uso)],
                            Descripcion= (string)reader[nameof(Inmueble.Descripcion)],
                            Ambientes= (string)reader[nameof(Inmueble.Ambientes)],
                            Precio= (decimal)reader[nameof(Inmueble.Precio)],
                            Disponibilidad=(string)reader[nameof(Inmueble.Disponibilidad)],
                            Estado = (int)reader[nameof(Inmueble.Estado)]
                        };
                        res.Add(inmueble);
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }
}
