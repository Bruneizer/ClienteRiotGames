using System.Data;
using Dapper;
using MySqlConnector;
using ClienteRiotGames.Core;
using System.Collections.Generic;

namespace ClienteRiotGames.Dapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;

    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;
    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);

    #region CuentaRiot

    public void InsertarCuentaRiot(string nombre, string password, string email, byte idServer)
        => _conexion.Execute("InsertarCuentaRiot", new { Nombre = nombre, Password = password, Email = email, IdServer = idServer }, commandType: CommandType.StoredProcedure);

    public uint ObtenerCuentaRiotId(string email)
        => _conexion.ExecuteScalar<uint>("SELECT idCuenta FROM CuentaRiot WHERE eMail = @Email", new { Email = email });

    public void ActualizarCuentaRiot(uint idCuenta, string nombre, string password, string email)
        => _conexion.Execute("ActualizarCuentaRiot", new { UnidCuenta = idCuenta, UnNombreUsuario = nombre, UnPassword = password, UneMail = email }, commandType: CommandType.StoredProcedure);

    public void EliminarCuentaRiot(uint idCuenta)
        => _conexion.Execute("EliminarCuentaRiot", new { UnidCuenta = idCuenta }, commandType: CommandType.StoredProcedure);

    public CuentaRiot? ObtenerCuentaRiot(uint idCuenta)
        => _conexion.QueryFirstOrDefault<CuentaRiot>("ObtenerDetallesCuentaRiot", new { UnidCuenta = idCuenta }, commandType: CommandType.StoredProcedure);

    public IEnumerable<CuentaRiot> ObtenerCuentasRiot()
        => _conexion.Query<CuentaRiot>("SELECT * FROM CuentaRiot");

    #endregion
}
