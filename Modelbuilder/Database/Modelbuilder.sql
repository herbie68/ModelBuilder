/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP DATABASE IF EXISTS `modelbuilder`;
CREATE DATABASE IF NOT EXISTS `modelbuilder` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `modelbuilder`;

DROP TABLE IF EXISTS `brand`;
CREATE TABLE IF NOT EXISTS `brand` (
  `brand_Id` int NOT NULL AUTO_INCREMENT,
  `brand_Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`brand_Id`),
  UNIQUE KEY `barnd_Name` (`brand_Name`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List of brands for tools, kits, suplies and all other stuf';

DELETE FROM `brand`;
/*!40000 ALTER TABLE `brand` DISABLE KEYS */;
INSERT INTO `brand` (`brand_Id`, `brand_Name`) VALUES
	(17, 'Aeronaut'),
	(4, 'Amati'),
	(5, 'Artesania'),
	(12, 'Billing Boats'),
	(14, 'Caldercraft'),
	(15, 'Constructo'),
	(18, 'Corel'),
	(2, 'Dremel'),
	(3, 'Excel'),
	(10, 'Humbrol'),
	(16, 'Krick'),
	(6, 'Mantua'),
	(13, 'Model Shipways'),
	(7, 'Modelcraft'),
	(8, 'Occre'),
	(19, 'Panart'),
	(1, 'Proxxon'),
	(9, 'Revell'),
	(20, 'Sergal'),
	(11, 'Tamiya');
/*!40000 ALTER TABLE `brand` ENABLE KEYS */;

DROP TABLE IF EXISTS `category`;
CREATE TABLE IF NOT EXISTS `category` (
  `category_Id` int NOT NULL AUTO_INCREMENT,
  `category_Name` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `category_Fullpath` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `category_ParentId` int DEFAULT NULL,
  PRIMARY KEY (`category_Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=777 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

DELETE FROM `category`;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` (`category_Id`, `category_Name`, `category_Fullpath`, `category_ParentId`) VALUES
	(510, 'Afwerkingerking', 'Afwerking', NULL),
	(511, 'Veroudering/Bruinering', 'Afwerking\\Veroudering/Bruinering', 510),
	(512, 'Maskering', 'Afwerking\\Maskering', 510),
	(513, 'Penselen', 'Afwerking\\Penselen', 510),
	(514, 'Verf', 'Afwerking\\Verf', 510),
	(515, 'Lak', 'Afwerking\\Lak', 510),
	(516, 'Lijm', 'Afwerking\\Lijm', 510),
	(517, 'Houtlijm', 'Afwerking\\Lijm\\Houtlijm', 516),
	(518, 'Secondenlijm', 'Afwerking\\Lijm\\Secondenlijm', 516),
	(519, 'Gereedschapeedschap', 'Gereedschap', NULL),
	(520, 'Algemeen', 'Gereedschap\\Algemeen', 519),
	(521, 'Accessoires', 'Gereedschap\\Algemeen\\Accessoires', 520),
	(522, 'Elektrisch', 'Gereedschap\\Elektrisch', 519),
	(523, 'Accessoires', 'Gereedschap\\Elektrisch\\Accessoires', 522),
	(524, 'Hand', 'Gereedschap\\Hand', 519),
	(525, 'Accessoires', 'Gereedschap\\Hand\\Accessoires', 524),
	(526, 'Meetgereedschap', 'Gereedschap\\Meetgereedschap', 519),
	(527, 'Accessoires', 'Gereedschap\\Meetgereedschap\\Accessoires', 526),
	(528, 'Hulpmiddelen', 'Hulpmiddelen', NULL),
	(529, 'Opbergmiddelen', 'Hulpmiddelen\\Opbergmiddelen', 528),
	(530, 'Schuurpapier', 'Hulpmiddelen\\Schuurpapier', 528),
	(531, 'Materiaal', 'Materiaal', NULL),
	(532, 'Scheepsbeslag', 'Materiaal\\Scheepsbeslag', 531),
	(533, 'Ankers', 'Materiaal\\Scheepsbeslag\\Ankers', 532),
	(534, 'Boegbeelden', 'Materiaal\\Scheepsbeslag\\Boegbeelden', 532),
	(535, 'Scheepsbellen/hoorns', 'Materiaal\\Scheepsbeslag\\Scheepsbellen/hoorns', 532),
	(536, 'Blokken', 'Materiaal\\Scheepsbeslag\\Blokken', 532),
	(537, 'Kikkers/Bolders/Klampen', 'Materiaal\\Scheepsbeslag\\Kikkers/Bolders/Klampen', 532),
	(538, 'Bijboten', 'Materiaal\\Scheepsbeslag\\Bijboten', 532),
	(539, 'Dekattributen', 'Materiaal\\Scheepsbeslag\\Dekattributen', 532),
	(540, 'Scheepsklokken', 'Materiaal\\Scheepsbeslag\\Scheepsklokken', 532),
	(541, 'Lantaarns', 'Materiaal\\Scheepsbeslag\\Lantaarns', 532),
	(542, 'Mastvoetten', 'Materiaal\\Scheepsbeslag\\Mastvoeten', 532),
	(543, 'Korvijnagels(Belayingpins)', 'Materiaal\\Scheepsbeslag\\Korvijnagels(Belayingpins)', 532),
	(544, 'Ogen', 'Materiaal\\Scheepsbeslag\\Ogen', 532),
	(545, 'Bevestigingspinnen', 'Materiaal\\Scheepsbeslag\\Bevestigingspinnen', 532),
	(546, 'Pompen', 'Materiaal\\Scheepsbeslag\\Pompen', 532),
	(547, 'Patrijspoorten', 'Materiaal\\Scheepsbeslag\\Patrijspoorten', 532),
	(548, 'Roosters', 'Materiaal\\Scheepsbeslag\\Roosters', 532),
	(549, 'Geschut', 'Materiaal\\Scheepsbeslag\\Geschut', 532),
	(550, 'Stuurwielen', 'Materiaal\\Scheepsbeslag\\Stuurwielen', 532),
	(551, 'Trappen', 'Materiaal\\Scheepsbeslag\\Trappen', 532),
	(552, 'Vlaggenmasten', 'Materiaal\\Scheepsbeslag\\Vlaggenmasten', 532),
	(553, 'Want/Mast/Geleiding', 'Materiaal\\Scheepsbeslag\\Want/Mast/Geleiding', 532),
	(554, 'Beuken', 'Materiaal\\Beuken', 531),
	(555, 'Lat', 'Materiaal\\Beuken\\Lat', 554),
	(556, 'Plaat', 'Materiaal\\Beuken\\Plaat', 554),
	(557, 'Profiel', 'Materiaal\\Beuken\\Profiel', 554),
	(558, 'Rond', 'Materiaal\\Beuken\\Rond', 554),
	(559, 'Vierkant', 'Materiaal\\Beuken\\Vierkant', 554),
	(560, 'Eiken', 'Materiaal\\Eiken', 531),
	(561, 'Lat', 'Materiaal\\Eiken\\Lat', 560),
	(562, 'Plaat', 'Materiaal\\Eiken\\Plaat', 560),
	(563, 'Profiel', 'Materiaal\\Eiken\\Profiel', 560),
	(564, 'Rond', 'Materiaal\\Eiken\\Rond', 560),
	(565, 'Vierkant', 'Materiaal\\Eiken\\Vierkant', 560),
	(566, 'Garen', 'Materiaal\\Garen', 531),
	(567, 'Beige', 'Materiaal\\Garen\\Beige', 566),
	(568, 'Bruin', 'Materiaal\\Garen\\Bruin', 566),
	(569, 'Wit', 'Materiaal\\Garen\\Wit', 566),
	(570, 'Zwart', 'Materiaal\\Garen\\Zwart', 566),
	(571, 'Koper', 'Materiaal\\Koper', 531),
	(572, 'Draad', 'Materiaal\\Koper\\Draad', 571),
	(573, 'Plaat', 'Materiaal\\Koper\\Plaat', 571),
	(574, 'Profiel', 'Materiaal\\Koper\\Profiel', 571),
	(575, 'Rond', 'Materiaal\\Koper\\Rond', 571),
	(576, 'Strip', 'Materiaal\\Koper\\Strip', 571),
	(577, 'Vierkant', 'Materiaal\\Koper\\Vierkant', 571),
	(578, 'Kunststof', 'Materiaal\\Kunststof', 531),
	(579, 'Plaat', 'Materiaal\\Kunststof\\Plaat', 578),
	(580, 'Profiel', 'Materiaal\\Kunststof\\Profiel', 578),
	(581, 'Rond', 'Materiaal\\Kunststof\\Rond', 578),
	(582, 'Strip', 'Materiaal\\Kunststof\\Strip', 578),
	(583, 'Vierkant', 'Materiaal\\Kunststof\\Vierkant', 578),
	(584, 'Mahoni', 'Materiaal\\Mahoni', 531),
	(585, 'Lat', 'Materiaal\\Mahoni\\Lat', 584),
	(586, 'Plaat', 'Materiaal\\Mahoni\\Plaat', 584),
	(587, 'Profiel', 'Materiaal\\Mahoni\\Profiel', 584),
	(588, 'Rond', 'Materiaal\\Mahoni\\Rond', 584),
	(589, 'Vierkant', 'Materiaal\\Mahoni\\Vierkant', 584),
	(590, 'Messing', 'Materiaal\\Messing', 531),
	(591, 'Draad', 'Materiaal\\Messing\\Draad', 590),
	(592, 'Plaat', 'Materiaal\\Messing\\Plaat', 590),
	(593, 'Profiel', 'Materiaal\\Messing\\Profiel', 590),
	(594, 'Rond', 'Materiaal\\Messing\\Rond', 590),
	(595, 'Strip', 'Materiaal\\Messing\\Strip', 590),
	(596, 'Vierkant', 'Materiaal\\Messing\\Vierkant', 590),
	(597, 'Noten', 'Materiaal\\Noten', 531),
	(598, 'Lat', 'Materiaal\\Noten\\Lat', 597),
	(599, 'Plaat', 'Materiaal\\Noten\\Plaat', 597),
	(600, 'Profiel', 'Materiaal\\Noten\\Profiel', 597),
	(601, 'Rond', 'Materiaal\\Noten\\Rond', 597),
	(602, 'Vierkant', 'Materiaal\\Noten\\Vierkant', 597),
	(603, 'Vuren', 'Materiaal\\Vuren', 531),
	(604, 'Lat', 'Materiaal\\Vuren\\Lat', 602),
	(605, 'Plaat', 'Materiaal\\Vuren\\Plaat', 602),
	(606, 'Profiel', 'Materiaal\\Vuren\\Profiel', 602),
	(607, 'Rond', 'Materiaal\\Vuren\\Rond', 602),
	(608, 'Vierkant', 'Materiaal\\Vuren\\Vierkant', 602),
	(609, 'Zeilen', 'Materiaal\\Zeilen', 531),
	(610, 'Bruin', 'Materiaal\\Zeilen\\Bruin', 609),
	(611, 'Creme', 'Materiaal\\Zeilen\\Creme', 609),
	(612, 'Wit', 'Materiaal\\Zeilen\\Wit', 609),
	(613, 'Zwart', 'Materiaal\\Zeilen\\Zwart', 609);
/*!40000 ALTER TABLE `category` ENABLE KEYS */;

DROP TABLE IF EXISTS `country`;
CREATE TABLE IF NOT EXISTS `country` (
  `country_Id` int NOT NULL AUTO_INCREMENT,
  `country_Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `country_Defaultcurrency_Symbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '€',
  `country_Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `country_Defaultcurrency_Id` int DEFAULT NULL,
  PRIMARY KEY (`country_Id`) USING BTREE,
  UNIQUE KEY `country_Code_UNIQUE` (`country_Code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `country`;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` (`country_Id`, `country_Code`, `country_Defaultcurrency_Symbol`, `country_Name`, `country_Defaultcurrency_Id`) VALUES
	(1, 'NL', '€', 'Nederland', 1),
	(2, 'UK', '£', 'Engeland', 2),
	(3, 'US', '$', 'Verenigde staten', 3),
	(4, 'DE', '€', 'Duitsland', 1),
	(5, 'ESP', '€', 'Spanje', 1),
	(6, 'CH', 'Y', 'China', 4),
	(15, 'IT', '€', 'Italë', 1);
/*!40000 ALTER TABLE `country` ENABLE KEYS */;

DROP TABLE IF EXISTS `currency`;
CREATE TABLE IF NOT EXISTS `currency` (
  `currency_Id` int NOT NULL AUTO_INCREMENT,
  `currency_Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `currency_Symbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `currency_Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `currency_ConversionRate` float(6,4) DEFAULT NULL,
  PRIMARY KEY (`currency_Id`) USING BTREE,
  UNIQUE KEY `currency_Code` (`currency_Code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `currency`;
/*!40000 ALTER TABLE `currency` DISABLE KEYS */;
INSERT INTO `currency` (`currency_Id`, `currency_Code`, `currency_Symbol`, `currency_Name`, `currency_ConversionRate`) VALUES
	(1, 'EUR', '€', 'Euro', 1.0000),
	(2, 'GBP', '£', 'Britse pond', 1.1079),
	(3, 'USD', '$', 'Amerikaanse dollar', 0.8442),
	(4, 'YEN', 'Y', 'Yen', 3.3400),
	(5, 'TST', 'T', 'Testje', 1.2300);
/*!40000 ALTER TABLE `currency` ENABLE KEYS */;

DROP TABLE IF EXISTS `language`;
CREATE TABLE IF NOT EXISTS `language` (
  `language_Id` int NOT NULL AUTO_INCREMENT,
  `language_Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `language_Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`language_Id`) USING BTREE,
  UNIQUE KEY `language_code` (`language_Code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `language`;
/*!40000 ALTER TABLE `language` DISABLE KEYS */;
INSERT INTO `language` (`language_Id`, `language_Code`, `language_Name`) VALUES
	(1, 'NL', 'Nederlands'),
	(2, 'EN', 'English'),
	(3, 'DE', 'Deutsch');
/*!40000 ALTER TABLE `language` ENABLE KEYS */;

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

DROP TABLE IF EXISTS `project`;
CREATE TABLE IF NOT EXISTS `project` (
  `projects_Id` int NOT NULL AUTO_INCREMENT,
  `projects_Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `projects_StartDate` date DEFAULT NULL,
  `projects_ExpectedEndDate` date DEFAULT NULL,
  `projects_EndDate` date DEFAULT NULL,
  `projects_TotalCost` decimal(5,2) DEFAULT NULL,
  `projects_TotalMinutes` int DEFAULT NULL,
  PRIMARY KEY (`projects_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `project`;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
/*!40000 ALTER TABLE `project` ENABLE KEYS */;

DROP TABLE IF EXISTS `storage`;
CREATE TABLE IF NOT EXISTS `storage` (
  `storage_Id` int NOT NULL AUTO_INCREMENT,
  `storage_ParentId` int DEFAULT NULL,
  `storage_FullPath` varchar(400) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `storage_Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`storage_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=265 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `storage`;
/*!40000 ALTER TABLE `storage` DISABLE KEYS */;
INSERT INTO `storage` (`storage_Id`, `storage_ParentId`, `storage_FullPath`, `storage_Name`) VALUES
	(1, NULL, 'Herberts Werf', 'Herberts Werf'),
	(2, 1, 'Herberts Werf\\Hoge kast', 'Hoge kast'),
	(3, 2, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken', 'Hoge kast  - Planken'),
	(4, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 0e plank', 'Hoge kast  - Planken: 0e plank'),
	(5, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 1e plank', 'Hoge kast  - Planken: 1e plank'),
	(6, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 2e plank', 'Hoge kast  - Planken: 2e plank'),
	(7, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 3e plank', 'Hoge kast  - Planken: 3e plank'),
	(8, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 4e plank', 'Hoge kast  - Planken: 4e plank'),
	(9, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 5e plank', 'Hoge kast  - Planken: 5e plank'),
	(10, 2, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant', 'Hoge kast  - Zijkant'),
	(11, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 0e klemmenlat', 'Hoge kast  - Zijkant: zijkant 0e klemmenlat'),
	(12, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 1e klemmenlat', 'Hoge kast  - Zijkant: zijkant 1e klemmenlat'),
	(13, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 2e klemmenlat', 'Hoge kast  - Zijkant: zijkant 2e klemmenlat'),
	(14, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 3e klemmenlat', 'Hoge kast  - Zijkant: zijkant 3e klemmenlat'),
	(15, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 4e klemmenlat', 'Hoge kast  - Zijkant: zijkant 4e klemmenlat'),
	(16, 1, 'Herberts Werf\\Ladenkast', 'Ladenkast'),
	(23, 1, 'Herberts Werf\\Onderste muurplank', 'Onderste muurplank'),
	(24, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 01 - Garen (rood) ', 'Onderste muurplank  - Bak 01 - Garen (rood) '),
	(43, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 02 - Garen (blauw) ', 'Onderste muurplank  - Bak 02 - Garen (blauw) '),
	(60, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 03 (turquoise)', 'Onderste muurplank  - Bak 03 (turquoise)'),
	(85, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 04', 'Onderste muurplank  - Bak 04'),
	(126, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 05', 'Onderste muurplank  - Bak 05'),
	(151, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06', 'Onderste muurplank  - Bak 06'),
	(157, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 1 vak 06', 'Onderste muurplank  - Bak 06: rij 1 vak 06'),
	(158, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 1 vak 07', 'Onderste muurplank  - Bak 06: rij 1 vak 07'),
	(159, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06Onderste muurplank  - Bak 06: rij 1 vak 08', 'Onderste muurplank  - Bak 06: rij 1 vak 08'),
	(160, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 1 vak 09', 'Onderste muurplank  - Bak 06: rij 1 vak 09'),
	(161, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 1 vak 10', 'Onderste muurplank  - Bak 06: rij 1 vak 10'),
	(170, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 2 vak 09', 'Onderste muurplank  - Bak 06: rij 2 vak 09'),
	(171, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 2 vak 10', 'Onderste muurplank  - Bak 06: rij 2 vak 10'),
	(180, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 3 vak 09', 'Onderste muurplank  - Bak 06: rij 3 vak 09'),
	(181, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 3 vak 10', 'Onderste muurplank  - Bak 06: rij 3 vak 10'),
	(186, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 05', 'Onderste muurplank  - Bak 06: rij 4 vak 05'),
	(187, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 06', 'Onderste muurplank  - Bak 06: rij 4 vak 06'),
	(188, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 07', 'Onderste muurplank  - Bak 06: rij 4 vak 07'),
	(189, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 08', 'Onderste muurplank  - Bak 06: rij 4 vak 08'),
	(190, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 09', 'Onderste muurplank  - Bak 06: rij 4 vak 09'),
	(191, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 10', 'Onderste muurplank  - Bak 06: rij 4 vak 10'),
	(192, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad', 'Onderste muurplank  - Bak 07 - Draad'),
	(193, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 01', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 01'),
	(194, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 02', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 02'),
	(195, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 03', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 03'),
	(196, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 04', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 04'),
	(197, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 05', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 05'),
	(198, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 01', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 01'),
	(199, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 02', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 02'),
	(200, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 03', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 03'),
	(201, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 04', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 04'),
	(202, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 05', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 05'),
	(203, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 06', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 06'),
	(204, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 07', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 07'),
	(205, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 08', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 08'),
	(206, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 01', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 01'),
	(207, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 02', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 02'),
	(208, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 03', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 03'),
	(209, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 04', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 04'),
	(210, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 05', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 05'),
	(211, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 06', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 06'),
	(212, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 07', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 07'),
	(213, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 08', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 08'),
	(214, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 4 vak 01', 'Onderste muurplank  - Bak 07 - Draad: rij 4 vak 01'),
	(215, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 4 vak 02', 'Onderste muurplank  - Bak 07 - Draad: rij 4 vak 02'),
	(216, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 4 vak 03', 'Onderste muurplank  - Bak 07 - Draad: rij 4 vak 03'),
	(217, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 4 vak 04', 'Onderste muurplank  - Bak 07 - Draad: rij 4 vak 04'),
	(218, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)', 'Onderste muurplank  - Ladenkast (5 lades)'),
	(219, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij boven links', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij boven links'),
	(220, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij boven midden', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij boven midden'),
	(221, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij boven rechts', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij boven rechts'),
	(222, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij midden', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij midden'),
	(223, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij onder', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij onder'),
	(224, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench', 'Onderste muurplank  - Workbench'),
	(225, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: gereedschaphouder', 'Onderste muurplank  - Workbench: gereedschaphouder'),
	(226, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 0', 'Onderste muurplank  - Workbench: top (boven lades)'),
	(227, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 1', 'Onderste muurplank  - Workbench: lade 1'),
	(228, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 2', 'Onderste muurplank  - Workbench: lade 2'),
	(229, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 3', 'Onderste muurplank  - Workbench: lade 3'),
	(230, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 4', 'Onderste muurplank  - Workbench: lade 4'),
	(231, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 5', 'Onderste muurplank  - Workbench: lade 5'),
	(232, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 6', 'Onderste muurplank  - Workbench: lade 6'),
	(233, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 7', 'Onderste muurplank  - Workbench: verzamellade'),
	(234, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: werkruimte', 'Onderste muurplank  - Workbench: werkruimte'),
	(235, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 1', 'Onderste muurplank  - Workbench: voorste zijvak'),
	(236, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 2', 'Onderste muurplank  - Workbench: zijvak 2'),
	(237, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 3', 'Onderste muurplank  - Workbench: zijvak 3'),
	(238, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 4', 'Onderste muurplank  - Workbench: zijvak 4'),
	(239, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 5', 'Onderste muurplank  - Workbench: achterste zijvak'),
	(240, 1, 'Herberts Werf\\Middelste muurplank', 'Middelste muurplank'),
	(241, 1, 'Herberts Werf\\Bovenste muurplank', 'Bovenste muurplank'),
	(242, 1, 'Herberts Werf\\Werkbank', 'Werkbank'),
	(243, 242, 'Herberts Werf\\Werkbank\\Gereedschaphouder 1', 'Werkbank  - Gereedschaphouder 1'),
	(244, 242, 'Herberts Werf\\Werkbank\\Gereedschaphouder 2', 'Werkbank  - Gereedschaphouder 2'),
	(245, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)', 'Werkbank  - Ladenkast (9 lades)'),
	(246, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Lade 1', 'Werkbank  - Ladenkast (9 lades): Lade 1'),
	(247, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 2', 'Werkbank  - Ladenkast (9 lades): boven midden'),
	(248, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 3', 'Werkbank  - Ladenkast (9 lades): boven rechts'),
	(249, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 4', 'Werkbank  - Ladenkast (9 lades): midden links'),
	(250, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 5', 'Werkbank  - Ladenkast (9 lades): midden midden'),
	(251, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 6', 'Werkbank  - Ladenkast (9 lades): midden rechts'),
	(252, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 7', 'Werkbank  - Ladenkast (9 lades): onder links'),
	(253, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 8', 'Werkbank  - Ladenkast (9 lades): onder midden'),
	(254, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 9', 'Werkbank  - Ladenkast (9 lades): onder rechts'),
	(255, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Onder de werkbak', 'Werkbank  - Onder de werkbak'),
	(256, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Tribune 1 (links)', 'Werkbank  - Tribune 1 (links)'),
	(257, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Tribune 2 (midden)', 'Werkbank  - Tribune 2 (lmidden)'),
	(258, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Tribune 3 (rechts)', 'Werkbank  - Tribune 3 (rechts)');
/*!40000 ALTER TABLE `storage` ENABLE KEYS */;

DROP TABLE IF EXISTS `supplier`;
CREATE TABLE IF NOT EXISTS `supplier` (
  `supplier_Id` int NOT NULL AUTO_INCREMENT,
  `supplier_Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `supplier_Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `supplier_Address1` varchar(150) DEFAULT NULL,
  `supplier_Address2` varchar(150) DEFAULT NULL,
  `supplier_Zip` varchar(15) DEFAULT NULL,
  `supplier_City` varchar(40) DEFAULT NULL,
  `supplier_Url` varchar(255) DEFAULT NULL,
  `supplier_OrderCosts` float(6,2) DEFAULT '0.00',
  `supplier_MinOrderCosts` float(6,2) DEFAULT '0.00',
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

DELETE FROM `supplier`;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` (`supplier_Id`, `supplier_Code`, `supplier_Name`, `supplier_Address1`, `supplier_Address2`, `supplier_Zip`, `supplier_City`, `supplier_Url`, `supplier_OrderCosts`, `supplier_MinOrderCosts`, `supplier_PhoneGeneral`, `supplier_PhoneSales`, `supplier_PhoneSupport`, `supplier_MailGeneral`, `supplier_MailSales`, `supplier_MailSupport`, `supplier_Memo`, `supplier_CurrencyId`, `supplier_CurrencySymbol`, `supplier_CountryId`, `supplier_CountryName`) VALUES
	(1, 'CORNWALL', 'Cornwall Model Boats Ltd', 'Unit 3B, Highfield Rd Ind Est', 'Camelford', 'PL32 9RA', 'Cornwall', 'https://www.cornwallmodelboats.co.uk/', 0.00, 0.00, '01840 211009', '123', NULL, NULL, 'sales@cornwallmodelboats.co.uk', NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs24\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch test }{\\b\\ltrch 123}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 2, '£', 2, 'Engeland'),
	(5, 'DORDRECHT', 'Modelbouw-Dordrecht', 'Voorstraat 360', NULL, '3311 CX', 'Dordrecht', 'https://modelbouw-dordrecht.nl/', 6.95, 50.00, '078-6312711', NULL, NULL, 'info@modelbouw-dordrecht.nl', NULL, NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql}\r\n}', 1, '€', 1, 'Nederland'),
	(6, 'KRIKKE', 'Modelbouw Krikke', 'Nieuweweg 22', NULL, '9711 TE', 'Groningen', 'https://www.modelbouwkrikke.nl/', 7.25, 100.00, '050-3140306', NULL, NULL, 'ron@krikke.net', NULL, NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Winkelopeningstijden: Dinsdag t/m Zaterdag: 10.00-17.00 uur}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, '€', 1, 'Nederland'),
	(7, 'HOBBYMODELBOUW', 'Hobby & Modelbouw', 'Angstelkade 2a unit 3.3', '(Let op alleen webshop!)', '3631 NA', 'Nieuwersluis', 'https://www.hobby-en-modelbouw.nl/', 6.95, 100.00, '0294-266587', NULL, NULL, 'info@hobby-en-modelbouw.nl', NULL, NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Bereikbaar op ma. t/m vr. tussen 11.00 en 15.00 uur}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch IBANnr: NL61 RBRB 0941 5046 03 BIC /Swift : RBRBNL21}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, '€', 1, 'Nederland'),
	(8, 'MEIJERBLESSING', 'Meijer & Blessing', 'Westewagenstraat 27', NULL, '3011 AR', 'Rotterdam', 'https://www.meijerenblessing.nl/', 6.99, 100.00, '010-4145591', NULL, NULL, 'info@meijerenblessing.nl', NULL, NULL, '{\\rtf1\\ansi\\ansicpg1252\\uc1\\htmautsp\\deff2{\\fonttbl{\\f0\\fcharset0 Times New Roman;}{\\f2\\fcharset0 Segoe UI;}}{\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;}\\loch\\hich\\dbch\\pard\\plain\\ltrpar\\itap0{\\lang1033\\fs18\\f2\\cf0 \\cf0\\ql{\\f2 {\\ltrch Openingstijden}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch maandag\\tab gesloten}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch dinsdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch woensdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch donderdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch vrijdag\\tab 09:30 \\endash  18:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch zaterdag\\tab 09:00 \\endash  17:00}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n{\\f2 {\\ltrch zondag\\tab gesloten}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}\r\n}\r\n}', 1, '€', 1, 'Nederland');
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;

