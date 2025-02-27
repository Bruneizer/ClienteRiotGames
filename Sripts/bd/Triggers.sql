DELIMITER $$


DROP TRIGGER IF EXISTS trgCuentaRiot $$
CREATE TRIGGER trgCuentaRiot
BEFORE INSERT ON CuentaRiot
FOR EACH ROW
BEGIN
    DECLARE serverExiste INT;
    
    SELECT COUNT(*) INTO serverExiste
    FROM Server 
    WHERE idServer = NEW.idServer;
    
    IF serverExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El servidor especificado no existe';
    END IF;
    
    IF LENGTH(NEW.Password) < 8 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La contraseña debe tener al menos 8 caracteres';
    END IF;
    
    IF NEW.Password NOT REGEXP '^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La contraseña debe contener al menos una mayúscula, un número y un carácter especial';
    END IF;
    
    IF NOT NEW.eMail REGEXP '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Formato de email inválido';
    END IF;
    
    SET NEW.Password = SHA2(NEW.Password, 256);
END $$

DELIMITER ;

-- Trigger para CuentaRiot
DELIMITER $$
DROP TRIGGER IF EXISTS trgCuentaRiot $$
CREATE TRIGGER trgCuentaRiot
BEFORE INSERT ON CuentaRiot
FOR EACH ROW
BEGIN
    DECLARE serverExiste INT;
    
    SELECT COUNT(*) INTO serverExiste
    FROM Server 
    WHERE idServer = NEW.idServer;
    
    IF serverExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El servidor especificado no existe';
    END IF;
    
    IF LENGTH(NEW.Password) < 8 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La contraseña debe tener al menos 8 caracteres';
    END IF;
    
    IF NEW.Password NOT REGEXP '^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La contraseña debe contener al menos una mayúscula, un número y un carácter especial';
    END IF;
    
    IF NOT NEW.eMail REGEXP '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Formato de email inválido';
    END IF;
    
    SET NEW.Password = SHA2(NEW.Password, 256);
END $$
DELIMITER ;

-- Trigger para RangoL
DELIMITER $$
DROP TRIGGER IF EXISTS trgRangoL $$
CREATE TRIGGER trgRangoL
BEFORE INSERT ON RangoL
FOR EACH ROW
BEGIN
    IF NEW.Numero < 0 OR NEW.Numero > 10 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El número de rango debe estar entre 0 y 10';
    END IF;
    
    IF NEW.PuntosCompetitivo < 0 OR NEW.PuntosCompetitivo > 100 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Los puntos competitivos deben estar entre 0 y 100';
    END IF;
END $$
DELIMITER ;

-- Trigger para RangoV
DELIMITER $$
DROP TRIGGER IF EXISTS trgRangoV $$
CREATE TRIGGER trgRangoV
BEFORE INSERT ON RangoV
FOR EACH ROW
BEGIN
    IF NEW.Numero < 0 OR NEW.Numero > 10 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El número de rango debe estar entre 0 y 10';
    END IF;
    
    IF NEW.PuntosCompetitivo < 0 OR NEW.PuntosCompetitivo > 100 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Los puntos competitivos deben estar entre 0 y 100';
    END IF;
END $$
DELIMITER ;

-- Trigger para CuentaLeagueOfLeguends
DELIMITER $$
DROP TRIGGER IF EXISTS trgLeague $$
CREATE TRIGGER trgLeague
BEFORE INSERT ON CuentaLeagueOfLeguends
FOR EACH ROW
BEGIN
    DECLARE cuentaExiste INT;
    DECLARE rangoExiste INT;
    
    SELECT COUNT(*) INTO cuentaExiste
    FROM CuentaRiot 
    WHERE idCuenta = NEW.idCuenta;
    
    SELECT COUNT(*) INTO rangoExiste
    FROM RangoL 
    WHERE idRangoL = NEW.idRangoL;
    
    IF cuentaExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La cuenta Riot especificada no existe';
    END IF;
    
    IF rangoExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El rango especificado no existe';
    END IF;
    
    IF NEW.Nivel <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nivel debe ser mayor a 0';
    END IF;
    
    IF NEW.Nombre IS NULL OR TRIM(NEW.Nombre) = '' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nombre no puede estar vacío';
    END IF;
    
    IF NEW.PuntosCompetitivo < 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Los puntos competitivos no pueden ser negativos';
    END IF;
