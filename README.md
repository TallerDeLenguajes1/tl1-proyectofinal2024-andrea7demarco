Alumna : De Marco, Andrea Sofía

##Proyecto final de Taller de Lenguajes I - RPG 2024#

Juego de Rol por Consola en C#
=====================================
Este es un juego de rol (RPG) por consola desarrollado en C# que utiliza la API de Rick and Morty para obtener personajes. La API está paginada y se extraen los nombres, las especies y los IDs de los personajes. El jugador elige un personaje con el cual realizar la batalla (Jugador 1), mientras que la computadora juega con un personaje aleatorio (Jugador 2).

Durante el combate, el jugador puede realizar movimientos de distintos niveles: básicos, intermedios, avanzados y un movimiento especial llamado "fatality". Estos movimientos especiales, llamados "combo ataques" en el proyecto, tienen diferentes longitudes de cadenas de texto que van desde el 0 al 9, siendo más difícil ejecutar la "fatality" debido a su longitud. Para ejecutar estos movimientos avanzados, el jugador debe responder correctamente a una serie de preguntas sobre la serie "Rick and Morty". Si responde correctamente, se aplicará un multiplicador de daño; de lo contrario, se realizará un ataque estándar. La computadora (Jugador 2) solo puede realizar ataques estándar.

Las partidas se van guardando con los jugadores que van ganando. Si un personaje muere, es eliminado de la lista del archivo Personajes.json, y los datos del jugador que gana son guardados en un archivo JSON llamado Ganador.json, junto con su nombre (el cual es ingresado por el usuario). Cuando la lista de jugadores queda solo con un jugador, o cuando el usuario desea reiniciar la batalla (es decir, borrar el archivo JSON), se crean otra vez 15 personajes aleatorios sin repetición en el documento Personajes.json.

API utilizada para cargar los datos: https://rickandmortyapi.com/api

#Descripción de la API de Rick and Morty 
La API de Rick and Morty proporciona acceso a una base de datos completa de personajes, ubicaciones y episodios de la popular serie animada Rick and Morty. Es una API gratuita y abierta

Características Principales
Personajes: La API permite acceder a datos detallados de todos los personajes de la serie, incluyendo información como nombre, especie, género, estado (vivo, muerto, desconocido), origen, y la primera aparición en un episodio.
Ubicaciones: Se puede obtener información sobre las diversas ubicaciones presentadas en la serie, incluidas sus características y los personajes que residen en ellas.
Episodios: Proporciona detalles sobre los episodios, como el nombre, número de episodio, fecha de emisión, y una lista de personajes que aparecieron en dicho episodio.


#Iniciar el Juego
Ejecución: Inicia el juego ejecutando el comando dotnet run en la terminal.
Selección de Personaje: Se te presentará una lista de personajes disponibles. Selecciona uno introduciendo el número correspondiente.
Batalla: Una vez que hayas seleccionado tu personaje, entrarás en una batalla. Introduce las secuencias de teclas según las instrucciones en pantalla para atacar a tu oponente.
Resultados: Después de la batalla, se mostrará el ganador y se actualizará el historial de ganadores.
Repetir o Salir: Puedes optar por participar en otra batalla o salir del juego.
Historial: Puede ver el historial de los ganadores históricos, este no lo puedes borrar


##Historial de Ganadores
El juego mantiene un registro de todos los ganadores de las partidas en un archivo JSON llamado Ganador.json. Este archivo almacena la información de cada jugador que ha ganado una partida, incluyendo su nombre y detalles sobre la batalla.

Características del Historial:

Registro de Ganadores: Cada vez que un jugador gana una batalla, se guarda una entrada en el archivo Ganador.json con el nombre del jugador y los detalles de la victoria.
Visualización: Puedes consultar el historial para ver una lista de los ganadores anteriores. Esto te permite hacer un seguimiento de tus victorias y comparar tu desempeño con el de otros jugadores.
Inmutabilidad: El historial de ganadores no se puede borrar. Esto asegura que el registro de victorias esté siempre disponible para su consulta y no se pierda ninguna información importante.


##Manejo de Errores al Acceder a la API
En caso de que no se pueda acceder a la API de Rick and Morty, el juego está diseñado para manejar este problema de manera eficiente. Aquí se explica cómo funciona el proceso:

Intento de Conexión a la API:

Al iniciar el juego, el sistema intenta obtener los datos de los personajes desde la API de Rick and Morty. Esto se realiza mediante solicitudes HTTP a la URL de la API.
Error al Conectar con la API:

En caso de que el acceso a la API falle, el sistema intentará leer los datos de un archivo local llamado personajes.json. Este archivo contiene una copia de los datos de los personajes obtenidos previamente.
Si el archivo personajes.json existe, el sistema lo leerá para recuperar los datos de los personajes. Si el archivo no existe o está vacío, el sistema creará una lista vacía de personajes.
Guardado de Datos en el Archivo Local:

Cada vez que se obtienen datos con éxito de la API, estos datos se guardan en el archivo personajes.json. Esto asegura que se tenga una copia local de los datos en caso de que sea necesario volver a usarlos en el futuro.

#A Mejorar#
-agregar más armas
-lugares

*me gustaría que en lugar de acordarse de la claves, pudiese agregar preguntas sobre la serie 