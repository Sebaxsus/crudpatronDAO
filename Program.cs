using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDAOWay
{
    class Program
    {
        /*static void Main()
        {
            try
            {
                Conexion conexion = Conexion.Instance;
                MySqlConnection connection = conexion.AbrirConexion();

                // Realiza operaciones con la base de datos
                Console.WriteLine("Conexión exitosa a la base de datos");

                // Aquí puedes agregar las operaciones específicas de la base de datos que necesitas realizar

                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
            }
        }*/
        private static IEmpleadoDao dao = EmpleadoDAOFactory.CrearEmpleadoDAO();

        public static void Main(string[] args)
        {
            string action;

            while (true)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("[L]istar | [R]egistrar | [A]ctualizar | [E]liminar | [S]alir: ");
                action = Console.ReadLine()?.ToUpper();

                if (!string.IsNullOrEmpty(action))
                {
                    try
                    {
                        switch (action)
                        {
                            case "L":
                                ListarEmpleados();
                                break;
                            case "R":
                                RegistrarEmpleado();
                                break;
                            case "A":
                                ActualizarEmpleado();
                                break;
                            case "E":
                                EliminarEmpleado();
                                break;
                            case "S":
                                return;
                            default:
                                Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                                break;
                        }
                    }
                    catch (DAOException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }
        }

        private static void RegistrarEmpleado()
        {
            try
            {
                Empleado empleado = InputEmpleado();
                if (dao.Registrar(empleado))
                {
                    Console.WriteLine("Registro exitoso: " + empleado.Id);
                    Console.WriteLine("\n\nCreado: " + empleado);
                }
                else
                {
                    Console.WriteLine("Error al registrar el estudiante.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al registrar el estudiante: " + e.Message);
            }
        }

        private static void ActualizarEmpleado()
        {
            int id = InputId();
            Empleado empleado = dao.ObtenerEmpleadoPorId(id);
            Console.WriteLine("------------Datos originales------------");
            Console.WriteLine(empleado);
            Console.WriteLine("Ingrese los nuevos datos");

            string nombre = InputNombre();
            string apellido = InputApellido();
            int edad = Int32.Parse(InputEdad());
            double nota_1 = InputNota_1("Ingrese la nota #1: ");
            double nota_2 = InputNota_2("Ingrese la nota #2: ");
            string log = InputFechaEjecucion();

            empleado = new Empleado(id, nombre, apellido, edad, nota_1, nota_2, log);
            try
            {
                if (dao.Actualizar(empleado))
                {
                    Console.WriteLine("Actualización exitosa");
                }
                else
                {
                    Console.WriteLine("Error al actualizar el estudiante.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el estudiante: " + e.Message);
            }
        }

        private static void EliminarEmpleado()
        {
            int id = InputId();
            Empleado empleado = null;

            try
            {
                empleado = dao.ObtenerEmpleadoPorId(id);
            }
            catch (DAOException daoe)
            {
                Console.WriteLine("Error: " + daoe.Message);
            }

            if (empleado != null && dao.Eliminar(empleado))
            {
                Console.WriteLine("Estudiante eliminado: " + empleado.Id);
            }
            else
            {
                Console.WriteLine("Error al eliminar el estudiante.");
            }
        }

        private static void ListarEmpleados()
        {
            try
            {
                List<Empleado> todosLosEmpleados = dao.ObtenerTodosLosEmpleados();
                foreach (Empleado empleado in todosLosEmpleados)
                {
                    Console.WriteLine(empleado.ToString() + "\n");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener todos los estudiantes: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
            }
        }

        private static Empleado InputEmpleado()
        {
            string nombre = InputNombre();
            string apellido = InputApellido();
            int edad = Int32.Parse(InputEdad());
            double nota_1 = InputNota_1("Ingrese la Nota #1: ");
            double nota_2 = InputNota_2("Ingrese la Nota #2: ");
            string log = InputFechaEjecucion();
            return new Empleado(nombre, apellido, edad, nota_1, nota_2, log);
        }

        private static int InputId()
        {
            int id;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese un valor entero para el ID del estudiante: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return id;
        }

        private static string InputEdad()
        {
            return InputString("Ingrese la edad del estudiante: ");
        }

        private static string InputNombre()
        {
            return InputString("Ingrese el nombre del estudiante: ");
        }

        private static string InputApellido()
        {
            return InputString("Ingrese el apellido del estudiante: ");
        }

        private static double InputNota_1(string message)
        {
            double nota_1;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (double.TryParse(Console.ReadLine(), out nota_1))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return nota_1;
        }

        private static double InputNota_2(string message)
        {
            double nota_2;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (double.TryParse(Console.ReadLine(), out nota_2))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return nota_2;
        }

        private static string InputString(string message)
        {
            string s;
            while (true)
            {
                Console.WriteLine(message);
                s = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(s) && s.Length >= 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("La longitud de la cadena debe ser >= 2");
                }
            }
            return s;
        }

        private static string InputFechaEjecucion()
        {
            DateTime Fecha = DateTime.Now;
            //string FormatoFechaSql = Fecha.ToString("u");
            //string FormatoFechaSql = Fecha.ToString("yyyy-MM-dd hh-mm-ss tt");
            //return FormatoFechaSql.Remove(FormatoFechaSql.Length-1);
            string FormatoFechaSql = Fecha.ToString("yyyy-MM-dd HH:mm:ss");
            return FormatoFechaSql;
        }
    }
}