END $$
DELIMITER ;

-- Trigger para CuentaValorant
DELIMITER $$
DROP TRIGGER IF EXISTS trgValorant $$
CREATE TRIGGER trgValorant
BEFORE INSERT ON CuentaValorant
FOR EACH ROW
BEGIN
    DECLARE cuentaExiste INT;
    DECLARE rangoExiste INT;
    
    SELECT COUNT(*) INTO cuentaExiste
    FROM CuentaRiot 
    WHERE idCuenta = NEW.idCuenta;
    
    SELECT COUNT(*) INTO rangoExiste
    FROM RangoV 
    WHERE idRangoV = NEW.idRangoV;
    
    IF cuentaExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La cuenta Riot especificada no existe';
    END IF;
    
    IF rangoExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El rango especificado no existe';
    END IF;
    
    IF NEW.Nivel < 1 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nivel debe ser mayor a 0';
    END IF;
    
    IF NEW.Experiencia < 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La experiencia no puede ser negativa';
    END IF;
    
    IF NEW.Nombre IS NULL OR TRIM(NEW.Nombre) = '' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nombre no puede estar vacío';
    END IF;
    
    IF NEW.Nivel > 500 THEN  -- Asumiendo un nivel máximo de 500
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nivel no puede ser mayor a 500';
    END IF;
END $$
DELIMITER ;

-- Trigger para Inventario
DELIMITER $$
DROP TRIGGER IF EXISTS trgInventario $$
CREATE TRIGGER trgInventario
BEFORE INSERT ON Inventario
FOR EACH ROW
BEGIN
    DECLARE cuentaLExiste INT;
    
    SELECT COUNT(*) INTO cuentaLExiste
    FROM CuentaLeagueOfLeguends 
    WHERE idCuentaL = NEW.idCuentaL;
    
    IF cuentaLExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La cuenta de League of Legends especificada no existe';
    END IF;
    
    IF NEW.EsenciaAzul < 0 OR NEW.PuntosRiot < 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Los valores de EsenciaAzul y PuntosRiot no pueden ser negativos';
    END IF;
END $$
DELIMITER ;

-- Trigger para TipoObjeto
DELIMITER $$
DROP TRIGGER IF EXISTS trgTipoObjeto $$
CREATE TRIGGER trgTipoObjeto
BEFORE INSERT ON TipoObjeto
FOR EACH ROW
BEGIN
    IF LENGTH(NEW.Nombre) < 3 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nombre del tipo de objeto debe tener al menos 3 caracteres';
    END IF;
END $$
DELIMITER ;

-- Trigger para Objeto
DELIMITER $$
DROP TRIGGER IF EXISTS trgObjeto $$
CREATE TRIGGER trgObjeto
BEFORE INSERT ON Objeto
FOR EACH ROW
BEGIN
    DECLARE tipoExiste INT;
    
    SELECT COUNT(*) INTO tipoExiste
    FROM TipoObjeto 
    WHERE idTipoObjeto = NEW.idTipoObjeto;
    
    IF tipoExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El tipo de objeto especificado no existe';
    END IF;
    
    IF NEW.PrecioEA < 0 OR NEW.PrecioRP < 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Los precios no pueden ser negativos';
    END IF;
    
    IF LENGTH(NEW.Nombre) < 3 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nombre del objeto debe tener al menos 3 caracteres';
    END IF;
END $$
DELIMITER ;

-- Trigger para InventarioObjeto
DELIMITER $$
CREATE TRIGGER trgInvObjeto
BEFORE INSERT ON InventarioObjeto
FOR EACH ROW
BEGIN
    DECLARE invExiste INT;
    DECLARE objExiste INT;
    DECLARE objetoExiste INT;
    
    SELECT COUNT(*) INTO invExiste
    FROM Inventario 
    WHERE idInventario = NEW.idInventario;
    
    SELECT COUNT(*) INTO objExiste
    FROM Objeto 
    WHERE idObjeto = NEW.idObjeto;
    
    SELECT COUNT(*) INTO objetoExiste
    FROM InventarioObjeto
    WHERE idInventario = NEW.idInventario AND idObjeto = NEW.idObjeto;
    
    IF invExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El inventario especificado no existe';
    END IF;
    
    IF objExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El objeto especificado no existe';
    END IF;
    
    IF NEW.Cantidad <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La cantidad debe ser mayor a 0';
    END IF;
    
    IF objetoExiste > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Este objeto ya existe en el inventario';
    END IF;
