using ClienteRiotGames.Core;
using System.Data;

namespace ClienteRiotGames.Test
{
    public class TestRangoVDapper : TestAdo
    {
        [Theory]
        [InlineData("Rango1", 6, 78)]
        [InlineData("Rango2", 7, 20)]
        public void InsertarYObtenerRangoV(string nombre, int numero, int puntosCompetitivo)
        {
            Ado.InsertarRangoV(nombre, numero, puntosCompetitivo);
            var rango = Ado.ObtenerRangoV((byte)numero);

            Assert.NotNull(rango);
            Assert.Equal(nombre, rango.Nombre);
            Assert.Equal(numero, rango.Numero);
            Assert.Equal(puntosCompetitivo, rango.PuntosCompetitivo);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void EliminarRangoV(byte idRangoV)
        {
            Ado.EliminarRangoV(idRangoV);
            var rango = Ado.ObtenerRangoV(idRangoV);

            Assert.Null(rango);
        }
    }
}