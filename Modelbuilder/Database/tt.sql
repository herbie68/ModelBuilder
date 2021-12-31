/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE DATABASE IF NOT EXISTS `modelbuilder` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `modelbuilder`;

CREATE TABLE IF NOT EXISTS `brand` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name` (`Name`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List of brands for tools, kits, suplies and all other stuf';

DELETE FROM `brand`;
/*!40000 ALTER TABLE `brand` DISABLE KEYS */;
INSERT INTO `brand` (`Id`, `Name`, `Modified`, `Created`) VALUES
	(1, '_Geen_', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(2, 'Proxxon', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(3, 'Dremel', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(4, 'Excel', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(5, 'Amati', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(6, 'Artesania Latina', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(7, 'Mantua', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(8, 'Modelcraft', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(9, 'Occre', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(10, 'Revell', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(11, 'Humbrol', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(12, 'Tamiya', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(13, 'Billing Boats', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(14, 'Model Shipways', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(15, 'Caldercraft', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(16, 'Constructo', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(17, 'Krick', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(18, 'Aeronaut', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(19, 'Corel', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(20, 'Panart', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(21, 'Sergal', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(22, 'CAP', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(23, 'CMK Czech', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(24, 'Disar', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(25, 'Dumas', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(26, 'Graupner', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(27, 'Mamoli', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(28, 'Shipyard', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(29, 'UHU', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(30, 'ZAP', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(31, 'Super Glue', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(32, 'Admirality', '2021-12-14 11:57:54', '2021-12-14 11:57:54'),
	(33, 'Aliphatic', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(34, 'Ever Build', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(35, 'Badger', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(36, 'Rotacraft', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(37, 'David', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(38, 'HobbyZone', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(39, 'Vallejo', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(40, 'AMMO Mig', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(41, 'Master Tools', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(42, 'Trumpeter', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(43, 'Faller', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(44, 'HMB', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(45, 'Proedge', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(46, 'Kinzo', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(47, 'Praxis', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(48, 'Gamma', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(49, 'Bosch', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(50, 'Makita', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(51, 'Xuron', '2021-12-14 11:57:55', '2021-12-14 11:57:55');
/*!40000 ALTER TABLE `brand` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `category` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ParentId` int DEFAULT NULL,
  `Name` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Fullpath` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `Name` (`Name`),
  KEY `FullPath` (`Fullpath`)
) ENGINE=InnoDB AUTO_INCREMENT=105 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `category`;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` (`Id`, `ParentId`, `Name`, `Fullpath`, `Created`, `Modified`) VALUES
	(1, NULL, 'Afwerking', 'Afwerking', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(2, 1, 'Veroudering/Bruinering', 'Afwerking\\Veroudering/Bruinering', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(3, 1, 'Maskering', 'Afwerking\\Maskering', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(4, 1, 'Penselen', 'Afwerking\\Penselen', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(5, 1, 'Verf', 'Afwerking\\Verf', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(6, 1, 'Lak', 'Afwerking\\Lak', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(7, 1, 'Lijm', 'Afwerking\\Lijm', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(8, 7, 'Houtlijm', 'Afwerking\\Lijm\\Houtlijm', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(9, 7, 'Secondenlijm', 'Afwerking\\Lijm\\Secondenlijm', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(10, NULL, 'Gereedschapeedschap', 'Gereedschap', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(11, 10, 'Algemeen', 'Gereedschap\\Algemeen', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(12, 11, 'Accessoires', 'Gereedschap\\Algemeen\\Accessoires', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(13, 10, 'Elektrisch', 'Gereedschap\\Elektrisch', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(14, 13, 'Accessoires', 'Gereedschap\\Elektrisch\\Accessoires', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(15, 10, 'Hand', 'Gereedschap\\Hand', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(16, 15, 'Accessoires', 'Gereedschap\\Hand\\Accessoires', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(17, 10, 'Meetgereedschap', 'Gereedschap\\Meetgereedschap', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(18, 17, 'Accessoires', 'Gereedschap\\Meetgereedschap\\Accessoires', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(19, NULL, 'Hulpmiddelen', 'Hulpmiddelen', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(20, 19, 'Opbergmiddelen', 'Hulpmiddelen\\Opbergmiddelen', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(21, 19, 'Schuurpapier', 'Hulpmiddelen\\Schuurpapier', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(22, NULL, 'Materiaal', 'Materiaal', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(23, 22, 'Scheepsbeslag', 'Materiaal\\Scheepsbeslag', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(24, 23, 'Ankers', 'Materiaal\\Scheepsbeslag\\Ankers', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(25, 23, 'Boegbeelden', 'Materiaal\\Scheepsbeslag\\Boegbeelden', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(26, 23, 'Scheepsbellen/hoorns', 'Materiaal\\Scheepsbeslag\\Scheepsbellen/hoorns', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(27, 23, 'Blokken', 'Materiaal\\Scheepsbeslag\\Blokken', '2021-12-14 11:57:55', '2021-12-14 11:57:55'),
	(28, 23, 'Kikkers/Bolders/Klampen', 'Materiaal\\Scheepsbeslag\\Kikkers/Bolders/Klampen', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(29, 23, 'Bijboten', 'Materiaal\\Scheepsbeslag\\Bijboten', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(30, 23, 'Dekattributen', 'Materiaal\\Scheepsbeslag\\Dekattributen', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(31, 23, 'Scheepsklokken', 'Materiaal\\Scheepsbeslag\\Scheepsklokken', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(32, 23, 'Lantaarns', 'Materiaal\\Scheepsbeslag\\Lantaarns', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(33, 23, 'Mastvoetten', 'Materiaal\\Scheepsbeslag\\Mastvoeten', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(34, 23, 'Korvijnagels(Belayingpins)', 'Materiaal\\Scheepsbeslag\\Korvijnagels(Belayingpins)', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(35, 23, 'Ogen', 'Materiaal\\Scheepsbeslag\\Ogen', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(36, 23, 'Bevestigingspinnen', 'Materiaal\\Scheepsbeslag\\Bevestigingspinnen', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(37, 23, 'Pompen', 'Materiaal\\Scheepsbeslag\\Pompen', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(38, 23, 'Patrijspoorten', 'Materiaal\\Scheepsbeslag\\Patrijspoorten', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(39, 23, 'Roosters', 'Materiaal\\Scheepsbeslag\\Roosters', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(40, 23, 'Geschut', 'Materiaal\\Scheepsbeslag\\Geschut', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(41, 23, 'Stuurwielen', 'Materiaal\\Scheepsbeslag\\Stuurwielen', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(42, 23, 'Trappen', 'Materiaal\\Scheepsbeslag\\Trappen', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(43, 23, 'Vlaggenmasten', 'Materiaal\\Scheepsbeslag\\Vlaggenmasten', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(44, 23, 'Want/Mast/Geleiding', 'Materiaal\\Scheepsbeslag\\Want/Mast/Geleiding', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(45, 22, 'Beuken', 'Materiaal\\Beuken', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(46, 45, 'Lat', 'Materiaal\\Beuken\\Lat', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(47, 45, 'Plaat', 'Materiaal\\Beuken\\Plaat', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(48, 45, 'Profiel', 'Materiaal\\Beuken\\Profiel', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(49, 45, 'Rond', 'Materiaal\\Beuken\\Rond', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(50, 45, 'Vierkant', 'Materiaal\\Beuken\\Vierkant', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(51, 22, 'Eiken', 'Materiaal\\Eiken', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(52, 51, 'Lat', 'Materiaal\\Eiken\\Lat', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(53, 51, 'Plaat', 'Materiaal\\Eiken\\Plaat', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(54, 51, 'Profiel', 'Materiaal\\Eiken\\Profiel', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(55, 51, 'Rond', 'Materiaal\\Eiken\\Rond', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(56, 51, 'Vierkant', 'Materiaal\\Eiken\\Vierkant', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(57, 22, 'Garen', 'Materiaal\\Garen', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(58, 57, 'Beige', 'Materiaal\\Garen\\Beige', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(59, 57, 'Bruin', 'Materiaal\\Garen\\Bruin', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(60, 57, 'Wit', 'Materiaal\\Garen\\Wit', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(61, 57, 'Zwart', 'Materiaal\\Garen\\Zwart', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(62, 22, 'Koper', 'Materiaal\\Koper', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(63, 62, 'Draad', 'Materiaal\\Koper\\Draad', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(64, 62, 'Plaat', 'Materiaal\\Koper\\Plaat', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(65, 62, 'Profiel', 'Materiaal\\Koper\\Profiel', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(66, 62, 'Rond', 'Materiaal\\Koper\\Rond', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(67, 62, 'Strip', 'Materiaal\\Koper\\Strip', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(68, 62, 'Vierkant', 'Materiaal\\Koper\\Vierkant', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(69, 22, 'Kunststof', 'Materiaal\\Kunststof', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(70, 69, 'Plaat', 'Materiaal\\Kunststof\\Plaat', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(71, 69, 'Profiel', 'Materiaal\\Kunststof\\Profiel', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(72, 69, 'Rond', 'Materiaal\\Kunststof\\Rond', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(73, 69, 'Strip', 'Materiaal\\Kunststof\\Strip', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(74, 69, 'Vierkant', 'Materiaal\\Kunststof\\Vierkant', '2021-12-14 11:57:56', '2021-12-14 11:57:56'),
	(75, 22, 'Mahoni', 'Materiaal\\Mahoni', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(76, 75, 'Lat', 'Materiaal\\Mahoni\\Lat', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(77, 75, 'Plaat', 'Materiaal\\Mahoni\\Plaat', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(78, 75, 'Profiel', 'Materiaal\\Mahoni\\Profiel', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(79, 75, 'Rond', 'Materiaal\\Mahoni\\Rond', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(80, 75, 'Vierkant', 'Materiaal\\Mahoni\\Vierkant', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(81, 22, 'Messing', 'Materiaal\\Messing', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(82, 81, 'Draad', 'Materiaal\\Messing\\Draad', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(83, 81, 'Plaat', 'Materiaal\\Messing\\Plaat', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(84, 81, 'Profiel', 'Materiaal\\Messing\\Profiel', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(85, 81, 'Rond', 'Materiaal\\Messing\\Rond', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(86, 81, 'Strip', 'Materiaal\\Messing\\Strip', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(87, 81, 'Vierkant', 'Materiaal\\Messing\\Vierkant', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(88, 22, 'Noten', 'Materiaal\\Noten', '2021-12-14 11:57:57', '2021-12-14 14:56:23'),
	(89, 88, 'Lat', 'Materiaal\\Noten\\Lat', '2021-12-14 11:57:57', '2021-12-14 14:56:27'),
	(90, 88, 'Plaat', 'Materiaal\\Noten\\Plaat', '2021-12-14 11:57:57', '2021-12-14 14:56:28'),
	(91, 88, 'Profiel', 'Materiaal\\Noten\\Profiel', '2021-12-14 11:57:57', '2021-12-14 14:56:30'),
	(92, 88, 'Rond', 'Materiaal\\Noten\\Rond', '2021-12-14 11:57:57', '2021-12-14 14:56:33'),
	(93, 88, 'Vierkant', 'Materiaal\\Noten\\Vierkant', '2021-12-14 11:57:57', '2021-12-14 14:56:35'),
	(94, 22, 'Vuren', 'Materiaal\\Vuren', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(100, 22, 'Zeilen', 'Materiaal\\Zeilen', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(101, 100, 'Bruin', 'Materiaal\\Zeilen\\Bruin', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(102, 100, 'Creme', 'Materiaal\\Zeilen\\Creme', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(103, 100, 'Wit', 'Materiaal\\Zeilen\\Wit', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(104, 100, 'Zwart', 'Materiaal\\Zeilen\\Zwart', '2021-12-14 11:57:57', '2021-12-14 11:57:57');
/*!40000 ALTER TABLE `category` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `contacttype` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `contacttype`;
/*!40000 ALTER TABLE `contacttype` DISABLE KEYS */;
INSERT INTO `contacttype` (`Id`, `Name`, `Created`, `Modified`) VALUES
	(1, '', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(2, 'Verkoop', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(3, 'Administratie', '2021-12-14 11:57:57', '2021-12-14 11:57:57'),
	(4, 'Algemeen', '2021-12-14 11:57:57', '2021-12-14 11:57:57');
/*!40000 ALTER TABLE `contacttype` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `country` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `DefaultSymbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '€',
  `Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `DefaultId` int DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE KEY `Code_UNIQUE` (`Code`) USING BTREE,
  KEY `FK_Id` (`DefaultId`),
  CONSTRAINT `FK_Id` FOREIGN KEY (`DefaultId`) REFERENCES `currency` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `country`;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` (`Id`, `Code`, `DefaultSymbol`, `Name`, `DefaultId`, `Created`, `Modified`) VALUES
	(1, 'NL', '€', 'Nederland', 1, '2021-12-14 15:07:11', '2021-12-14 15:07:47'),
	(2, 'UK', '£', 'Engeland', 2, '2021-12-14 15:07:11', '2021-12-14 15:07:51'),
	(3, 'US', '$', 'Verenigde staten', 3, '2021-12-14 15:07:11', '2021-12-14 15:07:53'),
	(4, 'DE', '€', 'Duitsland', 1, '2021-12-14 15:07:11', '2021-12-14 15:07:54'),
	(5, 'ESP', '€', 'Spanje', 1, '2021-12-14 15:07:11', '2021-12-14 15:07:55'),
	(6, 'CH', 'Y', 'China', 4, '2021-12-14 15:07:11', '2021-12-14 15:07:57'),
	(7, 'IT', '€', 'Italë', 1, '2021-12-14 15:07:11', '2021-12-14 15:07:58');
/*!40000 ALTER TABLE `country` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `currency` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Symbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConversionRate` double(6,4) DEFAULT '1.0000',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE KEY `Code` (`Code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `currency`;
/*!40000 ALTER TABLE `currency` DISABLE KEYS */;
INSERT INTO `currency` (`Id`, `Code`, `Symbol`, `Name`, `ConversionRate`, `Created`, `Modified`) VALUES
	(1, 'EUR', '€', 'Euro', 1.0000, '2021-12-14 15:07:05', '2021-12-14 15:07:05'),
	(2, 'GBP', '£', 'Britse pond', 1.1079, '2021-12-14 15:07:05', '2021-12-14 15:07:05'),
	(3, 'USD', '$', 'Amerikaanse dollar', 0.8442, '2021-12-14 15:07:05', '2021-12-14 15:07:05'),
	(4, 'YEN', 'Y', 'Yen', 3.3400, '2021-12-14 15:07:05', '2021-12-14 15:07:05'),
	(5, 'TST', 'T', 'Testje', 1.2300, '2021-12-14 15:07:05', '2021-12-14 15:07:05');
/*!40000 ALTER TABLE `currency` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `language` (
  `language_Id` int NOT NULL AUTO_INCREMENT,
  `language_Code` varchar(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `language_Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`language_Id`) USING BTREE,
  UNIQUE KEY `language_code` (`language_Code`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `language`;
/*!40000 ALTER TABLE `language` DISABLE KEYS */;
/*!40000 ALTER TABLE `language` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `product` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Dimensions` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `Price` double(10,2) DEFAULT '0.00',
  `MinimalStock` double(6,2) DEFAULT '0.00',
  `StandardOrderQuantity` double(6,2) DEFAULT '0.00',
  `ProjectCosts` int DEFAULT '0',
  `Id` int DEFAULT NULL,
  `ImageRotationAngle` varchar(4) DEFAULT '0',
  `Image` longblob,
  `Id` int DEFAULT NULL,
  `Id` int DEFAULT NULL,
  `Id` int DEFAULT NULL,
  `Memo` longtext,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Code` (`Code`) /*!80000 INVISIBLE */,
  KEY `Id` (`Id`),
  KEY `Id` (`Id`) /*!80000 INVISIBLE */,
  KEY `Id` (`Id`) /*!80000 INVISIBLE */,
  KEY `Id` (`Id`),
  CONSTRAINT `FK_Id` FOREIGN KEY (`Id`) REFERENCES `brand` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Id` FOREIGN KEY (`Id`) REFERENCES `category` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Id` FOREIGN KEY (`Id`) REFERENCES `storage` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Id` FOREIGN KEY (`Id`) REFERENCES `unit` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `product`;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
/*!40000 ALTER TABLE `product` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `productsupplier` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Id` int NOT NULL DEFAULT '0',
  `Id` int NOT NULL DEFAULT '0',
  `Id` int NOT NULL DEFAULT '0',
  `ProductNumber` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Price` double NOT NULL DEFAULT '0',
  `Default` varchar(1) DEFAULT '',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `Id` (`Id`) /*!80000 INVISIBLE */,
  KEY `Id` (`Id`),
  KEY `Id` (`Id`),
  CONSTRAINT `FK_ProdId` FOREIGN KEY (`Id`) REFERENCES `currency` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_ProdId` FOREIGN KEY (`Id`) REFERENCES `product` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_ProdId` FOREIGN KEY (`Id`) REFERENCES `supplier` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List for all products per supplier';

DELETE FROM `productsupplier`;
/*!40000 ALTER TABLE `productsupplier` DISABLE KEYS */;
/*!40000 ALTER TABLE `productsupplier` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `project` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  `ExpectedTime` int DEFAULT NULL,
  `Image` longblob,
  `ImageRotationAngle` varchar(4) DEFAULT '0',
  `Memo` longtext,
  `Closed` tinyint DEFAULT '0',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE KEY `Code` (`Code`) /*!80000 INVISIBLE */,
  KEY `Name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `project`;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
/*!40000 ALTER TABLE `project` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `storage` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ParentId` int DEFAULT NULL,
  `FullPath` varchar(400) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `storage`;
/*!40000 ALTER TABLE `storage` DISABLE KEYS */;
/*!40000 ALTER TABLE `storage` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `supplier` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Address1` varchar(150) DEFAULT NULL,
  `Address2` varchar(150) DEFAULT NULL,
  `Zip` varchar(15) DEFAULT NULL,
  `City` varchar(40) DEFAULT NULL,
  `Url` varchar(255) DEFAULT NULL,
  `ShippingCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `MinShippingCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `OrderCosts` double(6,2) NOT NULL DEFAULT '0.00',
  `Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Id` int NOT NULL DEFAULT '1',
  `Id` int NOT NULL DEFAULT '1',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Code` (`Code`) /*!80000 INVISIBLE */,
  KEY `Name` (`Name`) /*!80000 INVISIBLE */,
  KEY `Id` (`Id`) /*!80000 INVISIBLE */,
  KEY `Id` (`Id`),
  CONSTRAINT `FK_Id` FOREIGN KEY (`Id`) REFERENCES `country` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Id` FOREIGN KEY (`Id`) REFERENCES `currency` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `supplier`;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `suppliercontact` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Id` int DEFAULT '0',
  `Name` varchar(150) DEFAULT '',
  `Id` int DEFAULT '1',
  `Mail` varchar(150) DEFAULT '',
  `Phone` varchar(150) DEFAULT '',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `Id` (`Id`),
  KEY `Id` (`Id`) /*!80000 INVISIBLE */,
  KEY `Name` (`Name`),
  CONSTRAINT `FK_SupContact_Id` FOREIGN KEY (`Id`) REFERENCES `contacttype` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_SupContact_Id` FOREIGN KEY (`Id`) REFERENCES `supplier` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `suppliercontact`;
/*!40000 ALTER TABLE `suppliercontact` DISABLE KEYS */;
/*!40000 ALTER TABLE `suppliercontact` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `supplyorder` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Id` int DEFAULT '0',
  `Id` int DEFAULT '0',
  `OrderNumber` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `OrderDate` date DEFAULT NULL,
  `CurrencySymbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '€',
  `CurrencyConversionRate` double(6,4) DEFAULT '0.0000',
  `ShippingCosts` double(10,2) DEFAULT '0.00',
  `OrderCosts` double(10,2) DEFAULT '0.00',
  `Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Closed` tinyint DEFAULT '0',
  `ClosedDate` date DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `OrderNumber` (`OrderNumber`),
  KEY `FK_Order_Id` (`Id`),
  KEY `FK_Order_Id` (`Id`),
  CONSTRAINT `FK_Order_Id` FOREIGN KEY (`Id`) REFERENCES `currency` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Order_Id` FOREIGN KEY (`Id`) REFERENCES `supplier` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

DELETE FROM `supplyorder`;
/*!40000 ALTER TABLE `supplyorder` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplyorder` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `supplyorderline` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Id` int NOT NULL DEFAULT '0',
  `Id` int NOT NULL DEFAULT '0',
  `Id` int DEFAULT '0',
  `SupplierProductName` varchar(150) DEFAULT '',
  `Id` int DEFAULT '0',
  `Id` int DEFAULT '0',
  `Amount` double(6,2) DEFAULT '0.00',
  `Price` double(6,2) DEFAULT '0.00',
  `RealRowTotal` double(6,2) DEFAULT '0.00',
  `Closed` tinyint DEFAULT '0',
  `ClosedDate` date DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `FK_OrderRow_Id` (`Id`),
  KEY `FK_OrderRow_Id` (`Id`),
  KEY `FK_OrderRow_Id` (`Id`),
  KEY `FK_OrderRow_Id` (`Id`),
  KEY `FK_OrderRow_Id` (`Id`),
  CONSTRAINT `FK_OrderRow_Id` FOREIGN KEY (`Id`) REFERENCES `category` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_OrderRow_Id` FOREIGN KEY (`Id`) REFERENCES `product` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_OrderRow_Id` FOREIGN KEY (`Id`) REFERENCES `project` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_OrderRow_Id` FOREIGN KEY (`Id`) REFERENCES `supplier` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_OrderRow_Id` FOREIGN KEY (`Id`) REFERENCES `supplyorder` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

DELETE FROM `supplyorderline`;
/*!40000 ALTER TABLE `supplyorderline` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplyorderline` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `unit` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modifief` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `unit`;
/*!40000 ALTER TABLE `unit` DISABLE KEYS */;
/*!40000 ALTER TABLE `unit` ENABLE KEYS */;

CREATE TABLE `view_prodbrandcatstorage` (
	`ID` INT(10) NOT NULL,
	`Product` VARCHAR(150) NULL COLLATE 'utf8mb4_0900_ai_ci',
	`Brand` VARCHAR(100) NOT NULL COLLATE 'utf8mb4_0900_ai_ci',
	`Category` VARCHAR(55) NULL COLLATE 'utf8mb4_0900_ai_ci',
	`Supplier` VARCHAR(150) NULL COLLATE 'utf8mb4_0900_ai_ci'
) ENGINE=MyISAM;

CREATE TABLE `view_product` (
	`Product` VARCHAR(150) NULL COLLATE 'utf8mb4_0900_ai_ci',
	`Price` DOUBLE(10,2) NULL,
	`Supplier product` VARCHAR(150) NULL COLLATE 'utf8mb4_0900_ai_ci',
	`Supplier price` DOUBLE NOT NULL
) ENGINE=MyISAM;

CREATE TABLE IF NOT EXISTS `worktype` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ParentId` int DEFAULT NULL,
  `Name` char(150) DEFAULT NULL,
  `FullPath` char(255) DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `worktype`;
/*!40000 ALTER TABLE `worktype` DISABLE KEYS */;
/*!40000 ALTER TABLE `worktype` ENABLE KEYS */;

DROP TABLE IF EXISTS `view_prodbrandcatstorage`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_prodbrandcatstorage` AS select `p`.`Id` AS `ID`,`p`.`Name` AS `Product`,`b`.`Name` AS `Brand`,`c`.`Name` AS `Category`,`s`.`Name` AS `Supplier` from (((`product` `p` join `brand` `b` on((`p`.`Id` = `b`.`Id`))) join `category` `c` on((`p`.`Id` = `c`.`Id`))) join `storage` `s` on((`p`.`Id` = `s`.`Id`))) order by `p`.`Name`;

DROP TABLE IF EXISTS `view_product`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_product` AS select `p`.`Name` AS `Product`,`p`.`Price` AS `Price`,`s`.`Name` AS `Supplier product`,`s`.`Price` AS `Supplier price` from (`product` `p` join `productsupplier` `s` on((`p`.`Id` = `s`.`Id`))) order by `p`.`Name`;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
