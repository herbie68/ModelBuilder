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

-- Structuur van  view modelbuilder.view_productsupplier wordt geschreven
-- Tijdelijke tabel wordt verwijderd, en definitieve VIEW wordt aangemaakt.
DROP TABLE IF EXISTS `view_productsupplier`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `view_productsupplier` AS select `ps`.`Id` AS `Id`,`ps`.`Product_Id` AS `Product_Id`,`p`.`Name` AS `DefProductName`,`ps`.`Supplier_Id` AS `Supplier_Id`,`s`.`Name` AS `SupplierName`,`ps`.`Currency_Id` AS `Currency_Id`,`c`.`Symbol` AS `CurrencySymbol`,`ps`.`ProductNumber` AS `ProductNumber`,`ps`.`ProductName` AS `Name`,format(`ps`.`Price`,2) AS `Price`,`ps`.`DefaultSupplier` AS `DefaultSupplier` from (((`productsupplier` `ps` join `product` `p` on((`ps`.`Product_Id` = `p`.`Id`))) join `supplier` `s` on((`ps`.`Supplier_Id` = `s`.`Id`))) join `currency` `c` on((`ps`.`Currency_Id` = `c`.`Id`)));

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
