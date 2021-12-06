/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `suppliercontact`;
CREATE TABLE IF NOT EXISTS `suppliercontact` (
  `suppliercontact_Id` int NOT NULL AUTO_INCREMENT,
  `suppliercontact_SupplierId` int DEFAULT '0',
  `suppliercontact_Name` varchar(150) DEFAULT '',
  `suppliercontact_TypeId` int DEFAULT '1',
  `suppliercontact_TypeName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `suppliercontact_Mail` varchar(150) DEFAULT '',
  `suppliercontact_Phone` varchar(150) DEFAULT '',
  PRIMARY KEY (`suppliercontact_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `suppliercontact`;
/*!40000 ALTER TABLE `suppliercontact` DISABLE KEYS */;
INSERT INTO `suppliercontact` (`suppliercontact_Id`, `suppliercontact_SupplierId`, `suppliercontact_Name`, `suppliercontact_TypeId`, `suppliercontact_TypeName`, `suppliercontact_Mail`, `suppliercontact_Phone`) VALUES
	(1, 7, NULL, 4, 'Algemeen', 'info@hobby-en-modelbouw.nl', '0294-266587'),
	(3, 5, 'Name5-1', 1, '', NULL, NULL),
	(24, 1, 'test', 3, 'Administratie', NULL, NULL);
/*!40000 ALTER TABLE `suppliercontact` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
