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

-- Structuur van  tabel modelbuilder.stock wordt geschreven
CREATE TABLE IF NOT EXISTS `stock` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `product_Id` int NOT NULL DEFAULT '0',
  `storage_Id` int DEFAULT '0',
  `Amount` double DEFAULT '0',
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `FK_Stock_Product_Id` (`product_Id`),
  KEY `FK_Stock_Storage_Id` (`storage_Id`),
  CONSTRAINT `FK_Stock_Product_Id` FOREIGN KEY (`product_Id`) REFERENCES `product` (`Id`),
  CONSTRAINT `FK_Stock_Storage_Id` FOREIGN KEY (`storage_Id`) REFERENCES `storage` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Registartie of ordered and received goods. Total of an product_Id gives the total ammount of that product that is available';

-- Dumpen data van tabel modelbuilder.stock: ~2 rows (ongeveer)
DELETE FROM `stock`;
/*!40000 ALTER TABLE `stock` DISABLE KEYS */;
INSERT INTO `stock` (`Id`, `product_Id`, `storage_Id`, `Amount`, `Created`, `Modified`) VALUES
	(7, 1, 242, 1, '2022-01-22 17:33:14', '2022-01-22 17:33:14'),
	(8, 2, 242, 2, '2022-01-23 10:51:06', '2022-01-23 10:51:06');
/*!40000 ALTER TABLE `stock` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
