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

-- Structuur van  tabel modelbuilder.suppliercontact wordt geschreven
DROP TABLE IF EXISTS `suppliercontact`;
CREATE TABLE IF NOT EXISTS `suppliercontact` (
  `suppliercontact_Id` int NOT NULL AUTO_INCREMENT,
  `suppliercontact_SupplierId` int DEFAULT '0',
  `suppliercontact_TypeId` int DEFAULT '0',
  `suppliercontact_Name` varchar(150) DEFAULT '',
  `suppliercontact_TypeName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '',
  `suppliercontact_Mail` varchar(150) DEFAULT '',
  `suppliercontact_Phone` varchar(150) DEFAULT '',
  PRIMARY KEY (`suppliercontact_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.suppliercontact: ~5 rows (ongeveer)
DELETE FROM `suppliercontact`;
/*!40000 ALTER TABLE `suppliercontact` DISABLE KEYS */;
INSERT INTO `suppliercontact` (`suppliercontact_Id`, `suppliercontact_SupplierId`, `suppliercontact_TypeId`, `suppliercontact_Name`, `suppliercontact_TypeName`, `suppliercontact_Mail`, `suppliercontact_Phone`) VALUES
	(1, 1, 4, NULL, 'Algemeen', 'sales@cornwallmodelboats.co.uk', '01840 211009'),
	(3, 5, 4, NULL, 'Algemeen', 'info@modelbouw-dordrecht.nl', '078-6312711'),
	(4, 6, 4, 'Ron', 'Algemeen', 'ron@krikke.net', '050-3140306'),
	(5, 7, 4, NULL, 'Algemeen', 'info@hobby-en-modelbouw.nl', '0294-266587'),
	(6, 8, 4, NULL, 'Algemeen', 'info@meijerenblessing.nl', '010-4145591');
/*!40000 ALTER TABLE `suppliercontact` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
