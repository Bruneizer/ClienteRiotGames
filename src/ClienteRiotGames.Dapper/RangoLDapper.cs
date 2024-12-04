using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;

public class RangoLDapper
{
    private readonly IDbConnection _connection;

    public RangoLDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public void InsertarRangoL(string nombre, int numero, int puntosCompetitivo)
    {
        if (puntosCompetitivo < 0 || puntosCompetitivo > 100)
            throw new ArgumentException("Los puntos competitivos deben estar entre 0 y 100");
        
        if (numero < 0 || numero > 4)
            throw new ArgumentException("El número de rango debe estar entre 0 y 4");

        var parameters = new DynamicParameters();
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnNumero", numero);
        parameters.Add("@UnPuntosCompetitivo", puntosCompetitivo);
        
        _connection.Execute("InsertarRangoL", parameters, commandType: CommandType.StoredProcedure);
    }

    public void ActualizarRangoL(byte idRangoL, string nombre, int numero, int puntosCompetitivo)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidRangoL", idRangoL);
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnNumero", numero);
        parameters.Add("@UnPuntosCompetitivo", puntosCompetitivo);
        
        _connection.Execute("ActualizarRangoL", parameters, commandType: CommandType.StoredProcedure);
    }

    public void EliminarRangoL(byte idRangoL)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidRangoL", idRangoL);
        
        _connection.Execute("EliminarRangoL", parameters, commandType: CommandType.StoredProcedure);
    }

    public RangoL? ObtenerDetallesRangoL(byte idRangoL)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidRangoL", idRangoL);
        
        return _connection.QueryFirstOrDefault<RangoL>("ObtenerDetallesRangoL", parameters, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<RangoL> ObtenerRangosL()
    {
        return _connection.Query<RangoL>("ObtenerRangosL", commandType: CommandType.StoredProcedure);
    }

    public RangoL? ObtenerRangoL(byte numero)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Numero", numero);
        
        return _connection.QueryFirstOrDefault<RangoL>(
            "SELECT * FROM RangoL WHERE Numero = @Numero", 
            parameters);
    }
} 