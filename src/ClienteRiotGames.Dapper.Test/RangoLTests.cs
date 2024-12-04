namespace RiotGames.Test
{
    public class RangoLTests : IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly RangoLDapper _rangoLDapper;

        public RangoLTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("MySQL") ?? "";
            _connection = new MySqlConnection(connectionString);
            _rangoLDapper = new RangoLDapper(_connection);
        }

        [Fact]
        public void InsertarRangoL_DebeInsertarCorrectamente()
        {
            // Arrange
            string nombreEsperado = "TestRango";
            int numeroEsperado = 4;
            int puntosCompetitivoEsperado = 80;

            try
            {
                // Act
                _rangoLDapper.InsertarRangoL(nombreEsperado, numeroEsperado, puntosCompetitivoEsperado);
                var rango = _rangoLDapper.ObtenerRangoL((byte)numeroEsperado);

                // Assert
                Assert.NotNull(rango);
                Assert.Equal(nombreEsperado, rango.Nombre);
                Assert.Equal(numeroEsperado, rango.Numero);
                Assert.Equal(puntosCompetitivoEsperado, rango.PuntosCompetitivo);
            }
            finally
            {
                // Cleanup - Solo si no hay referencias
                try
                {
                    _connection.Execute("DELETE FROM RangoL WHERE Numero = @Numero", new { Numero = numeroEsperado });
                }
                catch
                {
                    // Ignorar errores de limpieza
                }
            }
        }

        [Theory]
        [InlineData(-1, 50)]   // Número inválido
        [InlineData(5, 50)]    // Número inválido
        [InlineData(2, -1)]    // Puntos competitivos inválidos
        [InlineData(2, 101)]   // Puntos competitivos inválidos
        public void InsertarRangoL_DatosInvalidos_DebeLanzarExcepcion(int numero, int puntosCompetitivo)
        {
            // Arrange
            string nombre = $"Test_{Guid.NewGuid()}";
            nombre = nombre.Length > 45 ? nombre.Substring(0, 45) : nombre;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                _rangoLDapper.InsertarRangoL(nombre, numero, puntosCompetitivo));
            
            if (numero < 0 || numero > 4)
            {
                Assert.Equal("El número de rango debe estar entre 0 y 4", exception.Message);
            }
            else
            {
                Assert.Equal("Los puntos competitivos deben estar entre 0 y 100", exception.Message);
            }
        }

        [Fact]
        public void InsertarRangoL_PuntosCompetitivosInvalidos_DebeLanzarExcepcion()
        {
            // Arrange
            string nombre = $"Test_{Guid.NewGuid()}";
            nombre = nombre.Length > 45 ? nombre.Substring(0, 45) : nombre;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _rangoLDapper.InsertarRangoL("Test", 2, 101));
            Assert.Equal("Los puntos competitivos deben estar entre 0 y 100", exception.Message);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}