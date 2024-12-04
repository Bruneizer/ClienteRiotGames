-- Creación de usuarios y asignación de privilegios
SET SQL_SAFE_UPDATES = 0;

-- Primero eliminamos los usuarios si existen
DROP USER IF EXISTS 'admin_riot'@'localhost';
DROP USER IF EXISTS 'developer_riot'@'localhost';
DROP USER IF EXISTS 'support_riot'@'localhost';
DROP USER IF EXISTS 'player_account'@'localhost';
DROP USER IF EXISTS 'guest_viewer'@'localhost';

-- 1. Administrador del Sistema
CREATE USER 'admin_riot'@'localhost' IDENTIFIED BY 'AdminR10t2024!';
GRANT ALL PRIVILEGES ON ClienteRiotGames.* TO 'admin_riot'@'localhost';

-- 2. Desarrollador
CREATE USER 'developer_riot'@'localhost' IDENTIFIED BY 'DevR10t2024!';
GRANT SELECT, INSERT, UPDATE, DELETE ON ClienteRiotGames.* TO 'developer_riot'@'localhost';
GRANT EXECUTE ON ClienteRiotGames.* TO 'developer_riot'@'localhost';

-- 3. Soporte al Cliente
CREATE USER 'support_riot'@'localhost' IDENTIFIED BY 'SuppR10t2024!';
-- Permisos de lectura general
GRANT SELECT ON ClienteRiotGames.* TO 'support_riot'@'localhost';
-- Permisos de actualización en tablas específicas
GRANT UPDATE ON ClienteRiotGames.CuentaRiot TO 'support_riot'@'localhost';
GRANT UPDATE ON ClienteRiotGames.CuentaLeagueOfLeguends TO 'support_riot'@'localhost';
GRANT UPDATE ON ClienteRiotGames.CuentaValorant TO 'support_riot'@'localhost';
-- Permisos para ejecutar procedimientos específicos
GRANT EXECUTE ON PROCEDURE ClienteRiotGames.InsertarServer TO 'support_riot'@'localhost';
GRANT EXECUTE ON PROCEDURE ClienteRiotGames.ActualizarServer TO 'support_riot'@'localhost';
GRANT EXECUTE ON PROCEDURE ClienteRiotGames.EliminarServer TO 'support_riot'@'localhost';
GRANT EXECUTE ON PROCEDURE ClienteRiotGames.ObtenerServer TO 'support_riot'@'localhost';

-- 4. Cuenta de Jugador
CREATE USER 'player_account'@'localhost' IDENTIFIED BY 'PlayR10t2024!';
-- Permisos de lectura en tablas específicas
GRANT SELECT ON ClienteRiotGames.Objeto TO 'player_account'@'localhost';
GRANT SELECT ON ClienteRiotGames.TipoObjeto TO 'player_account'@'localhost';
GRANT SELECT ON ClienteRiotGames.RangoL TO 'player_account'@'localhost';
GRANT SELECT ON ClienteRiotGames.RangoV TO 'player_account'@'localhost';
GRANT SELECT ON ClienteRiotGames.Server TO 'player_account'@'localhost';
-- Permisos para ejecutar procedimientos específicos
GRANT EXECUTE ON PROCEDURE ClienteRiotGames.ObtenerServer TO 'player_account'@'localhost';

-- 5. Visitante
CREATE USER 'guest_viewer'@'localhost' IDENTIFIED BY 'GuestR10t2024!';
-- Permisos de solo lectura en tablas públicas
GRANT SELECT ON ClienteRiotGames.Objeto TO 'guest_viewer'@'localhost';
GRANT SELECT ON ClienteRiotGames.TipoObjeto TO 'guest_viewer'@'localhost';
GRANT SELECT ON ClienteRiotGames.RangoL TO 'guest_viewer'@'localhost';
GRANT SELECT ON ClienteRiotGames.RangoV TO 'guest_viewer'@'localhost';
GRANT SELECT ON ClienteRiotGames.Server TO 'guest_viewer'@'localhost';

FLUSH PRIVILEGES;

-- Verificación final
SELECT User, Host FROM mysql.user WHERE Host = 'localhost';

SET SQL_SAFE_UPDATES = 1; 