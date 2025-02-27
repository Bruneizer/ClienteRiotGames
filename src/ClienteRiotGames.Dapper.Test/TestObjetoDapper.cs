using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestObjetoDapper : TestAdo
    {
        [Theory]
        [InlineData(1, "Objeto1", 100, 200)]
        [InlineData(2, "Objeto2", 200, 400)]
        public void InsertarYObtenerObjeto(int idTipoObjeto, string nombre, uint precioEA, uint precioRP)
        {
            Ado.InsertarObjeto(idTipoObjeto, nombre, precioEA, precioRP);
            var objeto = Ado.ObtenerObjeto(idTipoObjeto);

            Assert.NotNull(objeto);
            Assert.Equal(nombre, objeto.Nombre);
            Assert.Equal(precioEA, objeto.PrecioEA);
            Assert.Equal(precioRP, objeto.PrecioRP);
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