using Dapper;
using System.Data;
using System.Data.SqlClient;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.AdoDapper;

public class AdoDapper : IAdo
{
    private readonly IDbConnection _dbConnection;

    public AdoDapper(string connectionString)
    {
        _dbConnection = new SqlConnection(connectionString);
    }

    public void InsertarCuentaLOL(string nombre, int nivel, int puntosCompetitivo, uint idCuenta, byte idRangoL)
    {
        var parametros = new DynamicParameters();
        parametros.Add("UnidCuenta", idCuenta, DbType.UInt32);
        parametros.Add("UnidRangoL", idRangoL, DbType.Byte);
        parametros.Add("UnNombre", nombre, DbType.String, size: 50);
        parametros.Add("UnNivel", nivel, DbType.Int32);
        parametros.Add("UnPuntosCompetitivo", puntosCompetitivo, DbType.Int32);

        _dbConnection.Execute("InsertarCuentaLOL", parametros, commandType: CommandType.StoredProcedure);
    }

    public void ActualizarCuentaLOL(uint idCuentaL, string nombre, int nivel, int puntosCompetitivo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("UnidCuentaL", idCuentaL, DbType.UInt32);
        parametros.Add("UnNombre", nombre, DbType.String, size: 50);
        parametros.Add("UnNivel", nivel, DbType.Int32);
        parametros.Add("UnPuntosCompetitivo", puntosCompetitivo, DbType.Int32);

        _dbConnection.Execute("ActualizarRangoLOL", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarCuentaLOL(uint idCuentaL)
    {
        var parametros = new DynamicParameters();
        parametros.Add("UnidCuentaL", idCuentaL, DbType.UInt32);

        _dbConnection.Execute("EliminarCuentaLOL", parametros, commandType: CommandType.StoredProcedure);
    }

    public CuentaLeagueOfLeguends? ObtenerCuentaLOL(uint idCuentaL)
    {
        var parametros = new DynamicParameters();
        parametros.Add("UnidCuentaL", idCuentaL, DbType.UInt32);

        return _dbConnection.QueryFirstOrDefault<CuentaLeagueOfLeguends>("ObtenerDetallesCuentaLOL", parametros, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<CuentaLeagueOfLeguends> ObtenerCuentasLOL()
    {
        return _dbConnection.Query<CuentaLeagueOfLeguends>("SELECT * FROM CuentaLeagueOfLeguends");
    }
}
