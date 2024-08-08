using System.Text.Json;

public class ApiQueryService
{
    private static readonly HttpClient client = new();
    private const int CantidadPaginasAObtener = 10;

    public static async Task<List<PersonajeApi>> ObtenerPersonajes()
    {
        List<PersonajeApi> personajeApis = [];

        // La API devuelve los datos de forma paginada. Posee 42 páginas que contienen 826 personajes en total.
        // Por una cuestión de tiempo, se decidió pedir los personajes de las primeras 10 páginas.
        for(int i = 1; i <= CantidadPaginasAObtener; i++)
        {
            var url = $"https://rickandmortyapi.com/api/character?page={i}";
            var response = await client.GetAsync(url);

            // if(!response.IsSuccessStatusCode)
            //     throw new Exception("Ocurrio un error al intentar consultar la API para obtener los personajes");
            response.EnsureSuccessStatusCode();
            //await importante porque la solicitud es asincrónica
            var json = await response.Content.ReadAsStringAsync();
            var listadoPersonajes = JsonSerializer.Deserialize<RespuestaPersonajes>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (listadoPersonajes?.Results == null) break;

            personajeApis.AddRange(listadoPersonajes.Results);
        }

        return personajeApis;
    }
}
