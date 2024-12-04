using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;

public class ObjetoDapper
{
    private readonly IDbConnection _connection;
    
    public ObjetoDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public void InsertarObjeto(uint idTipoObjeto, string nombre, uint precioEA, uint precioRP)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidTipoObjeto", idTipoObjeto);
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnPrecioEA", precioEA);
        parameters.Add("@UnPrecioRP", precioRP);
        
        _connection.Execute("InsertarObjeto", parameters, commandType: CommandType.StoredProcedure);
    }

    public void ActualizarObjeto(uint idObjeto, string nombre, uint precioEA, uint precioRP, uint idTipoObjeto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidObjeto", idObjeto);
        parameters.Add("@UnNombre", nombre);
        parameters.Add("@UnPrecioEA", precioEA);
        parameters.Add("@UnPrecioRP", precioRP);
        parameters.Add("@UnidTipoObjeto", idTipoObjeto);
        
        _connection.Execute("ActualizarObjeto", parameters, commandType: CommandType.StoredProcedure);
    }

    public void EliminarObjeto(uint idObjeto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidObjeto", idObjeto);
        
        _connection.Execute("EliminarObjeto", parameters, commandType: CommandType.StoredProcedure);
    }

    public Objeto? ObtenerObjeto(uint idObjeto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidObjeto", idObjeto);
        
        return _connection.QueryFirstOrDefault<Objeto>(
            "SELECT * FROM Objeto WHERE idObjeto = @UnidObjeto",
            parameters);
    }

    public IEnumerable<Objeto> ObtenerObjetos()
    {
        return _connection.Query<Objeto>("ObtenerObjetos", commandType: CommandType.StoredProcedure);
    }

    public Objeto? ObtenerObjetoPorNombre(string nombre)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnNombre", nombre);
        
        return _connection.QueryFirstOrDefault<Objeto>(
            "SELECT * FROM Objeto WHERE Nombre = @UnNombre",
            parameters);
    }
} 