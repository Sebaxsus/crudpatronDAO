// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using MySql.Data.MySqlClient;


namespace CrudDAOWay
{
    public sealed class Conexion
    {
        //"server=localhost;user=MinTic;password=JsgrMc1;database=estudiantes;"
        //"server=localhost;user=root;password=;database=sampledb;"
        private static readonly string ConnectionString = "server=localhost;user=root;password=;database=sampledb;";
        private MySqlConnection _connection;

        //Se implementa el patrón Singleton utilizando la propiedad estática Instance para garantizar
        //que solo haya una instancia de Conexion en toda la aplicación.

        private static Conexion _instance = null;
        public static Conexion Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Conexion();
                }
                return _instance;
            }
        }

        public Conexion()
        {
            _connection = new MySqlConnection(ConnectionString);
        }

        public MySqlConnection AbrirConexion()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                    Console.WriteLine("Conectado");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }

            return _connection;
        }

        public void CerrarConexion()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Closed)
                {
                    _connection.Close();
                    Console.WriteLine("Conexión cerrada");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }
    }
}
