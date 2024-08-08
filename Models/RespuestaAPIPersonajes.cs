public class RespuestaPersonajes
{
    public List<PersonajeApi> Results { get; set; } = new List<PersonajeApi>();
}

public class PersonajeApi
{
    public string? Name { get; set; }
    public string? Species { get; set; }
    public int Id { get; set; }
}