public class FabricaPersonajesService
{
    private readonly Random _random;
    private const int NumeroPersonajesSeleccionados = 10;

    public FabricaPersonajesService()
    {
        _random = new Random();
    }

    public List<Personaje> CrearPersonaje()
    {
        List<Personaje> personajes = [];
        List<PersonajeApi> personajesSeleccionados = PersonajesSeleccionados();

        foreach (var personaje in personajesSeleccionados)
        {
            var fechaNacimiento = GenerarFechaRandom(1700, 2024);

            personajes.Add(new()
            {
                Caracteristicas = {
                    Destreza = GenerarRandom(1,6),
                    Nivel = GenerarRandom(1,11),
                    Velocidad = GenerarRandom(1,11),
                    Fuerza = GenerarRandom(1,11),
                    Armadura = GenerarRandom(1,11),
                    Salud = 100
                },
                Datos = {
                    Id = personaje.Id,
                    Nombre = personaje.Name,
                    Tipo = personaje.Species,
                    Fecha_nac = fechaNacimiento,
                    Edad = CalcularEdad(fechaNacimiento)
                },
                ComboAtaques = GenerarComboAtaques()
            });
        }

        return personajes;
    }

// crea la lista Personaje con sus combos y también se utiliza cuando reseteo la partida para volver
// a cargar los datos 
    public void ResetearCombosAtaques(List<Personaje> personajes)
    {
        foreach (Personaje personaje in personajes)
        {
            personaje.ComboAtaques = GenerarComboAtaques();
        }
    }

//resetea los combos de ronda en ronda
    public void ResetearCombosDeAtaqueEnRonda(Personaje personajes)
    {
        personajes.ComboAtaques = GenerarComboAtaques();
    }

    public int GenerarRandom(int a, int b) => _random.Next(a, b);

    // Selecciona los personajes sin que se repitan
    private List<PersonajeApi> PersonajesSeleccionados()
    {
        // Se crea un conjunto para almacenar numeros que no se repitan
        HashSet<int> numeros = [];

        // Se obtiene la lista de personajes de la API
        List<PersonajeApi> personajesAPI = ApiQueryService.ObtenerPersonajes().Result;

        // Se seleccionan números aleatorios hasta que el conjunto tenga el tamaño deseado
        while (numeros.Count < NumeroPersonajesSeleccionados)
        {
            // Se agrega un número aleatorio al conjunto, solo si no existe en el conjunto
            numeros.Add(_random.Next(0, personajesAPI.Count + 1));
        }

        // Se filtran y devuelven los personajes cuyos IDs están en el conjunto de números creado con anterioridad
        return personajesAPI.Where(p => numeros.Contains(p.Id)).ToList();
    }

    private static int CalcularEdad(DateTime fechaNacimiento)
    {
        var Hoy = DateTime.Now;
        return (int)(Hoy.Subtract(fechaNacimiento).TotalDays / 365);
    }

    private DateTime GenerarFechaRandom(int anioInicio, int anioFin)
    {
        if (anioInicio > anioFin)
        {
            throw new ArgumentException("The start year must be less than or equal to the end year.");
        }

        int year = GenerarRandom(anioInicio, anioFin + 1);
        int month = GenerarRandom(1, 13);
        int day = GenerarRandom(1, DateTime.DaysInMonth(year, month) + 1);

        return new DateTime(year, month, day);
    }

    private string GeneradorClavesAleatoria(int longitud)
    {
        const string letras = "0123456789";
        var clave = new char[longitud];

        for (int i = 0; i < longitud; i++)
        {
            clave[i] = letras[_random.Next(letras.Length)];
        }

        return new string(clave); //paso de arreglo a string
    }

    public ComboAtaques GenerarComboAtaques()
    {   //todos van a tener un multiplicador MENOS la fatality, que va a sacar mucha más vida
        return new ComboAtaques
        {
            Basico = GeneradorClavesAleatoria(4),
            Intermedio = GeneradorClavesAleatoria(5),
            Avanzado = GeneradorClavesAleatoria(6),
            Fatality = GeneradorClavesAleatoria(7)
        };
    }
}


