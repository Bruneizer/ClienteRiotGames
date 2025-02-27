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

        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        public void EliminarRangoL(byte idRangoL)
        {
            Ado.EliminarRangoL(idRangoL);

        }
    }
}