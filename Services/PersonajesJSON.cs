using System.Text.Json;
public class PersonajesJson
{
    private const string ArchivoPersonajes = "Data/Personajes.json";
    private const string ArchivoGanador = "Data/Ganador.json";


    public void GuardarPersonajes(List<Personaje> personaje)
    {
        //serializo - convierto la lista personajes a una cadena json
        string json = JsonSerializer.Serialize(personaje);
        //escribo en un archivo .json
        File.WriteAllText(ArchivoPersonajes, json);
    }

    public void GuardarGanador(Personaje ganador)
    {
        //serializo - convierto la lista personajes a una cadena json
        string json = JsonSerializer.Serialize(ganador);
        //escribo en un archivo .json
        File.WriteAllText(ArchivoGanador, json);
    }

    public List<Personaje> LeerPersonajes()
    {
        //deserealizo - lee un json y lo pasa a una lista de obj personajes
        string json = File.ReadAllText(ArchivoPersonajes);
        List<Personaje>? personajesDeserealizados = JsonSerializer.Deserialize<List<Personaje>>(json) 
            ?? throw new Exception("No se pudo leer el archivo JSON");
            
        return personajesDeserealizados;
    }

    public Personaje LeerGanador()
    {
        //deserealizo - lee un json y lo pasa a una lista de obj personajes
        string json = File.ReadAllText(ArchivoGanador);
        Personaje ganadorDeserealizado = JsonSerializer.Deserialize<Personaje>(json) 
            ?? throw new Exception("No se pudo leer el archivo JSON");
            
        return ganadorDeserealizado;
    }
     
    public bool Existe(string nombreArchivo)
    {
        //verifico que existe el archivo
        return File.Exists(nombreArchivo);
    }

    public void BorrarArchivo(string nombreArchivo)
    {
        if (Existe(nombreArchivo))
        {
            File.Delete(nombreArchivo);
        }
    }
}