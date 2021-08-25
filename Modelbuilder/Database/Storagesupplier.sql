-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server versie:                8.0.26 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Versie:              11.3.0.6336
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Structuur van  tabel modelbuilder.storagesupplier wordt geschreven
CREATE TABLE IF NOT EXISTS `storagesupplier` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `supplier_Id` int NOT NULL DEFAULT '0',
  `supplier_Name` varchar(150) DEFAULT NULL,
  `product_Id` int NOT NULL DEFAULT '0',
  `supplierProductNumber` varchar(150) DEFAULT NULL,
  `supplierProductName` varchar(150) DEFAULT NULL,
  `supplierProductPrice` float(10,2) DEFAULT '0.00',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List for all products per supplier';

-- Dumpen data van tabel modelbuilder.storagesupplier: ~0 rows (ongeveer)
DELETE FROM `storagesupplier`;
/*!40000 ALTER TABLE `storagesupplier` DISABLE KEYS */;
INSERT INTO `storagesupplier` (`Id`, `supplier_Id`, `supplier_Name`, `product_Id`, `supplierProductNumber`, `supplierProductName`, `supplierProductPrice`) VALUES
	(1, 1, 'Cornwall Model Boats Ltd', 1, '1234', 'Proxxon Vench', 29.95);
/*!40000 ALTER TABLE `storagesupplier` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
