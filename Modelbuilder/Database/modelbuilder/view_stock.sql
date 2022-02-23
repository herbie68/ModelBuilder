/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP VIEW IF EXISTS `view_stock`;
DROP TABLE IF EXISTS `view_stock`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_stock` AS select `s`.`Id` AS `Id`,`s`.`product_Id` AS `product_Id`,`p`.`Name` AS `ProductName`,`s`.`storage_Id` AS `storage_Id`,`st`.`Name` AS `StorageName`,`s`.`Amount` AS `Amount`,`p`.`Unit_Id` AS `Unit_Id`,`u`.`Name` AS `UnitName` from (((`stock` `s` join `product` `p` on((`s`.`product_Id` = `p`.`Id`))) join `storage` `st` on((`s`.`storage_Id` = `st`.`Id`))) join `unit` `u` on((`p`.`Unit_Id` = `u`.`Id`)));

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
