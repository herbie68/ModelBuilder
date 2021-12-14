/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `supplier`;
CREATE TABLE IF NOT EXISTS `supplier` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Address1` varchar(150) DEFAULT NULL,
  `Address2` varchar(150) DEFAULT NULL,
  `Zip` varchar(15) DEFAULT NULL,
  `City` varchar(40) DEFAULT NULL,
  `Url` varchar(255) DEFAULT NULL,
  `ShippingCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `MinShippingCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `OrderCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Country_Id` int NOT NULL DEFAULT '1',
  `Currency_Id` int NOT NULL DEFAULT '1',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Code` (`Code`) /*!80000 INVISIBLE */,
  KEY `Name` (`Name`) /*!80000 INVISIBLE */,
  KEY `Currency_Id` (`Currency_Id`) /*!80000 INVISIBLE */,
  KEY `Country_Id` (`Country_Id`),
  CONSTRAINT `FK_Supplier_Country_Id` FOREIGN KEY (`Country_Id`) REFERENCES `country` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Supplier_Currency_Id` FOREIGN KEY (`Currency_Id`) REFERENCES `currency` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `supplier`;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
