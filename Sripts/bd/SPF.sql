DELIMITER $$

CREATE PROCEDURE InsertarServer(
    IN UnNombre VARCHAR(45),
    IN UnAbreviado VARCHAR(5)
)
BEGIN
    IF LENGTH(UnNombre) > 45 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nombre del servidor es demasiado largo';
    END IF;

    IF LENGTH(UnAbreviado) > 5 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La abreviatura del servidor es demasiado larga';
    END IF;

    INSERT INTO Server(Nombre, Abreviado)
    VALUES (UnNombre, UnAbreviado);
END $$

CREATE PROCEDURE ActualizarServer(
    IN UnidServer INT,
    IN UnNombre VARCHAR(50),
    IN UnAbreviado VARCHAR(10)
)
BEGIN
    UPDATE Server 
    SET Nombre = UnNombre, Abreviado = UnAbreviado
    WHERE idServer = UnidServer;
END $$

CREATE PROCEDURE EliminarServer(
    IN UnidServer INT
)
BEGIN
    DELETE FROM Server WHERE idServer = UnidServer;
END $$

CREATE PROCEDURE ObtenerServer(
    IN UnidServer INT
)
BEGIN
    SELECT * FROM Server WHERE idServer = UnidServer;
END $$

-- Store Functions para Server
CREATE FUNCTION fn_ExisteServer(
    UnidServer INT
) RETURNS BOOLEAN
DETERMINISTIC
BEGIN
    DECLARE existe BOOLEAN;
    SELECT COUNT(*) > 0 INTO existe
    FROM Server
    WHERE idServer = UnidServer;
    RETURN existe;
END $$

CREATE FUNCTION fn_ObtenerNombreServer(
    UnidServer INT
) RETURNS VARCHAR(50)
DETERMINISTIC
BEGIN
    DECLARE nombre VARCHAR(50);
    SELECT Nombre INTO nombre
    FROM Server
    WHERE idServer = UnidServer;
    RETURN nombre;
END $$

-- Store Procedures para CuentaRiot
CREATE PROCEDURE InsertarCuentaRiot(
    IN UnNombre VARCHAR(50),
    IN UnPassword VARCHAR(255),
    IN UnEmail VARCHAR(45),
    IN UnIdServer TINYINT
)
BEGIN
    IF LENGTH(UnEmail) > 45 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El Email es demasiado largo';
    END IF;

    INSERT INTO CuentaRiot(Nombre, Password, Email, IdServer)
    VALUES (UnNombre,UnPassword, UnEmail, UnIdServer);
END $$

CREATE PROCEDURE ActualizarCuentaRiot(
    IN UnidCuenta INT,
    IN UnidServer INT,
    IN UnEmail VARCHAR(100),
    IN UnPassword VARCHAR(255),
    IN UnNombre VARCHAR(50)
)
BEGIN
    UPDATE CuentaRiot 
    SET idServer = UnidServer,
        Email = UnEmail,
        Password = UnPassword,
        Nombre = UnNombre
    WHERE idCuenta = UnidCuenta;
END $$

CREATE PROCEDURE ObtenerCuentasRiotPorServer(
    IN UnidServer INT
)
BEGIN
    SELECT * FROM CuentaRiot WHERE idServer = UnidServer;
END $$

-- Store Functions para CuentaRiot
CREATE FUNCTION fn_ExisteCuentaRiot(
    UnEmail VARCHAR(100)
) RETURNS BOOLEAN
DETERMINISTIC
BEGIN
    DECLARE existe BOOLEAN;
    SELECT COUNT(*) > 0 INTO existe
    FROM CuentaRiot
    WHERE Email = UnEmail;
    RETURN existe;
END $$

-- Store Procedures para CuentaLeagueOfLeguends
CREATE PROCEDURE InsertarCuentaLOL(
    IN UnidCuenta INT,
    IN UnidRangoL INT,
    IN UnNombre VARCHAR(50),
    IN UnNivel INT,
    IN UnPuntosCompetitivo INT
)
BEGIN
    INSERT INTO CuentaLeagueOfLeguends(idCuenta, idRangoL, Nombre, Nivel, PuntosCompetitivo)
    VALUES (UnidCuenta, UnidRangoL, UnNombre, UnNivel, UnPuntosCompetitivo);
END $$

