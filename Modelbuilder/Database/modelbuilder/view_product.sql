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

-- Structuur van  view modelbuilder.view_product wordt geschreven
-- Tijdelijke tabel wordt verwijderd, en definitieve VIEW wordt aangemaakt.
DROP TABLE IF EXISTS `view_product`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `view_product` AS select `p`.`Id` AS `Id`,`p`.`Code` AS `Code`,`p`.`Name` AS `Name`,`p`.`Dimensions` AS `Dimensions`,format(`p`.`Price`,2) AS `FORMAT(p.Price, 2)`,format(`p`.`MinimalStock`,2) AS `FORMAT(p.MinimalStock, 2)`,format(`p`.`StandardOrderQuantity`,2) AS `FORMAT(p.StandardOrderQuantity, 2)`,`p`.`ProjectCosts` AS `ProjectCosts`,`p`.`Unit_Id` AS `Unit_Id`,`u`.`Name` AS `UnitName`,`p`.`ImageRotationAngle` AS `ImageRotationAngle`,`p`.`Image` AS `Image`,`p`.`Brand_Id` AS `Brand_Id`,`b`.`Name` AS `BrandName`,`p`.`Category_Id` AS `Category_Id`,`c`.`Name` AS `CategoryName`,`p`.`Storage_Id` AS `Storage_Id`,`s`.`Name` AS `StorageName`,`p`.`Memo` AS `Memo` from ((((`product` `p` join `brand` `b` on((`p`.`Brand_Id` = `b`.`Id`))) join `category` `c` on((`p`.`Category_Id` = `c`.`Id`))) join `storage` `s` on((`p`.`Storage_Id` = `s`.`Id`))) join `unit` `u` on((`p`.`Unit_Id` = `u`.`Id`)));

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
