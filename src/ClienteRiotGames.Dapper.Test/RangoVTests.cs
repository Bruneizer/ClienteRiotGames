namespace RiotGames.Test;

public class RangoVTests : IDisposable
{
    private readonly IDbConnection _connection;
    private readonly RangoVDapper _rangoVDapper;

    public RangoVTests()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("MySQL") ?? "";
        _connection = new MySqlConnection(connectionString);
        _rangoVDapper = new RangoVDapper(_connection);
    }

    [Fact]
    public void InsertarRangoV_DebeInsertarCorrectamente()
    {
        // Arrange
        string nombre = $"RangoV_{Guid.NewGuid()}";
        int numero = 2;  // Entre 0 y 4 según el trigger
        int puntosCompetitivo = 40;  // Entre 0 y 100 según el trigger

        // Act
        _rangoVDapper.InsertarRangoV(nombre, numero, puntosCompetitivo);

        // Assert
        var rango = _rangoVDapper.ObtenerRangoVPorNombre(nombre);
        Assert.NotNull(rango);
        Assert.Equal(nombre, rango.Nombre);
        Assert.Equal(numero, rango.Numero);
        Assert.Equal(puntosCompetitivo, rango.PuntosCompetitivo);

        // Cleanup
        _rangoVDapper.EliminarRangoV(rango.IdRangoV);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
} 