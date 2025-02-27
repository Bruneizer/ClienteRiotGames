using ClienteRiotGames.Core;
using ClienteRiotGames.Dapper;
using System.Data;

namespace ClienteRiotGames.Test;

/// <summary>
/// El objetivo de esta clase es brindar una instancia de Ado para los tests.
/// </summary>
public class TestAdo
{
    protected readonly IAdo Ado;
    private const string _cadena = "Server=localhost;Database=ClienteRiotGames;Uid=admin_riot;pwd=AdminR10t2024!;Allow User Variables=True";

    public TestAdo() => Ado = new AdoDapper(new MySqlConnection(_cadena));
    public TestAdo(string cadena) => Ado = new AdoDapper(new MySqlConnection(cadena));
}