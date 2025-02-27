using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestObjetoDapper : TestAdo
    {
        [Theory]
        [InlineData(1, "aabjeto1", 20, 200)]
        [InlineData(2, "aabjeto2", 0, 400)]
        public void InsertarYObtenerObjeto(int idTipoObjeto, string nombre, uint precioEA, uint precioRP)
        {
            Ado.InsertarObjeto(idTipoObjeto, nombre, precioEA, precioRP);
            var objeto = Ado.ObtenerObjeto(idTipoObjeto);
;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarObjeto(int idObjeto)
        {
            Ado.EliminarObjeto(idObjeto);
            var objeto = Ado.ObtenerObjeto(idObjeto);

            Assert.Null(objeto);
        }
    }
}