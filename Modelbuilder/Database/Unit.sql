-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server versie:                8.0.26 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Versie:              11.3.0.6336
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Structuur van  tabel modelbuilder.unit wordt geschreven
CREATE TABLE IF NOT EXISTS `unit` (
  `unit_Id` int NOT NULL AUTO_INCREMENT,
  `unit_Name` varchar(150) NOT NULL DEFAULT '0',
  PRIMARY KEY (`unit_Id`),
  UNIQUE KEY `unit_Name` (`unit_Name`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.unit: ~0 rows (ongeveer)
DELETE FROM `unit`;
/*!40000 ALTER TABLE `unit` DISABLE KEYS */;
INSERT INTO `unit` (`unit_Id`, `unit_Name`) VALUES
	(1, ''),
	(6, 'cm'),
	(11, 'dl'),
	(4, 'Fles'),
	(7, 'gr'),
	(9, 'kg'),
	(12, 'ltr'),
	(8, 'mgr'),
	(10, 'ml'),
	(5, 'mm'),
	(13, 'mtr'),
	(3, 'Set'),
	(2, 'Stuk');
/*!40000 ALTER TABLE `unit` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
