public class Datos
{
    private string? nombre;
    private string? apodo;
    private DateTime fecha_nac;
    private int edad;
    public int id;
    private string? tipo;

    public string? Nombre
    { get => nombre; set => nombre = value; }

    public string? Apodo
    { get => apodo; set => apodo = value; }

    public DateTime Fecha_nac
    { get => fecha_nac; set => fecha_nac = value; }

    public int Edad
    { get => edad; set => edad = value; }

    public string? Tipo
    { get => tipo; set => tipo = value; }

    public int Id
    { get => id; set => id = value; }
}
