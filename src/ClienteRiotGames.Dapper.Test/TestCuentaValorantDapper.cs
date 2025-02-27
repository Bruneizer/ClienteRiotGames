using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestCuentaValorantDapper : TestAdo
    {
        [Theory]
        [InlineData("Valorant1", 10, 1000, 1, 1)]
        [InlineData("Valorant2", 20, 2000, 2, 2)]
        public void InsertarYObtenerCuentaValorant(string nombre, int nivel, int experiencia, uint idCuenta, byte idRangoV)
        {
            Ado.InsertarCuentaValorant(nombre, nivel, experiencia, idCuenta, idRangoV);
            var cuenta = Ado.ObtenerCuentaValorant(idCuenta);

            Assert.NotNull(cuenta);
            Assert.Equal(nombre, cuenta.Nombre);
            Assert.Equal(nivel, cuenta.Nivel);
            Assert.Equal(experiencia, cuenta.Experiencia);
            Assert.Equal(idCuenta, cuenta.IdCuenta);
            Assert.Equal(idRangoV, cuenta.IdRangoV);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarCuentaValorant(uint idCuentaV)
        {
            Ado.EliminarCuentaValorant(idCuentaV);
            var cuenta = Ado.ObtenerCuentaValorant(idCuentaV);

            Assert.Null(cuenta);
        }
    }
}