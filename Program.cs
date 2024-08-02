using EspacioPersonaje;

FabricaDePersonajes FabricarPj = new FabricaDePersonajes();
List<Personaje> personajesList;
PersonajesJson json = new PersonajesJson();

personajesList = FabricarPj.CrearPersonajeCaract();
MostrarPersonajes(personajesList);
//Funciones mostrar luego las paso a otr .cs


if(!json.Existe("Personajes.json") || new FileInfo("Personajes.json").Length == 0){  //si no existe o está vacío --corregido
    personajesList = FabricarPj.CrearPersonajeCaract();
    json.GuardarPersonajes(personajesList, "Personajes");
}

void MostrarPersonajes(List<Personaje> personajes)
{
    foreach (Personaje pj in personajes)
    {
        Console.WriteLine($"Nombre : {pj.Datos.Nombre}\n" +
        $"Tipo : {pj.Datos.Tipo}\n" +
        $"Velocidad : {pj.Caracteristicas.Velocidad}\n" +
        $"Nivel : {pj.Caracteristicas.Nivel}\n" +
        $"Fuerza : {pj.Caracteristicas.Fuerza}\n" +
        $"Salud : {pj.Caracteristicas.Salud}\n" +
        $"Edad : {pj.Datos.Edad} \n");
    }
}
