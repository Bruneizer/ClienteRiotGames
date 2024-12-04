-- Inserción de datos usando Store Procedures
DELIMITER $$

SELECT 'Iniciando inserción de datos' AS 'Estado' $$

-- Inserción de Servidores
CALL InsertarServer('Latinoamérica Norte', 'LAN');
CALL InsertarServer('Latinoamérica Sur', 'LAS');
CALL InsertarServer('Brasil', 'BR');
CALL InsertarServer('Norte América', 'NA');
CALL InsertarServer('Europa Oeste', 'EUW');

-- Inserción de Rangos de League of Legends
CALL InsertarRangoL('Hierro', 0, 0);
CALL InsertarRangoL('Bronce', 1, 20);
CALL InsertarRangoL('Plata', 2, 40);
CALL InsertarRangoL('Oro', 3, 60);


-- Inserción de Rangos de Valorant
CALL InsertarRangoV('Hierro', 0, 0);
CALL InsertarRangoV('Bronce', 1, 20);
CALL InsertarRangoV('Plata', 2, 40);
CALL InsertarRangoV('Oro', 3, 60);
CALL InsertarRangoV('Platino', 4, 80);

-- Inserción de Tipos de Objeto
CALL InsertarTipoObjeto('Campeón');
CALL InsertarTipoObjeto('Aspecto');
CALL InsertarTipoObjeto('Ward');
CALL InsertarTipoObjeto('Icono');
CALL InsertarTipoObjeto('Emote');

-- Inserción de Objetos
CALL InsertarObjeto(1, 'Ahri', 4800, 880);
CALL InsertarObjeto(1, 'Zed', 4800, 880);
CALL InsertarObjeto(1, 'Lux', 3150, 790);
CALL InsertarObjeto(2, 'Ahri Espíritu Guardián', 0, 1350);
CALL InsertarObjeto(2, 'Zed Proyecto', 0, 1350);
CALL InsertarObjeto(3, 'Ward Poro', 640, 640);
CALL InsertarObjeto(4, 'Icono Poro', 250, 250);
CALL InsertarObjeto(5, 'Emote Pulgares', 350, 350);

-- Inserción de Cuentas Riot (ajustado para cumplir con el patrón de contraseña)
CALL InsertarCuentaRiot('Usuario1', 'P@ssword1', 'usuario1@email.com', 1);
CALL InsertarCuentaRiot('Usuario2', 'P@ssword2', 'usuario2@email.com', 1);
CALL InsertarCuentaRiot('Usuario3', 'P@ssword3', 'usuario3@email.com', 2);
CALL InsertarCuentaRiot('Usuario4', 'P@ssword4', 'usuario4@email.com', 3);
CALL InsertarCuentaRiot('Usuario5', 'P@ssword5', 'usuario5@email.com', 4);

-- Inserción de Cuentas League of Legends (después de crear las cuentas Riot)
CALL InsertarCuentaLOL(1, 3, 'Invocador1', 150, 75);
CALL InsertarCuentaLOL(2, 4, 'Invocador2', 200, 85);
CALL InsertarCuentaLOL(3, 2, 'Invocador3', 100, 45);
CALL InsertarCuentaLOL(4, 1, 'Invocador4', 50, 15);
CALL InsertarCuentaLOL(5, 5, 'Invocador5', 300, 95);

-- Inserción de Cuentas Valorant (después de crear las cuentas Riot)
CALL InsertarCuentaValorant(1, 3, 'Agente1', 50, 100000);
CALL InsertarCuentaValorant(2, 4, 'Agente2', 75, 150000);
CALL InsertarCuentaValorant(3, 2, 'Agente3', 25, 50000);
CALL InsertarCuentaValorant(4, 1, 'Agente4', 10, 20000);
CALL InsertarCuentaValorant(5, 5, 'Agente5', 100, 200000);

-- Inserción de Inventarios (después de crear las cuentas LOL)
CALL InsertarInventario(1, 5000, 1000);
CALL InsertarInventario(2, 7500, 1500);
CALL InsertarInventario(3, 3000, 500);
CALL InsertarInventario(4, 1000, 200);
CALL InsertarInventario(5, 10000, 2000);

-- Inserción de Objetos en Inventarios (sin duplicados)
CALL InsertarInventarioObjeto(1, 1, 1);
CALL InsertarInventarioObjeto(1, 3, 1);
CALL InsertarInventarioObjeto(2, 2, 1);
CALL InsertarInventarioObjeto(2, 4, 2);
CALL InsertarInventarioObjeto(3, 5, 1);
CALL InsertarInventarioObjeto(3, 7, 3);
CALL InsertarInventarioObjeto(4, 6, 1);
CALL InsertarInventarioObjeto(5, 8, 2);

SELECT 'Inserción de datos completada' AS 'Estado' $$

DELIMITER ; 