using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;

public class ServerDapper
{
    private readonly IDbConnection _connection;

    public ServerDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public byte InsertarServer(string nombre, string abreviado)
    {
        // Verificar si ya existe un servidor con ese nombre
        var existingServer = _connection.QueryFirstOrDefault<Server>(
            "SELECT * FROM Server WHERE Nombre = @Nombre",
            new { Nombre = nombre });

        if (existingServer != null)
        {
            return existingServer.IdServer; // Retornamos el ID del servidor existente
        }

        var parameters = new DynamicParameters();
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnAbreviado", abreviado);
        
        _connection.Execute("InsertarServer", parameters, commandType: CommandType.StoredProcedure);
        
        // Obtener el ID del servidor recién insertado
        var newServer = _connection.QueryFirstOrDefault<Server>(
            "SELECT * FROM Server WHERE Nombre = @Nombre",
            new { Nombre = nombre });
            
        return newServer?.IdServer ?? 0;
    }

    public Server ObtenerDetallesServer(byte serverId)
    {
        string sql = "SELECT IdServer, Nombre, Abreviado FROM Server WHERE IdServer = @IdServer";
        return _connection.QuerySingleOrDefault<Server>(sql, new { IdServer = serverId });
    }

    public int ObtenerServerId(string nombre)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnNombre", nombre);
        
        return _connection.QuerySingle<int>("SELECT idServer FROM Server WHERE Nombre = @UnNombre", parameters);
    }

    public void EliminarServer(byte idServer)
    {
        string sql = "CALL EliminarServer(@IdServer)";
        _connection.Execute(sql, new { IdServer = idServer });
    }

    public void ActualizarServer(int idServer, string nombre, string abreviado)
    {
        if (nombre.Length > 45) nombre = nombre.Substring(0, 45);
        if (abreviado.Length > 5) abreviado = abreviado.Substring(0, 5);

        var parameters = new DynamicParameters();
        parameters.Add("@UnidServer", idServer);
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnAbreviado", abreviado);
        
        _connection.Execute("ActualizarServer", parameters, commandType: CommandType.StoredProcedure);
    }

    public void EliminarServer(int idServer)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidServer", idServer);
        
        _connection.Execute("EliminarServer", parameters, commandType: CommandType.StoredProcedure);
    }

    public Server ObtenerServer(int idServer)
    {
        var server = _connection.QueryFirstOrDefault<Server>(
            "ObtenerDetallesServer", 
            new { UnidServer = idServer }, 
            commandType: CommandType.StoredProcedure);

        return server ?? throw new InvalidOperationException("Servidor no encontrado.");
    }

    public IEnumerable<Server> ObtenerServers()
    {
        return _connection.Query<Server>("SELECT * FROM Server");
    }

    public Server? ObtenerDetallesServer(int idServer)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidServer", idServer);
        
        return _connection.QueryFirstOrDefault<Server>("ObtenerDetallesServer", parameters, commandType: CommandType.StoredProcedure);
    }
} 