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
    

     //con valores repetidos
    public int obtRandom(int a, int b)
    {
        Random random = new Random();
        return random.Next(a,b);

    }

    //random con valores que no se repiten
    public static int[] RandomsDistintos(int tamanio, int min, int max)
    {
        if(max - min + 1 < tamanio)
        {   //pongo eso??
            throw new ArgumentException("El rango es muy chico");

        }

        HashSet<int> numeros = new HashSet<int>();
        Random random = new Random();
        while(numeros.Count <tamanio)
        {
            int nuevoNumero = random.Next(min,max + 1);
            numeros.Add(nuevoNumero);

        }

        return numeros.ToArray();

    }

    public int CalcEdad(DateTime fechaNacimiento)
    {
        var Hoy = DateTime.Now;
        return (int)((Hoy.Subtract(fechaNacimiento).TotalDays)/365);
    }

    public List<Personaje> CrearPersonajeCaract()
    {
        List<Personaje> personajes = new List<Personaje>();
        int[] indicesNombres = RandomsDistintos(15, 0, nombresApi.Count - 1);

        for(int i = 0; i < 15; i++){
            Personaje NuevoPersonaje = new Personaje();
            NuevoPersonaje.Caracteristicas.Destreza = obtRandom(1,6);
            NuevoPersonaje.Caracteristicas.Nivel = obtRandom(1,11);
            NuevoPersonaje.Caracteristicas.Velocidad = obtRandom(1,11);
            NuevoPersonaje.Caracteristicas.Fuerza = obtRandom(1,11);
            NuevoPersonaje.Caracteristicas.Armadura = obtRandom(1,11);
            NuevoPersonaje.Caracteristicas.Salud = 100;
            NuevoPersonaje.Datos.Fecha_nac = new DateTime(obtRandom(1992,2000) , obtRandom(1,12), obtRandom(1,31));
            NuevoPersonaje.Datos.Edad = CalcEdad(NuevoPersonaje.Datos.Fecha_nac);
            NuevoPersonaje.Datos.Nombre = nombresApi[indicesNombres[i]];
            NuevoPersonaje.Datos.Tipo = tiposApi[indicesNombres[i]];
            NuevoPersonaje.Datos.ID_seleccion = i+1;

            personajes.Add(NuevoPersonaje);
        }
        

        return personajes;

    }





  /* */
       
}
