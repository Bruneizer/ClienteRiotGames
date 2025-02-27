SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';


DROP SCHEMA `ClienteRiotGames` ;


CREATE SCHEMA IF NOT EXISTS `ClienteRiotGames` DEFAULT CHARACTER SET utf8 ;
USE `ClienteRiotGames` ;

DROP TABLE IF EXISTS `ClienteRiotGames`.`Server` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`Server` (
  `idServer` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  `Abreviado` VARCHAR(5) NULL,
  PRIMARY KEY (`idServer`),
  UNIQUE INDEX `Nombre_UNIQUE` (`Nombre` ASC) VISIBLE,
  UNIQUE INDEX `Abreviado_UNIQUE` (`Abreviado` ASC) VISIBLE)
ENGINE = InnoDB;


DROP TABLE IF EXISTS `ClienteRiotGames`.`CuentaRiot` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`CuentaRiot` (
  `idCuenta` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(50) NOT NULL,
  `Password` VARCHAR(255) NULL,
  `Email` VARCHAR(45) NULL,
  `idServer` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`idCuenta`),
  INDEX `fk_CuentaRiot_Server_idx` (`idServer` ASC) VISIBLE,
  UNIQUE INDEX `Email_UNIQUE` (`Email` ASC) VISIBLE,
  UNIQUE INDEX `Nombre_UNIQUE` (`Nombre` ASC) VISIBLE,
  CONSTRAINT `fk_CuentaRiot_Server`
    FOREIGN KEY (`idServer`)
    REFERENCES `ClienteRiotGames`.`Server` (`idServer`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


DROP TABLE IF EXISTS `ClienteRiotGames`.`RangoL` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`RangoL` (
  `idRangoL` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  `Numero` INT NULL,
  `PuntosCompetitivo` INT NULL,
  PRIMARY KEY (`idRangoL`))
ENGINE = InnoDB;


DROP TABLE IF EXISTS `ClienteRiotGames`.`CuentaLeagueOfLeguends` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`CuentaLeagueOfLeguends` (
  `idCuentaL` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(50) NULL,
  `Nivel` INT NULL,
  `PuntosCompetitivo` INT NULL,
  `idCuenta` INT UNSIGNED NOT NULL,
  `idRangoL` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`idCuentaL`, `idCuenta`, `idRangoL`),
  INDEX `fk_CuentaLeagueOfLeguends_CuentaRiot_idx` (`idCuenta` ASC) VISIBLE,
  INDEX `fk_CuentaLeagueOfLeguends_RangoL_idx` (`idRangoL` ASC) VISIBLE,
  CONSTRAINT `fk_CuentaLeagueOfLeguends_CuentaRiot`
    FOREIGN KEY (`idCuenta`)
    REFERENCES `ClienteRiotGames`.`CuentaRiot` (`idCuenta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CuentaLeagueOfLeguends_RangoL`
    FOREIGN KEY (`idRangoL`)
    REFERENCES `ClienteRiotGames`.`RangoL` (`idRangoL`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


DROP TABLE IF EXISTS `ClienteRiotGames`.`RangoV` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`RangoV` (
  `idRangoV` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  `Numero` INT NULL,
  `PuntosCompetitivo` INT NULL,
  PRIMARY KEY (`idRangoV`))
ENGINE = InnoDB;


DROP TABLE IF EXISTS `ClienteRiotGames`.`CuentaValorant` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`CuentaValorant` (
  `idCuentaV` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  `Nivel` INT NULL,
  `Experiencia` INT NULL,
  `idCuenta` INT UNSIGNED NOT NULL,
  `idRangoV` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`idCuentaV`, `idCuenta`, `idRangoV`),
  INDEX `fk_CuentaValorant_copy1_CuentaRiot1_idx` (`idCuenta` ASC) VISIBLE,
  INDEX `fk_CuentaValorant_copy1_RangoV1_idx` (`idRangoV` ASC) VISIBLE,
  CONSTRAINT `fk_CuentaValorant_copy1_CuentaRiot1`
    FOREIGN KEY (`idCuenta`)
    REFERENCES `ClienteRiotGames`.`CuentaRiot` (`idCuenta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CuentaValorant_copy1_RangoV1`
    FOREIGN KEY (`idRangoV`)
    REFERENCES `ClienteRiotGames`.`RangoV` (`idRangoV`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


DROP TABLE IF EXISTS `ClienteRiotGames`.`Inventario` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`Inventario` (
  `idInventario` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `idCuentaL` INT UNSIGNED NOT NULL,
  `EsenciaAzul` INT UNSIGNED NULL,
  `PuntosRiot` INT UNSIGNED NULL,
  PRIMARY KEY (`idInventario`, `idCuentaL`),
  INDEX `fk_Inventario_CuentaLeagueOfLeguends1_idx` (`idCuentaL` ASC) VISIBLE,
  CONSTRAINT `fk_Inventario_CuentaLeagueOfLeguends1`
    FOREIGN KEY (`idCuentaL`)
    REFERENCES `ClienteRiotGames`.`CuentaLeagueOfLeguends` (`idCuentaL`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


DROP TABLE IF EXISTS `ClienteRiotGames`.`TipoObjeto` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`TipoObjeto` (
  `idTipoObjeto` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  PRIMARY KEY (`idTipoObjeto`),
  UNIQUE INDEX `Nombre_UNIQUE` (`Nombre` ASC) VISIBLE)
ENGINE = InnoDB;


DROP TABLE IF EXISTS `ClienteRiotGames`.`Objeto` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`Objeto` (
  `idObjeto` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  `PrecioEA` MEDIUMINT UNSIGNED NULL,
  `PrecioRP` MEDIUMINT UNSIGNED NULL,
  `idTipoObjeto` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`idObjeto`, `idTipoObjeto`),
  INDEX `fk_Objeto_TipoObjeto1_idx` (`idTipoObjeto` ASC) VISIBLE,
  UNIQUE INDEX `Nombre_UNIQUE` (`Nombre` ASC) VISIBLE,
  CONSTRAINT `fk_Objeto_TipoObjeto1`
    FOREIGN KEY (`idTipoObjeto`)
    REFERENCES `ClienteRiotGames`.`TipoObjeto` (`idTipoObjeto`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


DROP TABLE IF EXISTS `ClienteRiotGames`.`InventarioObjeto` ;

CREATE TABLE IF NOT EXISTS `ClienteRiotGames`.`InventarioObjeto` (
  `idInventario` INT UNSIGNED NOT NULL,
  `idObjeto` INT UNSIGNED NOT NULL,
  `Cantidad` TINYINT UNSIGNED NULL,
  PRIMARY KEY (`idInventario`, `idObjeto`),
  INDEX `fk_Inventario_has_Objeto_Objeto1_idx` (`idObjeto` ASC) VISIBLE,
  INDEX `fk_Inventario_has_Objeto_Inventario1_idx` (`idInventario` ASC) VISIBLE,
  CONSTRAINT `fk_Inventario_has_Objeto_Inventario1`
    FOREIGN KEY (`idInventario`)
    REFERENCES `ClienteRiotGames`.`Inventario` (`idInventario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Inventario_has_Objeto_Objeto1`
    FOREIGN KEY (`idObjeto`)
    REFERENCES `ClienteRiotGames`.`Objeto` (`idObjeto`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
