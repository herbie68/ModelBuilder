-- --------------------------------------------------------
-- Host:                         localhost
-- Server versie:                8.0.25 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Versie:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Databasestructuur van modelbuilder wordt geschreven
CREATE DATABASE IF NOT EXISTS `modelbuilder` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `modelbuilder`;

-- Structuur van  tabel modelbuilder.storage wordt geschreven
DROP TABLE IF EXISTS `storage`;
CREATE TABLE IF NOT EXISTS `storage` (
  `storage_Id` int NOT NULL AUTO_INCREMENT,
  `storage_ParentId` int DEFAULT NULL,
  `storage_FullPath` varchar(400) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `storage_Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`storage_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=265 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.storage: ~105 rows (ongeveer)
DELETE FROM `storage`;
/*!40000 ALTER TABLE `storage` DISABLE KEYS */;
INSERT INTO `storage` (`storage_Id`, `storage_ParentId`, `storage_FullPath`, `storage_Name`) VALUES
	(1, NULL, 'Herberts Werf', 'Herberts Werf'),
	(2, 1, 'Herberts Werf\\Hoge kast', 'Hoge kast'),
	(3, 2, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken', 'Hoge kast  - Planken'),
	(4, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 0e plank', 'Hoge kast  - Planken: 0e plank'),
	(5, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 1e plank', 'Hoge kast  - Planken: 1e plank'),
	(6, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 2e plank', 'Hoge kast  - Planken: 2e plank'),
	(7, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 3e plank', 'Hoge kast  - Planken: 3e plank'),
	(8, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 4e plank', 'Hoge kast  - Planken: 4e plank'),
	(9, 3, 'Herberts Werf\\Hoge kast\\Hoge kast  - Planken\\Hoge kast  - Planken: 5e plank', 'Hoge kast  - Planken: 5e plank'),
	(10, 2, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant', 'Hoge kast  - Zijkant'),
	(11, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 0e klemmenlat', 'Hoge kast  - Zijkant: zijkant 0e klemmenlat'),
	(12, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 1e klemmenlat', 'Hoge kast  - Zijkant: zijkant 1e klemmenlat'),
	(13, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 2e klemmenlat', 'Hoge kast  - Zijkant: zijkant 2e klemmenlat'),
	(14, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 3e klemmenlat', 'Hoge kast  - Zijkant: zijkant 3e klemmenlat'),
	(15, 10, 'Herberts Werf\\Hoge kast\\Hoge kast  - Zijkant\\Hoge kast  - Zijkant: zijkant 4e klemmenlat', 'Hoge kast  - Zijkant: zijkant 4e klemmenlat'),
	(16, 1, 'Herberts Werf\\Ladenkast', 'Ladenkast'),
	(23, 1, 'Herberts Werf\\Onderste muurplank', 'Onderste muurplank'),
	(24, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 01 - Garen (rood) ', 'Onderste muurplank  - Bak 01 - Garen (rood) '),
	(43, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 02 - Garen (blauw) ', 'Onderste muurplank  - Bak 02 - Garen (blauw) '),
	(60, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 03 (turquoise)', 'Onderste muurplank  - Bak 03 (turquoise)'),
	(85, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 04', 'Onderste muurplank  - Bak 04'),
	(126, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 05', 'Onderste muurplank  - Bak 05'),
	(151, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06', 'Onderste muurplank  - Bak 06'),
	(157, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 1 vak 06', 'Onderste muurplank  - Bak 06: rij 1 vak 06'),
	(158, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 1 vak 07', 'Onderste muurplank  - Bak 06: rij 1 vak 07'),
	(159, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06Onderste muurplank  - Bak 06: rij 1 vak 08', 'Onderste muurplank  - Bak 06: rij 1 vak 08'),
	(160, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 1 vak 09', 'Onderste muurplank  - Bak 06: rij 1 vak 09'),
	(161, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 1 vak 10', 'Onderste muurplank  - Bak 06: rij 1 vak 10'),
	(170, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 2 vak 09', 'Onderste muurplank  - Bak 06: rij 2 vak 09'),
	(171, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 2 vak 10', 'Onderste muurplank  - Bak 06: rij 2 vak 10'),
	(180, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 3 vak 09', 'Onderste muurplank  - Bak 06: rij 3 vak 09'),
	(181, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 3 vak 10', 'Onderste muurplank  - Bak 06: rij 3 vak 10'),
	(186, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 05', 'Onderste muurplank  - Bak 06: rij 4 vak 05'),
	(187, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 06', 'Onderste muurplank  - Bak 06: rij 4 vak 06'),
	(188, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 07', 'Onderste muurplank  - Bak 06: rij 4 vak 07'),
	(189, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 08', 'Onderste muurplank  - Bak 06: rij 4 vak 08'),
	(190, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 09', 'Onderste muurplank  - Bak 06: rij 4 vak 09'),
	(191, 151, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 06\\Onderste muurplank  - Bak 06: rij 4 vak 10', 'Onderste muurplank  - Bak 06: rij 4 vak 10'),
	(192, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad', 'Onderste muurplank  - Bak 07 - Draad'),
	(193, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 01', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 01'),
	(194, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 02', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 02'),
	(195, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 03', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 03'),
	(196, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 04', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 04'),
	(197, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 1 vak 05', 'Onderste muurplank  - Bak 07 - Draad: rij 1 vak 05'),
	(198, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 01', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 01'),
	(199, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 02', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 02'),
	(200, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 03', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 03'),
	(201, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 04', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 04'),
	(202, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 05', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 05'),
	(203, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 06', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 06'),
	(204, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 07', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 07'),
	(205, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 2 vak 08', 'Onderste muurplank  - Bak 07 - Draad: rij 2 vak 08'),
	(206, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 01', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 01'),
	(207, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 02', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 02'),
	(208, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 03', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 03'),
	(209, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 04', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 04'),
	(210, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 05', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 05'),
	(211, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 06', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 06'),
	(212, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 07', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 07'),
	(213, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 3 vak 08', 'Onderste muurplank  - Bak 07 - Draad: rij 3 vak 08'),
	(214, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 4 vak 01', 'Onderste muurplank  - Bak 07 - Draad: rij 4 vak 01'),
	(215, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 4 vak 02', 'Onderste muurplank  - Bak 07 - Draad: rij 4 vak 02'),
	(216, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 4 vak 03', 'Onderste muurplank  - Bak 07 - Draad: rij 4 vak 03'),
	(217, 192, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Bak 07 - Draad\\Rij 4 vak 04', 'Onderste muurplank  - Bak 07 - Draad: rij 4 vak 04'),
	(218, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)', 'Onderste muurplank  - Ladenkast (5 lades)'),
	(219, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij boven links', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij boven links'),
	(220, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij boven midden', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij boven midden'),
	(221, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij boven rechts', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij boven rechts'),
	(222, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij midden', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij midden'),
	(223, 218, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Ladenkast (5 lades)\\Onderste muurplank  - Ladenkast (5 lades): Lade rij onder', 'Onderste muurplank  - Ladenkast (5 lades): Lade rij onder'),
	(224, 23, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench', 'Onderste muurplank  - Workbench'),
	(225, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: gereedschaphouder', 'Onderste muurplank  - Workbench: gereedschaphouder'),
	(226, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 0', 'Onderste muurplank  - Workbench: top (boven lades)'),
	(227, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 1', 'Onderste muurplank  - Workbench: lade 1'),
	(228, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 2', 'Onderste muurplank  - Workbench: lade 2'),
	(229, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 3', 'Onderste muurplank  - Workbench: lade 3'),
	(230, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 4', 'Onderste muurplank  - Workbench: lade 4'),
	(231, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 5', 'Onderste muurplank  - Workbench: lade 5'),
	(232, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 6', 'Onderste muurplank  - Workbench: lade 6'),
	(233, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: lade 7', 'Onderste muurplank  - Workbench: verzamellade'),
	(234, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: werkruimte', 'Onderste muurplank  - Workbench: werkruimte'),
	(235, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 1', 'Onderste muurplank  - Workbench: voorste zijvak'),
	(236, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 2', 'Onderste muurplank  - Workbench: zijvak 2'),
	(237, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 3', 'Onderste muurplank  - Workbench: zijvak 3'),
	(238, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 4', 'Onderste muurplank  - Workbench: zijvak 4'),
	(239, 225, 'Herberts Werf\\Onderste muurplank  - Op de plank\\Onderste muurplank  - Workbench\\Onderste muurplank  - Workbench: zijvak 5', 'Onderste muurplank  - Workbench: achterste zijvak'),
	(240, 1, 'Herberts Werf\\Middelste muurplank', 'Middelste muurplank'),
	(241, 1, 'Herberts Werf\\Bovenste muurplank', 'Bovenste muurplank'),
	(242, 1, 'Herberts Werf\\Werkbank', 'Werkbank'),
	(243, 242, 'Herberts Werf\\Werkbank\\Gereedschaphouder 1', 'Werkbank  - Gereedschaphouder 1'),
	(244, 242, 'Herberts Werf\\Werkbank\\Gereedschaphouder 2', 'Werkbank  - Gereedschaphouder 2'),
	(245, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)', 'Werkbank  - Ladenkast (9 lades)'),
	(246, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Lade 1', 'Werkbank  - Ladenkast (9 lades): Lade 1'),
	(247, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 2', 'Werkbank  - Ladenkast (9 lades): boven midden'),
	(248, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 3', 'Werkbank  - Ladenkast (9 lades): boven rechts'),
	(249, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 4', 'Werkbank  - Ladenkast (9 lades): midden links'),
	(250, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 5', 'Werkbank  - Ladenkast (9 lades): midden midden'),
	(251, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 6', 'Werkbank  - Ladenkast (9 lades): midden rechts'),
	(252, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 7', 'Werkbank  - Ladenkast (9 lades): onder links'),
	(253, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 8', 'Werkbank  - Ladenkast (9 lades): onder midden'),
	(254, 245, 'Herberts Werf\\Werkbank\\Werkbank  - Ladenkast (9 lades)\\Werkbank  - Ladenkast (9 lades): Lade 9', 'Werkbank  - Ladenkast (9 lades): onder rechts'),
	(255, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Onder de werkbak', 'Werkbank  - Onder de werkbak'),
	(256, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Tribune 1 (links)', 'Werkbank  - Tribune 1 (links)'),
	(257, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Tribune 2 (midden)', 'Werkbank  - Tribune 2 (lmidden)'),
	(258, 242, 'Herberts Werf\\Werkbank\\Werkbank  - Tribune 3 (rechts)', 'Werkbank  - Tribune 3 (rechts)');
/*!40000 ALTER TABLE `storage` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
