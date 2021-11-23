/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `supplyorderline`;
CREATE TABLE IF NOT EXISTS `supplyorderline` (
  `orderline_Id` int NOT NULL AUTO_INCREMENT,
  `orderline_OrderId` int NOT NULL DEFAULT '0',
  `orderline_ProductId` int DEFAULT '0',
  `orderline_ProjectId` int DEFAULT '0',
  `orderline_CategoryId` int DEFAULT '0',
  `orderline_Number` double(6,2) DEFAULT '0.00',
  `orderline_Price` double(6,2) DEFAULT '0.00',
  `orderline_RealRowTotal` double(6,2) DEFAULT '0.00',
  `orderline_Closed` tinyint DEFAULT '0',
  `orderline_ClosedDate` date DEFAULT NULL,
  PRIMARY KEY (`orderline_Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

DELETE FROM `supplyorderline`;
/*!40000 ALTER TABLE `supplyorderline` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplyorderline` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
