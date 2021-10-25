/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE TABLE IF NOT EXISTS `project` (
  `project_Id` int NOT NULL AUTO_INCREMENT,
  `project_Code` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `project_Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `project_StartDate` date DEFAULT NULL,
  `project_EndDate` date DEFAULT NULL,
  `project_ExpectedTime` int DEFAULT NULL,
  `project_Image` longblob,
  `project_Closed` tinyint DEFAULT '0',
  `project_Memo` longtext,
  PRIMARY KEY (`project_Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `project`;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
INSERT INTO `project` (`project_Id`, `project_Code`, `project_Name`, `project_StartDate`, `project_EndDate`, `project_ExpectedTime`, `project_Image`, `project_Closed`, `project_Memo`) VALUES
	(1, 'SILHOUET', 'Silhouet (Groningen 1893)', '2004-09-25', '2004-11-13', 150, NULL, 1, NULL),
	(2, 'YACHTMARY', 'Yacht Mary', '2004-11-14', '2005-06-09', 150, NULL, 1, NULL),
	(3, 'ZUIDERZEEBOTTER', 'Zuiderzee Botter', '2005-06-09', '2005-08-09', 150, NULL, 1, NULL),
	(4, 'PANDORA', 'HMS Pandora', '2006-09-11', '2012-11-01', 500, NULL, 1, NULL),
	(5, 'ENDEAVOUR', 'HMB Endeavour (1768)', '2014-08-26', NULL, 500, NULL, 0, NULL);
/*!40000 ALTER TABLE `project` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
