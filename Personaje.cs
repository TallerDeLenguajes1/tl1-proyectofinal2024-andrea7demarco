using System.Security.Cryptography;

namespace espacioPersonaje 
{
    class espacioPersonaje
    {
        private int velocidad;
        private int destreza;
        private int fuerza;
        private int nivel;
        private int armadura;
        private decimal salud;


        //private tipos Tipo;

        private string? nombre;
        private string? apodo;
        private DateTime fecha_nac;
        private int edad;

        public int Velocidad { get => velocidad; set =>velocidad = value; }
        public int Destreza { get => destreza; set =>destreza = value; }
        public int Fuerza { get => fuerza; set =>fuerza = value; }

        public int Nivel { get => nivel; set =>nivel = value; }
        public int Armadura { get => armadura; set =>armadura = value; }
        public decimal Salud { get => salud; set =>salud = value; }

        public string? Nombre { get => nombre; set =>nombre = value; }

        public string? Apodo { get => apodo; set =>apodo = value; }
        public DateTime Fecha_nac { get => fecha_nac; set =>fecha_nac = value; }
        public int Edad { get => edad; set =>edad = value; }






    }
}