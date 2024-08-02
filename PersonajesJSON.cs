using EspacioPersonaje;
using System.Text.Json;
public class PersonajesJson
{
    public void GuardarPersonajes(List<Personaje> personaje, string archivo)
    {
        //serializo - convierto la lista personajes a una cadena json
        string json = JsonSerializer.Serialize(personaje);
        //escribo en un archivo .json
        File.WriteAllText(archivo +".json", json);
    }

    public List<Personaje> LeerPersonajes(string archivo)
    {
        //deserealizo - lee un json y lo pasa a una lista de obj personajes
        string json = File.ReadAllText(archivo);
        List<Personaje>? PersonajeDeserealizado = JsonSerializer.Deserialize<List<Personaje>>(json);
        
        if(PersonajeDeserealizado == null)
        {
            throw new Exception("No se pudo leer el archivo JSON");

        }

        return PersonajeDeserealizado;

    }
     
    public bool Existe(string archivo)
    {
        //verifico que existe el archivo
        return File.Exists(archivo);
    }
}