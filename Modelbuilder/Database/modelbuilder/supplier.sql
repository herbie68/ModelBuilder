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
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Address1` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Address2` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Zip` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `City` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ShippingCosts` double NOT NULL DEFAULT '0',
  `MinShippingCosts` double NOT NULL DEFAULT '0',
  `OrderCosts` double NOT NULL DEFAULT '0',
  `Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Currency_Id` int DEFAULT '1',
  `Country_Id` int DEFAULT '1',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE KEY `supplier_Code` (`Code`) USING BTREE,
  KEY `Currency_Id` (`Currency_Id`) USING BTREE,
  KEY `Country_Id` (`Country_Id`) USING BTREE,
  CONSTRAINT `FK_Supplier_Country_Id` FOREIGN KEY (`Country_Id`) REFERENCES `country` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Supplier_Currency_Id` FOREIGN KEY (`Currency_Id`) REFERENCES `currency` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.supplier: ~6 rows (ongeveer)
DELETE FROM `supplier`;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` (`Id`, `Code`, `Name`, `Address1`, `Address2`, `Zip`, `City`, `Url`, `ShippingCosts`, `MinShippingCosts`, `OrderCosts`, `Memo`, `Currency_Id`, `Country_Id`, `Created`, `Modified`) VALUES
	(1, 'CORNWALL', 'Cornwall Model Boats Ltd', 'Unit 3B, Highfield Rd Ind Est', 'Camelford', 'PL32 9RA', 'Cornwall', 'https://www.cornwallmodelboats.co.uk/', 0, 0, 0, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs24\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch test }{\\b\\ltrch 123}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 2, 2, '2022-01-11 11:14:05', '2022-01-11 11:14:05'),
	(2, 'DORDRECHT', 'Modelbouw-Dordrecht', 'Voorstraat 360', NULL, '3311 CX', 'Dordrecht', 'https://modelbouw-dordrecht.nl/', 6.95, 50, 0, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql}\r\n}', 1, 1, '2022-01-11 11:14:05', '2022-01-11 11:14:05'),
	(3, 'KRIKKE', 'Modelbouw Krikke', 'Nieuweweg 22', NULL, '9711 TE', 'Groningen', 'https://www.modelbouwkrikke.nl/', 7.25, 100, 0, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Winkelopeningstijden: Dinsdag t/m Zaterdag: 10.00-17.00 uur}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, 1, '2022-01-11 11:14:05', '2022-01-11 11:14:05'),
	(4, 'HOBBYMODELBOUW', 'Hobby & Modelbouw', 'Angstelkade 2a unit 3.3', '(Let op alleen webshop!)', '3631 NA', 'Nieuwersluis', 'https://www.hobby-en-modelbouw.nl/', 6.95, 100, 0, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Bereikbaar op ma. t/m vr. tussen 11.00 en 15.00 uur}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch IBANnr: NL61 RBRB 0941 5046 03 BIC /Swift : RBRBNL21}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, 1, '2022-01-11 11:14:05', '2022-01-11 11:14:05'),
	(5, 'MEIJERBLESSING', 'Meijer & Blessing', 'Westewagenstraat 27', NULL, '3011 AR', 'Rotterdam', 'https://www.meijerenblessing.nl/', 6.99, 100, 0, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Openingstijden}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch maandag\\tab gesloten}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch dinsdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch woensdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch donderdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch vrijdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch zaterdag\\tab 09:00 \\endash  17:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch zondag\\tab gesloten}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, 1, '2022-01-11 11:14:05', '2022-01-11 11:14:05'),
	(6, 'TOOLSTATION', 'Toolstation', 'Wegtersweg 14 24', NULL, '7556 BR', 'Hengelo', 'https://toolstation.nl', 5, 20, 0, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\b\\ltrch Openingstijden:}\\line {\\ltrch Ma - Vr: 7:00 - 18:00 uur}\\line {\\ltrch Za: 8:00 - 17:00 uur}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\b\\ltrch Bezorgdagen:}\\line {\\b\\ltrch }{\\ltrch Besteld voor 22.00 uur op\\tab Bezorgd op:*}\\line {\\ltrch maandag\\tab \\tab dinsdag\\tab }\\line {\\ltrch dinsdag\\tab \\tab                woensdag\\tab }\\line {\\ltrch woensdag\\tab \\tab donderdag\\tab }\\line {\\ltrch donderdag\\tab \\tab vrijdag\\tab }\\line {\\ltrch vrijdag\\tab \\tab               zaterdag of maandag\\tab }\\line {\\ltrch zaterdag (voor 15.00 uur)\\tab maandag}\\line {\\ltrch zaterdag (na 15.00 uur)       dinsdag }\\line {\\ltrch zondag\\tab \\tab                dinsdag\\tab }\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\i\\ltrch *Het kan voorkomen dat een product tijdelijk niet op voorraad is. De actuele voorraad van het product kun je op de website zien. Als een product niet op voorraad is, dan ontvang je hierover bericht.}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch }\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, 1, '2022-01-11 11:14:05', '2022-01-11 11:14:05');
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
