using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ClienteRiotGames.Data
{
    public class AdoDapper : IAdo
    {
        private readonly IDbConnection _dbConnection;

        public AdoDapper(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

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
    }
}
