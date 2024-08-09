
using System.Text.Json;
public class PersonajesJsonService
{
    private const string ArchivoPersonajes = "Data/Personajes.json";
    private const string ArchivoHistorial = "Data/Historial.json";
    public void GuardarPersonajes(List<Personaje> personaje)
    {
        //serializo - convierto la lista personajes a una cadena json
        string json = JsonSerializer.Serialize(personaje);
        //escribo en un archivo .json
        File.WriteAllText(ArchivoPersonajes, json);
    }

    public List<Personaje> LeerPersonajes()
    {
        //deserealizo - lee un json y lo pasa a una lista de obj personajes
        string json = File.ReadAllText(ArchivoPersonajes);
        List<Personaje>? personajesDeserealizados = JsonSerializer.Deserialize<List<Personaje>>(json)
            ?? throw new Exception("No se pudo leer el archivo JSON");

        return personajesDeserealizados;
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

    //historial

    public void GuardarHistorial(GanadoresHistorial ganador)
    {
        List<GanadoresHistorial> historial = LeerHistorial();
        historial.Add(ganador);

        // Serializa la lista completa de ganadores al archivo
        string json = JsonSerializer.Serialize(historial, new JsonSerializerOptions
        {
            WriteIndented = true //mas formateado + facil de leer
        });
        File.WriteAllText(ArchivoHistorial, json);
    }
    public List<GanadoresHistorial> LeerHistorial()
    {
        if (!File.Exists(ArchivoHistorial))
            return new List<GanadoresHistorial>();

        string json = File.ReadAllText(ArchivoHistorial);
        if (string.IsNullOrEmpty(json))
            return new List<GanadoresHistorial>();

        try
        {
            return JsonSerializer.Deserialize<List<GanadoresHistorial>>(json) ?? new List<GanadoresHistorial>();
        }
        catch (JsonException)
        {
            // Manejo de errores si el JSON está malformado
            return new List<GanadoresHistorial>();
        }
    }
}