using EspacioPersonaje;

public class FabricaDePersonajes
    {

    private List<string> nombresApi ;
    private List<string> tiposApi;
    public FabricaDePersonajes()
    {
            nombresApi = ManejoDeArchivos.GetNombres().Result;
            tiposApi = ManejoDeArchivos.GetTipos().Result;
    }

     
    public int obtRandom(int a, int b)
    {
        Random random = new Random();
        return random.Next(a,b);

    }

    public int CalcEdad(DateTime fechaNacimiento)
    {
        var Hoy = DateTime.Now;
        return (int)((Hoy.Subtract(fechaNacimiento).TotalDays)/365);
    }
/*controlar 
    private T ObtenerElementoAleatorio<T>(List<T> lista)
    {
        if (lista == null || lista.Count == 0)
            return default(T);
        var randomIndex = obtRandom(0, lista.Count);
        return lista[randomIndex];
    }
*/
    public List<Personaje> CrearPersonajeCaract()
    {
        List<Personaje> personajes = new List<Personaje>();

        for(int i = 0; i < 20; i++){

            Personaje NuevoPersonaje = new Personaje();
            NuevoPersonaje.Caracteristicas.Destreza = obtRandom(1,6);
            NuevoPersonaje.Caracteristicas.Nivel = obtRandom(1,11);
            NuevoPersonaje.Caracteristicas.Velocidad = obtRandom(1,11);
            NuevoPersonaje.Caracteristicas.Fuerza = obtRandom(1,11);
            NuevoPersonaje.Caracteristicas.Armadura = obtRandom(1,11);
            NuevoPersonaje.Caracteristicas.Salud = 100;
            NuevoPersonaje.Datos.Fecha_nac = new DateTime(obtRandom(1992,2000) , obtRandom(1,12), obtRandom(1,31));
            NuevoPersonaje.Datos.Edad = CalcEdad(NuevoPersonaje.Datos.Fecha_nac);
            NuevoPersonaje.Datos.Nombre = nombresApi[ i % nombresApi.Count];
            NuevoPersonaje.Datos.Tipo = tiposApi[i % tiposApi.Count];

            personajes.Add(NuevoPersonaje);
        }
        

        return personajes;

    }





  /* */
       
}
