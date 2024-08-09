public class ControlEntrada
{
    
    public static int LeerEntradaEntera(string mensaje, int opcion1, int opcion2)
    {
        int resultado;
        while (true)
        {
            Console.WriteLine(mensaje);
            var entrada = Console.ReadLine();
            if (int.TryParse(entrada, out resultado) && (resultado == opcion1 || resultado == opcion2))
            {
                return resultado;
            }
            else
            {
                Console.WriteLine($"Entrada inválida. Debe ingresar {opcion1} o {opcion2}.");
            }
        }
    }

}
