using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;

public class CuentaLeagueOfLeguendsDapper
{
    private readonly IDbConnection _connection;

    public CuentaLeagueOfLeguendsDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public void InsertarCuentaLOL(uint idCuenta, byte idRangoL, string nombre, int nivel, int puntosCompetitivo)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuenta", idCuenta);
        parameters.Add("@UnidRangoL", idRangoL);
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnNivel", nivel);
        parameters.Add("@UnPuntosCompetitivo", puntosCompetitivo);
        
        _connection.Execute("InsertarCuentaLOL", parameters, commandType: CommandType.StoredProcedure);
    }

    public void ActualizarCuentaLOL(uint idCuentaL, string nombre, int nivel, int puntosCompetitivo)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuentaL", idCuentaL);
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnNivel", nivel);
        parameters.Add("@UnPuntosCompetitivo", puntosCompetitivo);
        
        _connection.Execute("ActualizarCuentaLOL", parameters, commandType: CommandType.StoredProcedure);
    }

    public void EliminarCuentaLOL(uint idCuentaL)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuentaL", idCuentaL);
        
        _connection.Execute("EliminarCuentaLOL", parameters, commandType: CommandType.StoredProcedure);
    }

    public CuentaLeagueOfLeguends? ObtenerCuentaLOL(uint idCuentaL)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuentaL", idCuentaL);
        
        return _connection.QueryFirstOrDefault<CuentaLeagueOfLeguends>("SELECT * FROM CuentaLeagueOfLeguends WHERE idCuentaL = @UnidCuentaL", parameters);
    }

    public IEnumerable<CuentaLeagueOfLeguends> ObtenerCuentasLOL()
    {
        return _connection.Query<CuentaLeagueOfLeguends>("SELECT * FROM CuentaLeagueOfLeguends");
    }
} 