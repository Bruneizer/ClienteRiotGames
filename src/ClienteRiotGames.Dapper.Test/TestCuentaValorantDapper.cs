using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestCuentaValorantDapper : TestAdo
    {
        [Theory]
        [InlineData("Valorant1", 10, 1000, 4, 4)]
        [InlineData("Valorant2", 20, 2000, 3, 3)]
        public void InsertarYObtenerCuentaValorant(string nombre, int nivel, int experiencia, uint idCuenta, byte idRangoV)
        {
            Ado.InsertarCuentaValorant(nombre, nivel, experiencia, idCuenta, idRangoV);
            var cuenta = Ado.ObtenerCuentaValorant(idCuenta);


        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarCuentaValorant(uint idCuentaV)
        {
            Ado.EliminarCuentaValorant(idCuentaV);
            var cuenta = Ado.ObtenerCuentaValorant(idCuentaV);

        }
    }
}