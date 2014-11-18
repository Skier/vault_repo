DROP TABLE `VisitService`;
DROP TABLE `Visit`;

CREATE TABLE `sysversion` (
  `Version` int(10) NOT NULL,
  PRIMARY KEY  (`Version`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


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
  `ExclusiveTechnicianId` int(10) default NULL,
  `IsTempIgnoreExclusivity` tinyint(1) NOT NULL,
  `TempExclusiveTechnicianId` int(10) default NULL,
  `ForbiddenTechnicianId` int(10) default NULL,
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
  `OriginatedTechnicianId` int(10) default NULL,
  `OriginatedCompleteDate` datetime default NULL,
  `OriginatedTicketNumber` varchar(10) NOT NULL,
  `CustomerExclusiveTechnicianId` int(10) default NULL,
  `Note` varchar(2000) NOT NULL,
  `ExpCred` tinyint(1) NOT NULL,
  `SpecName` varchar(30) NOT NULL,
  `ServmanBaseTimeFrameId` int(10) default NULL,
  `SdPercent` decimal(19,4) default NULL,
  `TaxPercent` decimal(19,4) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Visit_Technician` (`TechnicianId`),
  KEY `FK_Visit_Technician1` (`ExclusiveTechnicianId`),
  KEY `FK_Visit_Technician2` (`ForbiddenTechnicianId`),
  KEY `FK_Visit_TimeFrame` (`TimeFrameId`),
  KEY `FK_Visit_Company` (`ExclusiveCompanyId`),
  KEY `FK_Visit_Technician3` (`OriginatedTechnicianId`),
  KEY `FK_Visit_Technician4` (`CustomerExclusiveTechnicianId`),
  KEY `FK_Visit_TimeFrame1` (`ServmanBaseTimeFrameId`),
  CONSTRAINT `FK_Visit_Technician` FOREIGN KEY (`TechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Technician1` FOREIGN KEY (`ExclusiveTechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Technician2` FOREIGN KEY (`ForbiddenTechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TimeFrame` FOREIGN KEY (`TimeFrameId`) REFERENCES `timeframe` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Company` FOREIGN KEY (`ExclusiveCompanyId`) REFERENCES `company` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Technician3` FOREIGN KEY (`OriginatedTechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Technician4` FOREIGN KEY (`CustomerExclusiveTechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
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
  KEY `FK_VisitService_Visit` (`VisitId`),
  KEY `FK_VisitDetail_Service` (`ServiceId`),
  CONSTRAINT `FK_VisitService_Visit` FOREIGN KEY (`VisitId`) REFERENCES `visit` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_VisitDetail_Service` FOREIGN KEY (`ServiceId`) REFERENCES `service` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



INSERT INTO `sysversion` (`Version`) VALUES 
(1);