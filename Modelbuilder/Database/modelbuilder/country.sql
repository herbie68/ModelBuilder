-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server versie:                8.0.26 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Versie:              11.3.0.6337
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Structuur van  tabel modelbuilder.country wordt geschreven
CREATE TABLE IF NOT EXISTS `country` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `Defaultcurrency_Symbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '€',
  `Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Defaultcurrency_Id` int DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE KEY `country_Code_UNIQUE` (`Code`) USING BTREE,
  KEY `FK_Country_Currency_Id` (`Defaultcurrency_Id`),
  CONSTRAINT `FK_Country_Currency_Id` FOREIGN KEY (`Defaultcurrency_Id`) REFERENCES `currency` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.country: ~7 rows (ongeveer)
DELETE FROM `country`;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` (`Id`, `Code`, `Defaultcurrency_Symbol`, `Name`, `Defaultcurrency_Id`, `Created`, `Modified`) VALUES
	(1, 'NL', '€', 'Nederland', 1, '2022-01-10 16:44:46', '2022-01-10 16:44:46'),
	(2, 'UK', '£', 'Engeland', 2, '2022-01-10 16:44:46', '2022-01-10 16:44:46'),
	(3, 'US', '$', 'Verenigde staten', 3, '2022-01-10 16:44:46', '2022-01-10 16:44:46'),
	(4, 'DE', '€', 'Duitsland', 1, '2022-01-10 16:44:46', '2022-01-10 16:44:46'),
	(5, 'ESP', '€', 'Spanje', 1, '2022-01-10 16:44:46', '2022-01-10 16:44:46'),
	(6, 'CH', 'Y', 'China', 4, '2022-01-10 16:44:46', '2022-01-10 16:44:46'),
	(7, 'IT', '€', 'Italë', 1, '2022-01-10 16:44:46', '2022-01-10 16:44:46');
/*!40000 ALTER TABLE `country` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
