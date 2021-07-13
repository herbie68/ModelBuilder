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
DROP DATABASE IF EXISTS `modelbuilder`;
CREATE DATABASE IF NOT EXISTS `modelbuilder` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `modelbuilder`;

-- Structuur van  tabel modelbuilder.storage wordt geschreven
DROP TABLE IF EXISTS `storage`;
CREATE TABLE IF NOT EXISTS `storage` (
  `storage_Id` int NOT NULL AUTO_INCREMENT,
  `storage_ParentId` int DEFAULT NULL,
  `storage_Parent` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `storage_Code` varchar(20) NOT NULL,
  `storage_Name` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `storage_Level` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`storage_Id`),
  UNIQUE KEY `storage_Code` (`storage_Code`)
) ENGINE=InnoDB AUTO_INCREMENT=259 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.storage: ~105 rows (ongeveer)
DELETE FROM `storage`;
/*!40000 ALTER TABLE `storage` DISABLE KEYS */;
INSERT INTO `storage` (`storage_Id`, `storage_ParentId`, `storage_Parent`, `storage_Code`, `storage_Name`, `storage_Level`) VALUES
	(1, NULL, NULL, 'WERF', 'Herberts Werf', 0),
	(2, 1, 'WERF/KAST', 'KAST', 'Hoge kast', 1),
	(3, 2, 'WERF/KAST/PLNKN', 'PLNKN', 'Hoge kast  - Planken', 2),
	(4, 3, 'WERF/KAST/PLNKN/KP0', 'KP0', 'Hoge kast  - Planken: op de kast', 3),
	(5, 3, 'WERF/KAST/PLNKN/PLNK1', 'PLNK1', 'Hoge kast  - Planken: bovenste plank', 3),
	(6, 3, 'WERF/KAST/PLNKN/PLNK2', 'PLNK2', 'Hoge kast  - Planken: 2e plank', 3),
	(7, 3, 'WERF/KAST/PLNKN/PLNK3', 'PLNK3', 'Hoge kast  - Planken: 3e plank', 3),
	(8, 3, 'WERF/KAST/PLNKN/PLNK4', 'PLNK4', 'Hoge kast  - Planken: 4e plank', 3),
	(9, 3, 'WERF/KAST/PLNKN/PLNK5', 'PLNK5', 'Hoge kast  - Planken: onderste plank', 3),
	(10, 2, 'WERF/KAST/ZIJKANT', 'ZIJKANT', 'Hoge kast  - Zijkant', 2),
	(11, 10, 'WERF/KAST/ZIJKANT/ZIJLAT0', 'ZIJLAT0', 'Hoge kast  - Zijkant: zijkant bovenste klemmenlat', 3),
	(12, 10, 'WERF/KAST/ZIJKANT/ZIJLAT1', 'ZIJLAT1', 'Hoge kast  - Zijkant: zijkant 2e klemmenlat', 3),
	(13, 10, 'WERF/KAST/ZIJKANT/ZIJLAT2', 'ZIJLAT2', 'Hoge kast  - Zijkant: zijkant 3e klemmenlat', 3),
	(14, 10, 'WERF/KAST/ZIJKANT/ZIJLAT3', 'ZIJLAT3', 'Hoge kast  - Zijkant: zijkant 4e klemmenlat', 3),
	(15, 10, 'WERF/KAST/ZIJKANT/ZIJLAT4', 'ZIJLAT4', 'Hoge kast  - Zijkant: onderste klemmenlat', 3),
	(16, 1, 'WERF/LK', 'LK', 'Ladenkast', 1),
	(23, 1, 'WERF/P1', 'P1', 'Onderste muurplank  - Op de plank', 1),
	(24, 23, 'WERF/P1/BAK01', 'BAK01', 'Onderste muurplank  - Garenbak 01 (rood) ', 2),
	(43, 42, 'WERF/P1/BAK02', 'BAK02', 'Onderste muurplank  - Garenbak 02 (blauw) ', 2),
	(60, 59, 'WERF/P1/BAK03', 'BAK03', 'Onderste muurplank  - Bak 03 (turquoise)', 2),
	(85, 84, 'WERF/P1/BAK04', 'BAK04', 'Onderste muurplank  - Bak 04', 2),
	(126, 125, 'WERF/P1/BAK05', 'BAK05', 'Onderste muurplank  - Bak 05', 2),
	(151, 150, 'WERF/P1/BAK06', 'BAK06', 'Onderste muurplank  - Bak 06', 2),
	(157, 151, 'WERF/P1/BAK06/R1V06', 'R1V06', 'Onderste muurplank  - Bak 06: rij 1 vak 06', 3),
	(158, 151, 'WERF/P1/BAK06/R1V07', 'R1V07', 'Onderste muurplank  - Bak 06: rij 1 vak 07', 3),
	(159, 151, 'WERF/P1/BAK06/R1V08', 'R1V08', 'Onderste muurplank  - Bak 06: rij 1 vak 08', 3),
	(160, 151, 'WERF/P1/BAK06/R1V09', 'R1V09', 'Onderste muurplank  - Bak 06: rij 1 vak 09', 3),
	(161, 151, 'WERF/P1/BAK06/R1V10', 'R1V10', 'Onderste muurplank  - Bak 06: rij 1 vak 10', 3),
	(170, 151, 'WERF/P1/BAK06/R2V09', 'R2V09', 'Onderste muurplank  - Bak 06: rij 2 vak 09', 3),
	(171, 151, 'WERF/P1/BAK06/R2V10', 'R2V10', 'Onderste muurplank  - Bak 06: rij 2 vak 10', 3),
	(180, 151, 'WERF/P1/BAK06/R3V09', 'R3V09', 'Onderste muurplank  - Bak 06: rij 3 vak 09', 3),
	(181, 151, 'WERF/P1/BAK06/R3V10', 'R3V10', 'Onderste muurplank  - Bak 06: rij 3 vak 10', 3),
	(186, 151, 'WERF/P1/BAK06/R4V05', 'R4V05', 'Onderste muurplank  - Bak 06: rij 4 vak 05', 3),
	(187, 151, 'WERF/P1/BAK06/R4V06', 'R4V06', 'Onderste muurplank  - Bak 06: rij 4 vak 06', 3),
	(188, 151, 'WERF/P1/BAK06/R4V07', 'R4V07', 'Onderste muurplank  - Bak 06: rij 4 vak 07', 3),
	(189, 151, 'WERF/P1/BAK06/R4V08', 'R4V08', 'Onderste muurplank  - Bak 06: rij 4 vak 08', 3),
	(190, 151, 'WERF/P1/BAK06/R4V09', 'R4V09', 'Onderste muurplank  - Bak 06: rij 4 vak 09', 3),
	(191, 151, 'WERF/P1/BAK06/R4V10', 'R4V10', 'Onderste muurplank  - Bak 06: rij 4 vak 10', 3),
	(192, 191, 'WERF/P1/BAK07', 'BAK07', 'Onderste muurplank  - Draadbak 07', 2),
	(193, 192, 'WERF/P1/BAK07/R1V01', 'R1V01', 'Onderste muurplank  - Draadbak 07: rij 1 vak 01', 3),
	(194, 192, 'WERF/P1/BAK07/R1V02', 'R1V02', 'Onderste muurplank  - Draadbak 07: rij 1 vak 02', 3),
	(195, 192, 'WERF/P1/BAK07/R1V03', 'R1V03', 'Onderste muurplank  - Draadbak 07: rij 1 vak 03', 3),
	(196, 192, 'WERF/P1/BAK07/R1V04', 'R1V04', 'Onderste muurplank  - Draadbak 07: rij 1 vak 04', 3),
	(197, 192, 'WERF/P1/BAK07/R1V05', 'R1V05', 'Onderste muurplank  - Draadbak 07: rij 1 vak 05', 3),
	(198, 192, 'WERF/P1/BAK07/R2V01', 'R2V01', 'Onderste muurplank  - Draadbak 07: rij 2 vak 01', 3),
	(199, 192, 'WERF/P1/BAK07/R2V02', 'R2V02', 'Onderste muurplank  - Draadbak 07: rij 2 vak 02', 3),
	(200, 192, 'WERF/P1/BAK07/R2V03', 'R2V03', 'Onderste muurplank  - Draadbak 07: rij 2 vak 03', 3),
	(201, 192, 'WERF/P1/BAK07/R2V04', 'R2V04', 'Onderste muurplank  - Draadbak 07: rij 2 vak 04', 3),
	(202, 192, 'WERF/P1/BAK07/R2V05', 'R2V05', 'Onderste muurplank  - Draadbak 07: rij 2 vak 05', 3),
	(203, 192, 'WERF/P1/BAK07/R2V06', 'R2V06', 'Onderste muurplank  - Draadbak 07: rij 2 vak 06', 3),
	(204, 192, 'WERF/P1/BAK07/R2V07', 'R2V07', 'Onderste muurplank  - Draadbak 07: rij 2 vak 07', 3),
	(205, 192, 'WERF/P1/BAK07/R2V08', 'R2V08', 'Onderste muurplank  - Draadbak 07: rij 2 vak 08', 3),
	(206, 192, 'WERF/P1/BAK07/R3V01', 'R3V01', 'Onderste muurplank  - Draadbak 07: rij 3 vak 01', 3),
	(207, 192, 'WERF/P1/BAK07/R3V02', 'R3V02', 'Onderste muurplank  - Draadbak 07: rij 3 vak 02', 3),
	(208, 192, 'WERF/P1/BAK07/R3V03', 'R3V03', 'Onderste muurplank  - Draadbak 07: rij 3 vak 03', 3),
	(209, 192, 'WERF/P1/BAK07/R3V04', 'R3V04', 'Onderste muurplank  - Draadbak 07: rij 3 vak 04', 3),
	(210, 192, 'WERF/P1/BAK07/R3V05', 'R3V05', 'Onderste muurplank  - Draadbak 07: rij 3 vak 05', 3),
	(211, 192, 'WERF/P1/BAK07/R3V06', 'R3V06', 'Onderste muurplank  - Draadbak 07: rij 3 vak 06', 3),
	(212, 192, 'WERF/P1/BAK07/R3V07', 'R3V07', 'Onderste muurplank  - Draadbak 07: rij 3 vak 07', 3),
	(213, 192, 'WERF/P1/BAK07/R3V08', 'R3V08', 'Onderste muurplank  - Draadbak 07: rij 3 vak 08', 3),
	(214, 192, 'WERF/P1/BAK07/R4V01', 'R4V01', 'Onderste muurplank  - Draadbak 07: rij 4 vak 01', 3),
	(215, 192, 'WERF/P1/BAK07/R4V02', 'R4V02', 'Onderste muurplank  - Draadbak 07: rij 4 vak 02', 3),
	(216, 192, 'WERF/P1/BAK07/R4V03', 'R4V03', 'Onderste muurplank  - Draadbak 07: rij 4 vak 03', 3),
	(217, 192, 'WERF/P1/BAK07/R4V04', 'R4V04', 'Onderste muurplank  - Draadbak 07: rij 4 vak 04', 3),
	(218, 217, 'WERF/P1/LKAST2', 'LKAST2', 'Onderste muurplank  - Ladenkast (5 lades)', 2),
	(219, 218, 'WERF/P1/LKAST2/L01', 'L01', 'Onderste muurplank  - Ladenkast (5 lades): boven links', 3),
	(220, 218, 'WERF/P1/LKAST2/L02', 'L02', 'Onderste muurplank  - Ladenkast (5 lades): boven midden', 3),
	(221, 218, 'WERF/P1/LKAST2/L03', 'L03', 'Onderste muurplank  - Ladenkast (5 lades): boven rechts', 3),
	(222, 218, 'WERF/P1/LKAST2/L04', 'L04', 'Onderste muurplank  - Ladenkast (5 lades): midden', 3),
	(223, 218, 'WERF/P1/LKAST2/L05', 'L05', 'Onderste muurplank  - Ladenkast (5 lades): onder', 3),
	(224, 223, 'WERF/P1/WORKB', 'WORKB', 'Onderste muurplank  - Workbench', 2),
	(225, 225, 'WERF/P1/WORKB/GH', 'GH', 'Onderste muurplank  - Workbench: gereedschaphouder', 3),
	(226, 225, 'WERF/P1/WORKB/LADE0', 'LADE0', 'Onderste muurplank  - Workbench: top (boven lades)', 3),
	(227, 225, 'WERF/P1/WORKB/LADE1', 'LADE1', 'Onderste muurplank  - Workbench: lade 1', 3),
	(228, 225, 'WERF/P1/WORKB/LADE2', 'LADE2', 'Onderste muurplank  - Workbench: lade 2', 3),
	(229, 225, 'WERF/P1/WORKB/LADE3', 'LADE3', 'Onderste muurplank  - Workbench: lade 3', 3),
	(230, 225, 'WERF/P1/WORKB/LADE4', 'LADE4', 'Onderste muurplank  - Workbench: lade 4', 3),
	(231, 225, 'WERF/P1/WORKB/LADE5', 'LADE5', 'Onderste muurplank  - Workbench: lade 5', 3),
	(232, 225, 'WERF/P1/WORKB/LADE6', 'LADE6', 'Onderste muurplank  - Workbench: lade 6', 3),
	(233, 225, 'WERF/P1/WORKB/LADE7', 'LADE7', 'Onderste muurplank  - Workbench: verzamellade', 3),
	(234, 225, 'WERF/P1/WORKB/WERKR', 'WERKR', 'Onderste muurplank  - Workbench: werkruimte', 3),
	(235, 225, 'WERF/P1/WORKB/ZIJVAK1', 'ZIJVAK1', 'Onderste muurplank  - Workbench: voorste zijvak', 3),
	(236, 225, 'WERF/P1/WORKB/ZIJVAK2', 'ZIJVAK2', 'Onderste muurplank  - Workbench: zijvak 2', 3),
	(237, 225, 'WERF/P1/WORKB/ZIJVAK3', 'ZIJVAK3', 'Onderste muurplank  - Workbench: zijvak 3', 3),
	(238, 225, 'WERF/P1/WORKB/ZIJVAK4', 'ZIJVAK4', 'Onderste muurplank  - Workbench: zijvak 4', 3),
	(239, 225, 'WERF/P1/WORKB/ZIJVAK5', 'ZIJVAK5', 'Onderste muurplank  - Workbench: achterste zijvak', 3),
	(240, 1, 'WERF/P2', 'P2', 'Middelste muurplank  - Op de plank', 1),
	(241, 1, 'WERF/P3', 'P3', 'Bovenste muurplank  - Op de plank', 1),
	(242, 1, 'WERF/WB', 'WB', 'Werkbank  - Op de werkbank', 1),
	(243, 243, 'WERF/WB/GH1', 'GH1', 'Werkbank  - Gereedschapshouder (hoofd)', 2),
	(244, 243, 'WERF/WB/GH2', 'GH2', 'Werkbank  - Gereedschaphouder (Sub)', 2),
	(245, 243, 'WERF/WB/LKAST1', 'LKAST1', 'Werkbank  - Ladenkast (9 lades)', 2),
	(246, 246, 'WERF/WB/LKAST1/LADE01', 'LADE01', 'Werkbank  - Ladenkast (9 lades): boven links', 3),
	(247, 246, 'WERF/WB/LKAST1/LADE02', 'LADE02', 'Werkbank  - Ladenkast (9 lades): boven midden', 3),
	(248, 246, 'WERF/WB/LKAST1/LADE03', 'LADE03', 'Werkbank  - Ladenkast (9 lades): boven rechts', 3),
	(249, 246, 'WERF/WB/LKAST1/LADE04', 'LADE04', 'Werkbank  - Ladenkast (9 lades): midden links', 3),
	(250, 246, 'WERF/WB/LKAST1/LADE05', 'LADE05', 'Werkbank  - Ladenkast (9 lades): midden midden', 3),
	(251, 246, 'WERF/WB/LKAST1/LADE06', 'LADE06', 'Werkbank  - Ladenkast (9 lades): midden rechts', 3),
	(252, 246, 'WERF/WB/LKAST1/LADE07', 'LADE07', 'Werkbank  - Ladenkast (9 lades): onder links', 3),
	(253, 246, 'WERF/WB/LKAST1/LADE08', 'LADE08', 'Werkbank  - Ladenkast (9 lades): onder midden', 3),
	(254, 246, 'WERF/WB/LKAST1/LADE09', 'LADE09', 'Werkbank  - Ladenkast (9 lades): onder rechts', 3),
	(255, 243, 'WERF/WB/OND', 'OND', 'Werkbank  - Onder de werkbak', 2),
	(256, 243, 'WERF/WB/TRIB1', 'TRIB1', 'Werkbank  - Linker tribube (verf)', 2),
	(257, 243, 'WERF/WB/TRIB2', 'TRIB2', 'Werkbank  - Middelste tribune', 2),
	(258, 243, 'WERF/WB/TRIB3', 'TRIB3', 'Werkbank  - Rechtertribune', 2);
/*!40000 ALTER TABLE `storage` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
