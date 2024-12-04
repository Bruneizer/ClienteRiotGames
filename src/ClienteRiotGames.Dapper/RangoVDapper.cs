using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;

public class RangoVDapper
{
    private readonly IDbConnection _connection;

    public RangoVDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public void InsertarRangoV(string nombre, int numero, int puntosCompetitivo)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnNumero", numero);
        parameters.Add("@UnPuntosCompetitivo", puntosCompetitivo);
        
        _connection.Execute("InsertarRangoV", parameters, commandType: CommandType.StoredProcedure);
    }

    public RangoV? ObtenerRangoVPorNombre(string nombre)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnNombre", nombre);
        
        return _connection.QueryFirstOrDefault<RangoV>(
            "SELECT * FROM RangoV WHERE Nombre = @UnNombre",
            parameters);
    }

    public void ActualizarRangoV(byte idRangoV, string nombre, int numero, int puntosCompetitivo)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidRangoV", idRangoV);
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnNumero", numero);
        parameters.Add("@UnPuntosCompetitivo", puntosCompetitivo);
        
        _connection.Execute("ActualizarRangoV", parameters, commandType: CommandType.StoredProcedure);
    }

    public void EliminarRangoV(byte idRangoV)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidRangoV", idRangoV);
        
        _connection.Execute("EliminarRangoV", parameters, commandType: CommandType.StoredProcedure);
    }

    public RangoV? ObtenerRangoV(byte idRangoV)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidRangoV", idRangoV);
        
        return _connection.QueryFirstOrDefault<RangoV>(
            "SELECT * FROM RangoV WHERE idRangoV = @UnidRangoV",
            parameters);
    }

    public IEnumerable<RangoV> ObtenerRangosV()
    {
        return _connection.Query<RangoV>("ObtenerRangosV", commandType: CommandType.StoredProcedure);
    }
} 