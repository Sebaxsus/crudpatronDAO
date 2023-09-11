using MySql.Data.MySqlClient;

namespace CrudDAOWay
{
    public class EmpleadoDAOFactory
    {
        public static IEmpleadoDao CrearEmpleadoDAO()
        {
            MySqlConnection connection = Conexion.Instance.AbrirConexion();
            return new EmpleadoDaoImpl(connection);
        }
    }
}
