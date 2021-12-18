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
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `worktype`;
/*!40000 ALTER TABLE `worktype` DISABLE KEYS */;
INSERT INTO `worktype` (`Id`, `ParentId`, `Name`, `FullPath`) VALUES
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
