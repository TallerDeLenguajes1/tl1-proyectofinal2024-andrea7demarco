using NAudio.Wave;
using System;
void main()
{

    ReproduccionMusica.ReproducirMusica("Musica/themeRemix.wav");
    App app = new();
    app.Iniciar();
    ReproduccionMusica.DetenerMusica();
}

// Llamar a la función Main para iniciar el programa
main();
