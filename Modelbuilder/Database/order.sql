CREATE TABLE IF NOT EXISTS `order` (
  `order_Id` int NOT NULL AUTO_INCREMENT,
  `order_SupplierId` int DEFAULT '0',
  `order_Date` date DEFAULT NULL,
  `order_CurrencySymbol` varchar(2) DEFAULT '€',
  `order_CurrencyConversionRate` double(6,4) DEFAULT '1.0000',
  `order_ShippingCosts` double(10,2) DEFAULT '0.00',
  `order_OrderCosts` double(10,2) DEFAULT '0.00',
  `order_Memo` longtext,
  `order_Closed` tinyint DEFAULT '0',
  `order_ClosedDate` date DEFAULT NULL,
  PRIMARY KEY (`order_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELETE FROM `order`;
