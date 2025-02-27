
namespace ClienteRiotGames.Test
{
    public class TestCuentaLolDapper : TestAdo
    {
        [Theory]
        [InlineData("Summoner1", 30, 1000, 1, 1)]
        [InlineData("Summoner2", 50, 1500, 2, 2)]
        public void InsertarYObtenerCuentaLOL(string nombre, int nivel, int puntosCompetitivo, uint idCuenta, byte idRangoL)
        {
            Ado.InsertarCuentaLOL(nombre, nivel, puntosCompetitivo, idCuenta, idRangoL);
            var cuenta = Ado.ObtenerCuentaLOL(idCuenta);

            Assert.NotNull(cuenta);
            Assert.Equal(nombre, cuenta.Nombre);
            Assert.Equal(nivel, cuenta.Nivel);
            Assert.Equal(puntosCompetitivo, cuenta.PuntosCompetitivo);
            Assert.Equal(idCuenta, cuenta.IdCuenta);
            Assert.Equal(idRangoL, cuenta.IdRangoL);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarCuentaLOL(uint idCuentaL)
        {
            Ado.EliminarCuentaLOL(idCuentaL);
            var cuenta = Ado.ObtenerCuentaLOL(idCuentaL);

            Assert.Null(cuenta);
        }
    }
}