using System.Runtime.CompilerServices;

public class App
{
    private const string ArchivoPersonajes = "Data/Personajes.json";
    private const string ArchivoHistorial = "Data/Historial.json";
    private FabricaPersonajesService fabricaPersonajesService = new();
    private PersonajesJsonService personajesJsonService = new();
    private BatallaService batallaService = new();
    private List<Personaje> personajesList = new();
    private Personaje? personajeElegido = null;
    private int? comenzar = null;
    private int resetear = 100;
    private int verHistorial = 100;

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
    }

    private bool SolicitarComienzoPelea()
    {
        comenzar = ControlEntrada.LeerEntradaEntera("Comenzar pelea Si[1] - No[0]", 1, 0);
        return comenzar == 1;
    }
 
private void ProcesarPelea()
{  
    // Verifica si el historial de ganadores existe y si el personaje elegido es el mismo que el último ganador en el historial.
    if ((personajesJsonService.Existe(ArchivoHistorial) && personajesJsonService.LeerHistorial().Any(g => g.Personaje?.Datos.Id == personajeElegido?.Datos.Id))
        || personajeElegido == null)
    {   
        personajeElegido = ElegirPersonaje();
    }

    List<Personaje> listaActualizada;
    try //manejo excpeciones
    {   //se inicia la pelea entre los pjs de PersonajesList y el PersonajeElegido
        listaActualizada = batallaService.IniciarPelea(personajesList, personajeElegido);

        // Guarda la lista actualizada de personajes.
        personajesJsonService.GuardarPersonajes(listaActualizada);
     

    }
    catch (ArgumentException)
    {
        // Si ocurre una excepción, carga nuevos personajes.
        CargarNuevosPersonajes();
        return; // Regresa al inicio del bucle.
    }
}

    private void SolicitarNuevaPelea()
    {
        comenzar = ControlEntrada.LeerEntradaEntera("¿Quiere iniciar una nueva pelea? SI[1] - NO[0]", 1, 0);
        
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
        resetear = ControlEntrada.LeerEntradaEntera("¿Desea borrar la partida?: Si[1] - No[0]", 1, 0);
        
        if (resetear == 1)
        {
            personajesJsonService.BorrarArchivo(ArchivoPersonajes);
            Console.WriteLine("Los personajes han sido borrados.");
        }
        else
        {
            Console.WriteLine("La partida no fue eliminada");
        }

        verHistorial = ControlEntrada.LeerEntradaEntera("¿Desea ver el historial de las partidas?: Si[1] - No [0]", 1, 0);
        if( verHistorial == 1)
        {
            if(personajesJsonService.Existe(ArchivoHistorial))
            {
                var historial = personajesJsonService.LeerHistorial();
                MostrarPorPantalla.MostrarHistorial(historial);
            }
        } else
        {
            Console.WriteLine("okey.");
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
            {   //linq ...controla q esté en la lista
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
