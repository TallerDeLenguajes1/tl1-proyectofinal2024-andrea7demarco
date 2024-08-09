using NAudio.Wave;

public static class ReproduccionMusica
{
    private static WaveOutEvent? _waveOut;
    private static AudioFileReader? _audioFileReader;

    public static void ReproducirMusica(string rutaArchivo)
    {
        DetenerMusica(); // 
        _audioFileReader = new AudioFileReader(rutaArchivo);
        _waveOut = new WaveOutEvent
        {
            Volume = 0.5f // Ajusta el volumen si es necesario
        };
        _waveOut.Init(_audioFileReader);
        _waveOut.Play(); // Reproduce el audio
    }

    public static void DetenerMusica()
    {
        if (_waveOut != null)
        {
            _waveOut.Stop();
            _waveOut.Dispose();
            _waveOut = null;
        }

        if (_audioFileReader != null)
        {
            _audioFileReader.Dispose();
            _audioFileReader = null;
        }
    }
}
