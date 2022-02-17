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

-- Structuur van  tabel modelbuilder.currency wordt geschreven
CREATE TABLE IF NOT EXISTS `currency` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Symbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConversionRate` double DEFAULT '1',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE KEY `currency_Code` (`Code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.currency: ~5 rows (ongeveer)
DELETE FROM `currency`;
/*!40000 ALTER TABLE `currency` DISABLE KEYS */;
INSERT INTO `currency` (`Id`, `Code`, `Symbol`, `Name`, `ConversionRate`, `Created`, `Modified`) VALUES
	(1, 'EUR', '€', 'Euro', 1, '2022-01-10 16:36:03', '2022-01-10 16:43:55'),
	(2, 'GBP', '£', 'Britse pond', 1.1078999999999999, '2022-01-10 16:36:03', '2022-01-10 16:44:01'),
	(3, 'USD', '$', 'Amerikaanse dollar', 0.8442, '2022-01-10 16:36:03', '2022-01-10 16:44:05'),
	(4, 'YEN', 'Y', 'Yen', 3.34, '2022-01-10 16:36:03', '2022-01-10 16:44:08'),
	(5, 'TST', 'T', 'Testje', 1.23, '2022-01-10 16:36:03', '2022-01-10 16:44:16');
/*!40000 ALTER TABLE `currency` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
