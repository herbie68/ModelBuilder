/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP VIEW IF EXISTS `view_prodbrandcatstorage`;
DROP TABLE IF EXISTS `view_prodbrandcatstorage`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `view_prodbrandcatstorage` AS select `p`.`Id` AS `ID`,`p`.`Name` AS `Product`,`b`.`Name` AS `Brand`,`c`.`Name` AS `Category`,`s`.`Name` AS `Supplier` from (((`product` `p` join `brand` `b` on((`p`.`Brand_Id` = `b`.`Id`))) join `category` `c` on((`p`.`Category_Id` = `c`.`Id`))) join `storage` `s` on((`p`.`Storage_Id` = `s`.`Id`))) order by `p`.`Name`;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