END $$
DELIMITER ;

-- Ejemplo para CuentaRiot
DELIMITER $$
DROP TRIGGER IF EXISTS trgCuentaRiotUpdate $$
CREATE TRIGGER trgCuentaRiotUpdate
BEFORE UPDATE ON CuentaRiot
FOR EACH ROW
BEGIN
    DECLARE serverExiste INT;
    
    IF NEW.Password != OLD.Password THEN
        IF LENGTH(NEW.Password) < 8 THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La contraseña debe tener al menos 8 caracteres';
        END IF;
        
        IF NEW.Password NOT REGEXP '^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])' THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'La contraseña debe contener al menos una mayúscula, un número y un carácter especial';
        END IF;
        
        SET NEW.Password = SHA2(NEW.Password, 256);
    END IF;
    
    -- Validar servidor existente
    SELECT COUNT(*) INTO serverExiste
    FROM Server 
    WHERE idServer = NEW.idServer;
    
    IF serverExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El servidor especificado no existe';
    END IF;
END $$
DELIMITER ;

-- Trigger para Inventario
DELIMITER $$
DROP TRIGGER IF EXISTS trgInventarioDelete $$
CREATE TRIGGER trgInventarioDelete
BEFORE DELETE ON Inventario
FOR EACH ROW
BEGIN
    -- Verificar si hay objetos asociados
    DECLARE objetosAsociados INT;
    SELECT COUNT(*) INTO objetosAsociados 
    FROM InventarioObjeto 
    WHERE idInventario = OLD.idInventario;
    
    IF objetosAsociados > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No se puede eliminar un inventario con objetos asociados';
    END IF;
END $$
DELIMITER ;

-- Ejemplo para CuentaRiot
DELIMITER $$
DROP TRIGGER IF EXISTS trgCuentaRiotDelete $$
CREATE TRIGGER trgCuentaRiotDelete
BEFORE DELETE ON CuentaRiot
FOR EACH ROW
BEGIN
    DECLARE cuentasLol INT;
    DECLARE cuentasValorant INT;
    
    SELECT COUNT(*) INTO cuentasLol
    FROM CuentaLeagueOfLeguends
    WHERE idCuenta = OLD.idCuenta;
    
    SELECT COUNT(*) INTO cuentasValorant
    FROM CuentaValorant
    WHERE idCuenta = OLD.idCuenta;
    
    IF cuentasLol > 0 OR cuentasValorant > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No se puede eliminar una cuenta con juegos asociados';
    END IF;
END $$
DELIMITER ; 

-- Trigger para InventarioObjeto
DELIMITER $$
DROP TRIGGER IF EXISTS trgInvObjeto $$
CREATE TRIGGER trgInvObjeto
BEFORE INSERT ON InventarioObjeto
FOR EACH ROW
BEGIN
    DECLARE invExiste INT;
    DECLARE objExiste INT;
    DECLARE objetoExiste INT;
    
    SELECT COUNT(*) INTO invExiste
    FROM Inventario 
    WHERE idInventario = NEW.idInventario;
    
    SELECT COUNT(*) INTO objExiste
    FROM Objeto 
    WHERE idObjeto = NEW.idObjeto;
    
    SELECT COUNT(*) INTO objetoExiste
    FROM InventarioObjeto
    WHERE idInventario = NEW.idInventario AND idObjeto = NEW.idObjeto;
    
    IF invExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El inventario especificado no existe';
    END IF;
    
    IF objExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El objeto especificado no existe';
    END IF;
    
    IF NEW.Cantidad <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La cantidad debe ser mayor a 0';
    END IF;
    
    IF objetoExiste > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Este objeto ya existe en el inventario';
    END IF;
END $$
DELIMITER ; 

-- Trigger para RangoL UPDATE
DELIMITER $$
DROP TRIGGER IF EXISTS trgRangoLUpdate $$
CREATE TRIGGER trgRangoLUpdate
BEFORE UPDATE ON RangoL
FOR EACH ROW
BEGIN
    IF NEW.Numero < 0 OR NEW.Numero > 4 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El número de rango debe estar entre 0 y 4';
    END IF;
    
    IF NEW.PuntosCompetitivo < 0 OR NEW.PuntosCompetitivo > 100 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Los puntos competitivos deben estar entre 0 y 100';
    END IF;
