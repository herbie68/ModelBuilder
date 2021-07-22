/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `supplier`;
CREATE TABLE IF NOT EXISTS `supplier` (
  `supplier_Id` int NOT NULL AUTO_INCREMENT,
  `supplier_Code` varchar(20) NOT NULL,
  `supplier_Name` varchar(150) NOT NULL,
  `supplier_Address1` varchar(150) DEFAULT NULL,
  `supplier_Address2` varchar(150) DEFAULT NULL,
  `supplier_Zip` varchar(15) DEFAULT NULL,
  `supplier_City` varchar(40) DEFAULT NULL,
  `supplier_Url` varchar(255) DEFAULT NULL,
  `supplier_PhoneGeneral` varchar(40) DEFAULT NULL,
  `supplier_PhoneSales` varchar(40) DEFAULT NULL,
  `supplier_PhoneSupport` varchar(40) DEFAULT NULL,
  `supplier_MailGeneral` varchar(80) DEFAULT NULL,
  `supplier_MailSales` varchar(80) DEFAULT NULL,
  `supplier_MailSupport` varchar(80) DEFAULT NULL,
  `supplier_Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `currency_Id` int DEFAULT '1',
  `currency_Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT 'EUR',
  `currency_Symbol` varchar(2) DEFAULT '€',
  `country_Id` int DEFAULT NULL,
  `country_Code` varchar(4) DEFAULT NULL,
  `country_Name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`supplier_Id`),
  UNIQUE KEY `supplier_Code` (`supplier_Code`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `supplier`;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` (`supplier_Id`, `supplier_Code`, `supplier_Name`, `supplier_Address1`, `supplier_Address2`, `supplier_Zip`, `supplier_City`, `country_Id`, `country_Code`, `supplier_Url`, `supplier_PhoneGeneral`, `supplier_PhoneSales`, `supplier_PhoneSupport`, `supplier_MailGeneral`, `supplier_MailSales`, `supplier_MailSupport`, `supplier_Memo`, `currency_Id`, `currency_Code`, `currency_Symbol`, `country_Name`) VALUES
	(1, 'CORNWALL', 'Cornwall Model Boats Ltd', 'Unit 3B, Highfield Rd Ind Est', 'Camelford', 'PL32 9RA', 'Cornwall', 2, 'UK', 'https://www.cornwallmodelboats.co.uk/', '01840 211009', NULL, NULL, NULL, 'sales@cornwallmodelboats.co.uk', NULL, NULL, 2, 'GBP', '£', 'Engeland');
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
