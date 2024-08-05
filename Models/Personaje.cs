public class Personaje
{
    public Datos Datos { get; set; }
    public Caracteristicas Caracteristicas { get; set; }
    public ComboAtaques ComboAtaques {get; set;}

    public Personaje()
    {
        Datos = new Datos();
        Caracteristicas = new Caracteristicas();
        ComboAtaques = new ComboAtaques();
    }
}
