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

-- Structuur van  tabel modelbuilder.supplyorder wordt geschreven
CREATE TABLE IF NOT EXISTS `supplyorder` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Supplier_Id` int DEFAULT '0',
  `Currency_Id` int DEFAULT '0',
  `OrderNumber` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `OrderDate` date DEFAULT NULL,
  `CurrencySymbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '€',
  `CurrencyConversionRate` double DEFAULT '0',
  `ShippingCosts` double DEFAULT '0',
  `OrderCosts` double DEFAULT '0',
  `Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Closed` tinyint DEFAULT '0',
  `ClosedDate` date DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `OrderNumber` (`OrderNumber`),
  KEY `FK_Order_Supplier_Id` (`Supplier_Id`),
  KEY `FK_Order_Currency_Id` (`Currency_Id`),
  CONSTRAINT `FK_Order_Currency_Id` FOREIGN KEY (`Currency_Id`) REFERENCES `currency` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Order_Supplier_Id` FOREIGN KEY (`Supplier_Id`) REFERENCES `supplier` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

-- Dumpen data van tabel modelbuilder.supplyorder: ~0 rows (ongeveer)
DELETE FROM `supplyorder`;
/*!40000 ALTER TABLE `supplyorder` DISABLE KEYS */;
INSERT INTO `supplyorder` (`Id`, `Supplier_Id`, `Currency_Id`, `OrderNumber`, `OrderDate`, `CurrencySymbol`, `CurrencyConversionRate`, `ShippingCosts`, `OrderCosts`, `Memo`, `Closed`, `ClosedDate`, `Created`, `Modified`) VALUES
	(1, 6, 1, 'OWW079696020', '2021-10-23', '€', 1, 5, 0, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql}\r\n}', 1, '2022-01-23', '2022-01-17 14:56:59', '2022-01-23 10:51:06');
/*!40000 ALTER TABLE `supplyorder` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
