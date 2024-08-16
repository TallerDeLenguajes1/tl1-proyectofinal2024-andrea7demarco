using System.Text.Json;
using System.IO;

public class ApiQueryService
{
    private static readonly HttpClient client = new();
    private const int CantidadPaginasAObtener = 10;
    private const string ArchivoPersonajes = "personajes.json";

    public static async Task<List<PersonajeApi>> ObtenerPersonajes()
    {
        List<PersonajeApi> personajeApis = new List<PersonajeApi>();

        try
        {
            // Intentar obtener los personajes desde la API
            for (int i = 1; i <= CantidadPaginasAObtener; i++)
            {
                var url = $"https://rickandmortyapi.com/api/character?page={i}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var listadoPersonajes = JsonSerializer.Deserialize<RespuestaPersonajes>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (listadoPersonajes?.Results == null) break;

                personajeApis.AddRange(listadoPersonajes.Results);
            }

            // Guardar los personajes en el archivo local para su uso futuro
            await GuardarPersonajesEnArchivo(personajeApis);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener personajes de la API: {ex.Message}");
            // Leer los personajes del archivo local en caso de error
            personajeApis = LeerPersonajesDeArchivo();
        }

        return personajeApis;
    }

    private static async Task GuardarPersonajesEnArchivo(List<PersonajeApi> personajes)
    {
        var json = JsonSerializer.Serialize(personajes, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(ArchivoPersonajes, json);
    }

    private static List<PersonajeApi> LeerPersonajesDeArchivo()
    {
        if (File.Exists(ArchivoPersonajes))
        {
            var json = File.ReadAllText(ArchivoPersonajes);
            return JsonSerializer.Deserialize<List<PersonajeApi>>(json) ?? new List<PersonajeApi>();
        }

        return new List<PersonajeApi>();
    }
}
