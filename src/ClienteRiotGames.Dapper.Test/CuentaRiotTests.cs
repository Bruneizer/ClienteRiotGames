using System;
using System.Data;
using Dapper;
using MySqlConnector;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace RiotGames.Test
{
    public class CuentaRiotTests : IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly CuentaRiotDapper _cuentaRiotDapper;

        public CuentaRiotTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("MySQL") ?? "";
            _connection = new MySqlConnection(connectionString);
            _cuentaRiotDapper = new CuentaRiotDapper(_connection);
        }

        [Fact]
        public void EliminarCuentaRiot_DebeEliminarCorrectamente()
        {
            // Arrange
            string nombre = "TestUser";
            string password = "P@ssword1";
            string email = "test@example.com";
            byte idServer = 1;

            // Act
            _cuentaRiotDapper.InsertarCuentaRiot(nombre, password, email, idServer);
            var cuenta = _cuentaRiotDapper.ObtenerCuentaRiotPorEmail(email);
            Assert.NotNull(cuenta);

            _cuentaRiotDapper.EliminarCuentaRiot(cuenta.IdCuenta);

            // Assert
            var cuentaEliminada = _cuentaRiotDapper.ObtenerCuentaRiotPorEmail(email);
            Assert.Null(cuentaEliminada);
        }

        [Fact]
        public void Login_DebeRetornarInformacionCorrecta()
        {
            // Arrange
            string email = "usuario3@email.com";
            string password = "P@ssword3";

            try
            {
                // Act
                var loginResponse = _cuentaRiotDapper.Login(email, password);

                // Assert
                Assert.True(loginResponse.Resultado, loginResponse.Mensaje);
                Assert.NotNull(loginResponse.Cuenta);
                Assert.Equal("Usuario3", loginResponse.Cuenta?.NombreUsuario);
                Assert.Equal(email, loginResponse.Cuenta?.Email);
                // El servidor 2 debería ser NA
                Assert.Equal("LAS", loginResponse.Cuenta?.AbreviadoServer);
            }
            catch (Exception ex)
            {
                Assert.Fail($"El test falló con el error: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
} 