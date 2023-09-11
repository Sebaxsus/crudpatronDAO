using System;
using System.Collections.Generic;
using System.Text;

namespace CrudDAOWay
{
    public class ViewEmpleado
    {
        public void VerEmpleado(Empleado empleado)
        {
            Console.WriteLine("Datos del Estudiante:\n" + empleado.ToString());
        }

        public void VerEmpleados(List<Empleado> empleados)
        {
            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay estudiantes para mostrar.");
                return;
            }

            Console.WriteLine("Lista de Estudianes:");
            foreach (Empleado empleado in empleados)
            {
                Console.WriteLine("------------");
                Console.WriteLine(empleado.ToString());
            }
        }
    }
}