using System.Data;
using Dapper;
using MySqlConnector;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper
{
    public class InventarioDapper : IInventarioDAO
    {
        private readonly IDbConnection _conexion;

        public InventarioDapper(IDbConnection conexion) => this._conexion = conexion;

        public void InsertarInventario(uint idCuentaL, uint esenciaAzul, uint puntosRiot)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdCuentaL", idCuentaL);
            parametros.Add("@unEsenciaAzul", esenciaAzul);
            parametros.Add("@unPuntosRiot", puntosRiot);

            _conexion.Execute("InsertarInventario", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarInventario(uint idInventario, uint esenciaAzul, uint puntosRiot)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdInventario", idInventario);
            parametros.Add("@unEsenciaAzul", esenciaAzul);
            parametros.Add("@unPuntosRiot", puntosRiot);

            _conexion.Execute("ActualizarPuntosInventario", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarInventario(uint idInventario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdInventario", idInventario);

            _conexion.Execute("EliminarInventario", parametros, commandType: CommandType.StoredProcedure);
        }

        public Inventario? ObtenerInventario(uint idInventario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdInventario", idInventario);

            return _conexion.QueryFirstOrDefault<Inventario>("ObtenerDetallesInventario", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Inventario> ObtenerInventarios()
        {
            const string query = "SELECT * FROM Inventario";
            return _conexion.Query<Inventario>(query).ToList();
        }
    }
}
