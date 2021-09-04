using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioInquilino
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InmobiliariaVsda;Trusted_Connection=True;MultipleActiveResultSets=true";

        public RepositorioInquilino() { }

        public int Alta(int id, Persona p)
        {
            int res = -1;
            if (id == 0)
            {
                Insert(p);
            }
            else
            {
                res = Modificar(id, p);
            }

            return res;
        }
        public int Insert(Persona p)
        {
            int res = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Personas (Dni, Apellido, Nombre, Nombre2, Mail,
                             Telefono, Movil, LugarTrabajo, TelLaboral, Estado) 
                             VALUES (@dni, @apellido, @nombre, @nombre2, @mail,
                             @telefono, @movil, @lugarTrabajo, @telLaboral, @estado);
                             SELECT SCOPE_IDENTITY();";

                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    comm.Parameters.AddWithValue("@dni", p.Dni);
                    comm.Parameters.AddWithValue("@apellido", p.Apellido);
                    comm.Parameters.AddWithValue("@nombre", p.Nombre);
                    comm.Parameters.AddWithValue("@nombre2", p.Nombre2);
                    comm.Parameters.AddWithValue("@mail", p.Mail);
                    comm.Parameters.AddWithValue("@telefono", p.Telefono);
                    comm.Parameters.AddWithValue("@movil", p.Movil);
                    comm.Parameters.AddWithValue("@lugarTrabajo", p.LugarTrabajo);
                    comm.Parameters.AddWithValue("@telLaboral", p.TelLaboral);
                    comm.Parameters.AddWithValue("@estado", 1);
                    conn.Open();
                    res = Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    p.IdPersona = res;

                }
            }
            return res;
        }
        public int Modificar(int id, Persona p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Personas SET Dni=@dni, Apellido=@apellido," +
                    $"Nombre= @nombre, Nombre2= @nombre2, Mail=@mail,Telefono=@telefono, " +
                    $"Movil=@movil, LugarTrabajo=@lugarTrabajo, TelLaboral= @telLaboral, " +
                    $"Estado=@estado WHERE IdPersona = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@nombre2", p.Nombre2);
                    command.Parameters.AddWithValue("@mail", p.Mail);
                    command.Parameters.AddWithValue("@telefono", p.Telefono);
                    command.Parameters.AddWithValue("@movil", p.Movil);
                    command.Parameters.AddWithValue("@lugarTrabajo", p.LugarTrabajo);
                    command.Parameters.AddWithValue("@telLaboral", p.TelLaboral);
                    command.Parameters.AddWithValue("@estado", 1);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public Persona ObtenerPorDni(string dni)
        {
            Persona p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdPersona, Dni, Estado FROM Personas WHERE Dni=@dni;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@dni", dni);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Persona
                        {
                            IdPersona = (int)reader[nameof(Persona.IdPersona)],
                            Dni = (string)reader[nameof(Persona.Dni)],
                            Estado = (int)reader[nameof(Persona.Estado)]
                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }
        public Persona ObtenerPorId(int id)
        {
            Persona p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdPersona, Dni, Apellido,Nombre, Nombre2," +
                    $" Mail, Telefono, Movil, LugarTrabajo, TelLaboral, Estado FROM Personas WHERE IdPersona=@id ;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Persona
                        {
                            IdPersona = reader.GetInt32(0),
                            Dni = (string)reader[nameof(Persona.Dni)],
                            Apellido = (string)reader[nameof(Persona.Apellido)],
                            Nombre = (string)reader[nameof(Persona.Nombre)],
                            Nombre2 = (string)reader[nameof(Persona.Nombre2)],
                            Mail = (string)reader[nameof(Persona.Mail)],
                            Telefono = (string)reader[nameof(Persona.Telefono)],
                            Movil = (string)reader[nameof(Persona.Movil)],
                            LugarTrabajo = (string)reader[nameof(Persona.LugarTrabajo)],
                            TelLaboral = (string)reader[nameof(Persona.TelLaboral)],
                            Estado = (int)reader[nameof(Persona.Estado)]
                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }
        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Personas SET Estado = 0 WHERE IdPersona= @id; ";
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
        public IList<Persona> ObtenerInquilinos()
        {
            IList<Persona> res = new List<Persona>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT IdPersona, Dni, Apellido, Nombre, Nombre2, Mail,
                    Telefono, Movil, LugarTrabajo, TelLaboral, Estado FROM Personas 
                    WHERE Estado !=0 AND TelLaboral IS NOT NULL AND LugarTrabajo IS NOT NULL";

                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var p = new Persona
                        {
                            IdPersona = reader.GetInt32(0),
                            Dni = (string)reader[nameof(Persona.Dni)],
                            Apellido = (string)reader[nameof(Persona.Apellido)],
                            Nombre = reader[nameof(Persona.Nombre)].ToString(),
                            Nombre2 = (string)reader[nameof(Persona.Nombre2)],
                            Mail = (string)reader[nameof(Persona.Mail)],
                            Telefono = (string)reader[nameof(Persona.Telefono)],
                            Movil = (string)reader[nameof(Persona.Movil)],
                            LugarTrabajo = (string)reader[nameof(Persona.LugarTrabajo)],
                            TelLaboral = (string)reader[nameof(Persona.TelLaboral)],
                            Estado = (int)reader[nameof(Persona.Estado)],
                        };
                        res.Add(p);
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }
   
}
