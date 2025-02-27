using System.Linq;
namespace RiotGames.Test
{
    public class ServerTests : IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly ServerDapper _serverDapper;

        public ServerTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("MySQL") ?? "";
            _connection = new MySqlConnection(connectionString);
            _serverDapper = new ServerDapper(_connection);
        }

        [Fact]
        public void ObtenerDetallesServer_DebeRetornarServerConCuentas()
        {
            // Arrange
            string nombreBase = $"ServidorTest_as";
            string nombre = new string(nombreBase.Take(45).ToArray());

            string abreviadoBase = $"STT_1111111111111111111111111";
            string abreviado = new string(abreviadoBase.Take(5).ToArray());

            // Act
            byte serverId = _serverDapper.InsertarServer(nombre, abreviado);
            var servidor = _serverDapper.ObtenerDetallesServer(serverId);

            // Assert
            Assert.NotNull(servidor);
            Assert.Equal(abreviado, servidor.Abreviado);
            Assert.Equal(nombre, servidor.Nombre);

            // Cleanup
            _serverDapper.EliminarServer(serverId);
        }

        [Fact]
        public void ActualizarServer_DebeActualizarCorrectamente()
        {
            // Arrange
            string nombreBase = $"ServidorTest_123123";
            string nombre = nombreBase.Length > 45 ? nombreBase.Substring(0, 45) : nombreBase;

            string abreviadoBase = $"STT_123123";
            string abreviado = abreviadoBase.Length > 5 ? abreviadoBase.Substring(0, 5) : abreviadoBase;

            string nombreActualizadoBase = $"ServerActualizado_32";
            string nombreActualizado = nombreActualizadoBase.Length > 50 ? nombreActualizadoBase.Substring(0, 50) : nombreActualizadoBase;

            string abreviadoActualizadoBase = $"SAT_32";
            string abreviadoActualizado = abreviadoActualizadoBase.Length > 5 ? abreviadoActualizadoBase.Substring(0, 5) : abreviadoActualizadoBase;

            // Act
            byte serverId = _serverDapper.InsertarServer(nombre, abreviado);
            _serverDapper.ActualizarServer(serverId, nombreActualizado, abreviadoActualizado);
            var servidorActualizado = _serverDapper.ObtenerServer(serverId);

            // Assert
            Assert.Equal(nombreActualizado.Substring(0, 45), servidorActualizado.Nombre);
            Assert.Equal(abreviadoActualizado, servidorActualizado.Abreviado);

            // Cleanup
            _serverDapper.EliminarServer(serverId);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
} 