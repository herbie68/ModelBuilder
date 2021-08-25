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

-- Structuur van  tabel modelbuilder.brand wordt geschreven
CREATE TABLE IF NOT EXISTS `brand` (
  `brand_Id` int NOT NULL AUTO_INCREMENT,
  `brand_Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`brand_Id`),
  UNIQUE KEY `barnd_Name` (`brand_Name`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List of brands for tools, kits, suplies and all other stuf';

-- Dumpen data van tabel modelbuilder.brand: ~0 rows (ongeveer)
DELETE FROM `brand`;
/*!40000 ALTER TABLE `brand` DISABLE KEYS */;
INSERT INTO `brand` (`brand_Id`, `brand_Name`) VALUES
	(17, 'Aeronaut'),
	(4, 'Amati'),
	(5, 'Artesania'),
	(12, 'Billing Boats'),
	(14, 'Caldercraft'),
	(15, 'Constructo'),
	(18, 'Corel'),
	(2, 'Dremel'),
	(3, 'Excel'),
	(10, 'Humbrol'),
	(16, 'Krick'),
	(6, 'Mantua'),
	(13, 'Model Shipways'),
	(7, 'Modelcraft'),
	(8, 'Occre'),
	(19, 'Panart'),
	(1, 'Proxxon'),
	(9, 'Revell'),
	(20, 'Sergal'),
	(11, 'Tamiya');
/*!40000 ALTER TABLE `brand` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
