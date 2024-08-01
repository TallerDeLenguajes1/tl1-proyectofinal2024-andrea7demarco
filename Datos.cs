namespace EspacioPersonaje
{
    public class Datos
    {
        private string? nombre;
        private string? apodo;
        private DateTime fecha_nac;
        private int edad;

        private int tipo;

        public string? Nombre 
        { get => nombre; set => nombre = value;}

        public string? Apodo 
        { get => apodo; set => apodo = value;}

        public DateTime Fecha_nac 
        {get => fecha_nac; set => fecha_nac = value;}

        public int Edad 
        {get => edad; set => edad = value;}

        public int Tipo
        {get => tipo; set => tipo = value;}
    }

}
