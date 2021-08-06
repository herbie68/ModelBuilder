/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `productsupplier`;
CREATE TABLE IF NOT EXISTS `productsupplier` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `supplier_Id` int NOT NULL DEFAULT '0',
  `supplier_Name` varchar(150) DEFAULT NULL,
  `product_Id` int NOT NULL DEFAULT '0',
  `supplierProductNumber` varchar(150) DEFAULT NULL,
  `supplierProductName` varchar(150) DEFAULT NULL,
  `supplierProductPrice` float(10,2) DEFAULT '0.00',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List for all products per supplier';

DELETE FROM `productsupplier`;
/*!40000 ALTER TABLE `productsupplier` DISABLE KEYS */;
INSERT INTO `productsupplier` (`Id`, `supplier_Id`, `supplier_Name`, `product_Id`, `supplierProductNumber`, `supplierProductName`, `supplierProductPrice`) VALUES
	(1, 1, 'Cornwall Model Boats Ltd', 1, '1234', 'Proxxon Vench', 29.95);
/*!40000 ALTER TABLE `productsupplier` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
