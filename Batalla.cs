public class Batalla 
{
    private readonly FabricaDePersonajes fabrica;
    private readonly PersonajesJson json;

    public Batalla()
    {
        json = new PersonajesJson();
        fabrica = new FabricaDePersonajes();

    }

    public List<EspacioPersonaje.Personaje> Pelea(List<EspacioPersonaje.Personaje> personajes)
{
    if (personajes == null || personajes.Count < 2)
    {
        throw new ArgumentException("Debe haber al menos dos personajes para iniciar una pelea.");
    }

    EspacioPersonaje.Personaje peleador1 = SeleccionarPeleadorAleatorio(personajes);
    EspacioPersonaje.Personaje peleador2;
    //para q no se repitan
    do
    {
        peleador2 = SeleccionarPeleadorAleatorio(personajes);
    } while (peleador1.Datos.Nombre == peleador2.Datos.Nombre);

    int contador = 1;

    while (true)
    {
        RealizarRonda(ref peleador1, ref peleador2, ref contador);

        if (peleador1.Caracteristicas.Salud <= 0)
        {
            Ganador(peleador2, peleador1, personajes);
            return personajes;
        }

        if (peleador2.Caracteristicas.Salud <= 0)
        {
            Ganador(peleador1, peleador2, personajes);
            return personajes;
        }

        Console.Clear();
    }
}

private EspacioPersonaje.Personaje SeleccionarPeleadorAleatorio(List<EspacioPersonaje.Personaje> personajes)
{
    int nroPersonajes = personajes.Count;
    return personajes[fabrica.obtRandom(0, nroPersonajes)];
}

public decimal DañoAtaque(EspacioPersonaje.Personaje pj, int velocidad){

    int ataque = pj.Caracteristicas.Destreza * pj.Caracteristicas.Fuerza * pj.Caracteristicas.Nivel;
    int efectividad = fabrica.obtRandom(1,101);
    int defensa = pj.Caracteristicas.Armadura * velocidad;
    int constanteAjuste = 500;

    decimal daño = ((ataque * efectividad) - defensa) / constanteAjuste;
    return daño;
    }

private void RealizarRonda(ref EspacioPersonaje.Personaje peleador1, ref EspacioPersonaje.Personaje peleador2, ref int contador)
{
    decimal daño1 = DañoAtaque(peleador1, peleador2.Caracteristicas.Velocidad);
    peleador2.Caracteristicas.Salud -= daño1;

    decimal daño2 = DañoAtaque(peleador2, peleador1.Caracteristicas.Velocidad);
    peleador1.Caracteristicas.Salud -= daño2;

    MostrarDatos(peleador1, peleador2);

    Console.WriteLine($"Ronda Numero {contador++}");
    Console.ReadKey();
}

public void Ganador(EspacioPersonaje.Personaje pj){
    pj.Caracteristicas.Fuerza += 5;
    pj.Caracteristicas.Salud += 10;
    pj.Caracteristicas.Nivel++;
    }

private void Ganador(EspacioPersonaje.Personaje ganador, EspacioPersonaje.Personaje perdedor, List<EspacioPersonaje.Personaje> personajes)
{
    Ganador(ganador);
    personajes.Remove(perdedor);
}

public void MostrarDatos(EspacioPersonaje.Personaje pj1, EspacioPersonaje.Personaje pj2){

    Console.WriteLine(
            $"El nombre del peronaje es Personaje 1: {pj1.Datos.Nombre} ------- Personaje 2: {pj2.Datos.Nombre}\n" +
            $"El tipo de personaje es Personaje 1: {pj1.Datos.Tipo} ------- Personaje 2: {pj2.Datos.Tipo}\n" + 
            $"La velocidad del peronaje es Personaje 1: {pj1.Caracteristicas.Velocidad} ------- Personaje 2: {pj2.Caracteristicas.Velocidad}\n" +
            $"El nivel del personaje es Personaje 1: {pj1.Caracteristicas.Nivel} ------- Personaje 2: {pj2.Caracteristicas.Nivel}\n"+
            $"La fuerza del personaje es Personaje 1: {pj1.Caracteristicas.Fuerza} ------- Personaje 2: {pj2.Caracteristicas.Fuerza}\n"+
            $"La salud del personaje es Personaje 1: {pj1.Caracteristicas.Salud} ------- Personaje 2: {pj2.Caracteristicas.Salud}\n");
    }



}