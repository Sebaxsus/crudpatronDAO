namespace CrudDAOWay
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; } //Cedula
        public double Nota_1 { get; set; } //Horas
        public double Nota_2 { get; set; } //Sueldo
        public string Log { get; set; }
        
        public Empleado()
        {
        }

        public Empleado(int id, string nombre, string apellido, int edad, double nota_1, double nota_2, string log)
        {
            Id = id; 
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Nota_1 = nota_1;
            Nota_2 = nota_2;
            Log = log;
        }

        public Empleado(string nombre, string apellido, int edad, double nota_1, double nota_2, string log)
        {
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Nota_1 = nota_1;
            Nota_2 = nota_2;
            Log = log;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nNombre: {Nombre}\nApellido: {Apellido}\nEdad: {Edad}\n\nNota 1: {Nota_1}\nNota 2: {Nota_2}\nLog: {Log}";
        }
    }
}
