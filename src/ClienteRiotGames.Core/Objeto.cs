namespace ClienteRiotGames.Core;
public class Objeto
{
    public uint IdObjeto { get; set; }
    public string? Nombre { get; set; }
    public uint? PrecioEsenciaAzul { get; set; }
    public uint? PrecioRiotPoints { get; set; }
    public uint IdTipoObjeto { get; set; }
    
    public TipoObjeto? TipoObjeto { get; set; }
} 