using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestInventarioDapper : TestAdo
    {
        [Theory]
        [InlineData(1, 1000, 200)]
        [InlineData(2, 2000, 400)]
        public void InsertarYObtenerInventario(uint idCuentaL, uint esenciaAzul, uint puntosRiot)
        {
            Ado.InsertarInventario(idCuentaL, esenciaAzul, puntosRiot);
            var inventario = Ado.ObtenerInventario(idCuentaL);

            Assert.NotNull(inventario);
            Assert.Equal(esenciaAzul, inventario.EsenciaAzul);
            Assert.Equal(puntosRiot, inventario.PuntosRiot);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarInventario(uint idInventario)
        {
            Ado.EliminarInventario(idInventario);
            var inventario = Ado.ObtenerInventario(idInventario);

            Assert.Null(inventario);
        }
    }
}