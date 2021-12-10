/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `productsupplier`;
CREATE TABLE IF NOT EXISTS `productsupplier` (
  `productSupplier_Id` int NOT NULL AUTO_INCREMENT,
  `productSupplier_ProductId` int NOT NULL DEFAULT '0',
  `productSupplier_SupplierId` int NOT NULL DEFAULT '0',
  `productSupplier_SupplierName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `productSupplier_CurrencyId` int NOT NULL DEFAULT '0',
  `productSupplier_CurrencySymbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `productSupplier_ProductNumber` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `productSupplier_ProductName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `productSupplier_ProductPrice` double NOT NULL DEFAULT '0',
  `productSupplier_Default` varchar(1) DEFAULT '',
  PRIMARY KEY (`productSupplier_Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List for all products per supplier';

DELETE FROM `productsupplier`;
/*!40000 ALTER TABLE `productsupplier` DISABLE KEYS */;
INSERT INTO `productsupplier` (`productSupplier_Id`, `productSupplier_ProductId`, `productSupplier_SupplierId`, `productSupplier_SupplierName`, `productSupplier_CurrencyId`, `productSupplier_CurrencySymbol`, `productSupplier_ProductNumber`, `productSupplier_ProductName`, `productSupplier_ProductPrice`, `productSupplier_Default`) VALUES
	(11, 7, 6, 'Modelbouw Krikke', 1, '€', '123456', 'Proxxon Bankschroef', 29.5, '*'),
	(12, 7, 5, 'Modelbouw-Dordrecht', 1, '€', 'TT5537', 'Bankschroef', 32.5, '');
/*!40000 ALTER TABLE `productsupplier` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;