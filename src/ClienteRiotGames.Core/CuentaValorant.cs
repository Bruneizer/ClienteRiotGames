namespace ClienteRiotGames.Core;
using System;

public class CuentaValorant
{
    public uint IdCuentaV { get; set; }
    public uint IdCuenta { get; set; }
    public byte IdRangoV { get; set; }
    public string Nombre { get; set; } = "";
    public int Nivel { get; set; }
    public int Experiencia { get; set; }

    public void ValidarExperiencia()
    {
        if (Experiencia < 0 || Experiencia > 1000000)
            throw new ArgumentException("La experiencia debe estar entre 0 y 1000000");
    }
} 