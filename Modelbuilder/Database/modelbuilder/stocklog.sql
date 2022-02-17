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

-- Structuur van  tabel modelbuilder.stocklog wordt geschreven
CREATE TABLE IF NOT EXISTS `stocklog` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `product_Id` int NOT NULL DEFAULT '0',
  `storage_Id` int DEFAULT '0',
  `supplyorder_Id` int DEFAULT '0',
  `supplyorderline_Id` int DEFAULT '0',
  `AmountReceived` double DEFAULT '0',
  `AmountUsed` double DEFAULT '0',
  `Date` date DEFAULT NULL,
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `FK_StockLog_Product_Id` (`product_Id`) USING BTREE,
  KEY `FK_StockLog_Storage_Id` (`storage_Id`) USING BTREE,
  KEY `FK_StockLog_SupplyOrderline_Id` (`supplyorderline_Id`),
  KEY `FK_Stocklog_Supplyorder_Id` (`supplyorder_Id`),
  CONSTRAINT `FK_StockLog_Product_Id` FOREIGN KEY (`product_Id`) REFERENCES `product` (`Id`),
  CONSTRAINT `FK_StockLog_Storage_Id` FOREIGN KEY (`storage_Id`) REFERENCES `storage` (`Id`),
  CONSTRAINT `FK_Stocklog_Supplyorder_Id` FOREIGN KEY (`supplyorder_Id`) REFERENCES `supplyorder` (`Id`),
  CONSTRAINT `FK_StockLog_SupplyOrderline_Id` FOREIGN KEY (`supplyorderline_Id`) REFERENCES `supplyorderline` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Registartion of ordered and received goods';

-- Dumpen data van tabel modelbuilder.stocklog: ~4 rows (ongeveer)
DELETE FROM `stocklog`;
/*!40000 ALTER TABLE `stocklog` DISABLE KEYS */;
INSERT INTO `stocklog` (`Id`, `product_Id`, `storage_Id`, `supplyorder_Id`, `supplyorderline_Id`, `AmountReceived`, `AmountUsed`, `Date`, `Created`, `Modified`) VALUES
	(8, 1, 242, 1, 1, 1, 0, '2022-01-22', '2022-01-22 17:14:10', '2022-01-22 17:14:10'),
	(9, 2, 242, 1, 2, 1, 0, '2022-01-22', '2022-01-22 17:20:29', '2022-01-22 17:20:29'),
	(10, 1, 242, 1, 1, 1, 0, '2022-01-22', '2022-01-22 17:33:14', '2022-01-22 17:33:14'),
	(11, 2, 242, 1, 2, 2, 0, '2022-01-23', '2022-01-23 10:51:06', '2022-01-23 10:51:06');
/*!40000 ALTER TABLE `stocklog` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
