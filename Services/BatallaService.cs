using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Colorful;
using SysConsole = System.Console;

public class BatallaService
{
    private readonly FabricaPersonajesService fabricaPersonajes;
    private readonly PersonajesJsonService personajesJson;

    public BatallaService()
    {
        personajesJson = new PersonajesJsonService();
        fabricaPersonajes = new FabricaPersonajesService();
    }

    public List<Personaje> IniciarPelea(List<Personaje> personajes, Personaje personajeElegido)
    {
        if (personajes == null || personajes.Count < 2)
        {
            throw new ArgumentException("Debe haber al menos dos personajes para iniciar una pelea.");
        }

        Personaje peleador1 = personajeElegido;
        Personaje peleador2;

        // Para que no se repitan
        do
        {
            peleador2 = SeleccionarPeleadorAleatorio(personajes);
        } while (peleador1.Datos.Nombre == peleador2.Datos.Nombre);

        int contador = 1;

        while (true)
        {
            RealizarRonda(ref peleador1, ref peleador2, ref contador);

            if (peleador1.Caracteristicas.Salud <= 0)
            {
                AumentarNivelDelGanador(peleador2);
                RemoverPerdedor(peleador1, personajes);

                Ganador ganador = new()
                {
                    Personaje = peleador2,
                    Nombre = "Computadora",
                };

                personajesJson.GuardarGanador(ganador);

                return personajes;
            }

            if (peleador2.Caracteristicas.Salud <= 0)
            {
                AumentarNivelDelGanador(peleador1);
                RemoverPerdedor(peleador2, personajes);

                Ganador ganador = new()
                {
                    Personaje = peleador1,
                    Nombre = Usuario.Nombre,
                };

                personajesJson.GuardarGanador(ganador);
                

                return personajes;
            }
            
            SysConsole.Clear();
        }

        
    }

    private static void RemoverPerdedor(Personaje perdedor, List<Personaje> personajes)
        => personajes.Remove(perdedor);

    private Personaje SeleccionarPeleadorAleatorio(List<Personaje> personajes)
    {
        int nroPersonajes = personajes.Count;
        return personajes[fabricaPersonajes.GenerarRandom(0, nroPersonajes)];
    }

    private decimal CalcularDañoAtaque(Personaje pj, int velocidad, decimal potenciador = 0)
    {
        int ataque = pj.Caracteristicas.Destreza * pj.Caracteristicas.Fuerza * pj.Caracteristicas.Nivel;
        int efectividad = fabricaPersonajes.GenerarRandom(1, 101);
        int defensa = pj.Caracteristicas.Armadura * velocidad;
        int constanteAjuste = 500;

        decimal daño = ((ataque * efectividad) - defensa) / constanteAjuste;
        return daño * (potenciador + 1);
    }

    private void RealizarRonda(ref Personaje peleador1, ref Personaje peleador2, ref int contador)
    {
        SysConsole.Clear();
        SysConsole.WriteLine($"Ronda Numero {contador++}");
        MostrarPorPantalla.MostrarTablaDeCombos(peleador1);
        SysConsole.WriteLine("Ingresar ATAQUE: ");
        string ataque_ingresado = SysConsole.ReadLine() ?? string.Empty;

        Dictionary<string, decimal> potenciadorAtaques = new()
        {
            {peleador1.ComboAtaques.Basico, 0.05m },
            {peleador1.ComboAtaques.Intermedio, 0.15m },
            {peleador1.ComboAtaques.Avanzado, 0.30m },
            {peleador1.ComboAtaques.Fatality, 0.90m }
        };

        decimal potenciador = 0m;

        if (potenciadorAtaques.TryGetValue(ataque_ingresado, out potenciador))
        {
            SysConsole.WriteLine("Coinciden.");
            SysConsole.ReadKey();
            SysConsole.Clear();
        }

        decimal daño1 = CalcularDañoAtaque(peleador1, peleador2.Caracteristicas.Velocidad, potenciador);
        peleador2.Caracteristicas.Salud = peleador2.Caracteristicas.Salud - daño1 < 0
            ? peleador2.Caracteristicas.Salud = 0
            : peleador2.Caracteristicas.Salud - daño1;

        decimal daño2 = CalcularDañoAtaque(peleador2, peleador1.Caracteristicas.Velocidad);
        peleador1.Caracteristicas.Salud = peleador1.Caracteristicas.Salud - daño2 < 0
            ? peleador1.Caracteristicas.Salud = 0
            : peleador1.Caracteristicas.Salud - daño2;

        MostrarPorPantalla.MostrarDatos(peleador1, peleador2);
        SysConsole.ReadKey();
    }

    private static void AumentarNivelDelGanador(Personaje pj)
    {
        pj.Caracteristicas.Fuerza += 5;
        pj.Caracteristicas.Salud += 10;
        pj.Caracteristicas.Nivel++;
    }

 


}
