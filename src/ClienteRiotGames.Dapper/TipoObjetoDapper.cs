using System.Data;
using Dapper;
using MySqlConnector;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper
{
    public class TipoObjetoDapper : ITipoObjetoDAO
    {
        private readonly IDbConnection _conexion;

        public TipoObjetoDapper(IDbConnection conexion) => this._conexion = conexion;

        public void InsertarTipoObjeto(string nombre)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unNombre", nombre);

            _conexion.Execute("InsertarTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarTipoObjeto(int idTipoObjeto, string nombre)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);
            parametros.Add("@unNombre", nombre);

            _conexion.Execute("ActualizarTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarTipoObjeto(int idTipoObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);

            _conexion.Execute("EliminarTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public TipoObjeto? ObtenerTipoObjeto(int idTipoObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);

            return _conexion.QueryFirstOrDefault<TipoObjeto>("ObtenerDetallesTipoObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<TipoObjeto> ObtenerTiposObjeto()
        {
            const string query = "SELECT * FROM TipoObjeto";
            return _conexion.Query<TipoObjeto>(query).ToList();
        }
    }
}
