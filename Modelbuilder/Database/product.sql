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

-- Structuur van  tabel modelbuilder.product wordt geschreven
CREATE TABLE IF NOT EXISTS `product` (
  `product_Id` int NOT NULL AUTO_INCREMENT,
  `product_Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `product_Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `product_CategoryId` int DEFAULT NULL,
  `product_CategoryName` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `product_SupplierId` int DEFAULT NULL,
  `product_SupplierName` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `product_SupplierProductNumber` varchar(20) DEFAULT NULL,
  `product_StorageId` int DEFAULT NULL,
  `product_StorageName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `product_Price` int DEFAULT '0',
  `product_ProjectCosts` int NOT NULL DEFAULT '0',
  `product_MinimalStock` int DEFAULT NULL,
  `product_StandardOrderQuantity` int DEFAULT NULL,
  `product_BrandId` int DEFAULT NULL,
  `product_BrandName` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`product_Id`),
  UNIQUE KEY `product_Code` (`product_Code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.product: ~1 rows (ongeveer)
DELETE FROM `product`;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` (`product_Id`, `product_Code`, `product_Name`, `product_CategoryId`, `product_CategoryName`, `product_SupplierId`, `product_SupplierName`, `product_SupplierProductNumber`, `product_StorageId`, `product_StorageName`, `product_Price`, `product_ProjectCosts`, `product_MinimalStock`, `product_StandardOrderQuantity`, `product_BrandId`, `product_BrandName`) VALUES
	(1, 'BANKSCHROEF', 'Proxxon Bankschroef', 510, 'Afwerkingerking', 1, 'Cornwall Model Boats Ltd', NULL, 1, 'Herberts Werf', 10500, 0, 0, 0, NULL, NULL);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
