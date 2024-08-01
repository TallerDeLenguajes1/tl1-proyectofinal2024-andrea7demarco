namespace EspacioPersonaje
{
    public class Personaje
    {
        public Datos Datos { get; set; }
        public Caracteristicas Caracteristicas { get; set; }

        public Personaje()
        {
            Datos = new Datos();
            Caracteristicas = new Caracteristicas();
        }
    }
}
