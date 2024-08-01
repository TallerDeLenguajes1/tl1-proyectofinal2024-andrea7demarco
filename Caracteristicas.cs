namespace EspacioPersonaje
{
    public class Caracteristicas
    {
        private int velocidad;
        private int destreza;
        private int fuerza;
        private int nivel;
        private int armadura;
        private decimal salud;

        public int Velocidad 
        {get => velocidad; set => velocidad = value;}

        public int Destreza 
        {get => destreza; set => destreza = value;}

        public int Fuerza 
        {get => fuerza; set => fuerza = value;}

        public int Nivel 
        {get => nivel; set => nivel = value;}

        public int Armadura 
        {get => armadura; set => armadura = value;}

        public decimal Salud 
        {get => salud; set => salud = value;}
    }
}
