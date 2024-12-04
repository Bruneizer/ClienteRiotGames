using System;
using System.Data;
using Dapper;
using MySqlConnector;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace RiotGames.Test;

public class CuentaLeagueOfLeguendsTests : IDisposable
{
    private readonly IDbConnection _connection;
    private readonly CuentaLeagueOfLeguendsDapper _cuentaLOLdapper;
    private readonly CuentaRiotDapper _cuentaRiotDapper;

    public CuentaLeagueOfLeguendsTests()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("MySQL") ?? "";
        _connection = new MySqlConnection(connectionString);
        _cuentaLOLdapper = new CuentaLeagueOfLeguendsDapper(_connection);
        _cuentaRiotDapper = new CuentaRiotDapper(_connection);
    }

    [Fact]
    public void InsertarCuentaLOL_DebeInsertarCorrectamente()
    {
        // Arrange
        string nombreInvocador = "TestInvocador";
        int nivel = 30;
        byte idRangoL = 1;
        string email = "usuario5@email.com"; // Usamos una cuenta existente

        try
        {
            // Act
            uint? cuentaId = _cuentaRiotDapper.ObtenerCuentaRiotId(email);
            Assert.True(cuentaId.HasValue, "La cuenta Riot debe existir");

            _cuentaLOLdapper.InsertarCuentaLOL(cuentaId.Value, idRangoL, nombreInvocador, nivel, 50);
            var cuentaLOL = _cuentaLOLdapper.ObtenerCuentaLOL(cuentaId.Value);

            // Assert
            Assert.NotNull(cuentaLOL);
            Assert.Equal(nombreInvocador, cuentaLOL.Nombre);
            Assert.Equal(nivel, cuentaLOL.Nivel);
            Assert.Equal(50, cuentaLOL.PuntosCompetitivo);
            Assert.Equal(idRangoL, cuentaLOL.IdRangoL);
        }
        finally
        {
            // Solo limpiamos la cuenta LOL
            var cuentaIdCleanup = _cuentaRiotDapper.ObtenerCuentaRiotId(email);
            if (cuentaIdCleanup.HasValue)
            {
                _cuentaLOLdapper.EliminarCuentaLOL(cuentaIdCleanup.Value);
            }
        }
    }

    public void Dispose()
    {
        // Limpieza de datos de prueba si es necesario
        try
        {
            _connection.Execute("DELETE FROM RangoL WHERE Numero = @Numero", new { Numero = 5 });
        }
        catch
        {
            // Manejar cualquier error de limpieza si es necesario
        }
        _connection.Dispose();
    }
} 