DROP TABLE IF EXISTS `unit`;
CREATE TABLE IF NOT EXISTS `unit` (
  `unit_Id` int NOT NULL AUTO_INCREMENT,
  `unit_Name` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0',
  PRIMARY KEY (`unit_Id`),
  UNIQUE KEY `unit_Name` (`unit_Name`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `unit`;
/*!40000 ALTER TABLE `unit` DISABLE KEYS */;
INSERT INTO `unit` (`unit_Id`, `unit_Name`) VALUES
	(1, ''),
	(6, 'cm'),
	(11, 'dl'),
	(4, 'Fles'),
	(7, 'gr'),
	(9, 'kg'),
	(12, 'ltr'),
	(8, 'mgr'),
	(10, 'ml'),
	(5, 'mm'),
	(13, 'mtr'),
	(3, 'Set'),
	(2, 'Stuk');
/*!40000 ALTER TABLE `unit` ENABLE KEYS */;

DROP TABLE IF EXISTS `worktype`;
CREATE TABLE IF NOT EXISTS `worktype` (
  `worktype_Id` int NOT NULL AUTO_INCREMENT,
  `worktype_ParentId` int DEFAULT NULL,
  `worktype_Name` char(150) DEFAULT NULL,
  `worktype_FullPath` char(255) DEFAULT NULL,
  PRIMARY KEY (`worktype_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `worktype`;
/*!40000 ALTER TABLE `worktype` DISABLE KEYS */;
INSERT INTO `worktype` (`worktype_Id`, `worktype_ParentId`, `worktype_Name`, `worktype_FullPath`) VALUES
	(1, NULL, 'Voorbereiding', 'Voorbereiding'),
	(2, NULL, 'Opruimen', 'Opruimen'),
	(3, NULL, 'Romp', 'Romp'),
	(4, 3, 'Kiel', 'Romp\\Kiel'),
	(5, 3, 'Spanten', 'Romp\\Spanten'),
	(6, 3, 'Eerste beplanking', 'Romp\\Eerste beplanking'),
	(7, 3, 'Tweede beplanking', 'Romp\\Tweede beplanking'),
	(8, 3, 'Achtersteven', 'Romp\\Achtersteven'),
	(9, 3, 'Afwerking romp', 'Romp\\Afwerking romp'),
	(10, 3, 'Schilderen/Lakken', 'Romp\\Schilderen/Lakken'),
	(11, NULL, 'Dek', 'Dek'),
	(12, 11, 'Dek beplanking', 'Dek\\Dek beplanking'),
	(13, 11, 'Dekbalk en reling', 'Dek\\Dekbalk en reling'),
	(14, 11, 'Schilderen/Lakken', 'Dek\\Schilderen/Lakken'),
	(15, 11, 'Afwerking dek', 'Dek\\Afwerking dek'),
	(16, NULL, 'Opbouw', 'Opbouw'),
	(17, 16, 'Bijboten', 'Opbouw\\Bijboten'),
	(18, 16, 'Wapens', 'Opbouw\\Wapens'),
	(19, 16, 'Ankers', 'Opbouw\\Ankers'),
	(20, 16, 'Heck', 'Opbouw\\Heck'),
	(21, 16, 'Masten', 'Opbouw\\Masten'),
	(22, NULL, 'Wand', 'Wand'),
	(23, 22, 'Staande wand', 'Wand\\Staande wand'),
	(24, 22, 'Lopend wand', 'Wand\\Lopend wand'),
	(25, 22, 'Afwerking', 'Wand\\Afwerking');
/*!40000 ALTER TABLE `worktype` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
