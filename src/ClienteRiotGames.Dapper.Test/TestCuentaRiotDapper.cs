using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestCuentaRiotDapper : TestAdo
    {
        [Theory]
        [InlineData("Summoner1", "Password1!", "email1@example.com", 3)]
        [InlineData("Summoner2", "Password2@", "email2@example.com", 3)]
        public void InsertarYObtenerCuentaRiot(string nombre, string password, string email, byte idServer)
        {
            Ado.InsertarCuentaRiot(nombre, password, email, idServer);
            var idCuenta = Ado.ObtenerCuentaRiotId(email);
            var cuenta = Ado.ObtenerCuentaRiot(idCuenta);

            Assert.NotNull(cuenta);
            Assert.Equal(nombre, cuenta.Nombre);
            Assert.Equal(email, cuenta.Email);
            Assert.Equal(idServer, cuenta.IdServer);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarCuentaRiot(uint idCuenta)
        {
            Ado.EliminarCuentaRiot(idCuenta);
            var cuenta = Ado.ObtenerCuentaRiot(idCuenta);

            Assert.Null(cuenta);
        }
    }
}