using System.Data;
using Dapper;
using MySqlConnector;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper
{
    public class RangoLDapper : IRangoLDAO
    {
        private readonly IDbConnection _conexion;

        public RangoLDapper(IDbConnection conexion) => this._conexion = conexion;

        public void InsertarRangoL(string nombre, int numero, int puntosCompetitivo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unNumero", numero);
            parametros.Add("@unPuntosCompetitivo", puntosCompetitivo);

            _conexion.Execute("InsertarRangoL", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarRangoL(byte idRangoL, string nombre, int numero, int puntosCompetitivo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoL", idRangoL);
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unNumero", numero);
            parametros.Add("@unPuntosCompetitivo", puntosCompetitivo);

            _conexion.Execute("ActualizarRangoL", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarRangoL(byte idRangoL)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoL", idRangoL);

            _conexion.Execute("EliminarRangoL", parametros, commandType: CommandType.StoredProcedure);
        }

        public RangoL? ObtenerRangoL(byte idRangoL)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoL", idRangoL);

            return _conexion.QueryFirstOrDefault<RangoL>("ObtenerDetallesRangoL", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<RangoL> ObtenerRangosL()
        {
            const string query = "SELECT * FROM RangoL";
            return _conexion.Query<RangoL>(query).ToList();
        }
    }
}
