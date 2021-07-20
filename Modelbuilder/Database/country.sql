-- --------------------------------------------------------
-- Host:                         localhost
-- Server versie:                8.0.25 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Versie:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Databasestructuur van modelbuilder wordt geschreven
CREATE DATABASE IF NOT EXISTS `modelbuilder` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `modelbuilder`;

-- Structuur van  tabel modelbuilder.country wordt geschreven
DROP TABLE IF EXISTS `country`;
CREATE TABLE IF NOT EXISTS `country` (
  `country_Id` int NOT NULL AUTO_INCREMENT,
  `country_Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `country_Defaultcurrency_Symbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '€',
  `country_Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `country_Defaultcurrency_Id` int DEFAULT NULL,
  PRIMARY KEY (`country_Id`) USING BTREE,
  UNIQUE KEY `country_Code_UNIQUE` (`country_Code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.country: ~8 rows (ongeveer)
DELETE FROM `country`;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` (`country_Id`, `country_Code`, `country_Defaultcurrency_Symbol`, `country_Name`, `country_Defaultcurrency_Id`) VALUES
	(1, 'NL', '€', 'Nederland', 1),
	(2, 'UK', '£', 'Engeland', 2),
	(3, 'US', '$', 'Verenigde staten', 3),
	(4, 'DE', '€', 'Duitsland', 1),
	(5, 'ESP', '€', 'Spanje', 1),
	(6, 'CH', 'Y', 'China', 4),
	(15, 'IT', '€', 'Italë', 1),
	(19, '*', '', '', 0);
/*!40000 ALTER TABLE `country` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