END $$
DELIMITER ; 

-- Trigger UPDATE para CuentaValorant
DELIMITER $$
DROP TRIGGER IF EXISTS trgValorantUpdate $$
CREATE TRIGGER trgValorantUpdate
BEFORE UPDATE ON CuentaValorant
FOR EACH ROW
BEGIN
    DECLARE rangoExiste INT;
    
    SELECT COUNT(*) INTO rangoExiste
    FROM RangoV 
    WHERE idRangoV = NEW.idRangoV;
    
    IF rangoExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El rango especificado no existe';
    END IF;
    
    IF NEW.Nivel < 1 OR NEW.Nivel > 500 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nivel debe estar entre 1 y 500';
    END IF;
    
    IF NEW.Experiencia < 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La experiencia no puede ser negativa';
    END IF;
    
    IF NEW.Nombre IS NULL OR TRIM(NEW.Nombre) = '' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nombre no puede estar vacío';
    END IF;
END $$
DELIMITER ;

-- Trigger UPDATE para CuentaLeagueOfLeguends
DELIMITER $$
DROP TRIGGER IF EXISTS trgLeagueUpdate $$
CREATE TRIGGER trgLeagueUpdate
BEFORE UPDATE ON CuentaLeagueOfLeguends
FOR EACH ROW
BEGIN
    DECLARE rangoExiste INT;
    
    SELECT COUNT(*) INTO rangoExiste
    FROM RangoL 
    WHERE idRangoL = NEW.idRangoL;
    
    IF rangoExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El rango especificado no existe';
    END IF;
    
    IF NEW.Nivel <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nivel debe ser mayor a 0';
    END IF;
    
    IF NEW.Nombre IS NULL OR TRIM(NEW.Nombre) = '' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El nombre no puede estar vacío';
    END IF;
    
    IF NEW.PuntosCompetitivo < 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Los puntos competitivos no pueden ser negativos';
    END IF;
END $$
DELIMITER ;

-- Trigger UPDATE para Inventario
DELIMITER $$
DROP TRIGGER IF EXISTS trgInventarioUpdate $$
CREATE TRIGGER trgInventarioUpdate
BEFORE UPDATE ON Inventario
FOR EACH ROW
BEGIN
    DECLARE cuentaLExiste INT;
    
    SELECT COUNT(*) INTO cuentaLExiste
    FROM CuentaLeagueOfLeguends 
    WHERE idCuentaL = NEW.idCuentaL;
    
    IF cuentaLExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La cuenta de League of Legends especificada no existe';
    END IF;
    
    IF NEW.EsenciaAzul < 0 OR NEW.PuntosRiot < 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Los valores de EsenciaAzul y PuntosRiot no pueden ser negativos';
    END IF;
END $$
DELIMITER ;

-- Trigger UPDATE para InventarioObjeto
DELIMITER $$
DROP TRIGGER IF EXISTS trgInvObjetoUpdate $$
CREATE TRIGGER trgInvObjetoUpdate
BEFORE UPDATE ON InventarioObjeto
FOR EACH ROW
BEGIN
    DECLARE invExiste INT;
    DECLARE objExiste INT;
    
    SELECT COUNT(*) INTO invExiste
    FROM Inventario 
    WHERE idInventario = NEW.idInventario;
    
    SELECT COUNT(*) INTO objExiste
    FROM Objeto 
    WHERE idObjeto = NEW.idObjeto;
    
    IF invExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El inventario especificado no existe';
    END IF;
    
    IF objExiste = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El objeto especificado no existe';
    END IF;
    
    IF NEW.Cantidad <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La cantidad debe ser mayor a 0';
    END IF;
END $$
DELIMITER ;

-- Trigger UPDATE para RangoV
DELIMITER $$
DROP TRIGGER IF EXISTS trgRangoVUpdate $$
CREATE TRIGGER trgRangoVUpdate
BEFORE UPDATE ON RangoV
FOR EACH ROW
BEGIN
    IF NEW.Numero < 0 OR NEW.Numero > 4 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El número de rango debe estar entre 0 y 4';
    END IF;
    
    IF NEW.PuntosCompetitivo < 0 OR NEW.PuntosCompetitivo > 100 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Los puntos competitivos deben estar entre 0 y 100';
    END IF;
END $$
DELIMITER ; 