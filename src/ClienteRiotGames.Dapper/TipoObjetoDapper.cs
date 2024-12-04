using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;

public class TipoObjetoDapper
{
    private readonly IDbConnection _connection;

    public TipoObjetoDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public void InsertarTipoObjeto(string nombre)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnNombre", nombre);
        
        _connection.Execute("InsertarTipoObjeto", parameters, commandType: CommandType.StoredProcedure);
    }

    public TipoObjeto? ObtenerTipoObjeto(uint idTipoObjeto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidTipoObjeto", idTipoObjeto);
        
        return _connection.QueryFirstOrDefault<TipoObjeto>(
            "SELECT * FROM TipoObjeto WHERE IdTipoObjeto = @UnidTipoObjeto",
            parameters);
    }

    public IEnumerable<TipoObjeto> ObtenerTiposObjeto()
    {
        return _connection.Query<TipoObjeto>("SELECT * FROM TipoObjeto");
    }

    public void EliminarTipoObjeto(uint idTipoObjeto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidTipoObjeto", idTipoObjeto);
        
        _connection.Execute("EliminarTipoObjeto", parameters, commandType: CommandType.StoredProcedure);
    }
} 