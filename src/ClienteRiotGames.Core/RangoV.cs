namespace ClienteRiotGames.Core;
public class RangoV
{
    public byte IdRangoV { get; set; }
    public string? Nombre { get; set; }
    public int? Numero { get; set; }
    public int? PuntosCompetitivo { get; set; }
    
    public ICollection<CuentaValorant>? CuentasValorant { get; set; }
} 