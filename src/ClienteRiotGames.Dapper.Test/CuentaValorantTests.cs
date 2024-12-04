namespace RiotGames.Test
{
    public class CuentaValorantTests : IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly CuentaValorantDapper _cuentaValorantDapper;

        public CuentaValorantTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("MySQL") ?? "";
            _connection = new MySqlConnection(connectionString);
            _cuentaValorantDapper = new CuentaValorantDapper(_connection);
        }

        [Theory]
        [InlineData(-1)]      // Experiencia inválida (menor a 0)
        [InlineData(1000001)] // Experiencia inválida (mayor a 1,000,000)
        public void InsertarCuentaValorant_DatosInvalidos_DebeLanzarExcepcion(int experienciaInvalida)
        {
            // Arrange
            string nombreBase = $"AgenteTes_125";
            string nombre = nombreBase.Length > 45 ? nombreBase.Substring(0, 45) : nombreBase;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _cuentaValorantDapper.InsertarCuentaValorant(1, 1, nombre, 10, experienciaInvalida));
            
            Assert.Equal("La experiencia debe estar entre 0 y 1000000", exception.Message);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
} 