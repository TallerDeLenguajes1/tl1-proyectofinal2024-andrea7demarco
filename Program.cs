using EspacioPersonaje;

FabricaDePersonajes FabricarPj = new FabricaDePersonajes();
List<Personaje> personajesList;

personajesList = FabricarPj.CrearPersonajeCaract();
MostrarPersonajes(personajesList);
//Funciones

void MostrarPersonajes(List<Personaje> personajes)
{
    foreach (Personaje pj in personajes)
    {
        Console.WriteLine($"Nombre : {pj.Datos.Nombre}\n" +
        $"Tipo : {pj.Datos.Tipo}\n" +
        $"Velocidad : {pj.Caracteristicas.Velocidad}\n" +
        $"Nivel : {pj.Caracteristicas.Nivel}\n" +
        $"Fuerza : {pj.Caracteristicas.Fuerza}\n" +
        $"Salud : {pj.Caracteristicas.Salud}\n");
    }
}
