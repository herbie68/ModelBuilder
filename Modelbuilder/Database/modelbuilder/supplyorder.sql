/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `supplyorder`;
CREATE TABLE IF NOT EXISTS `supplyorder` (
  `order_Id` int NOT NULL AUTO_INCREMENT,
  `order_SupplierId` int DEFAULT '0',
  `order_CurrencyId` int DEFAULT '0',
  `order_OrderNumber` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `order_SupplierName` varchar(150) DEFAULT NULL,
  `order_Date` date DEFAULT NULL,
  `order_CurrencySymbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '€',
  `order_CurrencyConversionRate` double(6,4) DEFAULT '0.0000',
  `order_ShippingCosts` double(10,2) DEFAULT '0.00',
  `order_OrderCosts` double(10,2) DEFAULT '0.00',
  `order_Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `order_Closed` tinyint DEFAULT '0',
  `order_ClosedDate` date DEFAULT NULL,
  PRIMARY KEY (`order_Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

DELETE FROM `supplyorder`;
/*!40000 ALTER TABLE `supplyorder` DISABLE KEYS */;
INSERT INTO `supplyorder` (`order_Id`, `order_SupplierId`, `order_CurrencyId`, `order_OrderNumber`, `order_SupplierName`, `order_Date`, `order_CurrencySymbol`, `order_CurrencyConversionRate`, `order_ShippingCosts`, `order_OrderCosts`, `order_Memo`, `order_Closed`, `order_ClosedDate`) VALUES
	(2, 5, 0, 'AV3254', 'Modelbouw-Dordrecht', '2021-11-04', '€', 1.0000, 6.95, 0.00, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql}\r\n}', 0, NULL),
	(3, 5, 0, 'AV3255', 'Modelbouw-Dordrecht', '2021-11-04', '€', 1.0000, 6.95, 0.00, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql}\r\n}', 0, NULL),
	(4, 5, 0, 'AV3256', 'Modelbouw-Dordrecht', '2021-11-04', '€', 1.0000, 6.95, 0.00, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql}\r\n}', 0, NULL);
/*!40000 ALTER TABLE `supplyorder` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
