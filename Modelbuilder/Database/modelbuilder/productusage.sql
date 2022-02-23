/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `productusage`;
CREATE TABLE IF NOT EXISTS `productusage` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `project_Id` int NOT NULL DEFAULT '0',
  `product_Id` int NOT NULL DEFAULT '0',
  `UsageDate` date NOT NULL,
  `AmountUsed` double NOT NULL DEFAULT '0',
  `Comment` varchar(1050) DEFAULT NULL,
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Modifed` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `FK_Project_Id` (`project_Id`),
  KEY `FK_Product_Id` (`product_Id`),
  CONSTRAINT `FK_ProductProduct_Id` FOREIGN KEY (`product_Id`) REFERENCES `product` (`Id`),
  CONSTRAINT `FK_ProductProject_Id` FOREIGN KEY (`project_Id`) REFERENCES `project` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Store the product usage as entered in the Time Management page. Part of the data is equal to the data in the StockLog, but because of Foreign keys necesary for relating to Project and Product age a separate tabble is needed.\r\nSame data , as needed in the Stocklog table, is also stored in the stocklog.';

DELETE FROM `productusage`;
/*!40000 ALTER TABLE `productusage` DISABLE KEYS */;
INSERT INTO `productusage` (`Id`, `project_Id`, `product_Id`, `UsageDate`, `AmountUsed`, `Comment`, `Created`, `Modifed`) VALUES
	(1, 2, 1, '2022-02-01', 1, '', '2022-02-19 14:32:59', '2022-02-19 14:32:59'),
	(2, 2, 1, '2022-02-01', 1, '', '2022-02-19 14:35:32', '2022-02-19 14:35:32');
/*!40000 ALTER TABLE `productusage` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
