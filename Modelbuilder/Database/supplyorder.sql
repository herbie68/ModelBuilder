DROP TABLE IF EXISTS `supplyorder`;
CREATE TABLE IF NOT EXISTS `supplyorder` (
  `order_Id` int NOT NULL AUTO_INCREMENT,
  `order_OrderNumber` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `order_SupplierId` int DEFAULT '0',
  `order_Date` date DEFAULT NULL,
  `order_CurrencySymbol` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '€',
  `order_CurrencyConversionRate` double(6,4) DEFAULT '0.0000',
  `order_ShippingCosts` double(10,2) DEFAULT '0.00',
  `order_OrderCosts` double(10,2) DEFAULT '0.00',
  `order_Memo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `order_Closed` tinyint DEFAULT '0',
  `order_ClosedDate` date DEFAULT NULL,
  PRIMARY KEY (`order_Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ROW_FORMAT=DYNAMIC;

