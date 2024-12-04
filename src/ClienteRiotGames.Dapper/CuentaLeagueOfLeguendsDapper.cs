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
        try
        {
            _connection.Open();
            using var transaction = _connection.BeginTransaction();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UnidCuentaL", idCuentaL);

                // Primero eliminamos registros relacionados en Inventario
                _connection.Execute(
                    "DELETE FROM Inventario WHERE idCuentaL = @UnidCuentaL",
                    parameters,
                    transaction: transaction
                );

                // Luego eliminamos la cuenta LOL
                _connection.Execute(
                    "DELETE FROM CuentaLeagueOfLeguends WHERE idCuentaL = @UnidCuentaL",
                    parameters,
                    transaction: transaction
                );

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        finally
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }

    public CuentaLeagueOfLeguends? ObtenerCuentaLOL(uint idCuentaL)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuentaL", idCuentaL);
        
        return _connection.QueryFirstOrDefault<CuentaLeagueOfLeguends>(
            "SELECT * FROM CuentaLeagueOfLeguends WHERE idCuentaL = @UnidCuentaL", 
            parameters
        );
    }

    public IEnumerable<CuentaLeagueOfLeguends> ObtenerCuentasLOL()
    {
        return _connection.Query<CuentaLeagueOfLeguends>("SELECT * FROM CuentaLeagueOfLeguends");
    }

    public void ActualizarRangoLOL(uint idCuentaL, byte idRangoL, int puntosCompetitivo)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuentaL", idCuentaL);
        parameters.Add("@UnidRangoL", idRangoL);
        parameters.Add("@UnPuntosCompetitivo", puntosCompetitivo);
        
        _connection.Execute(
            "UPDATE CuentaLeagueOfLeguends SET idRangoL = @UnidRangoL, PuntosCompetitivo = @UnPuntosCompetitivo WHERE idCuentaL = @UnidCuentaL",
            parameters
        );
    }

    public CuentaLeagueOfLeguends? ObtenerDetallesCuentaLOL(uint idCuentaL)
    {
        var sql = @"
            SELECT 
                l.*,
                r.Nombre as NombreRango,
                cr.Nombre as NombreCuentaRiot,
                cr.eMail,
                i.EsenciaAzul,
                i.PuntosRiot
            FROM CuentaLeagueOfLeguends l
            LEFT JOIN RangoL r ON l.idRangoL = r.idRangoL
            LEFT JOIN CuentaRiot cr ON l.idCuenta = cr.idCuenta
            LEFT JOIN Inventario i ON l.idCuentaL = i.idCuentaL
            WHERE l.idCuentaL = @UnidCuentaL";

        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuentaL", idCuentaL);

        return _connection.QueryFirstOrDefault<CuentaLeagueOfLeguends>(sql, parameters);
    }
}