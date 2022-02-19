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

-- Structuur van  tabel modelbuilder.time wordt geschreven
CREATE TABLE IF NOT EXISTS `time` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `project_Id` int NOT NULL DEFAULT '0',
  `worktype_Id` int NOT NULL DEFAULT '0',
  `WorkDate` date NOT NULL,
  `StartTime` time DEFAULT NULL,
  `EndTime` time DEFAULT NULL,
  `Comment` varchar(1050) NOT NULL DEFAULT '',
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `FK_TimeProject_Id` (`project_Id`),
  KEY `FK_TimeWorktype_Id` (`worktype_Id`),
  CONSTRAINT `FK_TimeProject_Id` FOREIGN KEY (`project_Id`) REFERENCES `project` (`Id`),
  CONSTRAINT `FK_TimeWorktype_Id` FOREIGN KEY (`worktype_Id`) REFERENCES `worktype` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Store the working time as entered in the Time Management page';

-- Dumpen data van tabel modelbuilder.time: ~10 rows (ongeveer)
DELETE FROM `time`;
/*!40000 ALTER TABLE `time` DISABLE KEYS */;
INSERT INTO `time` (`Id`, `project_Id`, `worktype_Id`, `WorkDate`, `StartTime`, `EndTime`, `Comment`, `Created`, `Modified`) VALUES
	(2, 5, 25, '2022-02-14', '09:00:00', '11:30:00', '', '2022-02-14 13:10:33', '2022-02-14 13:10:33'),
	(3, 1, 15, '2022-02-02', '09:10:00', '11:35:00', 'Test', '2022-02-14 16:09:05', '2022-02-15 14:04:59'),
	(4, 1, 15, '2022-02-02', '12:10:00', '15:25:00', '', '2022-02-14 16:10:01', '2022-02-14 16:10:01'),
	(13, 2, 11, '2022-02-01', '10:10:00', '11:50:00', '', '2022-02-16 10:34:58', '2022-02-16 10:34:58'),
	(14, 2, 11, '2022-02-01', '12:10:00', '13:20:00', '', '2022-02-16 10:37:06', '2022-02-16 10:37:06'),
	(15, 2, 15, '2022-02-01', '13:30:00', '14:00:00', '', '2022-02-16 10:40:45', '2022-02-16 10:53:56'),
	(16, 2, 12, '2022-02-01', '14:00:00', '14:35:00', '', '2022-02-16 10:52:37', '2022-02-16 10:52:37'),
	(17, 2, 12, '2022-02-01', '14:35:00', '15:00:00', '', '2022-02-16 11:59:54', '2022-02-16 11:59:54'),
	(18, 2, 13, '2022-02-01', '15:00:00', '15:15:00', '', '2022-02-16 12:04:17', '2022-02-16 12:04:17'),
	(19, 2, 11, '2022-02-01', '15:15:00', '15:30:00', '', '2022-02-16 12:07:13', '2022-02-16 12:07:13');
/*!40000 ALTER TABLE `time` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
