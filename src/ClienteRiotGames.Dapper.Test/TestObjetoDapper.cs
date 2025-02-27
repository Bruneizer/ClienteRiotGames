namespace RiotGames.Test;

public class ObjetoTests : IDisposable
{
    private readonly IDbConnection _connection;
    private readonly ObjetoDapper _objetoDapper;
    private readonly TipoObjetoDapper _tipoObjetoDapper;

    public ObjetoTests()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("MySQL") ?? "";
        _connection = new MySqlConnection(connectionString);
        _objetoDapper = new ObjetoDapper(_connection);
        _tipoObjetoDapper = new TipoObjetoDapper(_connection);
    }

    [Fact]
    public void ActualizarObjeto_DebeActualizarCorrectamente()
    {
        // Arrange
        uint idObjeto = 1;
        string nombre = "NuevoObjeto";
        uint precioEA = 4800;
        uint precioRP = 880;
        uint idTipoObjeto = 1; // Asegúrate de que este ID existe en la tabla TipoObjeto

        // Act
        _objetoDapper.ActualizarObjeto(idObjeto, nombre, precioEA, precioRP, idTipoObjeto);

        // Assert
        // Verifica que el objeto se ha actualizado correctamente
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}