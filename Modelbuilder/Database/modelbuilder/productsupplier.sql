/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `productsupplier`;
CREATE TABLE IF NOT EXISTS `productsupplier` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Product_Id` int NOT NULL DEFAULT '0',
  `Supplier_Id` int NOT NULL DEFAULT '0',
  `Currency_Id` int DEFAULT '0',
  `ProductNumber` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ProductName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Price` double NOT NULL DEFAULT '0',
  `DefaultSupplier` varchar(1) DEFAULT '',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `Product_Id` (`Product_Id`) /*!80000 INVISIBLE */,
  KEY `Supplier_Id` (`Supplier_Id`),
  KEY `Currency_Id` (`Currency_Id`),
  CONSTRAINT `FK_ProdSupplier_Currency_Id` FOREIGN KEY (`Currency_Id`) REFERENCES `currency` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_ProdSupplier_Product_Id` FOREIGN KEY (`Product_Id`) REFERENCES `product` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_ProdSupplier_Supplier_Id` FOREIGN KEY (`Supplier_Id`) REFERENCES `supplier` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List for all products per supplier';

DELETE FROM `productsupplier`;
/*!40000 ALTER TABLE `productsupplier` DISABLE KEYS */;
INSERT INTO `productsupplier` (`Id`, `Product_Id`, `Supplier_Id`, `Currency_Id`, `ProductNumber`, `ProductName`, `Price`, `DefaultSupplier`, `Created`, `Modified`) VALUES
	(1, 1, 6, 1, '16435', 'Everbuild secondelijm medium viscositeit', 4.070000171661377, '*', '2022-01-10 16:49:00', '2022-01-10 16:55:00'),
	(4, 2, 6, 1, '10893', 'Everbuild secondelijm hoge viscosoteit', 4.07, '*', '2022-01-10 16:49:00', '2022-01-10 16:55:27');
/*!40000 ALTER TABLE `productsupplier` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
