/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Registartie of ordered and received goods. Total of an product_Id gives the total ammount of that product that is available';

DELETE FROM `stock`;
/*!40000 ALTER TABLE `stock` DISABLE KEYS */;
INSERT INTO `stock` (`Id`, `product_Id`, `storage_Id`, `Amount`, `Created`, `Modified`) VALUES
	(5, 1, 242, 1, '2022-01-24 16:47:27', '2022-01-26 14:34:36'),
	(6, 2, 242, 1, '2022-01-24 16:54:37', '2022-01-26 14:34:56');
/*!40000 ALTER TABLE `stock` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
