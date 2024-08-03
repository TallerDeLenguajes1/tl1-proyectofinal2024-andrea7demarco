using System.Runtime.InteropServices;
using EspacioPersonaje;

FabricaDePersonajes FabricarPj = new FabricaDePersonajes();
List<Personaje> personajesList;
PersonajesJson json = new PersonajesJson();
Batalla pelea = new Batalla();

int? comenzar;
int? resetear;
personajesList = FabricarPj.CrearPersonajeCaract();
MostrarPersonajes(personajesList);
//Funciones mostrar luego las paso a otr .cs


if(!json.Existe("Personajes.json") || new FileInfo("Personajes.json").Length == 0){  //si no existe o está vacío --corregido
    personajesList = FabricarPj.CrearPersonajeCaract();
    json.GuardarPersonajes(personajesList, "Personajes");
}
//chequear corregir esos readline
Console.WriteLine("Comenzar pelea Si[1] - No[0]");
comenzar = int.Parse(Console.ReadLine());

while(comenzar == 1)
{
    List<Personaje> listaActualizada = pelea.Pelea(personajesList);
    json.GuardarPersonajes(listaActualizada, "Personajes");
    Console.WriteLine("Quiere iniciar una nueva pelea? SI[1] - NO[0]");
    comenzar = int.Parse(Console.ReadLine());
    Console.Clear();
}
// chequear - ubicar bien
Console.WriteLine("Desea borrar la partida?: Si[1] - No[0]");
resetear = int.Parse(Console.ReadLine());
if(resetear == 1)
{
    json.BorrarArchivo("Personajes.json");
} else 
{
    Console.WriteLine("Json no eliminado\n");
}


void MostrarPersonajes(List<Personaje> personajes)
{   Console.WriteLine("PERSONAJES:\n");
    foreach (Personaje pj in personajes)
    {
        Console.WriteLine($"Nombre : {pj.Datos.Nombre}\n" +
        $"ID: {pj.Datos.ID_seleccion}\n" +
        $"Tipo : {pj.Datos.Tipo}\n" +
        $"Velocidad : {pj.Caracteristicas.Velocidad}\n" +
        $"Nivel : {pj.Caracteristicas.Nivel}\n" +
        $"Fuerza : {pj.Caracteristicas.Fuerza}\n" +
        $"Salud : {pj.Caracteristicas.Salud}\n" +
        $"Edad : {pj.Datos.Edad} \n");
    }
}



