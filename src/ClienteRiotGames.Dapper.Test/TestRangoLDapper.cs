using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestRangoLDapper : TestAdo
    {
        [Theory]
        [InlineData("Rango1", 5, 53)]
        [InlineData("Rango2", 6, 22)]
        public void InsertarYObtenerRangoL(string nombre, int numero, int puntosCompetitivo)
        {
            Ado.InsertarRangoL(nombre, numero, puntosCompetitivo);
            var rango = Ado.ObtenerRangoL((byte)numero);

            Assert.NotNull(rango);
            Assert.Equal(nombre, rango.Nombre);
            Assert.Equal(numero, rango.Numero);
            Assert.Equal(puntosCompetitivo, rango.PuntosCompetitivo);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarRangoL(byte idRangoL)
        {
            Ado.EliminarRangoL(idRangoL);
            var rango = Ado.ObtenerRangoL(idRangoL);

            Assert.Null(rango);
        }
    }
}