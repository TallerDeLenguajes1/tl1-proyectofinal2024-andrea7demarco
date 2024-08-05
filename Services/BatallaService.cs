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

    private decimal CalcularDañoAtaque(Personaje pj, int velocidad, decimal potenciador = 0)
    {
        int ataque = pj.Caracteristicas.Destreza * pj.Caracteristicas.Fuerza * pj.Caracteristicas.Nivel;
        int efectividad = fabricaPersonajes.GenerarRandom(1, 101);
        int defensa = pj.Caracteristicas.Armadura * velocidad;
        int constanteAjuste = 500;

        decimal daño = ((ataque * efectividad) - defensa) / constanteAjuste;
        return daño * (potenciador + 1);
    }

    private void RealizarRonda(ref Personaje peleador1, ref Personaje peleador2, ref int contador)
    {
        string? ataque_ingresado;
        Console.WriteLine($"Ronda Numero {contador++}");
        MostrarTablaDeCombos(peleador1);
        Console.WriteLine("Ingresar ATAQUE: ");
        ataque_ingresado = Console.ReadLine();

        Dictionary<string, decimal> potenciadorAtaques = new()
        {
            {peleador1.ComboAtaques.Basico, 0.05m },
            {peleador1.ComboAtaques.Intermedio, 0.15m },
            {peleador1.ComboAtaques.Avanzado, 0.30m },
            {peleador1.ComboAtaques.Fatality, 0.90m }
        };

        decimal potenciador = 0m;

        if (potenciadorAtaques.TryGetValue(ataque_ingresado, out potenciador))
        {
            Console.WriteLine("Coinciden.");
        }

        decimal daño1 = CalcularDañoAtaque(peleador1, peleador2.Caracteristicas.Velocidad, potenciador);
        peleador2.Caracteristicas.Salud = peleador2.Caracteristicas.Salud - daño1 < 0
            ? peleador2.Caracteristicas.Salud = 0
            : peleador2.Caracteristicas.Salud - daño1;

        decimal daño2 = CalcularDañoAtaque(peleador2, peleador1.Caracteristicas.Velocidad);
        peleador1.Caracteristicas.Salud = peleador1.Caracteristicas.Salud - daño2 < 0
            ? peleador1.Caracteristicas.Salud = 0
            : peleador1.Caracteristicas.Salud - daño2;

        MostrarDatos(peleador1, peleador2);
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

    public void MostrarTablaDeCombos(Personaje pj1)
    {
        Console.WriteLine("Esto se mostrara por unos segundos...");

        Console.WriteLine(
            "TABLA DE JUGADAS:\n" +
            $"Jugada basica [{pj1.ComboAtaques.Basico}]\n" +
            $"Jugada intermedia [{pj1.ComboAtaques.Intermedio}]\n" +
            $"Jugada avanzada [{pj1.ComboAtaques.Avanzado}]\n" +
            $"Jugada fatality [{pj1.ComboAtaques.Fatality}]\n");
        Thread.Sleep(3000); // se muestra 3 segs
        Console.Clear();
    }
}