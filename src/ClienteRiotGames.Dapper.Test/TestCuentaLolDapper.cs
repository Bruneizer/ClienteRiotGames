
namespace ClienteRiotGames.Test
{
    public class TestCuentaLolDapper : TestAdo
    {
        [Theory]
        [InlineData("Summoner1", 30, 1000, 1, 1)]

        public void InsertarYObtenerCuentaLOL(string nombre, int nivel, int puntosCompetitivo, uint idCuenta, byte idRangoL)
        {
            Ado.InsertarCuentaRiot("RiotAccount", "passWORD1@", "email@example.com", 2); // Ensure a Riot account is created first
            Ado.InsertarCuentaLOL(nombre, nivel, puntosCompetitivo, idCuenta, idRangoL);
            var cuenta = Ado.ObtenerCuentaLOL(idCuenta);

            Assert.NotNull(cuenta);
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