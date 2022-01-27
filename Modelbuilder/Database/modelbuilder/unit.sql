/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE TABLE IF NOT EXISTS `unit` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `Created` datetime DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `unit_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `unit`;
/*!40000 ALTER TABLE `unit` DISABLE KEYS */;
INSERT INTO `unit` (`Id`, `Name`, `Created`, `Modified`) VALUES
	(1, '', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(2, 'cm', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(3, 'dl', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(4, 'Fles', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(5, 'gr', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(6, 'kg', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(7, 'ltr', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(8, 'mgr', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(9, 'ml', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(10, 'mm', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(11, 'mtr', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(12, 'Set', '2022-01-11 12:10:54', '2022-01-11 12:10:54'),
	(13, 'Stuk', '2022-01-11 12:10:54', '2022-01-11 12:10:54');
/*!40000 ALTER TABLE `unit` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
