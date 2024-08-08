void main()
{

    ReproductorDeMusica.ReproducirMusica("Musica/themeRemix.wav");
    App app = new();
    app.Iniciar();
    ReproductorDeMusica.DetenerMusica();
}

// Llamar a la función Main para iniciar el programa
main();
