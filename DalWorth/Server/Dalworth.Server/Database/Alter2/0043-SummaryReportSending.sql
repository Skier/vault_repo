DROP TABLE IF EXISTS `partnersummaryreportitem`;
CREATE TABLE `partnersummaryreportitem` (
  `GenerateDate` datetime NOT NULL,
  `OrderSourceId` int(10) NOT NULL,
  `PhoneNumber` varchar(50) NOT NULL,
  `CallCount` int(10) NOT NULL,
  `BookCount` int(10) NOT NULL,
  `CancelCount` int(10) NOT NULL,
  `InProcessCount` int(10) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  `IsSent` tinyint(1) NOT NULL,
  PRIMARY KEY (`GenerateDate`,`OrderSourceId`,`PhoneNumber`),
  KEY `FK_PartnerSummaryReportItem_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_PartnerSummaryReportItem_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

insert into BackgroundJobType(id, type, description) 
values (9, 'PartnerSiteSummaryReport', 'Send monthly summary report for each partner');

ALTER TABLE `Order` ADD COLUMN `TransNum` varchar(6) NOT NULL AFTER `ScheduleDate`;
ALTER TABLE `Order` ADD INDEX `IX_TransNum`(`TransNum`);

INSERT INTO `sysversion` (`Version`) VALUES (43);