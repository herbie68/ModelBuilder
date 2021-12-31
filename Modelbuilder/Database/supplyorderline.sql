/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `supplyorderline`;
CREATE TABLE IF NOT EXISTS `supplyorderline` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OrderId` int NOT NULL DEFAULT '0',
  `SupplierId` int NOT NULL DEFAULT '0',
  `ProductId` int DEFAULT '0',
  `ProductName` varchar(150) DEFAULT '',
  `ProjectId` int DEFAULT '0',
  `ProjectName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `CategoryId` int DEFAULT '0',
  `CategoryName` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `Number` double(6,2) DEFAULT '0.00',
  `Price` double(6,2) DEFAULT '0.00',
  `RealRowTotal` double(6,2) DEFAULT '0.00',
  `Closed` tinyint DEFAULT '0',
  `ClosedDate` date DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

DELETE FROM `supplyorderline`;
/*!40000 ALTER TABLE `supplyorderline` DISABLE KEYS */;
INSERT INTO `supplyorderline` (`Id`, `OrderId`, `SupplierId`, `ProductId`, `ProductName`, `ProjectId`, `ProjectName`, `CategoryId`, `CategoryName`, `Number`, `Price`, `RealRowTotal`, `Closed`, `ClosedDate`) VALUES
	(6, 2, 5, 9, 'Potlood', 1, 'Silhouet (Groningen 1893)', 520, 'Afwerkingerking', 3.00, 1.00, 0.00, 0, NULL),
	(7, 2, 5, 7, 'Proxxon bankschroef 150mm', 0, '', 0, '', 1.00, 2.50, 0.00, 0, NULL),
	(8, 2, 5, 7, 'Proxxon bankschroef 150mm', 2, 'Yacht Mary', 0, '', 1.00, 12.50, 0.00, 0, NULL),
	(10, 2, 5, 7, 'Proxxon bankschroef 150mm', 1, 'Silhouet (Groningen 1893)', 0, '', 1.00, 32.50, 0.00, 0, NULL),
	(13, 2, 5, 9, 'Potlood', 3, 'Zuiderzee Botter', 513, 'Penselen', 1.00, 32.50, 0.00, 0, NULL),
	(14, 2, 5, 9, 'Potlood', 1, 'Silhouet (Groningen 1893)', 510, 'Afwerkingerking', 2.00, 1.00, 0.00, 0, NULL),
	(22, 2, 5, 0, '', 0, '', 0, '', 0.00, 0.00, 0.00, 0, NULL),
	(23, 3, 5, 0, '', 0, '', 0, '', 0.00, 0.00, 0.00, 0, NULL),
	(24, 3, 5, 7, 'Proxxon bankschroef 150mm', 1, 'Silhouet (Groningen 1893)', 0, '', 1.00, 32.50, 0.00, 0, NULL);
/*!40000 ALTER TABLE `supplyorderline` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
