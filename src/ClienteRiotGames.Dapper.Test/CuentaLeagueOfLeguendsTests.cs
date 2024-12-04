using System.Data;


namespace RiotGames.Test;

public class CuentaLeagueOfLeguendsTests : IDisposable
{
    private readonly IDbConnection _connection;
    private readonly CuentaLeagueOfLeguendsDapper _cuentaLOLdapper;
    private readonly CuentaRiotDapper _cuentaRiotDapper;
    private uint _testCuentaLId;

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
        
        // Crear datos de prueba
        ConfigurarDatosPrueba();
    }

    private void ConfigurarDatosPrueba()
    {
        // Generar valores únicos usando timestamp
        var timestamp = DateTime.Now.Ticks;
        string email = $"test{timestamp}@test.com";
        string nombre = $"TestUser{timestamp}";
        string password = "Test123!";
        byte idServer = 1;

        try
        {
            // Limpiar datos anteriores primero
            _connection.Execute("DELETE FROM CuentaLeagueOfLeguends WHERE Nombre LIKE 'TestUser%'");
            _connection.Execute("DELETE FROM CuentaRiot WHERE eMail LIKE 'test%@test.com'");

            // Insertar cuenta de prueba
            _connection.Execute(
                "INSERT INTO CuentaRiot (Nombre, Password, eMail, idServer) VALUES (@nombre, @password, @email, @idServer)",
                new { nombre, password, email, idServer }
            );

            var cuentaRiot = _connection.QuerySingle<CuentaRiot>(
                "SELECT * FROM CuentaRiot WHERE eMail = @email",
                new { email }
            );

            // Insertar cuenta LOL de prueba
            _connection.Execute(@"
                INSERT INTO CuentaLeagueOfLeguends (idCuenta, idRangoL, Nombre, Nivel, PuntosCompetitivo) 
                VALUES (@idCuenta, @idRangoL, @nombre, @nivel, @puntos)",
                new { 
                    idCuenta = cuentaRiot.IdCuenta, 
                    idRangoL = (byte)1, 
                    nombre = $"TestLOL{timestamp}", 
                    nivel = 30, 
                    puntos = 50 
                }
            );

            _testCuentaLId = _connection.QuerySingle<uint>(
                "SELECT idCuentaL FROM CuentaLeagueOfLeguends WHERE idCuenta = @idCuenta", 
                new { idCuenta = cuentaRiot.IdCuenta }
            );
        }
        catch
        {
            // Si algo falla, intentamos limpiar
            _connection.Execute("DELETE FROM CuentaLeagueOfLeguends WHERE Nombre LIKE 'TestUser%'");
            _connection.Execute("DELETE FROM CuentaRiot WHERE eMail LIKE 'test%@test.com'");
            throw;
        }
    }

    [Fact]
    public void InsertarCuentaLOL_DebeInsertarCorrectamente()
    {
        // Arrange
        string nombreInvocador = "TestInvocador2";
        int nivel = 30;
        byte idRangoL = 1;
        string email = "test2@test.com";

        // Insertar cuenta Riot primero
        _connection.Execute(
            "INSERT INTO CuentaRiot (Nombre, Password, eMail, idServer) VALUES (@nombre, @password, @email, @idServer)",
            new { nombre = "TestUser2", password = "Test123!", email, idServer = (byte)1 }
        );

        var cuentaRiot = _connection.QuerySingle<CuentaRiot>("SELECT * FROM CuentaRiot WHERE eMail = @email", new { email });

        try
        {
            // Act
            _cuentaLOLdapper.InsertarCuentaLOL(cuentaRiot.IdCuenta, idRangoL, nombreInvocador, nivel, 50);
            var cuentaLOL = _connection.QueryFirstOrDefault<CuentaLeagueOfLeguends>(
                "SELECT * FROM CuentaLeagueOfLeguends WHERE idCuenta = @idCuenta",
                new { idCuenta = cuentaRiot.IdCuenta }
            );

            // Assert
            Assert.NotNull(cuentaLOL);
            Assert.Equal(nombreInvocador, cuentaLOL.Nombre);
            Assert.Equal(nivel, cuentaLOL.Nivel);
            Assert.Equal(50, cuentaLOL.PuntosCompetitivo);
            Assert.Equal(idRangoL, cuentaLOL.IdRangoL);
        }
        finally
        {
            // Limpieza
            _connection.Execute("DELETE FROM CuentaLeagueOfLeguends WHERE idCuenta = @idCuenta", new { idCuenta = cuentaRiot.IdCuenta });
            _connection.Execute("DELETE FROM CuentaRiot WHERE idCuenta = @idCuenta", new { idCuenta = cuentaRiot.IdCuenta });
        }
    }

    [Fact]
    public void EliminarCuentaLOL_DebeEliminarCuentaExistente()
    {
        // Act
        _cuentaLOLdapper.EliminarCuentaLOL(_testCuentaLId);

        // Assert
        var cuentaEliminada = _connection.QueryFirstOrDefault<CuentaLeagueOfLeguends>(
            "SELECT * FROM CuentaLeagueOfLeguends WHERE idCuentaL = @idCuentaL",
            new { idCuentaL = _testCuentaLId }
        );
        Assert.Null(cuentaEliminada);
    }

    [Fact]
    public void ActualizarRangoLOL_DebeActualizarRangoYPuntos()
    {
        // Arrange
        byte nuevoRango = 2;
        int nuevosPuntos = 75;
        
        // Act
        _cuentaLOLdapper.ActualizarRangoLOL(_testCuentaLId, nuevoRango, nuevosPuntos);
        
        // Assert
        var cuentaActualizada = _cuentaLOLdapper.ObtenerCuentaLOL(_testCuentaLId);
        Assert.NotNull(cuentaActualizada);
        Assert.Equal(nuevoRango, cuentaActualizada.IdRangoL);
        Assert.Equal(nuevosPuntos, cuentaActualizada.PuntosCompetitivo);
    }

    [Fact]
    public void ObtenerDetallesCuentaLOL_DebeRetornarDetallesCompletos()
    {
        // Act
        var detallesCuenta = _cuentaLOLdapper.ObtenerDetallesCuentaLOL(_testCuentaLId);
        
        // Assert
        Assert.NotNull(detallesCuenta);
        Assert.NotNull(detallesCuenta.Nombre);
        Assert.NotNull(detallesCuenta.Nivel);
        Assert.NotNull(detallesCuenta.PuntosCompetitivo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(99999)]
    public void ObtenerDetallesCuentaLOL_DebeRetornarNullParaCuentaInexistente(uint idCuentaL)
    {
        // Act
        var detallesCuenta = _cuentaLOLdapper.ObtenerDetallesCuentaLOL(idCuentaL);
        
        // Assert
        Assert.Null(detallesCuenta);
    }

    public void Dispose()
    {
        // Limpieza de datos de prueba
        try
        {
            var cuentaL = _connection.QueryFirstOrDefault<CuentaLeagueOfLeguends>(
                "SELECT * FROM CuentaLeagueOfLeguends WHERE idCuentaL = @idCuentaL",
                new { idCuentaL = _testCuentaLId }
            );

            if (cuentaL != null)
            {
                _connection.Execute("DELETE FROM CuentaLeagueOfLeguends WHERE idCuentaL = @idCuentaL",
                    new { idCuentaL = _testCuentaLId });
                _connection.Execute("DELETE FROM CuentaRiot WHERE idCuenta = @idCuenta",
                    new { idCuenta = cuentaL.IdCuenta });
            }
        }
        catch
        {
            // Ignorar errores de limpieza
        }
        finally
        {
            _connection.Dispose();
        }
    }
}