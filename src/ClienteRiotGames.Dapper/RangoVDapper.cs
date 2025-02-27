using System.Data;
using Dapper;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper
{
    public class RangoVDapper : RangoV
    {
        private readonly IDbConnection _conexion;

        public RangoVDapper(IDbConnection conexion) => this._conexion = conexion;

        public void InsertarRangoV(string nombre, int numero, int puntosCompetitivo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unNumero", numero);
            parametros.Add("@unPuntosCompetitivo", puntosCompetitivo);

            _conexion.Execute("InsertarRangoV", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarRangoV(byte idRangoV, string nombre, int numero, int puntosCompetitivo)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoV", idRangoV);
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unNumero", numero);
            parametros.Add("@unPuntosCompetitivo", puntosCompetitivo);

            _conexion.Execute("ActualizarRangoV", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarRangoV(byte idRangoV)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoV", idRangoV);

            _conexion.Execute("EliminarRangoV", parametros, commandType: CommandType.StoredProcedure);
        }

        public RangoV? ObtenerRangoV(byte idRangoV)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdRangoV", idRangoV);

            return _conexion.QueryFirstOrDefault<RangoV>("ObtenerDetallesRangoV", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<RangoV> ObtenerRangosV()
        {
            const string query = "SELECT * FROM RangoV";
            return _conexion.Query<RangoV>(query).ToList();
        }
    }
}
