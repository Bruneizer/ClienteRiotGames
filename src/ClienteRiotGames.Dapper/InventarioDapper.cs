using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;

public class InventarioDapper
{
    private readonly IDbConnection _connection;

    public InventarioDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public void InsertarInventario(uint idCuentaL, uint esenciaAzul, uint puntosRiot)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuentaL", idCuentaL);
        parameters.Add("@UnEsenciaAzul", esenciaAzul);
        parameters.Add("@UnPuntosRiot", puntosRiot);
        
        _connection.Execute("InsertarInventario", parameters, commandType: CommandType.StoredProcedure);
    }

    public void ActualizarInventario(uint idInventario, uint esenciaAzul, uint puntosRiot)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidInventario", idInventario);
        parameters.Add("@UnEsenciaAzul", esenciaAzul);
        parameters.Add("@UnPuntosRiot", puntosRiot);
        
        _connection.Execute("ActualizarInventario", parameters, commandType: CommandType.StoredProcedure);
    }

    public void EliminarInventario(uint idInventario)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidInventario", idInventario);
        
        _connection.Execute("EliminarInventario", parameters, commandType: CommandType.StoredProcedure);
    }

    public Inventario? ObtenerInventario(uint idInventario)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidInventario", idInventario);
        
        return _connection.QueryFirstOrDefault<Inventario>("ObtenerInventario", parameters, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Inventario> ObtenerInventarios()
    {
        return _connection.Query<Inventario>("ObtenerInventarios", commandType: CommandType.StoredProcedure);
    }

    public Inventario ObtenerInventarioPorIdCuenta(uint idCuentaL)
    {
        var query = "SELECT * FROM Inventario WHERE idCuentaL = @IdCuentaL LIMIT 1";
        return _connection.QuerySingleOrDefault<Inventario>(query, new { IdCuentaL = idCuentaL });
    }
} 