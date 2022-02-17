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

-- Structuur van  tabel modelbuilder.time_copy wordt geschreven
CREATE TABLE IF NOT EXISTS `time_copy` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `project_Id` int NOT NULL DEFAULT '0',
  `worktype_Id` int NOT NULL DEFAULT '0',
  `Date` date NOT NULL,
  `StartHour` int NOT NULL DEFAULT '0',
  `StartMinute` int NOT NULL DEFAULT '0',
  `EndHour` int NOT NULL DEFAULT '0',
  `EndMinute` int NOT NULL DEFAULT '0',
  `Comment` varchar(1050) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `FK_TimeProject_Id` (`project_Id`) USING BTREE,
  KEY `FK_TimeWorktype_Id` (`worktype_Id`) USING BTREE,
  CONSTRAINT `time_copy_ibfk_1` FOREIGN KEY (`project_Id`) REFERENCES `project` (`Id`),
  CONSTRAINT `time_copy_ibfk_2` FOREIGN KEY (`worktype_Id`) REFERENCES `worktype` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Store the working time as entered in the Time Management page';

-- Dumpen data van tabel modelbuilder.time_copy: ~7 rows (ongeveer)
DELETE FROM `time_copy`;
/*!40000 ALTER TABLE `time_copy` DISABLE KEYS */;
INSERT INTO `time_copy` (`Id`, `project_Id`, `worktype_Id`, `Date`, `StartHour`, `StartMinute`, `EndHour`, `EndMinute`, `Comment`, `Created`, `Modified`) VALUES
	(1, 5, 8, '2022-02-09', 10, 15, 11, 25, '', '2022-02-09 16:45:27', '2022-02-09 16:45:27'),
	(2, 5, 8, '2022-02-09', 11, 30, 11, 45, '', '2022-02-09 17:02:32', '2022-02-09 17:02:32'),
	(3, 4, 25, '2022-02-10', 10, 0, 12, 30, '', '2022-02-10 08:22:17', '2022-02-10 08:22:17'),
	(4, 4, 19, '2022-02-10', 13, 15, 17, 30, '', '2022-02-10 08:23:07', '2022-02-10 08:23:08'),
	(5, 5, 9, '2022-02-10', 10, 0, 15, 30, '', '2022-02-10 08:23:45', '2022-02-10 08:23:45'),
	(6, 5, 9, '2022-02-10', 8, 5, 9, 5, '', '2022-02-10 09:07:15', '2022-02-10 09:07:16'),
	(7, 4, 19, '2022-02-10', 8, 0, 9, 5, '', '2022-02-10 09:07:46', '2022-02-10 09:07:46');
/*!40000 ALTER TABLE `time_copy` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