CREATE PROCEDURE ActualizarRangoLOL(
    IN UnidCuentaL INT,
    IN UnidRangoL INT,
    IN UnPuntosCompetitivo INT
)
BEGIN
    UPDATE CuentaLeagueOfLeguends 
    SET idRangoL = UnidRangoL,
        PuntosCompetitivo = UnPuntosCompetitivo
    WHERE idCuentaL = UnidCuentaL;
END $$

-- Store Functions para CuentaLeagueOfLeguends
CREATE FUNCTION fn_ObtenerNivelLOL(
    UnidCuentaL INT
) RETURNS INT
DETERMINISTIC
BEGIN
    DECLARE nivel INT;
    SELECT Nivel INTO nivel
    FROM CuentaLeagueOfLeguends
    WHERE idCuentaL = UnidCuentaL;
    RETURN nivel;
END $$

-- Store Procedures para CuentaValorant
CREATE PROCEDURE InsertarCuentaValorant(
    IN UnidCuenta INT,
    IN UnidRangoV INT,
    IN UnNombre VARCHAR(50),
    IN UnNivel INT,
    IN UnExperiencia INT
)
BEGIN
    INSERT INTO CuentaValorant(idCuenta, idRangoV, Nombre, Nivel, Experiencia)
    VALUES (UnidCuenta, UnidRangoV, UnNombre, UnNivel, UnExperiencia);
END $$

CREATE PROCEDURE ActualizarRangoValorant(
    IN UnidCuentaV INT,
    IN UnidRangoV INT
)
BEGIN
    UPDATE CuentaValorant 
    SET idRangoV = UnidRangoV
    WHERE idCuentaV = UnidCuentaV;
END $$
-- Store Functions para CuentaValorant
CREATE FUNCTION fn_ObtenerNivelValorant(
    UnidCuentaV INT
) RETURNS INT
DETERMINISTIC
BEGIN
    DECLARE nivel INT;
    SELECT Nivel INTO nivel
    FROM CuentaValorant
    WHERE idCuentaV = UnidCuentaV;
    RETURN nivel;
END $$

-- Store Procedures para Inventario
CREATE PROCEDURE InsertarInventario(
    IN UnidCuentaL INT,
    IN UnEsenciaAzul INT,
    IN UnPuntosRiot INT
)
BEGIN
    INSERT INTO Inventario(idCuentaL, EsenciaAzul, PuntosRiot)
    VALUES (UnidCuentaL, UnEsenciaAzul, UnPuntosRiot);
END $$

CREATE PROCEDURE ActualizarPuntosInventario(
    IN UnidInventario INT,
    IN UnEsenciaAzul INT,
    IN UnPuntosRiot INT
)
BEGIN
    UPDATE Inventario 
    SET EsenciaAzul = UnEsenciaAzul,
        PuntosRiot = UnPuntosRiot
    WHERE idInventario = UnidInventario;
END $$

-- Store Functions para Inventario
CREATE FUNCTION fn_ObtenerTotalPuntosRiot(
    UnidCuentaL INT
) RETURNS INT
DETERMINISTIC
BEGIN
    DECLARE total INT;
    SELECT PuntosRiot INTO total
    FROM Inventario
    WHERE idCuentaL = UnidCuentaL;
    RETURN IFNULL(total, 0);
END $$

-- Store Procedures para RangoL
DELIMITER $$
CREATE PROCEDURE InsertarRangoL(
    IN UnNombre VARCHAR(50),
    IN UnNumero INT,
    IN UnPuntosCompetitivo INT
)
BEGIN
    INSERT INTO RangoL(Nombre, Numero, PuntosCompetitivo)
    VALUES (UnNombre, UnNumero, UnPuntosCompetitivo);
END $$

CREATE PROCEDURE EliminarRangoL(
    IN UnidRangoL INT
)
BEGIN
    DELETE FROM RangoL WHERE idRangoL = UnidRangoL;
END $$

-- Store Procedures para RangoV
CREATE PROCEDURE InsertarRangoV(
    IN UnNombre VARCHAR(50),
    IN UnNumero INT,
    IN UnPuntosCompetitivo INT
)
BEGIN
    INSERT INTO RangoV(Nombre, Numero, PuntosCompetitivo)
    VALUES (UnNombre, UnNumero, UnPuntosCompetitivo);
END $$

CREATE PROCEDURE EliminarRangoV(
    IN UnidRangoV INT
)
BEGIN
    DELETE FROM RangoV WHERE idRangoV = UnidRangoV;
END $$

-- Store Procedures para TipoObjeto
CREATE PROCEDURE InsertarTipoObjeto(
    IN UnNombre VARCHAR(50)
)
BEGIN
    INSERT INTO TipoObjeto(Nombre)
    VALUES (UnNombre);
