using System.Collections.Generic;

namespace ClienteRiotGames.Core
{
    public interface IAdo
    {
        #region Server
        void InsertarServer(string nombre, string abreviado);
        int ObtenerServerId(string nombre);
        void ActualizarServer(int idServer, string nombre, string abreviado);
        void EliminarServer(int idServer);
        Server? ObtenerServer(int idServer);
        IEnumerable<Server> ObtenerServers();
        Server? ObtenerDetallesServer(int idServer);
        #endregion

        #region CuentaRiot
        void InsertarCuentaRiot(string nombre, string password, string email, byte idServer);
        uint ObtenerCuentaRiotId(string email);
        void ActualizarCuentaRiot(uint idCuenta, string nombre, string password, string email);
        void EliminarCuentaRiot(uint idCuenta);
        CuentaRiot? ObtenerCuentaRiot(uint idCuenta);
        IEnumerable<CuentaRiot> ObtenerCuentasRiot();
        #endregion

        #region CuentaValorant
        void InsertarCuentaValorant(string nombre, int nivel, int experiencia, uint idCuenta, byte idRangoV);
        void ActualizarCuentaValorant(uint idCuentaV, string nombre, int nivel, int experiencia);
        void EliminarCuentaValorant(uint idCuentaV);
        CuentaValorant? ObtenerCuentaValorant(uint idCuentaV);
        IEnumerable<CuentaValorant> ObtenerCuentasValorant();
        #endregion

        #region RangoL
        void InsertarRangoL(string nombre, int numero, int puntosCompetitivo);
        void ActualizarRangoL(byte idRangoL, string nombre, int numero, int puntosCompetitivo);
        void EliminarRangoL(byte idRangoL);
        #endregion

        #region RangoV
        void InsertarRangoV(string nombre, int numero, int puntosCompetitivo);
        void ActualizarRangoV(byte idRangoV, string nombre, int numero, int puntosCompetitivo);
        void EliminarRangoV(byte idRangoV);

        #endregion

        #region Inventario
        void InsertarInventario(uint idCuentaL, uint esenciaAzul, uint puntosRiot);
        void ActualizarInventario(uint idInventario, uint esenciaAzul, uint puntosRiot);
        void EliminarInventario(uint idInventario);
        Inventario? ObtenerInventario(uint idInventario);
        IEnumerable<Inventario> ObtenerInventarios();
        #endregion

        #region TipoObjeto
        void InsertarTipoObjeto(string nombre);
        void ActualizarTipoObjeto(int idTipoObjeto, string nombre);
        void EliminarTipoObjeto(int idTipoObjeto);
        TipoObjeto? ObtenerTipoObjeto(int idTipoObjeto);
        IEnumerable<TipoObjeto> ObtenerTiposObjeto();
        #endregion

        #region Objeto
        void InsertarObjeto(int idTipoObjeto, string nombre, uint precioEA, uint precioRP);
        void ActualizarObjeto(int idObjeto, string nombre, uint precioEA, uint precioRP, int idTipoObjeto);
        void EliminarObjeto(int idObjeto);
        Objeto? ObtenerObjeto(int idObjeto);
        IEnumerable<Objeto> ObtenerObjetos();
        #endregion

        #region CuentaLeagueOfLegends
        void InsertarCuentaLOL(string nombre, int nivel, int puntosCompetitivo, uint idCuenta, byte idRangoL);
        void ActualizarCuentaLOL(uint idCuentaL, string nombre, int nivel, int puntosCompetitivo);
        void EliminarCuentaLOL(uint idCuentaL);
        CuentaLeagueOfLeguends? ObtenerCuentaLOL(uint idCuentaL);
        IEnumerable<CuentaLeagueOfLeguends> ObtenerCuentasLOL();
        #endregion

        #region InventarioObjeto
        void InsertarInventarioObjeto(int idInventario, int idObjeto, byte cantidad);
        void ActualizarInventarioObjeto(int idInventario, int idObjeto, byte cantidad);
        void EliminarInventarioObjeto(int idInventario, int idObjeto);
        IEnumerable<InventarioObjeto> ObtenerInventariosObjeto();
        #endregion
    }
}