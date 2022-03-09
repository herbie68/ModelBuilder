/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `time`;
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Store the working time as entered in the Time Management page';

DELETE FROM `time`;
/*!40000 ALTER TABLE `time` DISABLE KEYS */;
INSERT INTO `time` (`Id`, `project_Id`, `worktype_Id`, `WorkDate`, `StartTime`, `EndTime`, `Comment`, `Created`, `Modified`) VALUES
	(2, 5, 11, '2022-02-01', '10:00:00', '11:00:00', '', '2022-02-22 14:57:23', '2022-02-22 14:57:23'),
	(3, 5, 15, '2022-02-01', '11:00:00', '12:15:00', 'Afwerking', '2022-02-23 11:44:30', '2022-02-23 11:44:30');
/*!40000 ALTER TABLE `time` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
