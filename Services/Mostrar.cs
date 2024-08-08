using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Colorful;
using SysConsole = System.Console;
public class MostrarPorPantalla
{
    //introduccion
    public static void MostrarIntroduccion()
    {
        SysConsole.WriteLine("¡Bienvenido a Rick and Morty! Enfréntate a una crisis interdimensional mientras exploras universos absurdos tu misión es restaurar el equilibrio del multiverso y pelear con criaturas bizarras");
    }
    // Definición de métodos

    public static void MostrarGanador(GanadoresHistorial pj)
    {
        SysConsole.WriteLine("Ganador:");
        SysConsole.WriteLine($"Usuario: {pj.Name}\n" +
        $"Nombre: {pj.Personaje?.Datos.Nombre}\n");
    }
    public static void MostrarPersonajes(List<Personaje> personajes)
    {
        SysConsole.WriteLine("PERSONAJES: ");
        foreach (Personaje pj in personajes)
        {
            SysConsole.WriteLine($"Nombre : {pj.Datos.Nombre}\n" +
            $"ID: {pj.Datos.Id}\n" +
            $"Tipo : {pj.Datos.Tipo}\n" +
            $"Velocidad : {pj.Caracteristicas.Velocidad}\n" +
            $"Nivel : {pj.Caracteristicas.Nivel}\n" +
            $"Fuerza : {pj.Caracteristicas.Fuerza}\n" +
            $"Salud : {pj.Caracteristicas.Salud}\n" +
            $"Edad : {pj.Datos.Edad} \n");
        }
    }

    public static void MostrarTablaDeCombos(Personaje pj1)
    {
        SysConsole.WriteLine("La tabla siempre se mostrara por unos segundos...");
        Thread.Sleep(1000); //aviso con tiempo
        SysConsole.Clear();
        
        // Crear los encabezados de columnas
        string encabezado = $"{"Tipo de Ataque".PadRight(20)}{"Descripción".PadRight(40)}";
        string separador = new string('-', encabezado.Length);

        // Crear las filas de datos
        string filaBasico = $"{ "Jugada básica".PadRight(20) }{ pj1.ComboAtaques.Basico.PadRight(40) }";
        string filaIntermedio = $"{ "Jugada intermedia".PadRight(20) }{ pj1.ComboAtaques.Intermedio.PadRight(40) }";
        string filaAvanzado = $"{ "Jugada avanzada".PadRight(20) }{ pj1.ComboAtaques.Avanzado.PadRight(40) }";
        string filaFatality = $"{ "Jugada fatality".PadRight(20) }{ pj1.ComboAtaques.Fatality.PadRight(40) }";

        // Se muestra así línea por línea porque se mostraba un count que contaba la cantidad de filas con el 
        //formato de tablas
        var colorAmarillo = Color.Yellow;
        Colorful.Console.WriteLine(encabezado, colorAmarillo);
        Colorful.Console.WriteLine(separador, colorAmarillo);
        Colorful.Console.WriteLine(filaBasico, colorAmarillo);
        Colorful.Console.WriteLine(filaIntermedio, colorAmarillo);
        Colorful.Console.WriteLine(filaAvanzado, colorAmarillo);
        Colorful.Console.WriteLine(filaFatality, colorAmarillo);

        Thread.Sleep(1000); // se muestra 4.5 segs
        SysConsole.Clear();
    }

    public static void MostrarDatos(Personaje pj1, Personaje pj2)
    {
        // Definir el ancho de las columnas
        int anchoColumna = 32;
        
        // Crear los encabezados de columnas
        string encabezado = $"{"Atributos".PadRight(anchoColumna)}{"PERSONAJE 1".PadRight(anchoColumna)}{"PERSONAJE 2".PadRight(anchoColumna)}";
        string separador = new string('-', encabezado.Length);

        // Crear las filas de datos
        string filaNombre = $"{ "Nombre".PadRight(anchoColumna) }{ pj1.Datos.Nombre.PadRight(anchoColumna) }{ pj2.Datos.Nombre.PadRight(anchoColumna) }";
        string filaTipo = $"{ "Tipo".PadRight(anchoColumna) }{ pj1.Datos.Tipo.PadRight(anchoColumna) }{ pj2.Datos.Tipo.PadRight(anchoColumna) }";
        string filaVelocidad = $"{ "Velocidad".PadRight(anchoColumna) }{ pj1.Caracteristicas.Velocidad.ToString().PadRight(anchoColumna) }{ pj2.Caracteristicas.Velocidad.ToString().PadRight(anchoColumna) }";
        string filaNivel = $"{ "Nivel".PadRight(anchoColumna) }{ pj1.Caracteristicas.Nivel.ToString().PadRight(anchoColumna) }{ pj2.Caracteristicas.Nivel.ToString().PadRight(anchoColumna) }";
        string filaFuerza = $"{ "Fuerza".PadRight(anchoColumna) }{ pj1.Caracteristicas.Fuerza.ToString().PadRight(anchoColumna) }{ pj2.Caracteristicas.Fuerza.ToString().PadRight(anchoColumna) }";
        string filaSalud = $"{ "Salud".PadRight(anchoColumna) }{ pj1.Caracteristicas.Salud.ToString().PadRight(anchoColumna) }{ pj2.Caracteristicas.Salud.ToString().PadRight(anchoColumna) }";

        // Mostrar la tabla de datos con color verde claro
        var colorVerdeClaro = Color.LightGreen;
        Colorful.Console.WriteLine(encabezado, colorVerdeClaro);
        Colorful.Console.WriteLine(separador, colorVerdeClaro);
        Colorful.Console.WriteLine(filaNombre, colorVerdeClaro);
        Colorful.Console.WriteLine(filaTipo, colorVerdeClaro);
        Colorful.Console.WriteLine(filaVelocidad, colorVerdeClaro);
        Colorful.Console.WriteLine(filaNivel, colorVerdeClaro);
        Colorful.Console.WriteLine(filaFuerza, colorVerdeClaro);
        Colorful.Console.WriteLine(filaSalud, colorVerdeClaro);
    }
}