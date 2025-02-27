using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestCuentaRiotDapper : TestAdo
    {
        [Theory]
        [InlineData("Summoner1", "password1", "email1@example.com", 1)]
        [InlineData("Summoner2", "password2", "email2@example.com", 2)]
        public void InsertarYObtenerCuentaRiot(string nombre, string password, string email, byte idServer)
        {
            Ado.InsertarCuentaRiot(nombre, password, email, idServer);
            var idCuenta = Ado.ObtenerCuentaRiotId(email);
            var cuenta = Ado.ObtenerCuentaRiot(idCuenta);

            Assert.NotNull(cuenta);
            Assert.Equal(nombre, cuenta.Nombre);
            Assert.Equal(password, cuenta.Password);
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