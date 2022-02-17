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

-- Structuur van  tabel modelbuilder.brand wordt geschreven
CREATE TABLE IF NOT EXISTS `brand` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `brand_Name` (`Name`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List of brands for tools, kits, suplies and all other stuf';

-- Dumpen data van tabel modelbuilder.brand: ~51 rows (ongeveer)
DELETE FROM `brand`;
/*!40000 ALTER TABLE `brand` DISABLE KEYS */;
INSERT INTO `brand` (`Id`, `Name`, `Modified`, `Created`) VALUES
	(1, '_Geen_', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(2, 'Admirality', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(3, 'Aeronaut', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(4, 'Aliphatic', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(5, 'Amati', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(6, 'AMMO Mig', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(7, 'Artesania Latina', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(8, 'Badger', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(9, 'Billing Boats', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(10, 'Bosch', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(11, 'Caldercraft', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(12, 'CAP', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(13, 'CMK Czech', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(14, 'Constructo', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(15, 'Corel', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(16, 'David', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(17, 'Disar', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(18, 'Dremel', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(19, 'Dumas', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(20, 'Ever Build', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(21, 'Excel', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(22, 'Faller', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(23, 'Gamma', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(24, 'Graupner', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(25, 'HMB', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(26, 'HobbyZone', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(27, 'Humbrol', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(28, 'Kinzo', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(29, 'Krick', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(30, 'Makita', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(31, 'Mamoli', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(32, 'Mantua', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(33, 'Master Tools', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(34, 'Model Shipways', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(35, 'Modelcraft', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(36, 'Occre', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(37, 'Panart', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(38, 'Praxis', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(39, 'Proedge', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(40, 'Proxxon', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(41, 'Revell', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(42, 'Rotacraft', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(43, 'Sergal', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(44, 'Shipyard', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(45, 'Super Glue', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(46, 'Tamiya', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(47, 'Trumpeter', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(48, 'UHU', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(49, 'Vallejo', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(50, 'Xuron', '2021-12-23 07:45:07', '2021-12-23 07:45:07'),
	(51, 'ZAP', '2021-12-23 07:45:07', '2021-12-23 07:45:07');
/*!40000 ALTER TABLE `brand` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
