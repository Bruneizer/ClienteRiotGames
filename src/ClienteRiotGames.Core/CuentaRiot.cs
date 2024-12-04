namespace ClienteRiotGames.Core;
public class CuentaRiot
{
    public uint IdCuenta { get; set; }
    public string? Nombre { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public byte IdServer { get; set; }
    
    public Server? Server { get; set; }
    public CuentaValorant? CuentaValorant { get; set; }
    public CuentaLeagueOfLeguends? CuentaLeagueOfLeguends { get; set; }
} 