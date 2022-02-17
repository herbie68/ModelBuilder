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

-- Structuur van  view modelbuilder.view_time wordt geschreven
-- Tijdelijke tabel wordt verwijderd, en definitieve VIEW wordt aangemaakt.
DROP TABLE IF EXISTS `view_time`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `view_time` AS select `t`.`Id` AS `Id`,`t`.`project_Id` AS `ProjectId`,`p`.`Name` AS `ProjectName`,`t`.`worktype_Id` AS `WorktypeId`,`w`.`Name` AS `WorktypeName`,`t`.`WorkDate` AS `WorkDate`,left(`t`.`StartTime`,(length(`t`.`StartTime`) - 3)) AS `StartTime`,left(`t`.`EndTime`,(length(`t`.`EndTime`) - 3)) AS `EndTime`,(((left(left(`t`.`EndTime`,(length(`t`.`EndTime`) - 3)),2) * 60) + right(left(`t`.`EndTime`,(length(`t`.`EndTime`) - 3)),2)) - ((left(left(`t`.`StartTime`,(length(`t`.`StartTime`) - 3)),2) * 60) + right(left(`t`.`StartTime`,(length(`t`.`StartTime`) - 3)),2))) AS `ElapsedMinutes`,concat(((((left(left(`t`.`EndTime`,(length(`t`.`EndTime`) - 3)),2) * 60) + right(left(`t`.`EndTime`,(length(`t`.`EndTime`) - 3)),2)) - ((left(left(`t`.`StartTime`,(length(`t`.`StartTime`) - 3)),2) * 60) + right(left(`t`.`StartTime`,(length(`t`.`StartTime`) - 3)),2))) DIV 60),':',((((left(left(`t`.`EndTime`,(length(`t`.`EndTime`) - 3)),2) * 60) + right(left(`t`.`EndTime`,(length(`t`.`EndTime`) - 3)),2)) - ((left(left(`t`.`StartTime`,(length(`t`.`StartTime`) - 3)),2) * 60) + right(left(`t`.`StartTime`,(length(`t`.`StartTime`) - 3)),2))) % 60)) AS `ElapsedTime`,`t`.`Comment` AS `Comment` from ((`time` `t` join `project` `p` on((`t`.`project_Id` = `p`.`Id`))) join `worktype` `w` on((`t`.`worktype_Id` = `w`.`Id`)));

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
