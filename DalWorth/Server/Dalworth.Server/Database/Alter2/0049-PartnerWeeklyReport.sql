DROP TABLE IF EXISTS `partnersummaryreportitem`;
CREATE TABLE `partnersummaryreportitem` (
  `GenerateDate` datetime NOT NULL,
  `OrderSourceId` int(10) NOT NULL,
  `PhoneNumber` varchar(50) NOT NULL,
  `AdsourceName` varchar(500) NOT NULL,
  `CallCount` int(10) NOT NULL,
  `BookCount` int(10) NOT NULL,
  `NonBookCount` int(10) NOT NULL,
  `NoActionCount` int(10) NOT NULL,
  `CancelCount` int(10) NOT NULL,
  `InProcessCount` int(10) NOT NULL,
  `CompletedCount` int(10) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  `IsSent` tinyint(1) NOT NULL,
  PRIMARY KEY  (`GenerateDate`,`OrderSourceId`,`PhoneNumber`,`AdsourceName`),
  KEY `FK_PartnerSummaryReportItem_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_PartnerSummaryReportItem_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


INSERT INTO `sysversion` (`Version`) VALUES (49);