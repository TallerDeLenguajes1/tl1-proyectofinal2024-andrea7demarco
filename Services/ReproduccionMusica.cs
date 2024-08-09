using NAudio.Wave;

public static class ReproduccionMusica
{
    private static WaveOutEvent? waveOut;
    private static AudioFileReader? audioFileReader;

    public static void ReproducirMusica(string rutaArchivo)
    {
        DetenerMusica(); // control 
        audioFileReader = new AudioFileReader(rutaArchivo);
        waveOut.Init(audioFileReader);
        waveOut.Play(); // Reproduce el audio
    }

    public static void DetenerMusica()
    {
        if (waveOut != null)
        {
            waveOut.Stop();
            waveOut.Dispose();
            waveOut = null;
        }

        if (audioFileReader != null)
        {
            audioFileReader.Dispose();
            audioFileReader = null;
        }
    }
}