END $$

CREATE PROCEDURE EliminarTipoObjeto(
    IN UnidTipoObjeto INT
)
BEGIN
    DELETE FROM TipoObjeto WHERE idTipoObjeto = UnidTipoObjeto;
END $$

-- Store Procedures para Objeto
CREATE PROCEDURE InsertarObjeto(
    IN UnidTipoObjeto INT,
    IN UnNombre VARCHAR(50),
    IN UnPrecioEA INT,
    IN UnPrecioRP INT
)
BEGIN
    INSERT INTO Objeto(idTipoObjeto, Nombre, PrecioEA, PrecioRP)
    VALUES (UnidTipoObjeto, UnNombre, UnPrecioEA, UnPrecioRP);
END $$

CREATE PROCEDURE EliminarObjeto(
    IN UnidObjeto INT
)
BEGIN
    DELETE FROM Objeto WHERE idObjeto = UnidObjeto;
END $$

-- Store Procedures para InventarioObjeto
CREATE PROCEDURE InsertarInventarioObjeto(
    IN UnidInventario INT,
    IN UnidObjeto INT,
    IN UnCantidad INT
)
BEGIN
    INSERT INTO InventarioObjeto(idInventario, idObjeto, Cantidad)
    VALUES (UnidInventario, UnidObjeto, UnCantidad);
END $$

CREATE PROCEDURE EliminarInventarioObjeto(
    IN UnidInventario INT,
    IN UnidObjeto INT
)
BEGIN
    DELETE FROM InventarioObjeto 
    WHERE idInventario = UnidInventario AND idObjeto = UnidObjeto;
END $$

-- Agregar procedimientos de eliminación faltantes
CREATE PROCEDURE EliminarCuentaRiot(
    IN UnidCuenta INT
)
BEGIN
    DELETE FROM CuentaRiot WHERE idCuenta = UnidCuenta;
END $$

CREATE PROCEDURE EliminarCuentaLOL(
    IN UnidCuentaL INT
)
BEGIN
    DELETE FROM CuentaLeagueOfLeguends WHERE idCuentaL = UnidCuentaL;
END $$

CREATE PROCEDURE EliminarCuentaValorant(
    IN UnidCuentaV INT
)
BEGIN
    DELETE FROM CuentaValorant WHERE idCuentaV = UnidCuentaV;
END $$

CREATE PROCEDURE EliminarInventario(
    IN UnidInventario INT
)
BEGIN
    DELETE FROM Inventario WHERE idInventario = UnidInventario;
END $$

-- Obtener detalles completos de Server
DELIMITER $$
CREATE PROCEDURE ObtenerDetallesServer(
    IN UnidServer INT
)
BEGIN
    SELECT Server.*, 
           COUNT(CuentaRiot.idCuenta) as TotalCuentas
    FROM Server
    LEFT JOIN CuentaRiot ON Server.idServer = CuentaRiot.idServer
    WHERE Server.idServer = UnidServer
    GROUP BY Server.idServer;
END $$

-- Obtener detalles completos de CuentaRiot con sus juegos
CREATE PROCEDURE ObtenerDetallesCuentaRiot(
    IN UnidCuenta INT UNSIGNED
)
BEGIN
    SELECT CuentaRiot.*,
           Server.Nombre AS NombreServer
    FROM CuentaRiot
    JOIN Server ON CuentaRiot.idServer = Server.idServer
    WHERE CuentaRiot.idCuenta = UnidCuenta;
END $$

-- Obtener detalles completos de CuentaLOL con inventario
CREATE PROCEDURE ObtenerDetallesCuentaLOL(
    IN UnidCuentaL INT
)
BEGIN
    SELECT CuentaLeagueOfLeguends.*,
           CuentaRiot.Email,
           CuentaRiot.Nombre,
           Server.Nombre as NombreServer,
           RangoL.Nombre as NombreRango,
           Inventario.EsenciaAzul,
           Inventario.PuntosRiot
    FROM CuentaLeagueOfLeguends
    JOIN CuentaRiot ON CuentaLeagueOfLeguends.idCuenta = CuentaRiot.idCuenta
    JOIN Server ON CuentaRiot.idServer = Server.idServer
    JOIN RangoL ON CuentaLeagueOfLeguends.idRangoL = RangoL.idRangoL
    LEFT JOIN Inventario ON CuentaLeagueOfLeguends.idCuentaL = Inventario.idCuentaL
    WHERE CuentaLeagueOfLeguends.idCuentaL = UnidCuentaL;
