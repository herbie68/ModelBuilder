/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `product`;
CREATE TABLE IF NOT EXISTS `product` (
  `product_Id` int NOT NULL AUTO_INCREMENT,
  `product_Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `product_Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `product_Dimensions` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `product_Price` float(10,2) DEFAULT '0.00',
  `product_MinimalStock` int DEFAULT '0',
  `product_StandardOrderQuantity` int DEFAULT '1',
  `product_ProjectCosts` int DEFAULT '0',
  `product_UnitId` int DEFAULT NULL,
  `product_UnitName` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `product_Image` blob,
  `product_BrandId` int DEFAULT NULL,
  `product_BrandName` varchar(150) DEFAULT NULL,
  `product_CategoryId` int DEFAULT NULL,
  `product_CategoryName` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `product_SupplierId` int DEFAULT NULL,
  `product_SupplierName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `product_SupplierProductNumber` varchar(20) DEFAULT NULL,
  `product_StorageId` int DEFAULT NULL,
  `product_StorageName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `product_Memo` longtext,
  PRIMARY KEY (`product_Id`),
  UNIQUE KEY `product_Code` (`product_Code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `product`;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` (`product_Id`, `product_Code`, `product_Name`, `product_Dimensions`, `product_Price`, `product_MinimalStock`, `product_StandardOrderQuantity`, `product_ProjectCosts`, `product_UnitId`, `product_UnitName`, `product_Image`, `product_BrandId`, `product_BrandName`, `product_CategoryId`, `product_CategoryName`, `product_SupplierId`, `product_SupplierName`, `product_SupplierProductNumber`, `product_StorageId`, `product_StorageName`, `product_Memo`) VALUES
	(1, 'BANKSCHROEF', 'Proxxon Bankschroef', '150mm', 29.95, 1, 1, 0, 2, 'Stuk', NULL, 1, 'Proxxon', 520, 'Algemeen', 4, 'Testing 123', NULL, 242, 'Werkbank', '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql}\r\n}');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
