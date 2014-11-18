ALTER TABLE `Technician` DROP COLUMN `MinRevenuePerMile`;
ALTER TABLE `TechnicianDefault` DROP COLUMN `MinRevenuePerMile`;
ALTER TABLE `ChangeRecord` ADD COLUMN `DashboardDate` datetime NOT NULL AFTER `ID`;

delete from TechnicianServiceDeny;
delete from TechnicianWorkTime;
delete from TechnicianZip;
delete from VisitDetail;
delete from Visit;
delete from Technician;

ALTER TABLE `Technician` ADD COLUMN `TechnicianDefaultId` int(10) NOT NULL AFTER `ID`,
 ADD KEY `FK_Technician_TechnicianDefault` (`TechnicianDefaultId`),
 ADD CONSTRAINT `FK_Technician_TechnicianDefault` FOREIGN KEY (`TechnicianDefaultId`)
	REFERENCES `TechnicianDefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `Technician` ADD COLUMN `ScheduleDate` datetime NOT NULL AFTER `TechnicianDefaultId`;
ALTER TABLE `Technician` MODIFY COLUMN `ID` int(10) NOT NULL auto_increment;

drop table VisitDetail;
drop table Visit;


CREATE TABLE `visit` (
  `ID` int(10) NOT NULL auto_increment,
  `TechnicianId` int(10) default NULL,
  `TimeFrameId` int(10) NOT NULL,
  `TimeStart` datetime NOT NULL,
  `TimeEnd` datetime NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `Zip` varchar(5) NOT NULL,
  `Cost` decimal(19,4) NOT NULL,
  `ExclusiveCompanyId` int(10) default NULL,
  `ExclusiveTechnicianDefaultId` int(10) default NULL,
  `IsTempIgnoreExclusivity` tinyint(1) NOT NULL,
  `TempExclusiveTechnicianId` int(10) default NULL,
  `ForbiddenTechnicianDefaultId` int(10) default NULL,
  `TicketNumber` varchar(10) NOT NULL,
  `CustomerName` varchar(100) NOT NULL,
  `Street` varchar(200) NOT NULL,
  `Address2` varchar(50) NOT NULL,
  `City` varchar(50) NOT NULL,
  `State` varchar(2) NOT NULL,
  `HomePhone` varchar(10) NOT NULL,
  `BusinessPhone` varchar(14) NOT NULL,
  `IsEstimate` tinyint(1) NOT NULL,
  `IsEstimateAndDo` tinyint(1) NOT NULL,
  `IsRework` tinyint(1) NOT NULL,
  `CallDateTime` datetime default NULL,
  `Mapsco` varchar(10) NOT NULL,
  `IsCalledCustomer` tinyint(1) NOT NULL,
  `IsFixed` tinyint(1) NOT NULL,
  `Area` varchar(5) NOT NULL,
  `ServType` int(10) NOT NULL,
  `AdsourceAcronym` varchar(6) NOT NULL,
  `CustomerRank` int(10) NOT NULL,
  `OriginatedTechnicianDefaultId` int(10) default NULL,
  `OriginatedCompleteDate` datetime default NULL,
  `OriginatedTicketNumber` varchar(10) NOT NULL,
  `CustomerExclusiveTechnicianDefaultId` int(10) default NULL,
  `Note` varchar(2000) NOT NULL,
  `ExpCred` tinyint(1) NOT NULL,
  `SpecName` varchar(30) NOT NULL,
  `ServmanBaseTimeFrameId` int(10) default NULL,
  `SdPercent` decimal(19,4) default NULL,
  `TaxPercent` decimal(19,4) default NULL,
  `DurationCost` decimal(19,4) NOT NULL,
  `SnapshotDate` datetime default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Visit_Technician` (`TechnicianId`),
  KEY `FK_Visit_Technician2` (`TempExclusiveTechnicianId`),
  KEY `FK_Visit_TechnicianDefault` (`ExclusiveTechnicianDefaultId`),
  KEY `FK_Visit_TechnicianDefault1` (`ForbiddenTechnicianDefaultId`),
  KEY `FK_Visit_TechnicianDefault2` (`OriginatedTechnicianDefaultId`),
  KEY `FK_Visit_TechnicianDefault3` (`CustomerExclusiveTechnicianDefaultId`),
  KEY `FK_Visit_TimeFrame` (`TimeFrameId`),
  KEY `FK_Visit_Company` (`ExclusiveCompanyId`),
  KEY `FK_Visit_TimeFrame1` (`ServmanBaseTimeFrameId`),
  CONSTRAINT `FK_Visit_Technician` FOREIGN KEY (`TechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Technician2` FOREIGN KEY (`TempExclusiveTechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TechnicianDefault` FOREIGN KEY (`ExclusiveTechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TechnicianDefault1` FOREIGN KEY (`ForbiddenTechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TechnicianDefault2` FOREIGN KEY (`OriginatedTechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TechnicianDefault3` FOREIGN KEY (`CustomerExclusiveTechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TimeFrame` FOREIGN KEY (`TimeFrameId`) REFERENCES `timeframe` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Company` FOREIGN KEY (`ExclusiveCompanyId`) REFERENCES `company` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TimeFrame1` FOREIGN KEY (`ServmanBaseTimeFrameId`) REFERENCES `timeframe` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


CREATE TABLE `visitdetail` (
  `ID` int(10) NOT NULL auto_increment,
  `VisitId` int(10) NOT NULL,
  `ServiceId` int(10) NOT NULL,
  `ItemSequence` int(10) NOT NULL,
  `Note` varchar(40) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_VisitDetail_Service` (`ServiceId`),
  KEY `FK_VisitDetail_Visit` (`VisitId`),
  CONSTRAINT `FK_VisitDetail_Service` FOREIGN KEY (`ServiceId`) REFERENCES `service` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_VisitDetail_Visit` FOREIGN KEY (`VisitId`) REFERENCES `visit` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE `Technician` DROP COLUMN `IsOff`;
ALTER TABLE `Technician` DROP COLUMN `IsVisible`;
ALTER TABLE `TechnicianDefault` DROP COLUMN `IsOff`;
ALTER TABLE `TechnicianDefault` DROP COLUMN `IsVisible`;


DROP TABLE IF EXISTS `changerecord`;
CREATE TABLE `changerecord` (
  `ID` int(10) NOT NULL,
  `DashboardDate` datetime NOT NULL,
  `DateCreated` datetime NOT NULL,
  `ChangeText` varchar(500) NOT NULL,
  `IsAllPreviousChangesOptimized` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ID`,`DashboardDate`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



INSERT INTO `sysversion` (`Version`) VALUES 
(10);