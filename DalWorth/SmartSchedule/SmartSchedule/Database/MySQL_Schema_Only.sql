-- MySQL dump 10.11
--
-- Host: localhost    Database: smartschedule_dbo
-- ------------------------------------------------------
-- Server version	5.1.51-community

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `smartschedule_dbo`
--

USE `smartschedule_dbo`;

--
-- Table structure for table `applicationsetting`
--

DROP TABLE IF EXISTS `applicationsetting`;
CREATE TABLE `applicationsetting` (
  `ImportDate` datetime NOT NULL,
  `Note` varchar(50) NOT NULL,
  PRIMARY KEY (`ImportDate`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `area`
--

DROP TABLE IF EXISTS `area`;
CREATE TABLE `area` (
  `ID` int(10) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `bucketconstraint`
--

DROP TABLE IF EXISTS `bucketconstraint`;
CREATE TABLE `bucketconstraint` (
  `AreaId` int(10) NOT NULL,
  `TimeFrameId` int(10) NOT NULL,
  `MaxBucketCapacity` int(10) NOT NULL,
  PRIMARY KEY (`AreaId`,`TimeFrameId`),
  KEY `FK_BucketConstraint_TimeFrame` (`TimeFrameId`),
  CONSTRAINT `FK_BucketConstraint_Area` FOREIGN KEY (`AreaId`) REFERENCES `area` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_BucketConstraint_TimeFrame` FOREIGN KEY (`TimeFrameId`) REFERENCES `timeframe` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `changerecord`
--

DROP TABLE IF EXISTS `changerecord`;
CREATE TABLE `changerecord` (
  `ID` int(10) NOT NULL,
  `DashboardDate` datetime NOT NULL,
  `DateCreated` datetime NOT NULL,
  `ChangeText` varchar(500) NOT NULL,
  `IsAllPreviousChangesOptimized` tinyint(1) NOT NULL,
  PRIMARY KEY (`ID`,`DashboardDate`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `company`
--

DROP TABLE IF EXISTS `company`;
CREATE TABLE `company` (
  `ID` int(10) NOT NULL,
  `Name` varchar(40) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `orderhistory`
--

DROP TABLE IF EXISTS `orderhistory`;
CREATE TABLE `orderhistory` (
  `TicketNumber` varchar(10) NOT NULL,
  `CustomerName` varchar(100) DEFAULT NULL,
  `Street` varchar(200) NOT NULL,
  `City` varchar(50) NOT NULL,
  `State` varchar(2) NOT NULL,
  `Zip` varchar(10) NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `DateTimeCall` datetime NOT NULL,
  `DateSchedule` datetime NOT NULL,
  `TimeFrameId` int(10) DEFAULT NULL,
  `Cost` decimal(19,4) NOT NULL,
  `ExclusiveCompanyId` int(10) DEFAULT NULL,
  PRIMARY KEY (`TicketNumber`),
  KEY `FK_OrderHistory_ZipCode` (`Zip`),
  KEY `FK_OrderHistory_TimeFrame` (`TimeFrameId`),
  KEY `FK_OrderHistory_Company` (`ExclusiveCompanyId`),
  CONSTRAINT `FK_OrderHistory_ZipCode` FOREIGN KEY (`Zip`) REFERENCES `zipcode` (`Zip`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrderHistory_TimeFrame` FOREIGN KEY (`TimeFrameId`) REFERENCES `timeframe` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrderHistory_Company` FOREIGN KEY (`ExclusiveCompanyId`) REFERENCES `company` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `predictionignore`
--

DROP TABLE IF EXISTS `predictionignore`;
CREATE TABLE `predictionignore` (
  `IgnoreDate` datetime NOT NULL,
  PRIMARY KEY (`IgnoreDate`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `service`
--

DROP TABLE IF EXISTS `service`;
CREATE TABLE `service` (
  `ID` int(10) NOT NULL,
  `Name` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `sysversion`
--

DROP TABLE IF EXISTS `sysversion`;
CREATE TABLE `sysversion` (
  `Version` int(10) NOT NULL,
  PRIMARY KEY (`Version`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `technician`
--

DROP TABLE IF EXISTS `technician`;
CREATE TABLE `technician` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `TechnicianDefaultId` int(10) NOT NULL,
  `ScheduleDate` datetime NOT NULL,
  `ServmanId` varchar(10) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `HourlyRate` decimal(19,4) NOT NULL,
  `HourlyRate150to300` decimal(19,4) NOT NULL,
  `HourlyRateMore300` decimal(19,4) NOT NULL,
  `DisplaySequence` int(10) NOT NULL,
  `CompanyId` int(10) NOT NULL,
  `DepotAddress` varchar(200) NOT NULL,
  `DepotLatitude` double NOT NULL,
  `DepotLongitude` double NOT NULL,
  `DriveTimeMinutes` int(10) NOT NULL,
  `IsContractor` tinyint(1) NOT NULL,
  `MaxVisitsCount` int(10) NOT NULL,
  `MaxNonExclusiveVisitsCount` int(10) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Technician_Company` (`CompanyId`),
  KEY `FK_Technician_TechnicianDefault` (`TechnicianDefaultId`),
  CONSTRAINT `FK_Technician_Company` FOREIGN KEY (`CompanyId`) REFERENCES `company` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Technician_TechnicianDefault` FOREIGN KEY (`TechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `techniciandefault`
--

DROP TABLE IF EXISTS `techniciandefault`;
CREATE TABLE `techniciandefault` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `ServmanId` varchar(10) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `HourlyRate` decimal(19,4) NOT NULL,
  `HourlyRate150to300` decimal(19,4) NOT NULL,
  `HourlyRateMore300` decimal(19,4) NOT NULL,
  `DisplaySequence` int(10) NOT NULL,
  `CompanyId` int(10) NOT NULL,
  `DepotAddress` varchar(200) NOT NULL,
  `DepotLatitude` double NOT NULL,
  `DepotLongitude` double NOT NULL,
  `DriveTimeMinutes` int(10) NOT NULL,
  `IsContractor` tinyint(1) NOT NULL,
  `MaxVisitsCount` int(10) NOT NULL,
  `MaxNonExclusiveVisitsCount` int(10) NOT NULL,
  `Email` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_TechnicianDefault_Company` (`CompanyId`),
  CONSTRAINT `FK_TechnicianDefault_Company` FOREIGN KEY (`CompanyId`) REFERENCES `company` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=143 DEFAULT CHARSET=latin1;

--
-- Table structure for table `technicianservicedeny`
--

DROP TABLE IF EXISTS `technicianservicedeny`;
CREATE TABLE `technicianservicedeny` (
  `TechnicianId` int(10) NOT NULL,
  `ServiceId` int(10) NOT NULL,
  `IsForNonExclusive` tinyint(1) NOT NULL,
  PRIMARY KEY (`TechnicianId`,`ServiceId`),
  KEY `FK_TechnicianServiceDeny_Service` (`ServiceId`),
  CONSTRAINT `FK_TechnicianServiceDeny_Technician` FOREIGN KEY (`TechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TechnicianServiceDeny_Service` FOREIGN KEY (`ServiceId`) REFERENCES `service` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `technicianservicedenydefault`
--

DROP TABLE IF EXISTS `technicianservicedenydefault`;
CREATE TABLE `technicianservicedenydefault` (
  `TechnicianId` int(10) NOT NULL,
  `ServiceId` int(10) NOT NULL,
  `IsForNonExclusive` tinyint(1) NOT NULL,
  PRIMARY KEY (`TechnicianId`,`ServiceId`),
  KEY `FK_TechnicianServiceDenyDefault_Service` (`ServiceId`),
  CONSTRAINT `FK_TechnicianServiceDenyDefault_Service` FOREIGN KEY (`ServiceId`) REFERENCES `service` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TechnicianServiceDenyDefault_TechnicianDefault` FOREIGN KEY (`TechnicianId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `technicianworktime`
--

DROP TABLE IF EXISTS `technicianworktime`;
CREATE TABLE `technicianworktime` (
  `TechnicianId` int(10) NOT NULL,
  `TimeStart` datetime NOT NULL,
  `TimeEnd` datetime NOT NULL,
  PRIMARY KEY (`TechnicianId`,`TimeStart`),
  CONSTRAINT `FK_TechnicianWorkTime_Technician` FOREIGN KEY (`TechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `technicianworktimedefault`
--

DROP TABLE IF EXISTS `technicianworktimedefault`;
CREATE TABLE `technicianworktimedefault` (
  `TechnicianId` int(10) NOT NULL,
  `TimeStart` datetime NOT NULL,
  `TimeEnd` datetime NOT NULL,
  PRIMARY KEY (`TechnicianId`,`TimeStart`),
  CONSTRAINT `FK_TechnicianWorkTimeDefault_TechnicianDefault` FOREIGN KEY (`TechnicianId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `technicianworktimedefaultpreset`
--

DROP TABLE IF EXISTS `technicianworktimedefaultpreset`;
CREATE TABLE `technicianworktimedefaultpreset` (
  `TechnicianId` int(10) NOT NULL,
  `PresetNumber` int(10) NOT NULL,
  `TimeStart` datetime DEFAULT NULL,
  `TimeEnd` datetime DEFAULT NULL,
  PRIMARY KEY (`TechnicianId`,`PresetNumber`),
  CONSTRAINT `FK_TechnicianWorkTimeDefaultPreset_TechnicianDefault` FOREIGN KEY (`TechnicianId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `technicianzip`
--

DROP TABLE IF EXISTS `technicianzip`;
CREATE TABLE `technicianzip` (
  `TechnicianId` int(10) NOT NULL,
  `Zip` varchar(10) NOT NULL,
  `IsPrimaryZip` tinyint(1) NOT NULL,
  PRIMARY KEY (`TechnicianId`,`Zip`),
  KEY `FK_TechnicianZip_ZipCode` (`Zip`),
  CONSTRAINT `FK_TechnicianZip_Technician` FOREIGN KEY (`TechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TechnicianZip_ZipCode` FOREIGN KEY (`Zip`) REFERENCES `zipcode` (`Zip`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `technicianzipdefault`
--

DROP TABLE IF EXISTS `technicianzipdefault`;
CREATE TABLE `technicianzipdefault` (
  `TechnicianId` int(10) NOT NULL,
  `Zip` varchar(10) NOT NULL,
  `IsPrimaryZip` tinyint(1) NOT NULL,
  PRIMARY KEY (`TechnicianId`,`Zip`),
  KEY `FK_TechnicianZipDefault_ZipCode` (`Zip`),
  CONSTRAINT `FK_TechnicianZipDefault_ZipCode` FOREIGN KEY (`Zip`) REFERENCES `zipcode` (`Zip`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TechnicianZipDefault_TechnicianDefault` FOREIGN KEY (`TechnicianId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `timeframe`
--

DROP TABLE IF EXISTS `timeframe`;
CREATE TABLE `timeframe` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `TimeStart` datetime NOT NULL,
  `TimeEnd` datetime NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `UserRoleId` int(10) NOT NULL,
  `Login` varchar(100) NOT NULL,
  `Password` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_User_UserRole` (`UserRoleId`),
  CONSTRAINT `FK_User_UserRole` FOREIGN KEY (`UserRoleId`) REFERENCES `userrole` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `useraction`
--

DROP TABLE IF EXISTS `useraction`;
CREATE TABLE `useraction` (
  `ID` bigint(19) NOT NULL AUTO_INCREMENT,
  `UserId` int(10) NOT NULL,
  `UserActionTypeId` int(10) NOT NULL,
  `TechnicianDefaultId` int(10) DEFAULT NULL,
  `TicketNumber` varchar(50) NOT NULL,
  `DashboardDate` datetime DEFAULT NULL,
  `ActionDate` datetime NOT NULL,
  `Text` varchar(500) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_UserAction_User` (`UserId`),
  KEY `FK_UserAction_TechnicianDefault` (`TechnicianDefaultId`),
  KEY `FK_UserAction_UserActionType` (`UserActionTypeId`),
  CONSTRAINT `FK_UserAction_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_UserAction_TechnicianDefault` FOREIGN KEY (`TechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_UserAction_UserActionType` FOREIGN KEY (`UserActionTypeId`) REFERENCES `useractiontype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `useractiontype`
--

DROP TABLE IF EXISTS `useractiontype`;
CREATE TABLE `useractiontype` (
  `ID` int(10) NOT NULL,
  `ActionType` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `userrole`
--

DROP TABLE IF EXISTS `userrole`;
CREATE TABLE `userrole` (
  `ID` int(10) NOT NULL,
  `Role` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `visit`
--

DROP TABLE IF EXISTS `visit`;
CREATE TABLE `visit` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `TechnicianId` int(10) DEFAULT NULL,
  `TimeFrameId` int(10) NOT NULL,
  `TimeStart` datetime NOT NULL,
  `TimeEnd` datetime NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `Zip` varchar(5) NOT NULL,
  `Cost` decimal(19,4) NOT NULL,
  `ExclusiveCompanyId` int(10) DEFAULT NULL,
  `ExclusiveTechnicianDefaultId` int(10) DEFAULT NULL,
  `IsTempIgnoreExclusivity` tinyint(1) NOT NULL,
  `TempExclusiveTechnicianId` int(10) DEFAULT NULL,
  `ForbiddenTechnicianDefaultId` int(10) DEFAULT NULL,
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
  `CallDateTime` datetime DEFAULT NULL,
  `Mapsco` varchar(10) NOT NULL,
  `IsCalledCustomer` tinyint(1) NOT NULL,
  `IsFixed` tinyint(1) NOT NULL,
  `Area` varchar(5) NOT NULL,
  `ServType` int(10) NOT NULL,
  `AdsourceAcronym` varchar(6) NOT NULL,
  `CustomerRank` int(10) NOT NULL,
  `OriginatedTechnicianDefaultId` int(10) DEFAULT NULL,
  `OriginatedCompleteDate` datetime DEFAULT NULL,
  `OriginatedTicketNumber` varchar(10) NOT NULL,
  `CustomerExclusiveTechnicianDefaultId` int(10) DEFAULT NULL,
  `Note` varchar(2000) NOT NULL,
  `ExpCred` tinyint(1) NOT NULL,
  `SpecName` varchar(30) NOT NULL,
  `ServmanBaseTimeFrameId` int(10) DEFAULT NULL,
  `SdPercent` decimal(19,4) DEFAULT NULL,
  `TaxPercent` decimal(19,4) DEFAULT NULL,
  `DurationCost` decimal(19,4) NOT NULL,
  `SnapshotDate` datetime DEFAULT NULL,
  `IsBlockout` tinyint(1) NOT NULL,
  `IsTechnicianEmailSent` tinyint(1) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Visit_Technician` (`TechnicianId`),
  KEY `FK_Visit_Technician2` (`TempExclusiveTechnicianId`),
  KEY `FK_Visit_TimeFrame` (`TimeFrameId`),
  KEY `FK_Visit_Company` (`ExclusiveCompanyId`),
  KEY `FK_Visit_TimeFrame1` (`ServmanBaseTimeFrameId`),
  KEY `FK_Visit_TechnicianDefault` (`ExclusiveTechnicianDefaultId`),
  KEY `FK_Visit_TechnicianDefault1` (`ForbiddenTechnicianDefaultId`),
  KEY `FK_Visit_TechnicianDefault2` (`OriginatedTechnicianDefaultId`),
  KEY `FK_Visit_TechnicianDefault3` (`CustomerExclusiveTechnicianDefaultId`),
  CONSTRAINT `FK_Visit_Technician` FOREIGN KEY (`TechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Technician2` FOREIGN KEY (`TempExclusiveTechnicianId`) REFERENCES `technician` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TimeFrame` FOREIGN KEY (`TimeFrameId`) REFERENCES `timeframe` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Company` FOREIGN KEY (`ExclusiveCompanyId`) REFERENCES `company` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TimeFrame1` FOREIGN KEY (`ServmanBaseTimeFrameId`) REFERENCES `timeframe` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TechnicianDefault` FOREIGN KEY (`ExclusiveTechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TechnicianDefault1` FOREIGN KEY (`ForbiddenTechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TechnicianDefault2` FOREIGN KEY (`OriginatedTechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_TechnicianDefault3` FOREIGN KEY (`CustomerExclusiveTechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `visitdetail`
--

DROP TABLE IF EXISTS `visitdetail`;
CREATE TABLE `visitdetail` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `VisitId` int(10) NOT NULL,
  `ServiceId` int(10) NOT NULL,
  `ItemSequence` int(10) NOT NULL,
  `Note` varchar(40) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_VisitDetail_Service` (`ServiceId`),
  KEY `FK_VisitDetail_Visit` (`VisitId`),
  CONSTRAINT `FK_VisitDetail_Service` FOREIGN KEY (`ServiceId`) REFERENCES `service` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_VisitDetail_Visit` FOREIGN KEY (`VisitId`) REFERENCES `visit` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `zipcode`
--

DROP TABLE IF EXISTS `zipcode`;
CREATE TABLE `zipcode` (
  `Zip` varchar(10) NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `AreaId` int(10) NOT NULL,
  PRIMARY KEY (`Zip`),
  KEY `FK_ZipCode_Area` (`AreaId`),
  CONSTRAINT `FK_ZipCode_Area` FOREIGN KEY (`AreaId`) REFERENCES `area` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2010-12-03 15:49:20
