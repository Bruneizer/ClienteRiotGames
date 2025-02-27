using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestInventarioDapper : TestAdo
    {
        [Theory]
        [InlineData(1, 1000, 200)]
        [InlineData(2, 100, 400)]
        public void InsertarYObtenerInventario(uint idCuentaL, uint esenciaAzul, uint puntosRiot)
        {
            // Insertar el inventario
            Ado.InsertarInventario(idCuentaL, esenciaAzul, puntosRiot);

            // Obtener el inventario insertado
            var inventario = Ado.ObtenerInventario(idCuentaL);


        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarInventario(uint idInventario)
        {
            // Eliminar el inventario
            Ado.EliminarInventario(idInventario);

            // Obtener el inventario eliminado
            var inventario = Ado.ObtenerInventario(idInventario);

            // Verificar que el inventario es nulo
            Assert.Null(inventario);
        }
    }
}