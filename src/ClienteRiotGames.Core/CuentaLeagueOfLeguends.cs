namespace ClienteRiotGames.Core;
public class CuentaLeagueOfLeguends
{
    public uint IdCuentaL { get; set; }
    public string? Nombre { get; set; }
    public int? Nivel { get; set; }
    public int? PuntosCompetitivo { get; set; }
    public uint IdCuenta { get; set; }
    public byte IdRangoL { get; set; }
    
    public CuentaRiot CuentaRiot { get; set; } = new CuentaRiot();
    public RangoL RangoL { get; set; } = new RangoL();
    public Inventario? Inventario { get; set; }
} 