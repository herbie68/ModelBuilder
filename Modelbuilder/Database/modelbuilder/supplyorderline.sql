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

-- Structuur van  tabel modelbuilder.supplyorderline wordt geschreven
CREATE TABLE IF NOT EXISTS `supplyorderline` (
  `orderline_Id` int NOT NULL AUTO_INCREMENT,
  `orderline_OrderId` int NOT NULL DEFAULT '0',
  `orderline_ProductId` int DEFAULT '0',
  `orderline_SupplierId` int DEFAULT '0',
  `orderline_ProductName` varchar(150) DEFAULT NULL,
  `orderline_ProjectId` int DEFAULT '0',
  `orderline_ProjectName` varchar(150) DEFAULT NULL,
  `orderline_CategoryId` int DEFAULT '0',
  `orderline_CategoryName` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `orderline_Number` double(6,2) DEFAULT '0.00',
  `orderline_Price` double(6,2) DEFAULT '0.00',
  `orderline_RealRowTotal` double(6,2) DEFAULT '0.00',
  `orderline_Closed` tinyint DEFAULT '0',
  `orderline_ClosedDate` date DEFAULT NULL,
  PRIMARY KEY (`orderline_Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

-- Dumpen data van tabel modelbuilder.supplyorderline: ~0 rows (ongeveer)
DELETE FROM `supplyorderline`;
/*!40000 ALTER TABLE `supplyorderline` DISABLE KEYS */;
INSERT INTO `supplyorderline` (`orderline_Id`, `orderline_OrderId`, `orderline_ProductId`, `orderline_SupplierId`, `orderline_ProductName`, `orderline_ProjectId`, `orderline_ProjectName`, `orderline_CategoryId`, `orderline_CategoryName`, `orderline_Number`, `orderline_Price`, `orderline_RealRowTotal`, `orderline_Closed`, `orderline_ClosedDate`) VALUES
	(1, 1, 1, 0, 'Proxxon Bankschroef', 1, 'Silhouet (Groningen 1893)', 510, 'Afwerkingerking', 1.00, 29.95, 0.00, 0, NULL);
/*!40000 ALTER TABLE `supplyorderline` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
