/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `brand`;
CREATE TABLE IF NOT EXISTS `brand` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE KEY `barnd_Name` (`Name`) USING BTREE,
  KEY `brand_Name` (`Name`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='List of brands for tools, kits, suplies and all other stuf';

DELETE FROM `brand`;
/*!40000 ALTER TABLE `brand` DISABLE KEYS */;
INSERT INTO `brand` (`Id`, `Name`, `Created`, `Modified`) VALUES
	(1, 'Proxxon', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(2, 'Dremel', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(3, 'Excel', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(4, 'Amati', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(5, 'Artesania', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(6, 'Mantua', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(7, 'Modelcraft', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(8, 'Occre', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(9, 'Revell', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(10, 'Humbrol', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(11, 'Tamiya', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(12, 'Billing Boats', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(13, 'Model Shipways', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(14, 'Caldercraft', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(15, 'Constructo', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(16, 'Krick', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(17, 'Aeronaut', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(18, 'Corel', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(19, 'Panart', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(20, 'Sergal', '2021-12-10 08:50:37', '2021-12-10 08:50:37'),
	(21, '_Geen_', '2021-12-10 16:16:58', '2021-12-10 16:16:58'),
	(22, 'CAP', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(23, 'CMK Czech', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(24, 'Disar', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(25, 'Dumas', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(26, 'Graupner', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(27, 'Mamoli', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(28, 'Shipyard', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(29, 'UHU', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(30, 'ZAP', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(31, 'Super Glue', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(32, 'Admirality', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(33, 'Aliphatic', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(34, 'Ever Build', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(35, 'Badger', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(36, 'Rotacraft', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(37, 'David', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(38, 'HobbyZone', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(39, 'Vallejo', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(40, 'AMMO Mig', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(41, 'Master Tools', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(42, 'Trumpeter', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(43, 'Faller', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(44, 'HMB', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(45, 'Proedge', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(46, 'Kinzo', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(47, 'Praxis', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(48, 'Gamma', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(49, 'Bosch', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(50, 'Makita', '2021-12-10 16:19:53', '2021-12-10 16:19:53'),
	(51, 'Xuron', '2021-12-10 16:19:53', '2021-12-10 16:19:53');
/*!40000 ALTER TABLE `brand` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
