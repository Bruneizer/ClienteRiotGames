using System.Collections.Generic;
using Dapper;
using System.Data;
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
            => _dbConnection.QuerySingle<int>("SELECT idServer FROM Server WHERE Nombre = @UnNombre", new { UnNombre = nombre });

        public void ActualizarServer(int idServer, string nombre, string abreviado)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@UnidServer", idServer);
            parametros.Add("@UnNombre", nombre);
            parametros.Add("@UnAbreviado", abreviado);
            _dbConnection.Execute("ActualizarServer", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarServer(int idServer)
        {
            // Eliminar registros dependientes en cuentariot
            var parametrosDependientes = new DynamicParameters();
            parametrosDependientes.Add("@unIdServer", idServer);
            var cuentasRiot = _dbConnection.Query<uint>("SELECT idCuenta FROM cuentariot WHERE idServer = @unIdServer", parametrosDependientes).ToList();

            foreach (var idCuenta in cuentasRiot)
            {
                // Eliminar registros dependientes en cuenta lol
                var parametrosCuentaLOL = new DynamicParameters();
                parametrosCuentaLOL.Add("@UnidCuenta", idCuenta);
                var cuentasLOL = _dbConnection.Query<uint>("SELECT idCuentaL FROM cuentaleagueofleguends WHERE idCuenta = @UnidCuenta", parametrosCuentaLOL).ToList();

                foreach (var idCuentaL in cuentasLOL)
                {
                    // Eliminar registros dependientes en inventario
                    var parametrosInventario = new DynamicParameters();
                    parametrosInventario.Add("@unIdCuentaL", idCuentaL);
                    _dbConnection.Execute("DELETE FROM inventario WHERE idCuentaL = @unIdCuentaL", parametrosInventario);

                    // Eliminar la cuenta lol
                    var parametrosEliminarCuentaLOL = new DynamicParameters();
                    parametrosEliminarCuentaLOL.Add("@UnidCuentaL", idCuentaL);
                    _dbConnection.Execute("DELETE FROM cuentaleagueofleguends WHERE idCuentaL = @UnidCuentaL", parametrosEliminarCuentaLOL);
                }

                // Eliminar registros dependientes en cuenta valorant
                var parametrosCuentaValorant = new DynamicParameters();
                parametrosCuentaValorant.Add("@UnidCuenta", idCuenta);
                _dbConnection.Execute("DELETE FROM CuentaValorant WHERE idCuenta = @UnidCuenta", parametrosCuentaValorant);

                // Eliminar la cuenta riot
                var parametrosCuentaRiot = new DynamicParameters();
                parametrosCuentaRiot.Add("@UnidCuenta", idCuenta);
                _dbConnection.Execute("DELETE FROM cuentariot WHERE idCuenta = @UnidCuenta", parametrosCuentaRiot);
            }

            // Eliminar el registro en server
            var parametros = new DynamicParameters();
            parametros.Add("@UnidServer", idServer);
            _dbConnection.Execute("EliminarServer", parametros, commandType: CommandType.StoredProcedure);
        }

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
        {
            var parametros = new DynamicParameters();
            parametros.Add("@UnNombre", nombre);
            parametros.Add("@UnPassword", password);
            parametros.Add("@UnEmail", email);
            parametros.Add("@UnidServer", idServer);
            _dbConnection.Execute("InsertarCuentaRiot", parametros, commandType: CommandType.StoredProcedure);
        }

        public uint ObtenerCuentaRiotId(string email)
            => _dbConnection.ExecuteScalar<uint>("SELECT idCuenta FROM CuentaRiot WHERE eMail = @Email", new { Email = email });

        public void ActualizarCuentaRiot(uint idCuenta, string nombre, string password, string email)
            => _dbConnection.Execute("ActualizarCuentaRiot", new { UnidCuenta = idCuenta, UnNombre = nombre, UnPassword = password, UneMail = email }, commandType: CommandType.StoredProcedure);

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
            parametros.Add("@UnidRangoL", idRangoL);
            parametros.Add("@UnNombre", nombre);
            parametros.Add("@UnNumero", numero);
            parametros.Add("@UnPuntosCompetitivo", puntosCompetitivo);
            _dbConnection.Execute("ActualizarRangoL", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarRangoL(byte idRangoL)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Update dependent records in CuentaLeagueOfLeguends to remove the foreign key reference
                    var updateParams = new DynamicParameters();
                    updateParams.Add("@UnidRangoL", idRangoL);
                    _dbConnection.Execute("UPDATE CuentaLeagueOfLeguends SET idRangoL = NULL WHERE idRangoL = @UnidRangoL", updateParams, transaction);

                    // Now delete the RangoL record
                    var deleteParams = new DynamicParameters();
                    deleteParams.Add("@UnidRangoL", idRangoL);
                    _dbConnection.Execute("EliminarRangoL", deleteParams, transaction, commandType: CommandType.StoredProcedure);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
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
            parametros.Add("@UnidRangoV", idRangoV);
            parametros.Add("@UnNombre", nombre);
            parametros.Add("@UnNumero", numero);
            parametros.Add("@UnPuntosCompetitivo", puntosCompetitivo);
            _dbConnection.Execute("ActualizarRangoV", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarRangoV(byte idRangoV)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@UnidRangoV", idRangoV);
            _dbConnection.Execute("EliminarRangoV", parametros, commandType: CommandType.StoredProcedure);
        }


        #endregion

        #region Inventario

        public void InsertarInventario(uint idCuentaL, uint esenciaAzul, uint puntosRiot)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@UnidCuentaL", idCuentaL);
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
            const string query = @"
                SELECT o.*, t.*
                FROM Objeto o
                JOIN TipoObjeto t ON o.idTipoObjeto = t.idTipoObjeto
                WHERE o.idObjeto = @IdObjeto";

            var objeto = _dbConnection.Query<Objeto, TipoObjeto, Objeto>(
                query,
                (obj, tipo) =>
                {
                    obj.TipoObjeto = tipo;
                    return obj;
                },
                new { IdObjeto = idObjeto },
                splitOn: "idTipoObjeto"
            ).FirstOrDefault();

            return objeto;
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

            return _dbConnection.QueryFirstOrDefault<CuentaLeagueOfLeguends>("SELECT * FROM CuentaLeagueOfLeguends WHERE idCuentaL = @UnidCuentaL", parametros);
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