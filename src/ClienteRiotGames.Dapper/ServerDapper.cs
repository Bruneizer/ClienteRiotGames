using System.Data;
using Dapper;
using MySqlConnector;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;

    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;
    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);

    #region Server
    
    public void InsertarServer(string nombre, string abreviado)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@UnNombre", nombre);
        parametros.Add("@UnAbreviado", abreviado);
        _conexion.Execute("InsertarServer", parametros, commandType: CommandType.StoredProcedure);
    }

    public int ObtenerServerId(string nombre)
        => _conexion.QuerySingle<int>("SELECT idServer FROM Server WHERE Nombre = @Nombre", new { Nombre = nombre });

    public void ActualizarServer(int idServer, string nombre, string abreviado)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@UnidServer", idServer);
        parametros.Add("@UnNombre", nombre);
        parametros.Add("@UnAbreviado", abreviado);
        _conexion.Execute("ActualizarServer", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarServer(int idServer)
        => _conexion.Execute("EliminarServer", new { UnidServer = idServer }, commandType: CommandType.StoredProcedure);

    public Server? ObtenerServer(int idServer)
        => _conexion.QueryFirstOrDefault<Server>("ObtenerServer", new { UnidServer = idServer }, commandType: CommandType.StoredProcedure);

    public IEnumerable<Server> ObtenerServers()
        => _conexion.Query<Server>("SELECT * FROM Server");

    public Server? ObtenerDetallesServer(int idServer)
    {
        return _conexion.Query<Server, int, Server>(
            "ObtenerDetallesServer",
            (server, totalCuentas) =>
            {
                server.CuentasRiot = new List<CuentaRiot>(totalCuentas);
                return server;
            },
            new { UnidServer = idServer },
            splitOn: "TotalCuentas",
            commandType: CommandType.StoredProcedure
        ).FirstOrDefault();
    }
    
    #endregion
}
