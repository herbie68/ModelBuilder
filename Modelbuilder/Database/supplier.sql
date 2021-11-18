-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server versie:                8.0.26 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Versie:              11.3.0.6337
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Structuur van  tabel modelbuilder.supplier wordt geschreven
CREATE TABLE IF NOT EXISTS `supplier` (
  `supplier_Id` int NOT NULL AUTO_INCREMENT,
  `supplier_Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `supplier_Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `supplier_Address1` varchar(150) DEFAULT NULL,
  `supplier_Address2` varchar(150) DEFAULT NULL,
  `supplier_Zip` varchar(15) DEFAULT NULL,
  `supplier_City` varchar(40) DEFAULT NULL,
  `supplier_Url` varchar(255) DEFAULT NULL,
  `supplier_ShippingCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `supplier_MinShippingCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `supplier_OrderCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `supplier_MinOrderCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `supplier_PhoneGeneral` varchar(40) DEFAULT NULL,
  `supplier_PhoneSales` varchar(40) DEFAULT NULL,
  `supplier_PhoneSupport` varchar(40) DEFAULT NULL,
  `supplier_MailGeneral` varchar(80) DEFAULT NULL,
  `supplier_MailSales` varchar(80) DEFAULT NULL,
  `supplier_MailSupport` varchar(80) DEFAULT NULL,
  `supplier_Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `supplier_CurrencyId` int DEFAULT '1',
  `supplier_CurrencySymbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '€',
  `supplier_CountryId` int DEFAULT '1',
  `supplier_CountryName` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT 'Nederland',
  PRIMARY KEY (`supplier_Id`),
  UNIQUE KEY `supplier_Code` (`supplier_Code`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.supplier: ~5 rows (ongeveer)
DELETE FROM `supplier`;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` (`supplier_Id`, `supplier_Code`, `supplier_Name`, `supplier_Address1`, `supplier_Address2`, `supplier_Zip`, `supplier_City`, `supplier_Url`, `supplier_ShippingCosts`, `supplier_MinShippingCosts`, `supplier_OrderCosts`, `supplier_MinOrderCosts`, `supplier_PhoneGeneral`, `supplier_PhoneSales`, `supplier_PhoneSupport`, `supplier_MailGeneral`, `supplier_MailSales`, `supplier_MailSupport`, `supplier_Memo`, `supplier_CurrencyId`, `supplier_CurrencySymbol`, `supplier_CountryId`, `supplier_CountryName`) VALUES
	(1, 'CORNWALL', 'Cornwall Model Boats Ltd', 'Unit 3B, Highfield Rd Ind Est', 'Camelford', 'PL32 9RA', 'Cornwall', 'https://www.cornwallmodelboats.co.uk/', 0.00, 0.00, 0.00, 0.00, '01840 211009', '123', NULL, NULL, 'sales@cornwallmodelboats.co.uk', NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs24\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch test }{\\b\\ltrch 123}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 2, '£', 2, 'Engeland'),
	(5, 'DORDRECHT', 'Modelbouw-Dordrecht', 'Voorstraat 360', NULL, '3311 CX', 'Dordrecht', 'https://modelbouw-dordrecht.nl/', 6.95, 50.00, 0.00, 0.00, '078-6312711', NULL, NULL, 'info@modelbouw-dordrecht.nl', NULL, NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql}\r\n}', 1, '€', 1, 'Nederland'),
	(6, 'KRIKKE', 'Modelbouw Krikke', 'Nieuweweg 22', NULL, '9711 TE', 'Groningen', 'https://www.modelbouwkrikke.nl/', 7.25, 100.00, 0.00, 0.00, '050-3140306', NULL, NULL, 'ron@krikke.net', NULL, NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Winkelopeningstijden: Dinsdag t/m Zaterdag: 10.00-17.00 uur}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, '€', 1, 'Nederland'),
	(7, 'HOBBYMODELBOUW', 'Hobby & Modelbouw', 'Angstelkade 2a unit 3.3', '(Let op alleen webshop!)', '3631 NA', 'Nieuwersluis', 'https://www.hobby-en-modelbouw.nl/', 6.95, 100.00, 0.00, 0.00, '0294-266587', NULL, NULL, 'info@hobby-en-modelbouw.nl', NULL, NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Bereikbaar op ma. t/m vr. tussen 11.00 en 15.00 uur}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch IBANnr: NL61 RBRB 0941 5046 03 BIC /Swift : RBRBNL21}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, '€', 1, 'Nederland'),
	(8, 'MEIJERBLESSING', 'Meijer & Blessing', 'Westewagenstraat 27', NULL, '3011 AR', 'Rotterdam', 'https://www.meijerenblessing.nl/', 6.99, 100.00, 0.00, 0.00, '010-4145591', NULL, NULL, 'info@meijerenblessing.nl', NULL, NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Openingstijden}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch maandag\\tab gesloten}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch dinsdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch woensdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch donderdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch vrijdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch zaterdag\\tab 09:00 \\endash  17:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch zondag\\tab gesloten}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, '€', 1, 'Nederland');
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
