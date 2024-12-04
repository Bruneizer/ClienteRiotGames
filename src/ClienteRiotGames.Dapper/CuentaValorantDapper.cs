using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;

public class CuentaValorantDapper
{
    private readonly IDbConnection _connection;

    public CuentaValorantDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public void InsertarCuentaValorant(uint idCuenta, byte idRangoV, string nombre, int nivel, int experiencia)
    {
        var cuentaValorant = new CuentaValorant
        {
            IdCuenta = idCuenta,
            IdRangoV = idRangoV,
            Nombre = nombre,
            Nivel = nivel,
            Experiencia = experiencia
        };

        // Validar experiencia antes de la inserción
        cuentaValorant.ValidarExperiencia();

        string sql = "CALL InsertarCuentaValorant(@IdCuenta, @IdRangoV, @Nombre, @Nivel, @Experiencia)";
        _connection.Execute(sql, cuentaValorant);
    }

    public void ActualizarCuentaValorant(uint idCuentaV, string nombre, int nivel, int experiencia)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuentaV", idCuentaV);
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnNivel", nivel);
        parameters.Add("@UnExperiencia", experiencia);
        
        _connection.Execute("ActualizarCuentaValorant", parameters, commandType: CommandType.StoredProcedure);
    }

    public void EliminarCuentaValorant(uint idCuentaV)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuentaV", idCuentaV);
        
        _connection.Execute("EliminarCuentaValorant", parameters, commandType: CommandType.StoredProcedure);
    }

    public CuentaValorant? ObtenerCuentaValorant(int idCuentaV)
    {
        var cuentaValorant = _connection.QueryFirstOrDefault<CuentaValorant>(
            "ObtenerCuentaValorant", 
            new { IdCuentaV = idCuentaV }, 
            commandType: CommandType.StoredProcedure);

        return cuentaValorant ?? throw new InvalidOperationException("Cuenta Valorant no encontrada.");
    }

    public IEnumerable<CuentaValorant> ObtenerCuentasValorant()
    {
        return _connection.Query<CuentaValorant>("ObtenerCuentasValorant", commandType: CommandType.StoredProcedure);
    }
} 