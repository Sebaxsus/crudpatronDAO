using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace CrudDAOWay
{
    public class EmpleadoDaoImpl : IEmpleadoDao
    {
        private const string INSERT_QUERY = "INSERT INTO estudiantes (nombre, apellido, edad, nota_1, nota_2, log) VALUES (@nombre, @apellido, @edad, @nota_1, @nota_2, log)";
        private const string SELECT_ALL_QUERY = "SELECT * FROM estudiantes ORDER BY ID";
        private const string UPDATE_QUERY = "UPDATE estudiantes SET nombre=@nombre, apellido=@apellido, edad=@edad, nota_1=@nota_1, nota_2=@nota_2, log=@log WHERE ID=@id";
        private const string DELETE_QUERY = "DELETE FROM estudiantes WHERE ID=@id";
        private const string SELECT_BY_ID_QUERY = "SELECT * FROM estudiantes WHERE id=@id";
        private const string SELECT_ALL_EMPLEADOS_QUERY = "SELECT * FROM estudiantes";

        private readonly MySqlConnection _connection;

        public EmpleadoDaoImpl(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public bool Registrar(Empleado empleado)
        {
            bool registrado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(INSERT_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", empleado.Apellido);
                    cmd.Parameters.AddWithValue("@edad", empleado.Edad);
                    cmd.Parameters.AddWithValue("@nota_1", empleado.Nota_1);
                    cmd.Parameters.AddWithValue("@nota_2", empleado.Nota_2);
                    cmd.Parameters.AddWithValue("@log", empleado.Log);
                    cmd.ExecuteNonQuery();

                    empleado.Id = (int)cmd.LastInsertedId;

                    registrado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al registrar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return registrado;
        }

        public List<Empleado> Obtener()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado empleado = CrearEmpleadoDesdeDataReader(reader);
                            listaEmpleados.Add(empleado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener los empleados", ex);
            }
            finally
            {
                _connection.Close();
            }

            return listaEmpleados;
        }

        public bool Actualizar(Empleado empleado)
        {
            bool actualizado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(UPDATE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", empleado.Apellido);
                    cmd.Parameters.AddWithValue("@edad", empleado.Edad);
                    cmd.Parameters.AddWithValue("@nota_1", empleado.Nota_1);
                    cmd.Parameters.AddWithValue("@nota_2", empleado.Nota_2);
                    cmd.Parameters.AddWithValue("@log", empleado.Log);
                    cmd.Parameters.AddWithValue("@id", empleado.Id);
                    cmd.ExecuteNonQuery();
                    actualizado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al actualizar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return actualizado;
        }

        public bool Eliminar(Empleado empleado)
        {
            bool eliminado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(DELETE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", empleado.Id);
                    cmd.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al eliminar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return eliminado;
        }

        public Empleado ObtenerEmpleadoPorId(int id)
        {
            Empleado empleado = null;

            try
            {

                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_BY_ID_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            empleado = CrearEmpleadoDesdeDataReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener el empleado por ID", ex);
            }
            finally
            {
                _connection.Close();
            }

            return empleado;
        }

        public List<Empleado> ObtenerTodosLosEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_EMPLEADOS_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado empleado = CrearEmpleadoDesdeDataReader(reader);
                            listaEmpleados.Add(empleado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener todos los empleados", ex);
            }
            finally
            {
                _connection.Close();
            }

            return listaEmpleados;
        }

        private Empleado CrearEmpleadoDesdeDataReader(MySqlDataReader reader)
        {
            int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
            string nombre = reader.GetString("nombre");
            string apellido = reader.GetString("apellido");
            int edad = reader.GetInt32("edad");
            double nota_1 = reader.GetDouble("nota_1");
            double nota_2 = reader.GetDouble("nota_2");
            string  log = reader.IsDBNull(reader.GetOrdinal("log")) ? "" : reader.GetString("log");
            return new Empleado(id, nombre, apellido, edad, nota_1, nota_2, log);
        }

        private void ProveState()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

    }
}
