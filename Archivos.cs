using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EspacioPersonaje;

public class ManejoDeArchivos
{
    private static readonly HttpClient client = new HttpClient();
    //esto deberia hacerlo una sola vez, no dos consultas??
    public static async Task<List<string>> GetNombres()
    {
        var url = "https://rickandmortyapi.com/api/character";
        var response = await client.GetStringAsync(url);
        var listadoPersonajes = JsonSerializer.Deserialize<RespuestaPersonajes>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return listadoPersonajes?.Results?.ConvertAll(p => p.Name) ?? new List<string>();
    }

    public static async Task<List<string>> GetTipos()
    {
        var url = "https://rickandmortyapi.com/api/character";
        var response = await client.GetStringAsync(url);
        var listadoPersonajes = JsonSerializer.Deserialize<RespuestaPersonajes>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return listadoPersonajes?.Results?.ConvertAll(p => p.Species) ?? new List<string>();
    }

    public class RespuestaPersonajes
    {
        public List<PersonajeApi> Results { get; set; } = new List<PersonajeApi>();
    }

    public class PersonajeApi
    {
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
    }
}
