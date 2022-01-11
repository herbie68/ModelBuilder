/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP VIEW IF EXISTS `view_product`;
DROP TABLE IF EXISTS `view_product`;
CREATE ALGORITHM=UNDEFINED DEFINER=`herbie68`@`%` SQL SECURITY DEFINER VIEW `view_product` AS select `p`.`Id` AS `Id`,`p`.`Code` AS `Code`,`p`.`Name` AS `Name`,`p`.`Dimensions` AS `Dimensions`,format(`p`.`Price`,2) AS `Price`,format(`p`.`MinimalStock`,2) AS `MinimalStock`,format(`p`.`StandardOrderQuantity`,2) AS `StandardOrderQuantity`,`p`.`ProjectCosts` AS `ProjectCosts`,`p`.`Unit_Id` AS `Unit_Id`,`u`.`Name` AS `UnitName`,`p`.`ImageRotationAngle` AS `ImageRotationAngle`,`p`.`Image` AS `Image`,`p`.`Brand_Id` AS `Brand_Id`,`b`.`Name` AS `BrandName`,`p`.`Category_Id` AS `Category_Id`,`c`.`Name` AS `CategoryName`,`p`.`Storage_Id` AS `Storage_Id`,`s`.`Name` AS `StorageName`,`p`.`Memo` AS `Memo` from ((((`product` `p` join `brand` `b` on((`p`.`Brand_Id` = `b`.`Id`))) join `category` `c` on((`p`.`Category_Id` = `c`.`Id`))) join `storage` `s` on((`p`.`Storage_Id` = `s`.`Id`))) join `unit` `u` on((`p`.`Unit_Id` = `u`.`Id`)));

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
