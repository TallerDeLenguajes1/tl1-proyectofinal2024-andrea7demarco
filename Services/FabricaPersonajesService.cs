public class FabricaPersonajesService
{
    private readonly Random _random;
    private const int NumeroPersonajesSeleccionados = 15;

    public FabricaPersonajesService()
    {
        _random = new Random();
    }
    
    public List<Personaje> CrearPersonaje()
    {
        List<Personaje> personajes = [];
        List<PersonajeApi> personajesSeleccionados = PersonajesSeleccionados();

        foreach(var personaje in personajesSeleccionados)
        {
            var fechaNacimiento = GenerarFechaRandom(1700,2024);

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
                }
            });
        }
        
        return personajes;
    }

    public int GenerarRandom(int a, int b)
    {
        return _random.Next(a,b);
    }

    private List<PersonajeApi> PersonajesSeleccionados()
    {
        HashSet<int> numeros = [];
        
        List<PersonajeApi> personajesAPI = ApiQueryService.ObtenerPersonajes().Result;

        while(numeros.Count < NumeroPersonajesSeleccionados)
        {
            numeros.Add(_random.Next(0, personajesAPI.Count + 1));
        }

        return personajesAPI.Where(p => numeros.Contains(p.Id)).ToList();
    }

    private static int CalcularEdad(DateTime fechaNacimiento)
    {
        var Hoy = DateTime.Now;
        return (int)(Hoy.Subtract(fechaNacimiento).TotalDays/365);
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
}
