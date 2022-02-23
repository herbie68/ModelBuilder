/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP VIEW IF EXISTS `view_productsupplier`;
DROP TABLE IF EXISTS `view_productsupplier`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_productsupplier` AS select `ps`.`Id` AS `Id`,`ps`.`Product_Id` AS `Product_Id`,`p`.`Name` AS `DefProductName`,`ps`.`Supplier_Id` AS `Supplier_Id`,`s`.`Name` AS `SupplierName`,`ps`.`Currency_Id` AS `Currency_Id`,`c`.`Symbol` AS `CurrencySymbol`,`ps`.`ProductNumber` AS `ProductNumber`,`ps`.`ProductName` AS `Name`,format(`ps`.`Price`,2) AS `Price`,`ps`.`DefaultSupplier` AS `DefaultSupplier` from (((`productsupplier` `ps` join `product` `p` on((`ps`.`Product_Id` = `p`.`Id`))) join `supplier` `s` on((`ps`.`Supplier_Id` = `s`.`Id`))) join `currency` `c` on((`ps`.`Currency_Id` = `c`.`Id`)));

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
