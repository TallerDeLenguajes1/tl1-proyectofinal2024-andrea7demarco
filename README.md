Alumna : De Marco, Andrea Sofía

##Proyecto final de Taller de Lenguajes I - RPG 2024#

Juego de Rol por Consola en C#
=====================================
#Descripción#
Este es un juego de rol (RPG) por consola desarrollado en C# que utiliza la API de Rick and Morty para obtener personajes. La API está paginada y se extraen los nombres, las especies y los IDs de los personajes. El jugador elige un personaje con el cual realizar la batalla (Jugador 1), mientras que la computadora juega con un personaje aleatorio (Jugador 2).

Durante el combate, el jugador puede realizar movimientos de distintos niveles: básicos, intermedios, avanzados y un movimiento especial llamado "fatality". Estos movimientos especiales, llamados "combo ataques" en el proyecto, tienen diferentes longitudes de cadenas de texto, siendo más difícil ejecutar la "fatality" debido a su longitud. Para ejecutar estos movimientos avanzados, el jugador debe ingresar correctamente una clave específica. Si la clave es correcta, se aplicará un multiplicador de daño; de lo contrario, se realizará un ataque estándar. La computadora (Jugador 2) solo puede realizar ataques estándar.

Las partidas se van guardando con los jugadores que van ganando. Si un personaje muere, es eliminado de la lista del archivo Personajes.json, y los datos del jugador que gana son guardados en un archivo JSON llamado Ganador.json, junto con su nombre (el cual es ingresado por el usuario). Cuando la lista de jugadores queda solo con un jugador, o cuando el usuario desea reiniciar la batalla (es decir, borrar el archivo JSON), se crean otra vez 15 personajes aleatorios sin repetición en el documento Personajes.json.
