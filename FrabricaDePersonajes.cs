using EspacioPersonaje;

public class FabricaDePersonajes
    {

        private List<string>? nombresApi;

        public FabricaDePersonajes()
        {
            nombresApi = ManejoDeArchivos.GetNombre().Result;
        }
        Dictionary<string, int> tipos = new Dictionary<string, int>(){
            ["Johnny Cage"] = 0,
            ["Sonya Blade"] = 0,
            ["Quan Chi"] = 1,
            ["Shang Tsung"] = 1,
            ["Raiden"] = 2,
            ["Sub-Zero"] = 2,
            ["Baraka"] = 3,
            ["Mileena"] = 3,
            ["Goro"] = 4,  
            ["Cyrax"] = 4

        };
     

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
            NuevoPersonaje.Datos.Tipo = tipos.GetValueOrDefault(NuevoPersonaje.Datos.Nombre);

            personajes.Add(NuevoPersonaje);
        }
        

        return personajes;

    }





  /* */
       
}
