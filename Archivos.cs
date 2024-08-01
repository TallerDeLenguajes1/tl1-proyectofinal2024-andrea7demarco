using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ManejoDeArchivos
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<List<string>> GetNombre()
    {
        var url = "https://rickandmortyapi.com/api/character";
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        //Lista nombres
        var listadoNombres = JsonSerializer.Deserialize<RespuestaPersonajes>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        List<string> nombres = new List<string>();
        
        if (listadoNombres != null && listadoNombres.Results != null)
        {
            foreach (var personaje in listadoNombres.Results)
            {
                nombres.Add(personaje.Name);
            }
        } 

        //lista apodos
        return nombres;         
    }

    public class RespuestaPersonajes
    {
        public List<PersonajeApi>? Results {get; set;}
    }

    public class PersonajeApi
    {
        public string Name {get ;set ;}
    }
}