END $$

-- Obtener detalles completos de CuentaValorant
CREATE PROCEDURE ObtenerDetallesCuentaValorant(
    IN UnidCuentaV INT
)
BEGIN
    SELECT CuentaValorant.*,
           CuentaRiot.Email,
           CuentaRiot.Nombre,
           Server.Nombre as NombreServer,
           RangoV.Nombre as NombreRango
    FROM CuentaValorant
    JOIN CuentaRiot ON CuentaValorant.idCuenta = CuentaRiot.idCuenta
    JOIN Server ON CuentaRiot.idServer = Server.idServer
    JOIN RangoV ON CuentaValorant.idRangoV = RangoV.idRangoV
    WHERE CuentaValorant.idCuentaV = UnidCuentaV;
END $$

-- Obtener detalles completos de Inventario con objetos
CREATE PROCEDURE ObtenerDetallesInventario(
    IN UnidInventario INT
)
BEGIN
    SELECT Inventario.*,
           CuentaLeagueOfLeguends.Nombre as NombreCuentaLOL,
           InventarioObjeto.Cantidad,
           Objeto.Nombre as NombreObjeto,
           Objeto.PrecioEA,
           Objeto.PrecioRP,
           TipoObjeto.Nombre as TipoObjeto
    FROM Inventario
    JOIN CuentaLeagueOfLeguends ON Inventario.idCuentaL = CuentaLeagueOfLeguends.idCuentaL
    LEFT JOIN InventarioObjeto ON Inventario.idInventario = InventarioObjeto.idInventario
    LEFT JOIN Objeto ON InventarioObjeto.idObjeto = Objeto.idObjeto
    LEFT JOIN TipoObjeto ON Objeto.idTipoObjeto = TipoObjeto.idTipoObjeto
    WHERE Inventario.idInventario = UnidInventario;
END $$

-- Obtener detalles completos de Objeto
CREATE PROCEDURE ObtenerDetallesObjeto(
    IN UnidObjeto INT
)
BEGIN
    SELECT Objeto.idObjeto,
           Objeto.Nombre,
           Objeto.PrecioEA,
           Objeto.PrecioRP,
           TipoObjeto.Nombre as TipoObjeto,
           COUNT(InventarioObjeto.idInventario) as TotalInventarios
    FROM Objeto
    LEFT JOIN TipoObjeto ON Objeto.idTipoObjeto = TipoObjeto.idTipoObjeto
    LEFT JOIN InventarioObjeto ON Objeto.idObjeto = InventarioObjeto.idObjeto
    WHERE Objeto.idObjeto = UnidObjeto
    GROUP BY Objeto.idObjeto, Objeto.Nombre, Objeto.PrecioEA, Objeto.PrecioRP, TipoObjeto.Nombre;
END $$
-- Obtener detalles completos de RangoL
CREATE PROCEDURE ObtenerDetallesRangoL(
    IN UnidRangoL INT
)
BEGIN
    SELECT RangoL.*,
           COUNT(CuentaLeagueOfLeguends.idCuentaL) as TotalJugadores
    FROM RangoL
    LEFT JOIN CuentaLeagueOfLeguends ON RangoL.idRangoL = CuentaLeagueOfLeguends.idRangoL
    WHERE RangoL.idRangoL = UnidRangoL
    GROUP BY RangoL.idRangoL;
END $$

-- Obtener detalles completos de RangoV
CREATE PROCEDURE ObtenerDetallesRangoV(
    IN UnidRangoV INT
)
BEGIN
    SELECT RangoV.*,
           COUNT(CuentaValorant.idCuentaV) as TotalJugadores
    FROM RangoV
    LEFT JOIN CuentaValorant ON RangoV.idRangoV = CuentaValorant.idRangoV
    WHERE RangoV.idRangoV = UnidRangoV
    GROUP BY RangoV.idRangoV;
END $$

-- Obtener detalles completos de TipoObjeto
CREATE PROCEDURE ObtenerDetallesTipoObjeto(
    IN UnidTipoObjeto INT
)
BEGIN
    SELECT TipoObjeto.*,
           COUNT(Objeto.idObjeto) as TotalObjetos
    FROM TipoObjeto
    LEFT JOIN Objeto ON TipoObjeto.idTipoObjeto = Objeto.idTipoObjeto
    WHERE TipoObjeto.idTipoObjeto = UnidTipoObjeto
    GROUP BY TipoObjeto.idTipoObjeto;
