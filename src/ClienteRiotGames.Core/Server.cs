namespace ClienteRiotGames.Core;
public class Server
{
    public byte IdServer { get; set; }
    public string? Nombre { get; set; }
    public string? Abreviado { get; set; }
    public ICollection<CuentaRiot>? CuentasRiot { get; set; }
} 