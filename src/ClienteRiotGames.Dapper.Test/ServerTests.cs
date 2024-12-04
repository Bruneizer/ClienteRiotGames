using System;
using System.Data;
using Dapper;
using MySqlConnector;
using Xunit;
using Microsoft.Extensions.Configuration;
using ClienteRiotGames.Core;

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
            string nombreBase = $"ServidorTest_{Guid.NewGuid()}";
            string nombre = nombreBase.Length > 45 ? nombreBase.Substring(0, 45) : nombreBase;

            string abreviadoBase = $"STT_{Guid.NewGuid()}";
            string abreviado = abreviadoBase.Length > 5 ? abreviadoBase.Substring(0, 5) : abreviadoBase;

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
            string nombreBase = $"ServidorTest_{Guid.NewGuid()}";
            string nombre = nombreBase.Length > 45 ? nombreBase.Substring(0, 45) : nombreBase;

            string abreviadoBase = $"STT_{Guid.NewGuid()}";
            string abreviado = abreviadoBase.Length > 5 ? abreviadoBase.Substring(0, 5) : abreviadoBase;

            string nombreActualizadoBase = $"ServerActualizado_{Guid.NewGuid()}";
            string nombreActualizado = nombreActualizadoBase.Length > 50 ? nombreActualizadoBase.Substring(0, 50) : nombreActualizadoBase;

            string abreviadoActualizadoBase = $"SAT_{Guid.NewGuid()}";
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