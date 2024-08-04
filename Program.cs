
FabricaPersonajesService FabricarPj = new();
List<Personaje> personajesList;
PersonajesJsonService personajesJson = new();
BatallaService pelea = new();
Personaje? personajeElegido = null;

const string ArchivoPersonajes = "Data/Personajes.json";
const string ArchivoGanador = "Data/Ganador.json";

int? comenzar;
int? resetear;
int personaje_id = 0;

if (!personajesJson.Existe(ArchivoPersonajes))
{
    InicializarPersonajes();
}
else
{
    personajesList = personajesJson.LeerPersonajes();
}

Console.WriteLine("Escribe tu nombre: \n");
Usuario.Nombre = Console.ReadLine();

MostrarPersonajes(personajesList);
//Funciones mostrar luego las paso a otr .cs

//chequear corregir esos readline
Console.WriteLine("Comenzar pelea Si[1] - No[0]");
comenzar = int.Parse(Console.ReadLine());

if (personajesJson.Existe(ArchivoGanador))
    personajesJson.BorrarArchivo(ArchivoGanador);

while (comenzar == 1)
{
    if ((personajesJson.Existe(ArchivoGanador) && personajesJson.LeerGanador().Personaje.Datos.id != personajeElegido?.Datos.id)
        || personajeElegido == null)
    {
        personajeElegido = ElegirPersonaje();
    }

    try
    {
        List<Personaje> listaActualizada = pelea.IniciarPelea(personajesList, personajeElegido);
        personajesJson.GuardarPersonajes(listaActualizada);
    }
    catch (ArgumentException) // esto obtiene la excepcion cuando no hay mas personajes
    {
        CargarNuevosPersonajes();
        continue; // para volver al inicio del while
    }

    Console.WriteLine("Quiere iniciar una nueva pelea? SI[1] - NO[0]");
    comenzar = int.Parse(Console.ReadLine());
    Console.Clear();
}


// chequear - ubicar bien
Console.WriteLine("Desea borrar la partida?: Si[1] - No[0]");
resetear = int.Parse(Console.ReadLine());

if (resetear == 1)
{
    personajesJson.BorrarArchivo(ArchivoPersonajes);
    Console.WriteLine("Los personajes han sido borrados.");
}
else
{
    Console.WriteLine("La partida no fue eliminada\n");
}

void MostrarPersonajes(List<Personaje> personajes)
{
    Console.WriteLine("PERSONAJES:\n");
    foreach (Personaje pj in personajes)
    {
        Console.WriteLine($"Nombre : {pj.Datos.Nombre}\n" +
        $"ID: {pj.Datos.Id}\n" +
        $"Tipo : {pj.Datos.Tipo}\n" +
        $"Velocidad : {pj.Caracteristicas.Velocidad}\n" +
        $"Nivel : {pj.Caracteristicas.Nivel}\n" +
        $"Fuerza : {pj.Caracteristicas.Fuerza}\n" +
        $"Salud : {pj.Caracteristicas.Salud}\n" +
        $"Edad : {pj.Datos.Edad} \n");
    }
}

Personaje ElegirPersonaje()
{
    do
    {
        //eleccion personaje
        Console.WriteLine("Que Personaje desea elegir? Ingresar id:\n");
        personaje_id = int.Parse(Console.ReadLine());

        personajeElegido = personajesList.Find(p => p.Datos.Id == personaje_id);
        if (personajeElegido != null)
        {
            Console.WriteLine("Personaje seleccionado: " + personajeElegido.Datos.Nombre);
        }
        else
        {
            Console.WriteLine("ID no valido. Ingrese nuevamente:");
        }
    } while (personajeElegido == null);

    return personajeElegido;
}

void InicializarPersonajes()
{
    personajesList = FabricarPj.CrearPersonaje();
    personajesJson.GuardarPersonajes(personajesList);
}

void CargarNuevosPersonajes()
{
    personajeElegido = null;
    InicializarPersonajes();
    Console.WriteLine("Al parecer no habian mas luchadores. Estamos creando nuevos...");
    Console.ReadKey();
    Console.Clear();

    // Se muestran los nuevos personajes
    MostrarPersonajes(personajesList);
}
