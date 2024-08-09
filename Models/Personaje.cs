public class Personaje
{
    public Datos Datos { get; set; } //informacion basica de los personajes
    public Caracteristicas Caracteristicas { get; set; } // nivel,velocidad,salud...
    public ComboAtaques ComboAtaques {get; set;} //armas

    public Personaje()
    {
        Datos = new Datos();
        Caracteristicas = new Caracteristicas();
        ComboAtaques = new ComboAtaques();
    }
}
