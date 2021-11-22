CREATE TABLE IF NOT EXISTS `orderline` (
  `orderline_Id` int NOT NULL AUTO_INCREMENT,
  `orderline_OrderId` int NOT NULL DEFAULT '0',
  `orderline_ProductId` int DEFAULT '0',
  `orderline_ProjectId` int DEFAULT '0',
  `orderline_CategoryId` int DEFAULT '0',
  `orderline_Number` double(6,2) DEFAULT '0.00',
  `orderline_Price` double(6,2) DEFAULT '0.00',
  `orderline_RealRowTotal` double(6,2) DEFAULT '0.00',
  `orderline_Closed` tinyint DEFAULT '0',
  `orderline_ClosedDate` date DEFAULT NULL,
  PRIMARY KEY (`orderline_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumpen data van tabel modelbuilder.orderline: ~0 rows (ongeveer)
DELETE FROM `orderline`;
