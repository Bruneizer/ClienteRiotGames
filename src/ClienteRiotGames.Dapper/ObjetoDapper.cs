using System.Data;
using Dapper;
using MySqlConnector;
using ClienteRiotGames.Core;

namespace ClienteRiotGames.Dapper
{
    public class ObjetoDapper : IObjetoDAO
    {
        private readonly IDbConnection _conexion;

        public ObjetoDapper(IDbConnection conexion) => this._conexion = conexion;

        public void InsertarObjeto(int idTipoObjeto, string nombre, int precioEA, int precioRP)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unPrecioEA", precioEA);
            parametros.Add("@unPrecioRP", precioRP);

            _conexion.Execute("InsertarObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void ActualizarObjeto(int idObjeto, string nombre, int precioEA, int precioRP, int idTipoObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdObjeto", idObjeto);
            parametros.Add("@unNombre", nombre);
            parametros.Add("@unPrecioEA", precioEA);
            parametros.Add("@unPrecioRP", precioRP);
            parametros.Add("@unIdTipoObjeto", idTipoObjeto);

            _conexion.Execute("ActualizarObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public void EliminarObjeto(int idObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdObjeto", idObjeto);

            _conexion.Execute("EliminarObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public Objeto? ObtenerObjeto(int idObjeto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdObjeto", idObjeto);

            return _conexion.QueryFirstOrDefault<Objeto>("ObtenerDetallesObjeto", parametros, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Objeto> ObtenerObjetos()
        {
            const string query = "SELECT * FROM Objeto";
            return _conexion.Query<Objeto>(query).ToList();
        }
    }
}
