/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP VIEW IF EXISTS `view_time`;
DROP TABLE IF EXISTS `view_time`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_time` AS select `t`.`Id` AS `Id`,`t`.`project_Id` AS `ProjectId`,`p`.`Name` AS `ProjectName`,`t`.`worktype_Id` AS `WorktypeId`,`w`.`Name` AS `WorktypeName`,`t`.`Date` AS `Date`,`t`.`StartTime` AS `StartTime`,`t`.`EndTime` AS `EndTime`,(`t`.`EndTime` - `t`.`StartTime`) AS `ElapsedTime` from ((`time` `t` join `project` `p` on((`t`.`project_Id` = `p`.`Id`))) join `worktype` `w` on((`t`.`worktype_Id` = `w`.`Id`)));

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;