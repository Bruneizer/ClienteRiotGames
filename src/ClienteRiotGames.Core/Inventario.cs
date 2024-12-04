

namespace ClienteRiotGames.Core;
public class Inventario
{
    public uint IdInventario { get; set; }
    public uint IdCuentaL { get; set; }
    public uint? EsenciaAzul { get; set; }
    public uint? PuntosRiot { get; set; }
    
    public CuentaLeagueOfLeguends? CuentaLeagueOfLeguends { get; set; }
} 