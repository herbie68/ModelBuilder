/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP VIEW IF EXISTS `view_productusage`;
DROP TABLE IF EXISTS `view_productusage`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_productusage` AS select `pu`.`Id` AS `Id`,`pu`.`project_Id` AS `ProjectId`,`pu`.`product_Id` AS `ProductId`,`pd`.`Storage_Id` AS `StorageId`,`pd`.`Category_Id` AS `CategoryId`,`pu`.`UsageDate` AS `UsageDate`,`pj`.`Name` AS `ProjectName`,`pd`.`Name` AS `ProductName`,`s`.`Name` AS `StorageName`,`c`.`Name` AS `CategoryName`,`pu`.`AmountUsed` AS `AmountUsed`,`pu`.`Comment` AS `Comment` from ((((`productusage` `pu` join `project` `pj` on((`pu`.`project_Id` = `pj`.`Id`))) join `product` `pd` on((`pu`.`project_Id` = `pd`.`Id`))) join `category` `c` on((`pd`.`Category_Id` = `c`.`Id`))) join `storage` `s` on((`pd`.`Storage_Id` = `s`.`Id`)));

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
