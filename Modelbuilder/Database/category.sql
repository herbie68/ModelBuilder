-- --------------------------------------------------------
-- Host:                         localhost
-- Server versie:                8.0.25 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Versie:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Databasestructuur van modelbuilder wordt geschreven
CREATE DATABASE IF NOT EXISTS `modelbuilder` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `modelbuilder`;

-- Structuur van  tabel modelbuilder.category wordt geschreven
DROP TABLE IF EXISTS `category`;
CREATE TABLE IF NOT EXISTS `category` (
  `category_Id` int NOT NULL AUTO_INCREMENT,
  `category_Name` varchar(55) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `category_Fullpath` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `category_ParentId` int DEFAULT NULL,
  PRIMARY KEY (`category_Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=777 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

-- Dumpen data van tabel modelbuilder.category: ~167 rows (ongeveer)
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

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
