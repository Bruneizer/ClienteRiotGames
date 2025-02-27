namespace RiotGames.Test;

public class InventarioTests : IDisposable
{
    private readonly IDbConnection _connection;
    private readonly InventarioDapper _inventarioDapper;

    public InventarioTests()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("MySQL") ?? "";
        _connection = new MySqlConnection(connectionString);
        _inventarioDapper = new InventarioDapper(_connection);
    }

    [Fact]
    public void InsertarInventario_DebeInsertarCorrectamente()
    {
        // Arrange
        uint idCuentaL = 1;
        uint esenciaAzul = 5000;
        uint puntosRiot = 1000;

        // Act
        _inventarioDapper.InsertarInventario(idCuentaL, esenciaAzul, puntosRiot);
        var inventario = _inventarioDapper.ObtenerInventarioPorIdCuenta(idCuentaL);

        // Assert
        Assert.NotNull(inventario);
        Assert.Equal(esenciaAzul, inventario.EsenciaAzul);
        Assert.Equal(puntosRiot, inventario.PuntosRiot);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
} 