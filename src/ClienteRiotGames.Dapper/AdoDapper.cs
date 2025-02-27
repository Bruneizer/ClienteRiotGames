using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper
{
    public class AdoDapper : IAdo
    {
        private readonly IDbConnection _dbConnection;

        public AdoDapper(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        #region Server

        public void InsertarServer(string nombre, string abreviado)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@UnNombre", nombre);
            parametros.Add("@UnAbreviado", abreviado);
            _dbConnection.Execute("InsertarServer", parametros, commandType: CommandType.StoredProcedure);
        }

        public int ObtenerServerId(string nombre)
            => _dbConnection.QuerySingle<int>("SELECT idServer FROM Server WHERE Nombre = @Nombre", new { Nombre = nombre });

        public void ActualizarServer(int idServer, string nombre, string abreviado)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@UnidServer", idServer);
            parametros.Add("@UnNombre", nombre);
            parametros.Add("@UnAbreviado", abreviado);
            _dbConnection.Execute("ActualizarServer", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarServer(int idServer)
            => _dbConnection.Execute("EliminarServer", new { UnidServer = idServer }, commandType: CommandType.StoredProcedure);

        public Server? ObtenerServer(int idServer)
            => _dbConnection.QueryFirstOrDefault<Server>("ObtenerServer", new { UnidServer = idServer }, commandType: CommandType.StoredProcedure);

        public IEnumerable<Server> ObtenerServers()
            => _dbConnection.Query<Server>("SELECT * FROM Server");

        public Server? ObtenerDetallesServer(int idServer)
        {
            return _dbConnection.Query<Server, int, Server>(
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

        #region CuentaRiot

        public void InsertarCuentaRiot(string nombre, string password, string email, byte idServer)
            => _dbConnection.Execute("InsertarCuentaRiot", new { Nombre = nombre, Password = password, Email = email, IdServer = idServer }, commandType: CommandType.StoredProcedure);

        public uint ObtenerCuentaRiotId(string email)
            => _dbConnection.ExecuteScalar<uint>("SELECT idCuenta FROM CuentaRiot WHERE eMail = @Email", new { Email = email });

        public void ActualizarCuentaRiot(uint idCuenta, string nombre, string password, string email)
            => _dbConnection.Execute("ActualizarCuentaRiot", new { UnidCuenta = idCuenta, UnNombreUsuario = nombre, UnPassword = password, UneMail = email }, commandType: CommandType.StoredProcedure);

        public void EliminarCuentaRiot(uint idCuenta)
            => _dbConnection.Execute("EliminarCuentaRiot", new { UnidCuenta = idCuenta }, commandType: CommandType.StoredProcedure);

        public CuentaRiot? ObtenerCuentaRiot(uint idCuenta)
            => _dbConnection.QueryFirstOrDefault<CuentaRiot>("ObtenerDetallesCuentaRiot", new { UnidCuenta = idCuenta }, commandType: CommandType.StoredProcedure);

        public IEnumerable<CuentaRiot> ObtenerCuentasRiot()
            => _dbConnection.Query<CuentaRiot>("SELECT * FROM CuentaRiot");

        #endregion

        #region CuentaValorant

        public void InsertarCuentaValorant(string nombre, int nivel, int experiencia, uint idCuenta, byte idRangoV)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UnidCuenta", idCuenta, DbType.UInt32);
            parameters.Add("UnidRangoV", idRangoV, DbType.Byte);
            parameters.Add("UnNombre", nombre, DbType.String);
            parameters.Add("UnNivel", nivel, DbType.Int32);
            parameters.Add("UnExperiencia", experiencia, DbType.Int32);

            _dbConnection.Execute("InsertarCuentaValorant", parameters, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarCuentaValorant(uint idCuentaV, string nombre, int nivel, int experiencia)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UnidCuentaV", idCuentaV, DbType.UInt32);
            parameters.Add("UnNombre", nombre, DbType.String);
            parameters.Add("UnNivel", nivel, DbType.Int32);
            parameters.Add("UnExperiencia", experiencia, DbType.Int32);

            _dbConnection.Execute("ActualizarCuentaValorant", parameters, commandType: CommandType.StoredProcedure);
        }

        public void EliminarCuentaValorant(uint idCuentaV)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UnidCuentaV", idCuentaV, DbType.UInt32);

            _dbConnection.Execute("EliminarCuentaValorant", parameters, commandType: CommandType.StoredProcedure);
        }

        public CuentaValorant? ObtenerCuentaValorant(uint idCuentaV)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UnidCuentaV", idCuentaV, DbType.UInt32);

            return _dbConnection.QueryFirstOrDefault<CuentaValorant>("ObtenerDetallesCuentaValorant", parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<CuentaValorant> ObtenerCuentasValorant()
        {
            return _dbConnection.Query<CuentaValorant>("SELECT * FROM CuentaValorant");
        }

        #endregion

        #region RangoL

        public void InsertarRangoL(string nombre, int numero, int puntosCompetitivo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unNumero", numero);
            parametros.Add("@unPuntosCompetitivo", puntosCompetitivo);

            _dbConnection.Execute("InsertarRangoL", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarRangoL(byte idRangoL, string nombre, int numero, int puntosCompetitivo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoL", idRangoL);
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unNumero", numero);
            parametros.Add("@unPuntosCompetitivo", puntosCompetitivo);

            _dbConnection.Execute("ActualizarRangoL", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarRangoL(byte idRangoL)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoL", idRangoL);

            _dbConnection.Execute("EliminarRangoL", parametros, commandType: CommandType.StoredProcedure);
        }

        public RangoL? ObtenerRangoL(byte idRangoL)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoL", idRangoL);

            return _dbConnection.QueryFirstOrDefault<RangoL>("ObtenerDetallesRangoL", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<RangoL> ObtenerRangosL()
        {
            const string query = "SELECT * FROM RangoL";
            return _dbConnection.Query<RangoL>(query).ToList();
        }

        #endregion

        #region RangoV

        public void InsertarRangoV(string nombre, int numero, int puntosCompetitivo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unNumero", numero);
            parametros.Add("@unPuntosCompetitivo", puntosCompetitivo);

            _dbConnection.Execute("InsertarRangoV", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarRangoV(byte idRangoV, string nombre, int numero, int puntosCompetitivo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoV", idRangoV);
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unNumero", numero);
            parametros.Add("@unPuntosCompetitivo", puntosCompetitivo);

            _dbConnection.Execute("ActualizarRangoV", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarRangoV(byte idRangoV)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoV", idRangoV);

            _dbConnection.Execute("EliminarRangoV", parametros, commandType: CommandType.StoredProcedure);
        }

        public RangoV? ObtenerRangoV(byte idRangoV)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoV", idRangoV);

            return _dbConnection.QueryFirstOrDefault<RangoV>("ObtenerDetallesRangoV", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<RangoV> ObtenerRangosV()
        {
            const string query = "SELECT * FROM RangoV";
            return _dbConnection.Query<RangoV>(query).ToList();
        }

        #endregion

        #region Inventario

        public void InsertarInventario(uint idCuentaL, uint esenciaAzul, uint puntosRiot)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdCuentaL", idCuentaL);
            parametros.Add("@unEsenciaAzul", esenciaAzul);
            parametros.Add("@unPuntosRiot", puntosRiot);

            _dbConnection.Execute("InsertarInventario", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarInventario(uint idInventario, uint esenciaAzul, uint puntosRiot)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdInventario", idInventario);
            parametros.Add("@unEsenciaAzul", esenciaAzul);
            parametros.Add("@unPuntosRiot", puntosRiot);

            _dbConnection.Execute("ActualizarPuntosInventario", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarInventario(uint idInventario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdInventario", idInventario);

            _dbConnection.Execute("EliminarInventario", parametros, commandType: CommandType.StoredProcedure);
        }

        public Inventario? ObtenerInventario(uint idInventario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdInventario", idInventario);

            return _dbConnection.QueryFirstOrDefault<Inventario>("ObtenerDetallesInventario", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Inventario> ObtenerInventarios()
        {
            const string query = "SELECT * FROM Inventario";
            return _dbConnection.Query<Inventario>(query).ToList();
        }

        #endregion

        #region TipoObjeto

        public void InsertarTipoObjeto(string nombre)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unNombre", nombre);

            _dbConnection.Execute("InsertarTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarTipoObjeto(int idTipoObjeto, string nombre)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);
            parametros.Add("@unNombre", nombre);

            _dbConnection.Execute("ActualizarTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarTipoObjeto(int idTipoObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);

            _dbConnection.Execute("EliminarTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public TipoObjeto? ObtenerTipoObjeto(int idTipoObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);

            return _dbConnection.QueryFirstOrDefault<TipoObjeto>("ObtenerDetallesTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<TipoObjeto> ObtenerTiposObjeto()
        {
            const string query = "SELECT * FROM TipoObjeto";
            return _dbConnection.Query<TipoObjeto>(query).ToList();
        }

        #endregion

        #region Objeto

        public void InsertarObjeto(int idTipoObjeto, string nombre, uint precioEA, uint precioRP)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unPrecioEA", precioEA);
            parametros.Add("@unPrecioRP", precioRP);

            _dbConnection.Execute("InsertarObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarObjeto(int idObjeto, string nombre, uint precioEA, uint precioRP, int idTipoObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdObjeto", idObjeto);
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unPrecioEA", precioEA);
            parametros.Add("@unPrecioRP", precioRP);
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);

            _dbConnection.Execute("ActualizarObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarObjeto(int idObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdObjeto", idObjeto);

            _dbConnection.Execute("EliminarObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public Objeto? ObtenerObjeto(int idObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdObjeto", idObjeto);

            return _dbConnection.QueryFirstOrDefault<Objeto>("ObtenerDetallesObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Objeto> ObtenerObjetos()
        {
            const string query = "SELECT * FROM Objeto";
            return _dbConnection.Query<Objeto>(query).ToList();
        }

        #endregion

        #region CuentaLeagueOfLeguends

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

        #endregion

        #region InventarioObjeto

        public void InsertarInventarioObjeto(int idInventario, int idObjeto, byte cantidad)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdInventario", idInventario);
            parametros.Add("@unIdObjeto", idObjeto);
            parametros.Add("@unCantidad", cantidad);

            _dbConnection.Execute("InsertarInventarioObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarInventarioObjeto(int idInventario, int idObjeto, byte cantidad)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdInventario", idInventario);
            parametros.Add("@unIdObjeto", idObjeto);
            parametros.Add("@unCantidad", cantidad);

            _dbConnection.Execute("ActualizarInventarioObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarInventarioObjeto(int idInventario, int idObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdInventario", idInventario);
            parametros.Add("@unIdObjeto", idObjeto);

            _dbConnection.Execute("EliminarInventarioObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<InventarioObjeto> ObtenerInventariosObjeto()
        {
            const string query = "SELECT * FROM InventarioObjeto";
            return _dbConnection.Query<InventarioObjeto>(query).ToList();
        }

        #endregion
    }
}