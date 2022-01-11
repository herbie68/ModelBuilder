/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `worktype`;
CREATE TABLE IF NOT EXISTS `worktype` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ParentId` int DEFAULT NULL,
  `Name` char(150) DEFAULT NULL,
  `FullPath` char(255) DEFAULT NULL,
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `worktype`;
/*!40000 ALTER TABLE `worktype` DISABLE KEYS */;
INSERT INTO `worktype` (`Id`, `ParentId`, `Name`, `FullPath`, `Created`, `Modified`) VALUES
	(1, NULL, 'Voorbereiding', 'Voorbereiding', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(2, NULL, 'Opruimen', 'Opruimen', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(3, NULL, 'Romp', 'Romp', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(4, 3, 'Kiel', 'Romp\\Kiel', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(5, 3, 'Spanten', 'Romp\\Spanten', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(6, 3, 'Eerste beplanking', 'Romp\\Eerste beplanking', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(7, 3, 'Tweede beplanking', 'Romp\\Tweede beplanking', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(8, 3, 'Achtersteven', 'Romp\\Achtersteven', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(9, 3, 'Afwerking romp', 'Romp\\Afwerking romp', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(10, 3, 'Schilderen/Lakken', 'Romp\\Schilderen/Lakken', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(11, NULL, 'Dek', 'Dek', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(12, 11, 'Dek beplanking', 'Dek\\Dek beplanking', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(13, 11, 'Dekbalk en reling', 'Dek\\Dekbalk en reling', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(14, 11, 'Schilderen/Lakken', 'Dek\\Schilderen/Lakken', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(15, 11, 'Afwerking dek', 'Dek\\Afwerking dek', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(16, NULL, 'Opbouw', 'Opbouw', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(17, 16, 'Bijboten', 'Opbouw\\Bijboten', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(18, 16, 'Wapens', 'Opbouw\\Wapens', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(19, 16, 'Ankers', 'Opbouw\\Ankers', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(20, 16, 'Heck', 'Opbouw\\Heck', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(21, 16, 'Masten', 'Opbouw\\Masten', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(22, NULL, 'Wand', 'Wand', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(23, 22, 'Staande wand', 'Wand\\Staande wand', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(24, 22, 'Lopend wand', 'Wand\\Lopend wand', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(25, 22, 'Afwerking', 'Wand\\Afwerking', '2021-12-29 08:04:40', '2021-12-29 08:04:40'),
	(26, 21, 'Ra', 'Opbouw\\Masten\\Ra', '2021-12-29 08:30:15', '2021-12-29 10:15:44'),
	(27, 21, 'Kraaiennest', 'Opbouw\\Masten\\Kraaiennest', '2021-12-29 08:30:33', '2021-12-29 08:30:33');
/*!40000 ALTER TABLE `worktype` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
