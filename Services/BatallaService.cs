public class BatallaService
{
    private readonly FabricaPersonajesService fabricaPersonajes;
    private readonly PersonajesJsonService personajesJson;

    public BatallaService()
    {
        personajesJson = new PersonajesJsonService();
        fabricaPersonajes = new FabricaPersonajesService();
    }

    public List<Personaje> IniciarPelea(List<Personaje> personajes, Personaje personajeElegido)
    {
        if (personajes == null || personajes.Count < 2)
        {
            throw new ArgumentException("Debe haber al menos dos personajes para iniciar una pelea.");
        }

        Personaje peleador1 = personajeElegido;
        Personaje peleador2;
        
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
                AumentarNivelDelGanador(peleador2);
                RemoverPerdedor(peleador1, personajes);

                Ganador ganador = new()
                {
                    Personaje = peleador2,
                    Nombre = "Computadora",
                };

                personajesJson.GuardarGanador(ganador);

                return personajes;
            }

            if (peleador2.Caracteristicas.Salud <= 0)
            {
                AumentarNivelDelGanador(peleador1);
                RemoverPerdedor(peleador2, personajes);

                Ganador ganador = new()
                {
                    Personaje = peleador1,
                    Nombre = Usuario.Nombre,
                };

                personajesJson.GuardarGanador(ganador);

                return personajes;
            }

            Console.Clear();
        }
    }

    private static void RemoverPerdedor(Personaje perdedor, List<Personaje> personajes)
        => personajes.Remove(perdedor);
    // {
    //     return personajes.Remove(perdedor);
    // }

    private Personaje SeleccionarPeleadorAleatorio(List<Personaje> personajes)
    {
        int nroPersonajes = personajes.Count;
        return personajes[fabricaPersonajes.GenerarRandom(0, nroPersonajes)];
    }

    private decimal CalcularDañoAtaque(Personaje pj, int velocidad)
    {
        int ataque = pj.Caracteristicas.Destreza * pj.Caracteristicas.Fuerza * pj.Caracteristicas.Nivel;
        int efectividad = fabricaPersonajes.GenerarRandom(1, 101);
        int defensa = pj.Caracteristicas.Armadura * velocidad;
        int constanteAjuste = 500;

        decimal daño = ((ataque * efectividad) - defensa) / constanteAjuste;
        return daño;
    }

    private void RealizarRonda(ref Personaje peleador1, ref Personaje peleador2, ref int contador)
    {
        decimal daño1 = CalcularDañoAtaque(peleador1, peleador2.Caracteristicas.Velocidad);
        peleador2.Caracteristicas.Salud = peleador2.Caracteristicas.Salud - daño1 < 0
            ? peleador2.Caracteristicas.Salud = 0
            : peleador2.Caracteristicas.Salud - daño1;

        decimal daño2 = CalcularDañoAtaque(peleador2, peleador1.Caracteristicas.Velocidad);
        peleador1.Caracteristicas.Salud = peleador1.Caracteristicas.Salud - daño2 < 0
            ? peleador1.Caracteristicas.Salud = 0
            : peleador1.Caracteristicas.Salud - daño2;

        MostrarDatos(peleador1, peleador2);

        Console.WriteLine($"Ronda Numero {contador++}");
        Console.ReadKey();
    }

    private static void AumentarNivelDelGanador(Personaje pj)
    {
        pj.Caracteristicas.Fuerza += 5;
        pj.Caracteristicas.Salud += 10;
        pj.Caracteristicas.Nivel++;
    }

    private static void MostrarDatos(Personaje pj1, Personaje pj2)
    {
        Console.WriteLine(
                $"El nombre del peronaje es Personaje 1: {pj1.Datos.Nombre} ------- Personaje 2: {pj2.Datos.Nombre}\n" +
                $"El tipo de personaje es Personaje 1: {pj1.Datos.Tipo} ------- Personaje 2: {pj2.Datos.Tipo}\n" +
                $"La velocidad del peronaje es Personaje 1: {pj1.Caracteristicas.Velocidad} ------- Personaje 2: {pj2.Caracteristicas.Velocidad}\n" +
                $"El nivel del personaje es Personaje 1: {pj1.Caracteristicas.Nivel} ------- Personaje 2: {pj2.Caracteristicas.Nivel}\n" +
                $"La fuerza del personaje es Personaje 1: {pj1.Caracteristicas.Fuerza} ------- Personaje 2: {pj2.Caracteristicas.Fuerza}\n" +
                $"La salud del personaje es Personaje 1: {pj1.Caracteristicas.Salud} ------- Personaje 2: {pj2.Caracteristicas.Salud}\n");
    }
}