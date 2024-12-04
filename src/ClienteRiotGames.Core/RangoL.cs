namespace ClienteRiotGames.Core;
public class RangoL
{
    public byte IdRangoL { get; set; }
    public string? Nombre { get; set; }
    public int? Numero { get; set; }
    public int? PuntosCompetitivo { get; set; }
    
    public ICollection<CuentaLeagueOfLeguends>? CuentasLOL { get; set; }
} 