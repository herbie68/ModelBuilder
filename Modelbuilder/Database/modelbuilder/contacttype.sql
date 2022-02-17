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

-- Structuur van  tabel modelbuilder.contacttype wordt geschreven
CREATE TABLE IF NOT EXISTS `contacttype` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.contacttype: ~5 rows (ongeveer)
DELETE FROM `contacttype`;
/*!40000 ALTER TABLE `contacttype` DISABLE KEYS */;
INSERT INTO `contacttype` (`Id`, `Name`, `Created`, `Modified`) VALUES
	(1, '', '2021-12-23 08:57:24', '2021-12-23 08:57:24'),
	(2, 'Verkoop', '2021-12-23 08:57:24', '2021-12-23 08:57:24'),
	(3, 'Administratie', '2021-12-23 08:57:24', '2021-12-23 08:57:24'),
	(4, 'Algemeen', '2021-12-23 08:57:24', '2021-12-23 08:57:24'),
	(5, 'Service', '2021-12-23 08:57:24', '2021-12-23 08:57:24');
/*!40000 ALTER TABLE `contacttype` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
