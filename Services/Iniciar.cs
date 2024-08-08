public class App
{
    private const string ArchivoPersonajes = "Data/Personajes.json";
    private const string ArchivoGanador = "Data/Ganador.json";
    private const string ArchivoHistorial = "Data/Historial.json";

    private FabricaPersonajesService fabricaPersonajesService = new();
    private PersonajesJsonService personajesJsonService = new();
    private BatallaService batallaService = new();

    private List<Personaje> personajesList = new();
    private Personaje? personajeElegido = null;
    private int? comenzar = null;
    private int resetear = 100;

    public void Iniciar()
    {
        InicializarDatos();
        
        while (SolicitarComienzoPelea())
        {
            ProcesarPelea();
            SolicitarNuevaPelea();
            Console.Clear();
        }

        ProcesarFinalizacionPartida();
    }

    private void InicializarDatos()
    {
        if (!personajesJsonService.Existe(ArchivoPersonajes) || new FileInfo(ArchivoPersonajes).Length == 0)
        {
            InicializarPersonajes();
        }
        else
        {
            personajesList = personajesJsonService.LeerPersonajes();
        }

        Console.WriteLine("Escribe tu nombre: ");
        Usuario.Nombre = Console.ReadLine();
        MostrarPorPantalla.MostrarPersonajes(personajesList);

        if (personajesJsonService.Existe(ArchivoGanador))
            personajesJsonService.BorrarArchivo(ArchivoGanador);
    }

    private bool SolicitarComienzoPelea()
    {
        comenzar = Ingresos.LeerEntradaEntera("Comenzar pelea Si[1] - No[0]", 1, 0);
        return comenzar == 1;
    }
//controla que el nuevo personaje elegido no es el mismo que ganó en la 
//partida anterior 
private void ProcesarPelea()
{  
    // Verifica si el historial de ganadores existe y si el personaje elegido es el mismo que el último ganador en el historial.
    if ((personajesJsonService.Existe(ArchivoHistorial) && personajesJsonService.LeerHistorial().Any(g => g.Personaje?.Datos.Id == personajeElegido?.Datos.Id))
        || personajeElegido == null)
    {   
        personajeElegido = ElegirPersonaje();
    }

    List<Personaje> listaActualizada;
    try
    {
        listaActualizada = batallaService.IniciarPelea(personajesList, personajeElegido);

        // Guarda la lista actualizada de personajes.
        personajesJsonService.GuardarPersonajes(listaActualizada);

        // Guarda el ganador en el historial.
        GanadoresHistorial ganador= new()
        {
            Personaje = personajeElegido, // Aquí asumes que `personajeElegido` es el ganador
            Name = Usuario.Nombre, // Aquí asumes que `Usuario.Nombre` es el nombre del usuario
        };

        personajesJsonService.GuardarHistorial(ganador);
    }
    catch (ArgumentException)
    {
        // Si ocurre una excepción, carga nuevos personajes.
        CargarNuevosPersonajes();
        return; // Regresa al inicio del bucle.
    }
}

/* private void ProcesarPelea() 
    {
        if ((personajesJsonService.Existe(ArchivoGanador) && personajesJsonService.LeerGanador()?.Personaje?.Datos.Id != personajeElegido?.Datos.Id)
            || personajeElegido == null)
        {
            personajeElegido = ElegirPersonaje();
        }

        List<Personaje> listaActualizada;
        try
        {
            listaActualizada = batallaService.IniciarPelea(personajesList, personajeElegido);
            personajesJsonService.GuardarPersonajes(listaActualizada);
        }
        catch (ArgumentException)
        {
            CargarNuevosPersonajes();
            return; // Volver al inicio del while
        }
    }
*/
    private void SolicitarNuevaPelea()
    {
        comenzar = Ingresos.LeerEntradaEntera("¿Quiere iniciar una nueva pelea? SI[1] - NO[0]", 1, 0);
        
        if (comenzar == 1)
        {
            fabricaPersonajesService.ResetearCombosAtaques(personajesList);
        }
        else
        {
            Console.WriteLine("No se iniciará una nueva partida\n");
        }
    }

    private void ProcesarFinalizacionPartida()
    {
        resetear = Ingresos.LeerEntradaEntera("¿Desea borrar la partida?: Si[1] - No[0]", 1, 0);
        
        if (resetear == 1)
        {
            personajesJsonService.BorrarArchivo(ArchivoPersonajes);
            Console.WriteLine("Los personajes han sido borrados.");
        }
        else
        {
            Console.WriteLine("La partida no fue eliminada");
        }
    }

    private Personaje ElegirPersonaje()
    {
        Personaje? personajeElegido = null;
        int personaje_id;
        do
        {
            Console.WriteLine("¿Qué personaje desea elegir? Ingrese Id: ");
            var entrada = Console.ReadLine();

            if (int.TryParse(entrada, out personaje_id))
            {
                personajeElegido = personajesList.Find(p => p.Datos.Id == personaje_id);
                if (personajeElegido != null)
                {
                    Console.WriteLine("Personaje seleccionado: " + personajeElegido.Datos.Nombre);
                }
                else
                {
                    Console.WriteLine("ID no válido. Ingrese nuevamente: ");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Ingrese un número de ID válido.");
            }
        } while (personajeElegido == null);

        return personajeElegido;
    }

    private void InicializarPersonajes()
    {
        personajesList = fabricaPersonajesService.CrearPersonaje();
        personajesJsonService.GuardarPersonajes(personajesList);
    }

    private void CargarNuevosPersonajes()
    {
        personajeElegido = null;
        Console.WriteLine("Al parecer no había más luchadores. Estamos creando nuevos...");
        InicializarPersonajes();
        Console.ReadKey();
        Console.Clear();
        MostrarPorPantalla.MostrarPersonajes(personajesList);
    }
}
