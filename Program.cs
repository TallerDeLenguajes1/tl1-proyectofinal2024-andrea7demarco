const string ArchivoPersonajes = "Data/Personajes.json";
const string ArchivoGanador = "Data/Ganador.json";

FabricaPersonajesService fabricaPersonajesService = new();
List<Personaje> personajesList;
PersonajesJsonService personajesJsonService = new();
BatallaService batallaService = new();
Personaje? personajeElegido = null;

int? comenzar = null;
int resetear = 100;
int personaje_id = 0;

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

// Validar la entrada del usuario para comenzar la pelea
comenzar = Ingresos.LeerEntradaEntera("Comenzar pelea Si[1] - No[0]", 1, 0);

while (comenzar == 1)
{
    if ((personajesJsonService.Existe(ArchivoGanador) && personajesJsonService.LeerGanador()?.Personaje?.Datos.Id != personajeElegido?.Datos.Id)
        || personajeElegido == null)
    {
        personajeElegido = ElegirPersonaje();
    }

    List<Personaje> listaActualizada = new();
    try
    {
        listaActualizada = batallaService.IniciarPelea(personajesList, personajeElegido);
        personajesJsonService.GuardarPersonajes(listaActualizada);
    }
    catch (ArgumentException) // esto obtiene la excepción cuando no hay más personajes
    {
        CargarNuevosPersonajes();
        continue; // para volver al inicio del while
    }

    // Validar la entrada del usuario para iniciar una nueva pelea
    comenzar = Ingresos.LeerEntradaEntera("¿Quiere iniciar una nueva pelea? SI[1] - NO[0]", 1, 0);

    if (comenzar == 1)
    {
        fabricaPersonajesService.ResetearCombosAtaques(listaActualizada);
    }
    else
    {
        Console.WriteLine("No se iniciará una nueva partida\n");
    }

    Console.Clear();
}

// Validar la entrada del usuario para borrar la partida
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

Personaje ElegirPersonaje()
{
    Personaje? personajeElegido = null;
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

void InicializarPersonajes()
{
    personajesList = fabricaPersonajesService.CrearPersonaje();
    personajesJsonService.GuardarPersonajes(personajesList);
}

void CargarNuevosPersonajes()
{
    personajeElegido = null;
    Console.WriteLine("Al parecer no había más luchadores. Estamos creando nuevos...");
    InicializarPersonajes();
    Console.ReadKey();
    Console.Clear();

    // Se muestran los nuevos personajes
    MostrarPorPantalla.MostrarPersonajes(personajesList);
}


