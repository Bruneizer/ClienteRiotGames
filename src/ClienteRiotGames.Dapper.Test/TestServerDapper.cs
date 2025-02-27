using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestServerDapper : TestAdo
    {
        [Theory]
        [InlineData("Server1", "S1")]
        [InlineData("Server2", "S2")]
        public void InsertarYObtenerServer(string nombre, string abreviado)
        {
            Ado.InsertarServer(nombre, abreviado);
            var idServer = Ado.ObtenerServerId(nombre);
            var server = Ado.ObtenerServer(idServer);

            Assert.NotNull(server);
            Assert.Equal(nombre, server.Nombre);
            Assert.Equal(abreviado, server.Abreviado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarServer(int idServer)
        {
            Ado.EliminarServer(idServer);
            var server = Ado.ObtenerServer(idServer);

            Assert.Null(server);
        }
    }
}