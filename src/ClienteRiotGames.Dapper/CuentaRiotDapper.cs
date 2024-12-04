using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper;
public class CuentaRiotDapper
{
    private readonly IDbConnection _connection;

    public CuentaRiotDapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public void InsertarCuentaRiot(string nombre, string password, string email, byte idServer)
    {
        if (email.Length > 45) throw new ArgumentException("El email es demasiado largo.");

        var parameters = new DynamicParameters();
        parameters.Add("@Nombre", nombre);
        parameters.Add("@Password", password);
        parameters.Add("@Email", email);
        parameters.Add("@IdServer", idServer);
        
        _connection.Execute("InsertarCuentaRiot", parameters, commandType: CommandType.StoredProcedure);
    }

    public uint? ObtenerCuentaRiotId(string email)
   {
       string sql = "SELECT idCuenta FROM CuentaRiot WHERE eMail = @Email";
       return _connection.QuerySingleOrDefault<uint?>(sql, new { Email = email });
   }

    public void ActualizarCuentaRiot(uint idCuenta, int idServer, string email, string password, string nombreUsuario)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuenta", idCuenta);
        parameters.Add("@UnidServer", idServer);
        parameters.Add("@UneMail", email);
        parameters.Add("@UnPassword", password);
        parameters.Add("@UnNombreUsuario", nombreUsuario);
        
        _connection.Execute("ActualizarCuentaRiot", parameters, commandType: CommandType.StoredProcedure);
    }

    public void EliminarCuentaRiot(uint idCuenta)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuenta", idCuenta);
        
        _connection.Execute("EliminarCuentaRiot", parameters, commandType: CommandType.StoredProcedure);
    }

    public CuentaRiot? ObtenerCuentaRiot(uint idCuenta)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidCuenta", idCuenta);
        
        return _connection.QueryFirstOrDefault<CuentaRiot>("ObtenerDetallesCuentaRiot", parameters, 
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<CuentaRiot> ObtenerCuentasRiot()
    {
        return _connection.Query<CuentaRiot>("SELECT * FROM CuentaRiot");
    }

    public IEnumerable<CuentaRiot> ObtenerCuentasRiotPorServer(int idServer)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UnidServer", idServer);
        
        return _connection.Query<CuentaRiot>("ObtenerCuentasRiotPorServer", parameters, 
            commandType: CommandType.StoredProcedure);
    }

    public CuentaRiot? ObtenerCuentaRiotPorEmail(string email)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UneMail", email);
        
        return _connection.QueryFirstOrDefault<CuentaRiot>(
            "SELECT * FROM CuentaRiot WHERE eMail = @UneMail",
            parameters);
    }

    public class LoginResponse
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public CuentaRiotDetalle? Cuenta { get; set; }
    }

    public class CuentaRiotDetalle
    {
        public uint IdCuenta { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NombreServer { get; set; } = string.Empty;
        public string AbreviadoServer { get; set; } = string.Empty;
        public string NombreLOL { get; set; } = string.Empty;
        public string NombreValorant { get; set; } = string.Empty;
    }

    public LoginResponse Login(string email, string password)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UneMail", email);
        parameters.Add("@UnPassword", password);
        parameters.Add("@UnResultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@UnMensaje", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

        var cuenta = _connection.QueryFirstOrDefault<CuentaRiotDetalle>(
            "LoginCuentaRiot",
            parameters,
            commandType: CommandType.StoredProcedure);

        return new LoginResponse
        {
            Resultado = parameters.Get<int>("@UnResultado") == 1,
            Mensaje = parameters.Get<string>("@UnMensaje"),
            Cuenta = cuenta
        };
    }
} 