END $$

DELIMITER $$

CREATE PROCEDURE LoginCuentaRiot(
    IN UnEmail VARCHAR(45),
    IN UnPassword VARCHAR(45),
    OUT UnResultado INT,
    OUT UnMensaje VARCHAR(100)
)
BEGIN
    DECLARE cuenta_id INT;
    DECLARE cuenta_password VARCHAR(64);
    
    SET UnResultado = 0;
    SET UnMensaje = '';
    
    SELECT idCuenta, Password 
    INTO cuenta_id, cuenta_password
    FROM CuentaRiot 
    WHERE Email = UnEmail;
    
    IF cuenta_id IS NULL THEN
        SET UnMensaje = 'Email no encontrado';
        SET UnResultado = 0;
    ELSE
        IF cuenta_password = SHA2(UnPassword, 256) THEN
            SET UnMensaje = 'Login exitoso';
            SET UnResultado = 1;

            SELECT 
                cr.idCuenta,
                cr.Nombre AS Nombre,
                cr.Email,
                s.Nombre AS NombreServer,
                s.Abreviado AS AbreviadoServer,
                COALESCE(cl.Nombre, '') AS NombreLOL,
                COALESCE(cv.Nombre, '') AS NombreValorant
            FROM CuentaRiot cr
            INNER JOIN Server s ON cr.idServer = s.idServer
            LEFT JOIN CuentaLeagueOfLeguends cl ON cr.idCuenta = cl.idCuenta
            LEFT JOIN CuentaValorant cv ON cr.idCuenta = cv.idCuenta
            WHERE cr.idCuenta = cuenta_id;
        ELSE
            SET UnMensaje = 'Contraseña incorrecta';
            SET UnResultado = 0;
        END IF;
    END IF;
END $$

-- Store Procedures de actualización faltantes
DELIMITER $$

CREATE PROCEDURE ActualizarTipoObjeto(
    IN UnidTipoObjeto INT,
    IN UnNombre VARCHAR(45)
)
BEGIN
    UPDATE TipoObjeto 
    SET Nombre = UnNombre
    WHERE idTipoObjeto = UnidTipoObjeto;
END $$

CREATE PROCEDURE ActualizarObjeto(
    IN UnidObjeto INT,
    IN UnNombre VARCHAR(45),
    IN UnPrecioEA MEDIUMINT,
    IN UnPrecioRP MEDIUMINT,
    IN UnidTipoObjeto INT
)
BEGIN
    UPDATE Objeto 
    SET Nombre = UnNombre,
        PrecioEA = UnPrecioEA,
        PrecioRP = UnPrecioRP,
        idTipoObjeto = UnidTipoObjeto
    WHERE idObjeto = UnidObjeto;
END $$

CREATE PROCEDURE ActualizarRangoL(
    IN UnidRangoL INT,
    IN UnNombre VARCHAR(45),
    IN UnNumero INT,
    IN UnPuntosCompetitivo INT
)
BEGIN
    UPDATE RangoL 
    SET Nombre = UnNombre,
        Numero = UnNumero,
        PuntosCompetitivo = UnPuntosCompetitivo
    WHERE idRangoL = UnidRangoL;
END $$

CREATE PROCEDURE ActualizarRangoV(
    IN UnidRangoV INT,
    IN UnNombre VARCHAR(45),
    IN UnNumero INT,
    IN UnPuntosCompetitivo INT
)
BEGIN
    UPDATE RangoV 
    SET Nombre = UnNombre,
        Numero = UnNumero,
        PuntosCompetitivo = UnPuntosCompetitivo
    WHERE idRangoV = UnidRangoV;
END $$

CREATE PROCEDURE ActualizarInventarioObjeto(
    IN UnidInventario INT,
    IN UnidObjeto INT,
    IN UnCantidad TINYINT
)
BEGIN
    UPDATE InventarioObjeto 
    SET Cantidad = UnCantidad
    WHERE idInventario = UnidInventario AND idObjeto = UnidObjeto;
END $$

DELIMITER $$


CREATE PROCEDURE ObtenerRangoL(
    IN UnidRangoL INT
)
BEGIN
    SELECT * FROM RangoL WHERE idRangoL = UnidRangoL;
END $$

CREATE PROCEDURE ObtenerRangoV(
    IN UnidRangoV INT
)
BEGIN
    SELECT * FROM RangoV WHERE idRangoV = UnidRangoV;
END $$

DELIMITER ;
