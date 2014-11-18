-- MySQL dump 10.11
--
-- Host: localhost    Database: dalworth_server_dbo
-- ------------------------------------------------------
-- Server version	5.0.45-community-nt

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
-- Current Database: `dalworth_server_dbo`
--

USE `dalworth_server_dbo`;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
CREATE TABLE `address` (
  `ID` int(10) NOT NULL auto_increment,
  `AreaId` tinyint(3) default NULL,
  `Block` varchar(10) character set utf8 NOT NULL,
  `Prefix` varchar(2) character set utf8 NOT NULL,
  `Street` varchar(30) character set utf8 NOT NULL,
  `Suffix` varchar(5) character set utf8 NOT NULL,
  `Unit` varchar(8) character set utf8 NOT NULL,
  `Address2` varchar(40) character set utf8 NOT NULL,
  `City` varchar(24) character set utf8 NOT NULL,
  `State` varchar(2) character set utf8 NOT NULL,
  `Zip` int(10) default NULL,
  `MapPage` varchar(6) character set utf8 NOT NULL,
  `MapLetter` varchar(3) character set utf8 NOT NULL,
  `Modified` datetime NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `IX_ZipBlockStreet` (`Zip`,`Block`,`Street`),
  KEY `FK_Address_Area` (`AreaId`),
  CONSTRAINT `FK_Address_Area` FOREIGN KEY (`AreaId`) REFERENCES `area` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `advertisingsource`
--

DROP TABLE IF EXISTS `advertisingsource`;
CREATE TABLE `advertisingsource` (
  `ID` int(10) NOT NULL,
  `AreaId` tinyint(3) NOT NULL,
  `Name` varchar(50) character set utf8 NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsTechnicianReference` tinyint(1) NOT NULL,
  `Acronym` varchar(6) character set utf8 NOT NULL,
  `IsRestoration` tinyint(1) NOT NULL,
  `TrackingUrl` varchar(1024) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_AdvertisingSource_Area` (`AreaId`),
  CONSTRAINT `FK_AdvertisingSource_Area` FOREIGN KEY (`AreaId`) REFERENCES `area` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `area`
--

DROP TABLE IF EXISTS `area`;
CREATE TABLE `area` (
  `ID` tinyint(3) NOT NULL auto_increment,
  `ServmanId` varchar(10) character set utf8 NOT NULL,
  `Name` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Table structure for table `backgroundjobpending`
--

DROP TABLE IF EXISTS `backgroundjobpending`;
CREATE TABLE `backgroundjobpending` (
  `ID` int(10) NOT NULL auto_increment,
  `BackgroundJobTypeId` int(10) NOT NULL,
  `LeadId` int(10) default NULL,
  `ProjectId` int(10) default NULL,
  `ProjectFeedbackId` int(10) default NULL,
  `PartnerInvitationKey` varchar(255) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_BackgroundJobPending_ProjectFeedback` (`ProjectFeedbackId`),
  KEY `FK_BackgroundJobPending_PartnerInvitation` (`PartnerInvitationKey`),
  KEY `FK_BackgroundJobPending_BackgroundJobType` (`BackgroundJobTypeId`),
  KEY `FK_BackgroundJobPending_Project` (`ProjectId`),
  KEY `FK_BackgroundJobPending_Lead` (`LeadId`),
  CONSTRAINT `FK_BackgroundJobPending_ProjectFeedback` FOREIGN KEY (`ProjectFeedbackId`) REFERENCES `projectfeedback` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_BackgroundJobPending_PartnerInvitation` FOREIGN KEY (`PartnerInvitationKey`) REFERENCES `partnerinvitation` (`InvitationKey`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_BackgroundJobPending_BackgroundJobType` FOREIGN KEY (`BackgroundJobTypeId`) REFERENCES `backgroundjobtype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_BackgroundJobPending_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_BackgroundJobPending_Lead` FOREIGN KEY (`LeadId`) REFERENCES `lead` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `backgroundjobtype`
--

DROP TABLE IF EXISTS `backgroundjobtype`;
CREATE TABLE `backgroundjobtype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) NOT NULL,
  `Description` varchar(200) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `bankcheckaccounttype`
--

DROP TABLE IF EXISTS `bankcheckaccounttype`;
CREATE TABLE `bankcheckaccounttype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(20) character set utf8 NOT NULL,
  `Description` varchar(50) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `businesspartner`
--

DROP TABLE IF EXISTS `businesspartner`;
CREATE TABLE `businesspartner` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `QbCustomerTypeListId` varchar(50) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_BusinessPartner_QbCustomerType` (`QbCustomerTypeListId`),
  CONSTRAINT `FK_BusinessPartner_QbCustomerType` FOREIGN KEY (`QbCustomerTypeListId`) REFERENCES `qbcustomertype` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Table structure for table `connectionkey`
--

DROP TABLE IF EXISTS `connectionkey`;
CREATE TABLE `connectionkey` (
  `ConnectionKeyValue` varchar(256) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ConnectionKeyValue`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `constructiondamagetype`
--

DROP TABLE IF EXISTS `constructiondamagetype`;
CREATE TABLE `constructiondamagetype` (
  `ID` int(10) NOT NULL,
  `DamageType` varchar(50) NOT NULL,
  `Description` varchar(200) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `counter`
--

DROP TABLE IF EXISTS `counter`;
CREATE TABLE `counter` (
  `CounterName` varchar(40) character set utf8 NOT NULL,
  `Val` int(10) NOT NULL,
  PRIMARY KEY  (`CounterName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `creditcardcvv2type`
--

DROP TABLE IF EXISTS `creditcardcvv2type`;
CREATE TABLE `creditcardcvv2type` (
  `ID` int(10) NOT NULL,
  `Type` varchar(20) character set utf8 default NULL,
  `Description` varchar(100) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `creditcardtype`
--

DROP TABLE IF EXISTS `creditcardtype`;
CREATE TABLE `creditcardtype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
CREATE TABLE `customer` (
  `ID` int(10) NOT NULL auto_increment,
  `ServmanCustId` varchar(6) character set utf8 NOT NULL,
  `CustomerTypeId` tinyint(3) default NULL,
  `AddressId` int(10) default NULL,
  `FirstName` varchar(40) character set utf8 NOT NULL,
  `LastName` varchar(40) character set utf8 NOT NULL,
  `Phone1` varchar(10) character set utf8 NOT NULL,
  `Phone2` varchar(14) character set utf8 NOT NULL,
  `Email` varchar(50) character set utf8 NOT NULL,
  `Modified` datetime NOT NULL,
  `LastSyncDate` datetime default NULL,
  PRIMARY KEY  (`ID`),
  KEY `IX_LastFirstName` (`LastName`,`FirstName`),
  KEY `IX_Phone1` (`Phone1`),
  KEY `IX_Phone2` (`Phone2`),
  KEY `IX_ServmanCustId` (`ServmanCustId`),
  KEY `FK_Customer_Address` (`AddressId`),
  KEY `FK_Customer_CustomerType` (`CustomerTypeId`),
  CONSTRAINT `FK_Customer_Address` FOREIGN KEY (`AddressId`) REFERENCES `address` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Customer_CustomerType` FOREIGN KEY (`CustomerTypeId`) REFERENCES `customertype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `customeraddressadditional`
--

DROP TABLE IF EXISTS `customeraddressadditional`;
CREATE TABLE `customeraddressadditional` (
  `CustomerId` int(10) NOT NULL,
  `AddressId` int(10) NOT NULL,
  PRIMARY KEY  (`CustomerId`,`AddressId`),
  KEY `FK_CustomerAddressAdditional_Address` (`AddressId`),
  CONSTRAINT `FK_CustomerAddressAdditional_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_CustomerAddressAdditional_Address` FOREIGN KEY (`AddressId`) REFERENCES `address` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `customertype`
--

DROP TABLE IF EXISTS `customertype`;
CREATE TABLE `customertype` (
  `ID` tinyint(3) NOT NULL,
  `Type` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `customerwebaccount`
--

DROP TABLE IF EXISTS `customerwebaccount`;
CREATE TABLE `customerwebaccount` (
  `ID` int(10) NOT NULL auto_increment,
  `CustomerId` int(10) default NULL,
  `BusinessPartnerId` int(10) default NULL,
  `Company` varchar(50) NOT NULL,
  `FirstName` varchar(40) NOT NULL,
  `LastName` varchar(40) NOT NULL,
  `Address1` varchar(60) NOT NULL,
  `Address2` varchar(40) NOT NULL,
  `City` varchar(24) NOT NULL,
  `State` varchar(2) NOT NULL,
  `Zip` int(10) default NULL,
  `Phone1` varchar(14) NOT NULL,
  `Phone2` varchar(14) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `LastModifiedDate` datetime NOT NULL,
  `Password` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_CustomerWebAccount_Customer` (`CustomerId`),
  KEY `FK_CustomerWebAccount_BusinessPartner` (`BusinessPartnerId`),
  CONSTRAINT `FK_CustomerWebAccount_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_CustomerWebAccount_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `businesspartner` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `dashboardsharedsetting`
--

DROP TABLE IF EXISTS `dashboardsharedsetting`;
CREATE TABLE `dashboardsharedsetting` (
  `DashboardDate` datetime NOT NULL,
  `TechnicianId` int(10) NOT NULL,
  `UnknownTechnicianId` int(10) NOT NULL,
  `IsVisible` tinyint(1) NOT NULL,
  `Sequence` int(10) NOT NULL,
  PRIMARY KEY  (`DashboardDate`,`TechnicianId`),
  KEY `FK_DashboardSharedSetting_Employee` (`TechnicianId`),
  KEY `FK_DashboardSharedSetting_Employee1` (`UnknownTechnicianId`),
  CONSTRAINT `FK_DashboardSharedSetting_Employee` FOREIGN KEY (`TechnicianId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_DashboardSharedSetting_Employee1` FOREIGN KEY (`UnknownTechnicianId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `dashboardstate`
--

DROP TABLE IF EXISTS `dashboardstate`;
CREATE TABLE `dashboardstate` (
  `ID` int(10) NOT NULL auto_increment,
  `EmployeeId` int(10) NOT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY  (`ID`),
  CONSTRAINT `FK_DashboardState_Employee` FOREIGN KEY (`ID`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `dashboardusersettings`
--

DROP TABLE IF EXISTS `dashboardusersettings`;
CREATE TABLE `dashboardusersettings` (
  `DispatchId` int(10) NOT NULL,
  `TechnicianId` int(10) NOT NULL,
  `IsVisible` tinyint(1) NOT NULL,
  `Sequence` int(10) NOT NULL,
  PRIMARY KEY  (`DispatchId`,`TechnicianId`),
  KEY `FK_DashboardUserSettings_Employee1` (`TechnicianId`),
  CONSTRAINT `FK_DashboardUserSettings_Employee` FOREIGN KEY (`DispatchId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_DashboardUserSettings_Employee1` FOREIGN KEY (`TechnicianId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `deflooddetail`
--

DROP TABLE IF EXISTS `deflooddetail`;
CREATE TABLE `deflooddetail` (
  `DefloodTaskId` int(10) NOT NULL,
  `FloodDate` datetime default NULL,
  `FloodClass` int(10) default NULL,
  `CubicFeet` decimal(18,5) default NULL,
  PRIMARY KEY  (`DefloodTaskId`),
  CONSTRAINT `FK_DefloodDetail_Task` FOREIGN KEY (`DefloodTaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DELIMITER ;;
/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedDefloodDetailUpdate` BEFORE UPDATE ON `deflooddetail` FOR EACH ROW BEGIN
  IF NOT (NEW.FloodDate <=> OLD.FloodDate)
  THEN
    call MarkTaskModified(NEW.DefloodTaskId);
  END IF;
END */;;

DELIMITER ;
/*!50003 SET SESSION SQL_MODE=@OLD_SQL_MODE */;

--
-- Table structure for table `deletedtask`
--

DROP TABLE IF EXISTS `deletedtask`;
CREATE TABLE `deletedtask` (
  `ServmanOrderNum` varchar(6) character set utf8 NOT NULL,
  `LastSyncDate` datetime default NULL,
  PRIMARY KEY  (`ServmanOrderNum`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `digiumlogitem`
--

DROP TABLE IF EXISTS `digiumlogitem`;
CREATE TABLE `digiumlogitem` (
  `ID` bigint(19) NOT NULL auto_increment,
  `CallId` varchar(30) NOT NULL,
  `IsIncoming` tinyint(1) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeCreatedOriginal` datetime NOT NULL,
  `TimeTalkStarted` datetime NOT NULL,
  `TimeTalkStartedOriginal` datetime NOT NULL,
  `DurationSec` int(10) NOT NULL,
  `CallerIdNumber` varchar(15) NOT NULL,
  `CallerName` varchar(200) NOT NULL,
  `Extension` varchar(5) NOT NULL,
  `ExtensionType` varchar(20) NOT NULL,
  `IncomingDid` varchar(15) NOT NULL,
  `IsIntermediateCall` tinyint(1) NOT NULL,
  `CallSourceId` int(10) default NULL,
  `VoiceFileName` varchar(200) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `IX_DigiumCallId` (`CallId`),
  KEY `IX_DigiumLogItem_TimeCreated` (`TimeCreated`),
  KEY `FK_DigiumLogItem_OrderSource` (`CallSourceId`),
  CONSTRAINT `FK_DigiumLogItem_OrderSource` FOREIGN KEY (`CallSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
CREATE TABLE `employee` (
  `ID` int(10) NOT NULL auto_increment,
  `EmployeeTypeId` int(10) NOT NULL,
  `ServmanUserId` varchar(3) character set utf8 default NULL,
  `ServmanTechId` varchar(3) character set utf8 default NULL,
  `AddressId` int(10) default NULL,
  `Login` varchar(50) character set utf8 default NULL,
  `FirstName` varchar(100) character set utf8 default NULL,
  `LastName` varchar(100) character set utf8 default NULL,
  `HireDate` datetime default NULL,
  `Phone1` varchar(50) character set utf8 default NULL,
  `Phone2` varchar(50) character set utf8 default NULL,
  `Password` varchar(100) character set utf8 default NULL,
  `IsActive` tinyint(1) default NULL,
  `IsRestoration` tinyint(1) NOT NULL,
  `IsUnknown` tinyint(1) NOT NULL,
  `DefaultVanId` int(10) default NULL,
  `SecurityRoleId` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  UNIQUE KEY `IX_EmployeeLogin` USING BTREE (`Login`),
  KEY `FK_Technician_Address` (`AddressId`),
  KEY `FK_Employee_EmployeeType` (`EmployeeTypeId`),
  KEY `FK_Employee_Van` (`DefaultVanId`),
  KEY `FK_Employee_SecurityRole` (`SecurityRoleId`),
  CONSTRAINT `FK_Employee_EmployeeType` FOREIGN KEY (`EmployeeTypeId`) REFERENCES `employeetype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Employee_SecurityRole` FOREIGN KEY (`SecurityRoleId`) REFERENCES `securityrole` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Employee_Van` FOREIGN KEY (`DefaultVanId`) REFERENCES `van` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Technician_Address` FOREIGN KEY (`AddressId`) REFERENCES `address` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1571 DEFAULT CHARSET=latin1;

--
-- Table structure for table `employeetype`
--

DROP TABLE IF EXISTS `employeetype`;
CREATE TABLE `employeetype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `equipmenttransaction`
--

DROP TABLE IF EXISTS `equipmenttransaction`;
CREATE TABLE `equipmenttransaction` (
  `ID` int(10) NOT NULL auto_increment,
  `WorkTransactionId` int(10) default NULL,
  `EmployeeId` int(10) NOT NULL,
  `SequenceDate` datetime NOT NULL,
  `TransactionDate` datetime NOT NULL,
  `Notes` varchar(200) character set utf8 NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `IX_SequnceDate` (`SequenceDate`),
  KEY `FK_EquipmentTransaction_Employee` (`EmployeeId`),
  KEY `FK_EquipmentTransaction_WorkTransaction` (`WorkTransactionId`),
  CONSTRAINT `FK_EquipmentTransaction_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransaction_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Table structure for table `equipmenttransactiondetail`
--

DROP TABLE IF EXISTS `equipmenttransactiondetail`;
CREATE TABLE `equipmenttransactiondetail` (
  `ID` int(10) NOT NULL auto_increment,
  `EquipmentTransactionId` int(10) NOT NULL,
  `EquipmentTypeId` int(10) NOT NULL,
  `VanId` int(10) default NULL,
  `AddressId` int(10) default NULL,
  `Quantity` int(10) NOT NULL,
  `QuantityChange` int(10) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_EquipmentTransactionDetail_Address` (`AddressId`),
  KEY `FK_EquipmentTransactionDetail_EquipmentTransaction` (`EquipmentTransactionId`),
  KEY `FK_EquipmentTransactionDetail_Van` (`VanId`),
  KEY `FK_EquipmentTransactionDetail_EquipmentType` (`EquipmentTypeId`),
  CONSTRAINT `FK_EquipmentTransactionDetail_Address` FOREIGN KEY (`AddressId`) REFERENCES `address` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetail_EquipmentTransaction` FOREIGN KEY (`EquipmentTransactionId`) REFERENCES `equipmenttransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetail_Van` FOREIGN KEY (`VanId`) REFERENCES `van` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetail_EquipmentType` FOREIGN KEY (`EquipmentTypeId`) REFERENCES `equipmenttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2938 DEFAULT CHARSET=latin1;

--
-- Table structure for table `equipmenttype`
--

DROP TABLE IF EXISTS `equipmenttype`;
CREATE TABLE `equipmenttype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 default NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `eventlog`
--

DROP TABLE IF EXISTS `eventlog`;
CREATE TABLE `eventlog` (
  `EventLogId` int(10) NOT NULL auto_increment,
  `EventType` int(10) NOT NULL,
  `Message` varchar(2000) character set utf8 NOT NULL,
  `Source` varchar(100) character set utf8 default NULL,
  `AssemblyName` varchar(100) character set utf8 default NULL,
  `CreateDate` datetime NOT NULL,
  PRIMARY KEY  (`EventLogId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `insurancecompany`
--

DROP TABLE IF EXISTS `insurancecompany`;
CREATE TABLE `insurancecompany` (
  `ID` int(10) NOT NULL auto_increment,
  `AddressId` int(10) default NULL,
  `Name` varchar(50) character set utf8 NOT NULL,
  `ContactPerson` varchar(50) character set utf8 NOT NULL,
  `Phone1` varchar(50) character set utf8 NOT NULL,
  `Phone2` varchar(50) character set utf8 NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_InsuranceCompany_Address` (`AddressId`),
  CONSTRAINT `FK_InsuranceCompany_Address` FOREIGN KEY (`AddressId`) REFERENCES `address` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `inventoryroomstatus`
--

DROP TABLE IF EXISTS `inventoryroomstatus`;
CREATE TABLE `inventoryroomstatus` (
  `ID` int(10) NOT NULL,
  `Status` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `item`
--

DROP TABLE IF EXISTS `item`;
CREATE TABLE `item` (
  `ID` int(10) NOT NULL auto_increment,
  `TaskId` int(10) NOT NULL,
  `ItemTypeId` int(10) NOT NULL,
  `SerialNumber` varchar(50) character set utf8 default NULL,
  `ItemShapeId` int(10) default NULL,
  `Width` decimal(18,2) default NULL,
  `Height` decimal(18,2) default NULL,
  `Diameter` decimal(18,2) default NULL,
  `IsProtectorApplied` tinyint(1) default NULL,
  `IsPaddingApplied` tinyint(1) default NULL,
  `IsMothRepelApplied` tinyint(1) default NULL,
  `IsRapApplied` tinyint(1) default NULL,
  `CleanCost` decimal(19,4) default NULL,
  `ProtectorCost` decimal(19,4) default NULL,
  `PaddingCost` decimal(19,4) default NULL,
  `MothRepelCost` decimal(19,4) default NULL,
  `RapCost` decimal(19,4) default NULL,
  `OtherCost` decimal(19,4) default NULL,
  `SubTotalCost` decimal(19,4) default NULL,
  `TaxCost` decimal(19,4) default NULL,
  `TotalCost` decimal(19,4) default NULL,
  `CleaningRate` decimal(19,4) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Item_ItemShape` (`ItemShapeId`),
  KEY `FK_Item_ItemType` (`ItemTypeId`),
  KEY `FK_Item_Task` (`TaskId`),
  CONSTRAINT `FK_Item_ItemShape` FOREIGN KEY (`ItemShapeId`) REFERENCES `itemshape` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Item_ItemType` FOREIGN KEY (`ItemTypeId`) REFERENCES `itemtype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Item_Task` FOREIGN KEY (`TaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DELIMITER ;;
/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedItemUpdate` BEFORE UPDATE ON `item` FOR EACH ROW BEGIN
  DECLARE taskIdVar int;
  IF NOT (NEW.ItemShapeId <=> OLD.ItemShapeId)
     OR NOT (NEW.Width <=> OLD.Width)
     OR NOT (NEW.Height <=> OLD.Height)
     OR NOT (NEW.Diameter <=> OLD.Diameter)
     OR NOT (NEW.SubTotalCost <=> OLD.SubTotalCost)
  THEN
    SELECT TaskId INTO taskIdVar FROM Item WHERE Item.ID = NEW.ID;
    call MarkTaskModified(taskIdVar);
  END IF;
END */;;

DELIMITER ;
/*!50003 SET SESSION SQL_MODE=@OLD_SQL_MODE */;

--
-- Table structure for table `itemshape`
--

DROP TABLE IF EXISTS `itemshape`;
CREATE TABLE `itemshape` (
  `ID` int(10) NOT NULL,
  `Shape` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `itemtype`
--

DROP TABLE IF EXISTS `itemtype`;
CREATE TABLE `itemtype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `lead`
--

DROP TABLE IF EXISTS `lead`;
CREATE TABLE `lead` (
  `ID` int(10) NOT NULL auto_increment,
  `LeadStatusId` int(10) NOT NULL,
  `ProjectTypeId` int(10) NOT NULL,
  `EmployeeId` int(10) default NULL,
  `CustomerWebAccountId` int(10) default NULL,
  `WebLogId` int(10) default NULL,
  `BusinessPartnerId` int(10) NOT NULL,
  `Company` varchar(50) NOT NULL,
  `FirstName` varchar(40) NOT NULL,
  `LastName` varchar(40) NOT NULL,
  `Address1` varchar(60) NOT NULL,
  `Address2` varchar(40) NOT NULL,
  `City` varchar(24) NOT NULL,
  `State` varchar(2) NOT NULL,
  `Zip` int(10) default NULL,
  `Phone1` varchar(14) NOT NULL,
  `Phone2` varchar(14) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `CustomerNotes` varchar(500) NOT NULL,
  `DispatchNotes` varchar(500) NOT NULL,
  `PreferredServiceDate` datetime default NULL,
  `PreferredTime` varchar(30) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateCancelled` datetime default NULL,
  `KeywordId` int(10) default NULL,
  `AdvertisingSourceAcronym` varchar(6) default NULL,
  `ServmanAdvertisingSource` varchar(10) default NULL,
  `ServmanTrackCode` varchar(10) default NULL,
  `DateLateNotificationSent` datetime default NULL,
  `DateFirstSetPending` datetime default NULL,
  `DateLastSetPending` datetime default NULL,
  `FirstUpdateEmployeeId` int(10) default NULL,
  `LastUpdateEmployeeId` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Lead_LeadStatus` (`LeadStatusId`),
  KEY `FK_Lead_ProjectType` (`ProjectTypeId`),
  KEY `FK_Lead_CustomerWebAccount` (`CustomerWebAccountId`),
  KEY `FK_Lead_Employee` (`EmployeeId`),
  KEY `FK_Lead_BusinessPartner` (`BusinessPartnerId`),
  KEY `FK_Lead_WebLog` (`WebLogId`),
  CONSTRAINT `FK_Lead_LeadStatus` FOREIGN KEY (`LeadStatusId`) REFERENCES `leadstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_ProjectType` FOREIGN KEY (`ProjectTypeId`) REFERENCES `projecttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_CustomerWebAccount` FOREIGN KEY (`CustomerWebAccountId`) REFERENCES `customerwebaccount` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `businesspartner` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_WebLog` FOREIGN KEY (`WebLogId`) REFERENCES `weblog` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `leadstatus`
--

DROP TABLE IF EXISTS `leadstatus`;
CREATE TABLE `leadstatus` (
  `ID` int(10) NOT NULL,
  `Status` varchar(20) NOT NULL,
  `Description` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `mapsco`
--

DROP TABLE IF EXISTS `mapsco`;
CREATE TABLE `mapsco` (
  `ID` int(10) NOT NULL auto_increment,
  `IdOld` double NOT NULL,
  `IdProduct` double NOT NULL,
  `Map` varchar(6) character set utf8 NOT NULL,
  `UpperLeftLatitude` double NOT NULL,
  `UpperLeftLongitude` double NOT NULL,
  `UpperRightLatitude` double NOT NULL,
  `UpperRightLongitude` double NOT NULL,
  `LowerLeftLatitude` double NOT NULL,
  `LowerLeftLongitude` double NOT NULL,
  `LowerRightLatitude` double NOT NULL,
  `LowerRightLongitude` double NOT NULL,
  `PrintingSc` double NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `mapscoaddress`
--

DROP TABLE IF EXISTS `mapscoaddress`;
CREATE TABLE `mapscoaddress` (
  `ID` int(10) NOT NULL,
  `Prefix` varchar(50) character set utf8 NOT NULL,
  `Street` varchar(50) character set utf8 NOT NULL,
  `Suffix` varchar(50) character set utf8 NOT NULL,
  `City` varchar(50) character set utf8 NOT NULL,
  `State` varchar(50) character set utf8 NOT NULL,
  `Zip` varchar(50) character set utf8 NOT NULL,
  `BlockBegin` int(10) NOT NULL,
  `BlockEnd` int(10) NOT NULL,
  `MapPage` varchar(50) character set utf8 NOT NULL,
  `MapLetter` varchar(50) character set utf8 NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `IX_MapscoSearch` (`Street`,`City`,`State`,`Zip`,`BlockBegin`,`BlockEnd`),
  KEY `IX_MapscoZip` (`Zip`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `message`
--

DROP TABLE IF EXISTS `message`;
CREATE TABLE `message` (
  `ID` int(10) NOT NULL auto_increment,
  `EmployeeId` int(10) NOT NULL,
  `MessageTypeId` int(10) NOT NULL,
  `VisitId` int(10) default NULL,
  `Notes` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Message_Visit` (`VisitId`),
  KEY `FK_MessageQueue_Employee` (`EmployeeId`),
  KEY `FK_MessageQueue_MessageType` (`MessageTypeId`),
  CONSTRAINT `FK_Message_Visit` FOREIGN KEY (`VisitId`) REFERENCES `visit` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_MessageQueue_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_MessageQueue_MessageType` FOREIGN KEY (`MessageTypeId`) REFERENCES `messagetype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `messagetype`
--

DROP TABLE IF EXISTS `messagetype`;
CREATE TABLE `messagetype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `monitoringdetail`
--

DROP TABLE IF EXISTS `monitoringdetail`;
CREATE TABLE `monitoringdetail` (
  `MonitoringTaskId` int(10) NOT NULL,
  `IsAllEquipmentOn` tinyint(1) default NULL,
  `IsPlacementCorrect` tinyint(1) default NULL,
  `IsCarpetRaked` tinyint(1) default NULL,
  `IsFurnitureBlocked` tinyint(1) default NULL,
  `IsOdorsPresent` tinyint(1) default NULL,
  `IsAreaClean` tinyint(1) default NULL,
  `IsBasePulled` tinyint(1) default NULL,
  `BaseLocation` varchar(50) character set utf8 default NULL,
  `IsReadingAndMoistureMapFilledOut` tinyint(1) default NULL,
  `IsConstructionNeeded` tinyint(1) default NULL,
  `ConstructionNeededNotes` varchar(100) character set utf8 default NULL,
  `CheckPadAndSubFloor` varchar(50) character set utf8 default NULL,
  `WallSurface` varchar(50) character set utf8 default NULL,
  `IsNoReadings` tinyint(1) default NULL,
  PRIMARY KEY  (`MonitoringTaskId`),
  CONSTRAINT `FK_MonitoringDetail_Task` FOREIGN KEY (`MonitoringTaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `monitoringreading`
--

DROP TABLE IF EXISTS `monitoringreading`;
CREATE TABLE `monitoringreading` (
  `ID` int(10) NOT NULL auto_increment,
  `MonitoringTaskId` int(10) NOT NULL,
  `MonitoringReadingTypeId` int(10) NOT NULL,
  `EquipmentSerialNumber` varchar(50) character set utf8 default NULL,
  `Temperature` decimal(18,5) default NULL,
  `RelativeHumidity` decimal(18,5) default NULL,
  `BtuTonnage` decimal(18,5) default NULL,
  `Notes` varchar(50) character set utf8 default NULL,
  `Gpp` decimal(18,0) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_MonitoringReading_Task` (`MonitoringTaskId`),
  KEY `FK_MonitoringReading_MonitoringReadingType` (`MonitoringReadingTypeId`),
  CONSTRAINT `FK_MonitoringReading_Task` FOREIGN KEY (`MonitoringTaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_MonitoringReading_MonitoringReadingType` FOREIGN KEY (`MonitoringReadingTypeId`) REFERENCES `monitoringreadingtype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `monitoringreadingtype`
--

DROP TABLE IF EXISTS `monitoringreadingtype`;
CREATE TABLE `monitoringreadingtype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `note`
--

DROP TABLE IF EXISTS `note`;
CREATE TABLE `note` (
  `ID` int(10) NOT NULL,
  `VisitId` int(10) default NULL,
  `EmployeeId` int(10) NOT NULL,
  `Note` varchar(1500) character set utf8 NOT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Note_Visit` (`VisitId`),
  KEY `FK_Note_Employee` (`EmployeeId`),
  CONSTRAINT `FK_Note_Visit` FOREIGN KEY (`VisitId`) REFERENCES `visit` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Note_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `noteproject`
--

DROP TABLE IF EXISTS `noteproject`;
CREATE TABLE `noteproject` (
  `NoteId` int(10) NOT NULL,
  `ProjectId` int(10) NOT NULL,
  PRIMARY KEY  (`NoteId`,`ProjectId`),
  KEY `FK_NoteProject_Project` (`ProjectId`),
  CONSTRAINT `FK_NoteProject_Note` FOREIGN KEY (`NoteId`) REFERENCES `note` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_NoteProject_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
CREATE TABLE `order` (
  `TicketNumber` varchar(6) NOT NULL,
  `CustomerId` int(10) NOT NULL,
  `OrderSourceId` int(10) default NULL,
  `AdvertisingSourceId` int(10) default NULL,
  `ScheduleDate` datetime NOT NULL,
  `TransNum` varchar(6) NOT NULL,
  `ServiceType` int(10) NOT NULL,
  `TransactionType` int(10) NOT NULL,
  `TransactionStatus` int(10) NOT NULL,
  `CompletionType` int(10) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  `DateCompleted` datetime default NULL,
  PRIMARY KEY  (`TicketNumber`),
  KEY `FK_Order_Customer` (`CustomerId`),
  KEY `FK_Order_AdvertisingSource` (`AdvertisingSourceId`),
  KEY `FK_Order_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_Order_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Order_AdvertisingSource` FOREIGN KEY (`AdvertisingSourceId`) REFERENCES `advertisingsource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Order_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `ordersource`
--

DROP TABLE IF EXISTS `ordersource`;
CREATE TABLE `ordersource` (
  `ID` int(10) NOT NULL auto_increment,
  `ParentOrderSourceId` int(10) default NULL,
  `WebUserId` int(10) default NULL,
  `Name` varchar(100) NOT NULL,
  `Active` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_OrderSource_OrderSource` (`ParentOrderSourceId`),
  KEY `FK_OrderSource_WebUser` (`WebUserId`),
  CONSTRAINT `FK_OrderSource_OrderSource` FOREIGN KEY (`ParentOrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrderSource_WebUser` FOREIGN KEY (`WebUserId`) REFERENCES `webuser` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `ordersourceadvertisingsource`
--

DROP TABLE IF EXISTS `ordersourceadvertisingsource`;
CREATE TABLE `ordersourceadvertisingsource` (
  `OrderSourceId` int(10) NOT NULL,
  `AdvertisingSourceId` int(10) NOT NULL,
  PRIMARY KEY  (`OrderSourceId`,`AdvertisingSourceId`),
  KEY `FK_OrderSourceAdvertisingSource_AdvertisingSource` (`AdvertisingSourceId`),
  CONSTRAINT `FK_OrderSourceAdvertisingSource_AdvertisingSource` FOREIGN KEY (`AdvertisingSourceId`) REFERENCES `advertisingsource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrderSourceAdvertisingSource_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `ordersourceownphone`
--

DROP TABLE IF EXISTS `ordersourceownphone`;
CREATE TABLE `ordersourceownphone` (
  `ID` int(10) NOT NULL auto_increment,
  `OrderSourceId` int(10) NOT NULL,
  `Number` varchar(20) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_OrderSourceOwnPhone_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_OrderSourceOwnPhone_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `partnerinvitation`
--

DROP TABLE IF EXISTS `partnerinvitation`;
CREATE TABLE `partnerinvitation` (
  `InvitationKey` varchar(255) NOT NULL,
  `OrderSourceId` int(10) default NULL,
  `WebUserId` int(10) default NULL,
  `Email` varchar(100) NOT NULL,
  `ExpirationDate` datetime NOT NULL,
  `IsInvitationSent` tinyint(1) NOT NULL,
  PRIMARY KEY  (`InvitationKey`),
  KEY `FK_PartnerInvitation_WebUser` (`WebUserId`),
  KEY `FK_PartnerInvitation_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_PartnerInvitation_WebUser` FOREIGN KEY (`WebUserId`) REFERENCES `webuser` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PartnerInvitation_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `partnersummaryreportitem`
--

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

--
-- Table structure for table `pendingtaskgridstate`
--

DROP TABLE IF EXISTS `pendingtaskgridstate`;
CREATE TABLE `pendingtaskgridstate` (
  `ID` int(10) NOT NULL auto_increment,
  `EmployeeId` int(10) NOT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY  (`ID`),
  CONSTRAINT `FK_PendingTaskGridState_Employee` FOREIGN KEY (`ID`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `project`
--

DROP TABLE IF EXISTS `project`;
CREATE TABLE `project` (
  `ID` int(10) NOT NULL auto_increment,
  `ParentProdjectId` int(10) default NULL,
  `ProjectTypeId` int(10) NOT NULL,
  `CustomerId` int(10) default NULL,
  `ServiceAddressId` int(10) default NULL,
  `ProjectStatusId` int(10) NOT NULL,
  `LeadId` int(10) default NULL,
  `Description` varchar(2000) character set utf8 NOT NULL,
  `InsuranceCompany` varchar(50) character set utf8 default NULL,
  `InsuranceAgency` varchar(50) character set utf8 default NULL,
  `InsuranceAgencyPhone` varchar(20) character set utf8 default NULL,
  `InsuranceAgent` varchar(50) character set utf8 default NULL,
  `InsuranceAdjustor` varchar(50) character set utf8 default NULL,
  `InsuranceAdjustorPhone` varchar(20) character set utf8 default NULL,
  `ClaimNumber` varchar(20) character set utf8 default NULL,
  `PolicyNumber` varchar(20) character set utf8 default NULL,
  `DeductibleAmount` decimal(19,4) default NULL,
  `AdvertisingSourceId` int(10) default NULL,
  `AdvertisingTechnicianId` int(10) default NULL,
  `DumpedProjectId` int(10) default NULL,
  `DumpWorkTransactionId` int(10) default NULL,
  `ClosedAmount` decimal(19,4) NOT NULL,
  `PaidAmount` decimal(19,4) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `QbCustomerTypeListId` varchar(50) default NULL,
  `QbSalesRepListId` varchar(50) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Project_QbCustomerType` (`QbCustomerTypeListId`),
  KEY `FK_Project_QbSalesRep` (`QbSalesRepListId`),
  KEY `FK_Project_AdvertisingSource` (`AdvertisingSourceId`),
  KEY `FK_Project_ProjectDump` (`DumpedProjectId`),
  KEY `FK_Project_WorkTransaction` (`DumpWorkTransactionId`),
  KEY `FK_Project_ProjectType` (`ProjectTypeId`),
  KEY `FK_Project_Project` (`ParentProdjectId`),
  KEY `FK_Project_ProjectStatus` (`ProjectStatusId`),
  KEY `FK_Project_Address` (`ServiceAddressId`),
  KEY `FK_Project_Customer` (`CustomerId`),
  KEY `FK_Project_Lead` (`LeadId`),
  CONSTRAINT `FK_Project_QbCustomerType` FOREIGN KEY (`QbCustomerTypeListId`) REFERENCES `qbcustomertype` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_QbSalesRep` FOREIGN KEY (`QbSalesRepListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_AdvertisingSource` FOREIGN KEY (`AdvertisingSourceId`) REFERENCES `advertisingsource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_ProjectDump` FOREIGN KEY (`DumpedProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_WorkTransaction` FOREIGN KEY (`DumpWorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_ProjectType` FOREIGN KEY (`ProjectTypeId`) REFERENCES `projecttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_Project` FOREIGN KEY (`ParentProdjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_ProjectStatus` FOREIGN KEY (`ProjectStatusId`) REFERENCES `projectstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_Address` FOREIGN KEY (`ServiceAddressId`) REFERENCES `address` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_Lead` FOREIGN KEY (`LeadId`) REFERENCES `lead` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DELIMITER ;;
/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedProject` BEFORE UPDATE ON `project` FOR EACH ROW BEGIN
  IF NOT (NEW.InsuranceCompany <=> OLD.InsuranceCompany)
     OR NOT (NEW.InsuranceAgency <=> OLD.InsuranceAgency)
     OR NOT (NEW.InsuranceAgencyPhone <=> OLD.InsuranceAgencyPhone)
     OR NOT (NEW.InsuranceAgent <=> OLD.InsuranceAgent)
     OR NOT (NEW.InsuranceAdjustor <=> OLD.InsuranceAdjustor)
     OR NOT (NEW.InsuranceAdjustorPhone <=> OLD.InsuranceAdjustorPhone)
     OR NOT (NEW.AdvertisingSourceId <=> OLD.AdvertisingSourceId)
     OR NOT (NEW.AdvertisingTechnicianId <=> OLD.AdvertisingTechnicianId)
     OR NOT (NEW.ClosedAmount <=> OLD.ClosedAmount)
  THEN
    UPDATE Task SET Task.Modified = NOW() 
        WHERE Task.ProjectId = NEW.ID;
  END IF;
END */;;

DELIMITER ;
/*!50003 SET SESSION SQL_MODE=@OLD_SQL_MODE */;

--
-- Table structure for table `projectconstructionbillpay`
--

DROP TABLE IF EXISTS `projectconstructionbillpay`;
CREATE TABLE `projectconstructionbillpay` (
  `ID` int(10) NOT NULL auto_increment,
  `ProjectId` int(10) NOT NULL,
  `ProjectConstructionBillPayTypeId` int(10) NOT NULL,
  `IssueDate` datetime NOT NULL,
  `Number` varchar(50) NOT NULL,
  `IsVoided` tinyint(1) NOT NULL,
  `Notes` varchar(200) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_ProjectConstructionBillPay_Project` (`ProjectId`),
  KEY `FK_ProjectConstructionBillPay_ProjectConstructionBillPayType` (`ProjectConstructionBillPayTypeId`),
  CONSTRAINT `FK_ProjectConstructionBillPay_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionBillPay_ProjectConstructionBillPayType` FOREIGN KEY (`ProjectConstructionBillPayTypeId`) REFERENCES `projectconstructionbillpaytype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projectconstructionbillpaytype`
--

DROP TABLE IF EXISTS `projectconstructionbillpaytype`;
CREATE TABLE `projectconstructionbillpaytype` (
  `ID` int(10) NOT NULL,
  `BillPayType` varchar(20) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projectconstructiondetail`
--

DROP TABLE IF EXISTS `projectconstructiondetail`;
CREATE TABLE `projectconstructiondetail` (
  `ProjectId` int(10) NOT NULL,
  `ProjectConstructionProgressId` int(10) NOT NULL,
  `ProjectManagerEmployeeId` int(10) default NULL,
  `AccountManagerEmployeeId` int(10) default NULL,
  `ScopeDate` datetime default NULL,
  `SignUpDate` datetime default NULL,
  `DeclineDate` datetime default NULL,
  `EstimatedAmount` decimal(19,4) NOT NULL,
  `ActualStartDate` datetime default NULL,
  `ActualCompletionDate` datetime default NULL,
  `SignOffDate` datetime default NULL,
  `LastBillingDate` datetime default NULL,
  `LastPaymentDate` datetime default NULL,
  `IsSelfGeneratedLead` tinyint(1) NOT NULL,
  `JobCost` decimal(19,4) NOT NULL,
  `ConstructionDamageTypeId` int(10) default NULL,
  `DamageTypeText` varchar(40) NOT NULL,
  `DamageOrigin` varchar(80) NOT NULL,
  `LossDate` datetime default NULL,
  `BilledAmount` decimal(19,4) default NULL,
  `LastModifiedDate` datetime NOT NULL,
  `JobNumber` varchar(50) default NULL,
  PRIMARY KEY  (`ProjectId`),
  UNIQUE KEY `IX_ProjectConstructionDetail_Jo` (`JobNumber`),
  KEY `FK_ProjectConstructionDetail_Employee` (`ProjectManagerEmployeeId`),
  KEY `FK_ProjectConstructionDetail_ConstructionDamageType` (`ConstructionDamageTypeId`),
  KEY `FK_ProjectConstructionDetail_ProjectConstructionProgress` (`ProjectConstructionProgressId`),
  KEY `FK_ProjectConstructionDetail_Employee1` (`AccountManagerEmployeeId`),
  CONSTRAINT `FK_ProjectConstructionDetail_Employee` FOREIGN KEY (`ProjectManagerEmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionDetail_ConstructionDamageType` FOREIGN KEY (`ConstructionDamageTypeId`) REFERENCES `constructiondamagetype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionDetail_ProjectConstructionProgress` FOREIGN KEY (`ProjectConstructionProgressId`) REFERENCES `projectconstructionprogress` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionDetail_Employee1` FOREIGN KEY (`AccountManagerEmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionDetail_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projectconstructionprogress`
--

DROP TABLE IF EXISTS `projectconstructionprogress`;
CREATE TABLE `projectconstructionprogress` (
  `ID` int(10) NOT NULL,
  `Progress` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projectconstructionscope`
--

DROP TABLE IF EXISTS `projectconstructionscope`;
CREATE TABLE `projectconstructionscope` (
  `ID` int(10) NOT NULL auto_increment,
  `ProjectId` int(10) NOT NULL,
  `ProjectConstructionScopeTypeId` int(10) NOT NULL,
  `ScopeDate` datetime NOT NULL,
  `JobType` varchar(50) NOT NULL,
  `IsVoided` tinyint(1) NOT NULL,
  `Notes` varchar(200) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_ProjectConstructionScope_ProjectConstructionScopeType` (`ProjectConstructionScopeTypeId`),
  KEY `FK_ProjectConstructionScope_Project` (`ProjectId`),
  CONSTRAINT `FK_ProjectConstructionScope_ProjectConstructionScopeType` FOREIGN KEY (`ProjectConstructionScopeTypeId`) REFERENCES `projectconstructionscopetype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionScope_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projectconstructionscopetype`
--

DROP TABLE IF EXISTS `projectconstructionscopetype`;
CREATE TABLE `projectconstructionscopetype` (
  `ID` int(10) NOT NULL,
  `ScopeType` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projectfeedback`
--

DROP TABLE IF EXISTS `projectfeedback`;
CREATE TABLE `projectfeedback` (
  `ID` int(10) NOT NULL auto_increment,
  `ProjectId` int(10) NOT NULL,
  `CustomerNote` varchar(2000) NOT NULL,
  `EditedCustomerNote` varchar(2000) NOT NULL,
  `DispatcherNote` varchar(2000) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateReviewed` datetime default NULL,
  `ReviewedByEmployeeId` int(10) default NULL,
  `CanBePostedOnWebSite` tinyint(1) NOT NULL,
  `CanBeUsedAsReferral` tinyint(1) NOT NULL,
  `IsSubscribeToMailingList` tinyint(1) NOT NULL,
  `RateId` int(10) NOT NULL,
  `IsCallbackSelected` tinyint(1) NOT NULL,
  `CallbackDaysInterval` int(10) default NULL,
  `IsDoNotCallSelected` tinyint(1) NOT NULL,
  `ReminderEmailSentDate` datetime default NULL,
  `ReminderPostCardSentDate` datetime default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_ProjectFeedback_Employee` (`ReviewedByEmployeeId`),
  KEY `FK_ProjectFeedback_ProjectFeedbackRate` (`RateId`),
  KEY `FK_ProjectFeedback_Project` (`ProjectId`),
  CONSTRAINT `FK_ProjectFeedback_Employee` FOREIGN KEY (`ReviewedByEmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectFeedback_ProjectFeedbackRate` FOREIGN KEY (`RateId`) REFERENCES `projectfeedbackrate` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectFeedback_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projectfeedbackrate`
--

DROP TABLE IF EXISTS `projectfeedbackrate`;
CREATE TABLE `projectfeedbackrate` (
  `ID` int(10) NOT NULL,
  `Rate` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projectpayment`
--

DROP TABLE IF EXISTS `projectpayment`;
CREATE TABLE `projectpayment` (
  `ProjectId` int(10) NOT NULL,
  `WorkTransactionId` int(10) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  PRIMARY KEY  (`ProjectId`,`WorkTransactionId`),
  KEY `FK_ProjectPayment_WorkTransaction` (`WorkTransactionId`),
  CONSTRAINT `FK_ProjectPayment_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectPayment_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projectstatus`
--

DROP TABLE IF EXISTS `projectstatus`;
CREATE TABLE `projectstatus` (
  `ID` int(10) NOT NULL,
  `Status` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projecttype`
--

DROP TABLE IF EXISTS `projecttype`;
CREATE TABLE `projecttype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  `QbClassListId` varchar(50) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_ProjectType_QbClass` (`QbClassListId`),
  CONSTRAINT `FK_ProjectType_QbClass` FOREIGN KEY (`QbClassListId`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projecttypeqbaccount`
--

DROP TABLE IF EXISTS `projecttypeqbaccount`;
CREATE TABLE `projecttypeqbaccount` (
  `ProjectTypeId` int(10) NOT NULL,
  `QbAccountListId` varchar(50) NOT NULL,
  `IsDefault` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ProjectTypeId`,`QbAccountListId`),
  KEY `FK_ProjectTypeQbAccount_QbAccount` (`QbAccountListId`),
  CONSTRAINT `FK_ProjectTypeQbAccount_ProjectType` FOREIGN KEY (`ProjectTypeId`) REFERENCES `projecttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectTypeQbAccount_QbAccount` FOREIGN KEY (`QbAccountListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projecttypeqbitem`
--

DROP TABLE IF EXISTS `projecttypeqbitem`;
CREATE TABLE `projecttypeqbitem` (
  `ProjectTypeId` int(10) NOT NULL,
  `QbItemListId` varchar(50) NOT NULL,
  PRIMARY KEY  (`ProjectTypeId`,`QbItemListId`),
  KEY `FK_ProjectTypeQbItem_QbItem` (`QbItemListId`),
  CONSTRAINT `FK_ProjectTypeQbItem_QbItem` FOREIGN KEY (`QbItemListId`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectTypeQbItem_ProjectType` FOREIGN KEY (`ProjectTypeId`) REFERENCES `projecttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `projecttypeqbtemplate`
--

DROP TABLE IF EXISTS `projecttypeqbtemplate`;
CREATE TABLE `projecttypeqbtemplate` (
  `ProjectTypeId` int(10) NOT NULL,
  `QbTemplateListId` varchar(50) NOT NULL,
  `IsDefault` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ProjectTypeId`,`QbTemplateListId`),
  KEY `FK_ProjectTypeQbTemplate_QbTemplate` (`QbTemplateListId`),
  CONSTRAINT `FK_ProjectTypeQbTemplate_ProjectType` FOREIGN KEY (`ProjectTypeId`) REFERENCES `projecttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectTypeQbTemplate_QbTemplate` FOREIGN KEY (`QbTemplateListId`) REFERENCES `qbtemplate` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbaccount`
--

DROP TABLE IF EXISTS `qbaccount`;
CREATE TABLE `qbaccount` (
  `ListId` varchar(50) NOT NULL,
  `FullName` varchar(256) NOT NULL,
  `AccountType` varchar(256) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbclass`
--

DROP TABLE IF EXISTS `qbclass`;
CREATE TABLE `qbclass` (
  `ListId` varchar(50) NOT NULL,
  `FullName` varchar(256) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `ParentClassListID` varchar(50) default NULL,
  `SubLevel` int(10) NOT NULL,
  PRIMARY KEY  (`ListId`),
  KEY `FK_QbClass_QbClass` (`ParentClassListID`),
  CONSTRAINT `FK_QbClass_QbClass` FOREIGN KEY (`ParentClassListID`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbcreditmemo`
--

DROP TABLE IF EXISTS `qbcreditmemo`;
CREATE TABLE `qbcreditmemo` (
  `TxnID` varchar(50) NOT NULL,
  `QbCustomerId` int(10) NOT NULL,
  `TimeCreatedInQb` datetime default NULL,
  `TimeModifiedInQb` datetime default NULL,
  `EditSequence` varchar(50) default NULL,
  `TxnNumber` int(10) default NULL,
  `QbClassListId` varchar(50) default NULL,
  `QbAccountListId` varchar(50) default NULL,
  `QbTemplateListId` varchar(50) default NULL,
  `TxnDate` datetime default NULL,
  `RefNumber` varchar(50) default NULL,
  `IsPending` tinyint(1) NOT NULL,
  `TermsRefListId` varchar(50) default NULL,
  `SalesRepRefListId` varchar(50) default NULL,
  `SubTotalAmount` decimal(19,4) default NULL,
  `ItemSalesTaxRef` varchar(50) default NULL,
  `SalesTaxPercentage` varchar(50) default NULL,
  `TaxAmount` decimal(19,4) default NULL,
  `TotalAmount` decimal(19,4) default NULL,
  `CreditRemaining` decimal(19,4) default NULL,
  `Memo` varchar(256) default NULL,
  PRIMARY KEY  (`TxnID`),
  KEY `FK_QbCreditMemo_QbCustomer` (`QbCustomerId`),
  KEY `FK_QbCreditMemo_QbSalesRep` (`SalesRepRefListId`),
  KEY `FK_QbCreditMemo_QbAccount` (`QbAccountListId`),
  KEY `FK_QbCreditMemo_QbClass` (`QbClassListId`),
  KEY `FK_QbCreditMemo_QbItem` (`ItemSalesTaxRef`),
  KEY `FK_QbCreditMemo_QbTemplate` (`QbTemplateListId`),
  CONSTRAINT `FK_QbCreditMemo_QbCustomer` FOREIGN KEY (`QbCustomerId`) REFERENCES `qbcustomer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbSalesRep` FOREIGN KEY (`SalesRepRefListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbAccount` FOREIGN KEY (`QbAccountListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbClass` FOREIGN KEY (`QbClassListId`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbItem` FOREIGN KEY (`ItemSalesTaxRef`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbTemplate` FOREIGN KEY (`QbTemplateListId`) REFERENCES `qbtemplate` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbcreditmemoline`
--

DROP TABLE IF EXISTS `qbcreditmemoline`;
CREATE TABLE `qbcreditmemoline` (
  `TxnLineID` varchar(50) NOT NULL,
  `QbCreditMemoTxnID` varchar(50) NOT NULL,
  `QbItemListId` varchar(50) NOT NULL,
  `Description` varchar(256) default NULL,
  `Quantity` decimal(18,0) default NULL,
  `Rate` decimal(19,4) default NULL,
  `RatePercent` decimal(18,0) default NULL,
  `Amount` decimal(19,4) default NULL,
  `QbSalesTaxCodeListId` varchar(50) default NULL,
  PRIMARY KEY  (`TxnLineID`),
  KEY `FK_QbCreditMemoLine_QbCreditMemo` (`QbCreditMemoTxnID`),
  KEY `FK_QbCreditMemoLine_QbItem` (`QbItemListId`),
  KEY `FK_QbCreditMemoLine_QbSalesTaxCode` (`QbSalesTaxCodeListId`),
  CONSTRAINT `FK_QbCreditMemoLine_QbCreditMemo` FOREIGN KEY (`QbCreditMemoTxnID`) REFERENCES `qbcreditmemo` (`TxnID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemoLine_QbItem` FOREIGN KEY (`QbItemListId`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemoLine_QbSalesTaxCode` FOREIGN KEY (`QbSalesTaxCodeListId`) REFERENCES `qbsalestaxcode` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbcustomer`
--

DROP TABLE IF EXISTS `qbcustomer`;
CREATE TABLE `qbcustomer` (
  `ID` int(10) NOT NULL auto_increment,
  `CustomerId` int(10) NOT NULL,
  `ProjectId` int(10) default NULL,
  `ListId` varchar(50) default NULL,
  `EditSequence` varchar(50) default NULL,
  `TimeCreated` datetime default NULL,
  `TimeModified` datetime default NULL,
  `SubLevel` int(10) default NULL,
  `Name` varchar(256) NOT NULL,
  `FullName` varchar(256) default NULL,
  `IsActive` tinyint(1) NOT NULL,
  `CompanyName` varchar(50) character set utf8 default NULL,
  `FirstName` varchar(50) character set utf8 default NULL,
  `LastName` varchar(50) character set utf8 default NULL,
  `Phone1` varchar(10) character set utf8 default NULL,
  `Phone2` varchar(10) character set utf8 default NULL,
  `Email` varchar(50) character set utf8 default NULL,
  `BillingAddressAddr1` varchar(256) character set utf8 default NULL,
  `BillingAddressAddr2` varchar(256) character set utf8 default NULL,
  `BillingAddressCity` varchar(256) character set utf8 default NULL,
  `BillingAddressState` varchar(2) character set utf8 default NULL,
  `BillingAddressPostalCode` varchar(50) character set utf8 default NULL,
  `BillingAddressCountry` varchar(50) character set utf8 default NULL,
  `BillingAddressNote` varchar(256) character set utf8 default NULL,
  `ShippingAddressAddr1` varchar(256) character set utf8 default NULL,
  `ShippingAddressAddr2` varchar(256) character set utf8 default NULL,
  `ShippingAddressCity` varchar(256) character set utf8 default NULL,
  `ShippingAddressState` varchar(2) character set utf8 default NULL,
  `ShippingAddressPostalCode` varchar(50) character set utf8 default NULL,
  `ShippingAddressCountry` varchar(50) character set utf8 default NULL,
  `ShippingAddressNote` varchar(256) character set utf8 default NULL,
  `Balance` decimal(19,4) default NULL,
  `QbCustomerTypeListId` varchar(50) default NULL,
  `QbSalesRepListId` varchar(50) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbCustomer_QbCustomerType` (`QbCustomerTypeListId`),
  KEY `FK_QbCustomer_Project` (`ProjectId`),
  KEY `FK_QbCustomer_Customer` (`CustomerId`),
  KEY `FK_QbCustomer_QbSalesRep` (`QbSalesRepListId`),
  CONSTRAINT `FK_QbCustomer_QbCustomerType` FOREIGN KEY (`QbCustomerTypeListId`) REFERENCES `qbcustomertype` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCustomer_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCustomer_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCustomer_QbSalesRep` FOREIGN KEY (`QbSalesRepListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbcustomertype`
--

DROP TABLE IF EXISTS `qbcustomertype`;
CREATE TABLE `qbcustomertype` (
  `ListId` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `FullName` varchar(256) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `ParentRefListId` varchar(50) default NULL,
  `SubLevel` int(10) NOT NULL,
  `RequiredQbSalesRepListId` varchar(50) default NULL,
  PRIMARY KEY  (`ListId`),
  KEY `FK_QbCustomerType_QbSalesRep` (`RequiredQbSalesRepListId`),
  CONSTRAINT `FK_QbCustomerType_QbSalesRep` FOREIGN KEY (`RequiredQbSalesRepListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbinvoice`
--

DROP TABLE IF EXISTS `qbinvoice`;
CREATE TABLE `qbinvoice` (
  `ID` int(10) NOT NULL auto_increment,
  `DumptedInvoiceId` int(10) default NULL,
  `DumpWorkTransactionId` int(10) default NULL,
  `QbCustomerId` int(10) NOT NULL,
  `CustomerRefListId` varchar(50) default NULL,
  `ProcessedDate` datetime default NULL,
  `ProcessedByEmployeeId` int(10) default NULL,
  `CreatedDate` datetime NOT NULL,
  `TxnID` varchar(50) default NULL,
  `TimeCreatedInQb` datetime default NULL,
  `TimeModifiedInQb` datetime default NULL,
  `TxnNumber` int(10) default NULL,
  `EditSequence` varchar(50) default NULL,
  `QbClassListId` varchar(50) default NULL,
  `QbAccountListId` varchar(50) default NULL,
  `QbTemplateListId` varchar(50) default NULL,
  `TxnDate` datetime default NULL,
  `RefNumber` varchar(50) default NULL,
  `BillingAddressAddr1` varchar(256) default NULL,
  `BillingAddressAddr2` varchar(256) default NULL,
  `BillingAddressAddr3` varchar(256) default NULL,
  `BillingAddressAddr4` varchar(256) default NULL,
  `BillingAddressAddr5` varchar(256) default NULL,
  `BillingAddressCity` varchar(50) default NULL,
  `BillingAddresState` varchar(2) default NULL,
  `BillingAddresPostalCode` varchar(10) default NULL,
  `BillingAddressCountry` varchar(50) default NULL,
  `BillingAddressNote` varchar(256) default NULL,
  `ShipAddressAddr1` varchar(256) default NULL,
  `ShipAddressAddr2` varchar(256) default NULL,
  `ShipAddressAddr3` varchar(256) default NULL,
  `ShipAddressAddr4` varchar(256) default NULL,
  `ShipAddressAddr5` varchar(256) default NULL,
  `ShipAddressCity` varchar(50) default NULL,
  `ShipAddressState` varchar(2) default NULL,
  `ShipAddressPostalCode` varchar(50) default NULL,
  `ShipAddressCountry` varchar(50) default NULL,
  `ShipAddressNote` varchar(256) default NULL,
  `QbInvoiceTermListId` varchar(50) default NULL,
  `Memo` varchar(256) default NULL,
  `ItemSalesTaxRef` varchar(50) default NULL,
  `QbSalesRepRefListId` varchar(50) default NULL,
  `SubTotalAmount` decimal(19,4) NOT NULL,
  `TaxAmount` decimal(19,4) NOT NULL,
  `TotalAmount` decimal(19,4) NOT NULL,
  `IsVoid` tinyint(1) default NULL,
  `IsPending` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbInvoice_Employee` (`ProcessedByEmployeeId`),
  KEY `FK_QbInvoice_QbInvoice` (`DumptedInvoiceId`),
  KEY `FK_QbInvoice_WorkTransaction` (`DumpWorkTransactionId`),
  KEY `FK_QbInvoice_QbCustomer` (`QbCustomerId`),
  KEY `FK_QbInvoice_QbSalesRep` (`QbSalesRepRefListId`),
  KEY `FK_QbInvoice_QbAccount` (`QbAccountListId`),
  KEY `FK_QbInvoice_QbClass` (`QbClassListId`),
  KEY `FK_QbInvoice_QbInvoiceTerm` (`QbInvoiceTermListId`),
  KEY `FK_QbInvoice_QbItem1` (`ItemSalesTaxRef`),
  KEY `FK_QbInvoice_QbTemplate` (`QbTemplateListId`),
  CONSTRAINT `FK_QbInvoice_Employee` FOREIGN KEY (`ProcessedByEmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbInvoice` FOREIGN KEY (`DumptedInvoiceId`) REFERENCES `qbinvoice` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_WorkTransaction` FOREIGN KEY (`DumpWorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbCustomer` FOREIGN KEY (`QbCustomerId`) REFERENCES `qbcustomer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbSalesRep` FOREIGN KEY (`QbSalesRepRefListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbAccount` FOREIGN KEY (`QbAccountListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbClass` FOREIGN KEY (`QbClassListId`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbInvoiceTerm` FOREIGN KEY (`QbInvoiceTermListId`) REFERENCES `qbinvoiceterm` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbItem1` FOREIGN KEY (`ItemSalesTaxRef`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbTemplate` FOREIGN KEY (`QbTemplateListId`) REFERENCES `qbtemplate` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbinvoiceline`
--

DROP TABLE IF EXISTS `qbinvoiceline`;
CREATE TABLE `qbinvoiceline` (
  `ID` int(10) NOT NULL auto_increment,
  `QbItemListId` varchar(50) NOT NULL,
  `QbInvoiceId` int(10) NOT NULL,
  `TxnLineID` varchar(50) default NULL,
  `Description` varchar(256) NOT NULL,
  `Quantity` decimal(10,0) NOT NULL,
  `UnitOfMeasure` varchar(50) default NULL,
  `Rate` decimal(19,4) NOT NULL,
  `RatePercent` decimal(18,0) default NULL,
  `QbClassListId` varchar(50) default NULL,
  `Amount` decimal(19,4) NOT NULL,
  `QbSalesTaxCodeListId` varchar(50) NOT NULL,
  `TaskId` int(10) default NULL,
  `ItemId` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbInvoiceLine_QbInvoice` (`QbInvoiceId`),
  KEY `FK_QbInvoiceLine_Item` (`ItemId`),
  KEY `FK_QbInvoiceLine_QbClass` (`QbClassListId`),
  KEY `FK_QbInvoiceLine_QbItem` (`QbItemListId`),
  KEY `FK_QbInvoiceLine_QbSalesTaxCode` (`QbSalesTaxCodeListId`),
  KEY `FK_QbInvoiceLine_Task` (`TaskId`),
  CONSTRAINT `FK_QbInvoiceLine_QbInvoice` FOREIGN KEY (`QbInvoiceId`) REFERENCES `qbinvoice` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_Item` FOREIGN KEY (`ItemId`) REFERENCES `item` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_QbClass` FOREIGN KEY (`QbClassListId`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_QbItem` FOREIGN KEY (`QbItemListId`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_QbSalesTaxCode` FOREIGN KEY (`QbSalesTaxCodeListId`) REFERENCES `qbsalestaxcode` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_Task` FOREIGN KEY (`TaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbinvoiceterm`
--

DROP TABLE IF EXISTS `qbinvoiceterm`;
CREATE TABLE `qbinvoiceterm` (
  `ListId` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsDateDriven` tinyint(1) NOT NULL,
  `StdDueDays` int(10) default NULL,
  `StdDiscountDays` int(10) default NULL,
  `DiscountPct` decimal(18,0) default NULL,
  `DayOfMonthDue` int(10) default NULL,
  `DueNextMonthDays` int(10) default NULL,
  `DiscountDayOfMonth` int(10) default NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbitem`
--

DROP TABLE IF EXISTS `qbitem`;
CREATE TABLE `qbitem` (
  `ListId` varchar(50) NOT NULL,
  `FullName` varchar(50) NOT NULL,
  `AccountRefListId` varchar(50) default NULL,
  `SalesTaxCodeRefListId` varchar(50) default NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `QbItemTypeId` int(10) NOT NULL,
  `TaxRate` decimal(19,4) default NULL,
  `Description` varchar(256) default NULL,
  `Price` decimal(19,4) default NULL,
  PRIMARY KEY  (`ListId`),
  KEY `FK_QbItem_QbItemType` (`QbItemTypeId`),
  KEY `FK_QbItemType_QbAccount` (`AccountRefListId`),
  CONSTRAINT `FK_QbItem_QbItemType` FOREIGN KEY (`QbItemTypeId`) REFERENCES `qbitemtype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbItemType_QbAccount` FOREIGN KEY (`AccountRefListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbitemtype`
--

DROP TABLE IF EXISTS `qbitemtype`;
CREATE TABLE `qbitemtype` (
  `ID` int(10) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbpayment`
--

DROP TABLE IF EXISTS `qbpayment`;
CREATE TABLE `qbpayment` (
  `TxnID` varchar(50) NOT NULL,
  `QbCustomerId` int(10) NOT NULL,
  `TimeCreatedInQb` datetime default NULL,
  `TimeModifiedInQb` datetime default NULL,
  `EditSequence` varchar(50) default NULL,
  `QbAccountListId` varchar(50) default NULL,
  `TxnNumber` int(10) default NULL,
  `TxnDate` datetime default NULL,
  `RefNumber` varchar(50) default NULL,
  `TotalAmount` decimal(19,4) default NULL,
  `QbPaymentMethodListId` varchar(50) default NULL,
  `Memo` varchar(256) default NULL,
  `DepositToAccountListId` varchar(50) default NULL,
  PRIMARY KEY  (`TxnID`),
  KEY `FK_QbPayment_QbCustomer` (`QbCustomerId`),
  KEY `FK_QbPayment_QbAccount` (`DepositToAccountListId`),
  KEY `FK_QbPayment_QbPaymentMethod` (`QbPaymentMethodListId`),
  CONSTRAINT `FK_QbPayment_QbCustomer` FOREIGN KEY (`QbCustomerId`) REFERENCES `qbcustomer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbPayment_QbAccount` FOREIGN KEY (`DepositToAccountListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbPayment_QbPaymentMethod` FOREIGN KEY (`QbPaymentMethodListId`) REFERENCES `qbpaymentmethod` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbpaymentmethod`
--

DROP TABLE IF EXISTS `qbpaymentmethod`;
CREATE TABLE `qbpaymentmethod` (
  `ListId` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbsalesrep`
--

DROP TABLE IF EXISTS `qbsalesrep`;
CREATE TABLE `qbsalesrep` (
  `ListId` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Initial` varchar(256) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsEmployee` tinyint(1) NOT NULL,
  `RefListId` varchar(50) default NULL,
  `FullName` varchar(256) default NULL,
  `EmployeeId` int(10) default NULL,
  `QbCustomerTypeListId` varchar(50) default NULL,
  `QbSalesRepTypeId` int(10) NOT NULL,
  `FirstName` varchar(25) default NULL,
  `LastName` varchar(25) default NULL,
  PRIMARY KEY  (`ListId`),
  KEY `FK_QbSalesRep_Employee` (`EmployeeId`),
  KEY `FK_QbSalesRep_QbSalesRepType` (`QbSalesRepTypeId`),
  KEY `FK_QbSalesRep_QbCustomerType` (`QbCustomerTypeListId`),
  CONSTRAINT `FK_QbSalesRep_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbSalesRep_QbSalesRepType` FOREIGN KEY (`QbSalesRepTypeId`) REFERENCES `qbsalesreptype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbSalesRep_QbCustomerType` FOREIGN KEY (`QbCustomerTypeListId`) REFERENCES `qbcustomertype` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbsalesreptype`
--

DROP TABLE IF EXISTS `qbsalesreptype`;
CREATE TABLE `qbsalesreptype` (
  `ID` int(10) NOT NULL,
  `Name` varchar(50) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbsalestaxcode`
--

DROP TABLE IF EXISTS `qbsalestaxcode`;
CREATE TABLE `qbsalestaxcode` (
  `ListId` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(256) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsTaxable` tinyint(1) NOT NULL,
  `Description` varchar(256) NOT NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbsyncaction`
--

DROP TABLE IF EXISTS `qbsyncaction`;
CREATE TABLE `qbsyncaction` (
  `ID` int(10) NOT NULL,
  `Description` varchar(64) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbsynclog`
--

DROP TABLE IF EXISTS `qbsynclog`;
CREATE TABLE `qbsynclog` (
  `ID` int(10) NOT NULL auto_increment,
  `LastRunDate` datetime NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbsynclogdetail`
--

DROP TABLE IF EXISTS `qbsynclogdetail`;
CREATE TABLE `qbsynclogdetail` (
  `ID` int(10) NOT NULL auto_increment,
  `QbSyncLogId` int(10) NOT NULL,
  `CompletedDate` datetime NOT NULL,
  `QbSyncActionId` int(10) NOT NULL,
  `IsSuccess` tinyint(1) NOT NULL,
  `ErrorMessage` varchar(1024) default NULL,
  `QbCustomerId` int(10) default NULL,
  `QbInvoiceId` int(10) default NULL,
  `QbXmlRequest` longtext,
  `QbXmlResponse` longtext,
  `TxnID` varchar(50) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbSyncLogDetail_QBSyncLog` (`QbSyncLogId`),
  KEY `FK_QbSyncLogDetail_QbSyncAction` (`QbSyncActionId`),
  CONSTRAINT `FK_QbSyncLogDetail_QBSyncLog` FOREIGN KEY (`QbSyncLogId`) REFERENCES `qbsynclog` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbSyncLogDetail_QbSyncAction` FOREIGN KEY (`QbSyncActionId`) REFERENCES `qbsyncaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbsyncrequest`
--

DROP TABLE IF EXISTS `qbsyncrequest`;
CREATE TABLE `qbsyncrequest` (
  `ID` int(10) NOT NULL auto_increment,
  `RequestDate` datetime NOT NULL,
  `QbSyncActionId` int(10) NOT NULL,
  `QbCustomerId` int(10) default NULL,
  `QbInvoiceId` int(10) default NULL,
  `TxnID` varchar(50) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbSyncRequest_QbSyncAction` (`QbSyncActionId`),
  CONSTRAINT `FK_QbSyncRequest_QbSyncAction` FOREIGN KEY (`QbSyncActionId`) REFERENCES `qbsyncaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbtemplate`
--

DROP TABLE IF EXISTS `qbtemplate`;
CREATE TABLE `qbtemplate` (
  `ListId` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `securitypermission`
--

DROP TABLE IF EXISTS `securitypermission`;
CREATE TABLE `securitypermission` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(200) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `securityrole`
--

DROP TABLE IF EXISTS `securityrole`;
CREATE TABLE `securityrole` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(200) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `securityrolepermission`
--

DROP TABLE IF EXISTS `securityrolepermission`;
CREATE TABLE `securityrolepermission` (
  `SecurityRoleId` int(10) NOT NULL,
  `SecurityPermissionId` int(10) NOT NULL,
  PRIMARY KEY  (`SecurityRoleId`,`SecurityPermissionId`),
  KEY `FK_SecurityRolePermission_SecurityPermission` (`SecurityPermissionId`),
  CONSTRAINT `FK_SecurityRolePermission_SecurityRole` FOREIGN KEY (`SecurityRoleId`) REFERENCES `securityrole` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_SecurityRolePermission_SecurityPermission` FOREIGN KEY (`SecurityPermissionId`) REFERENCES `securitypermission` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `synctoolinfo`
--

DROP TABLE IF EXISTS `synctoolinfo`;
CREATE TABLE `synctoolinfo` (
  `ID` int(10) NOT NULL,
  `LastCustomerImportDate` datetime NOT NULL,
  `LastImportedTicketNumber` varchar(6) NOT NULL,
  `LastTomorrowPrintJobDate` datetime NOT NULL,
  `LastImportedAdSourceId` varchar(6) NOT NULL,
  `LastImportedTruckId` varchar(6) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `systemlog`
--

DROP TABLE IF EXISTS `systemlog`;
CREATE TABLE `systemlog` (
  `ID` bigint(19) NOT NULL auto_increment,
  `EmployeeId` int(10) default NULL,
  `DateCreated` datetime NOT NULL,
  `SystemOperationId` int(10) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `TimeTakenMiliseconds` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_SystemLog_SystemOperation` (`SystemOperationId`),
  CONSTRAINT `FK_SystemLog_SystemOperation` FOREIGN KEY (`SystemOperationId`) REFERENCES `systemoperation` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `systemoperation`
--

DROP TABLE IF EXISTS `systemoperation`;
CREATE TABLE `systemoperation` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(200) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `sysversion`
--

DROP TABLE IF EXISTS `sysversion`;
CREATE TABLE `sysversion` (
  `Version` int(10) NOT NULL,
  PRIMARY KEY  (`Version`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `task`
--

DROP TABLE IF EXISTS `task`;
CREATE TABLE `task` (
  `ID` int(10) NOT NULL auto_increment,
  `ParentTaskId` int(10) default NULL,
  `ServmanOrderNum` varchar(6) character set utf8 default NULL,
  `ProjectId` int(10) NOT NULL,
  `TaskTypeId` int(10) NOT NULL,
  `TaskStatusId` int(10) default NULL,
  `TaskFailTypeId` int(10) default NULL,
  `IsReady` tinyint(1) default NULL,
  `Number` varchar(50) character set utf8 default NULL,
  `Sequence` int(10) default NULL,
  `CreateDate` datetime default NULL,
  `ServiceDate` datetime default NULL,
  `DurationMin` int(10) default NULL,
  `Description` varchar(500) character set utf8 default NULL,
  `Message` varchar(500) character set utf8 default NULL,
  `Notes` varchar(500) character set utf8 default NULL,
  `FailReason` varchar(500) character set utf8 default NULL,
  `IsSentToServman` tinyint(1) NOT NULL,
  `ClosedAmount` decimal(19,4) NOT NULL,
  `IsClosedAmountAutoCalculated` tinyint(1) NOT NULL,
  `EstimatedClosedAmount` decimal(19,4) NOT NULL,
  `IsEstimatedClosedAmountAutoCalculated` tinyint(1) NOT NULL,
  `IsReincluded` tinyint(1) NOT NULL,
  `Modified` datetime NOT NULL,
  `LastSyncDate` datetime default NULL,
  `DumpedTaskId` int(10) default NULL,
  `DumpWorkTransactionId` int(10) default NULL,
  `FailCount` int(10) NOT NULL,
  `LastFailDate` datetime default NULL,
  `IsRugCleaningDepartment` tinyint(1) NOT NULL,
  `DiscountPercentage` int(10) NOT NULL,
  `ReadyDate` datetime default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Task_TaskDump` (`DumpedTaskId`),
  KEY `FK_Task_WorkTransaction` (`DumpWorkTransactionId`),
  KEY `FK_Task_TaskStatus` (`TaskStatusId`),
  KEY `FK_Task_TaskFailType` (`TaskFailTypeId`),
  KEY `FK_Task_Task` (`ParentTaskId`),
  KEY `FK_Task_TaskType` (`TaskTypeId`),
  KEY `FK_Ticket_Project` (`ProjectId`),
  CONSTRAINT `FK_Task_TaskDump` FOREIGN KEY (`DumpedTaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Task_WorkTransaction` FOREIGN KEY (`DumpWorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Task_TaskStatus` FOREIGN KEY (`TaskStatusId`) REFERENCES `taskstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Task_TaskFailType` FOREIGN KEY (`TaskFailTypeId`) REFERENCES `taskfailtype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Task_Task` FOREIGN KEY (`ParentTaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Task_TaskType` FOREIGN KEY (`TaskTypeId`) REFERENCES `tasktype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Ticket_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DELIMITER ;;
/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedTaskInsert` BEFORE INSERT ON `task` FOR EACH ROW SET NEW.Modified = NOW() */;;

/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedTask` BEFORE UPDATE ON `task` FOR EACH ROW BEGIN
  IF NOT (NEW.TaskStatusId <=> OLD.TaskStatusId)
     OR NOT (NEW.TaskFailTypeId <=> OLD.TaskFailTypeId)
     OR NOT (NEW.Message <=> OLD.Message)
     OR NOT (NEW.ClosedAmount <=> OLD.ClosedAmount)
     OR NOT (NEW.EstimatedClosedAmount <=> OLD.EstimatedClosedAmount)
  THEN
     SET NEW.Modified = NOW();
  END IF;
END */;;

DELIMITER ;
/*!50003 SET SESSION SQL_MODE=@OLD_SQL_MODE */;

--
-- Table structure for table `taskfailtype`
--

DROP TABLE IF EXISTS `taskfailtype`;
CREATE TABLE `taskfailtype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `taskitemrequirement`
--

DROP TABLE IF EXISTS `taskitemrequirement`;
CREATE TABLE `taskitemrequirement` (
  `ID` int(10) NOT NULL auto_increment,
  `TaskId` int(10) NOT NULL,
  `ItemType` varchar(50) character set utf8 default NULL,
  `ServiceQuantity` int(10) default NULL,
  `CaptureQuantity` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_TaskItemRequirement_Task` (`TaskId`),
  CONSTRAINT `FK_TaskItemRequirement_Task` FOREIGN KEY (`TaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `taskstatus`
--

DROP TABLE IF EXISTS `taskstatus`;
CREATE TABLE `taskstatus` (
  `ID` int(10) NOT NULL,
  `Status` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `tasktype`
--

DROP TABLE IF EXISTS `tasktype`;
CREATE TABLE `tasktype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `tasktypeqbitem`
--

DROP TABLE IF EXISTS `tasktypeqbitem`;
CREATE TABLE `tasktypeqbitem` (
  `TaskTypeId` int(10) NOT NULL,
  `QbItemListId` varchar(50) NOT NULL,
  PRIMARY KEY  (`TaskTypeId`,`QbItemListId`),
  KEY `FK_TaskTypeQbItem_QbItem` (`QbItemListId`),
  CONSTRAINT `FK_TaskTypeQbItem_TaskType` FOREIGN KEY (`TaskTypeId`) REFERENCES `tasktype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TaskTypeQbItem_QbItem` FOREIGN KEY (`QbItemListId`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `trackingphone`
--

DROP TABLE IF EXISTS `trackingphone`;
CREATE TABLE `trackingphone` (
  `ID` int(10) NOT NULL auto_increment,
  `Number` varchar(20) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `trackingphoneordersource`
--

DROP TABLE IF EXISTS `trackingphoneordersource`;
CREATE TABLE `trackingphoneordersource` (
  `TrackingPhoneId` int(10) NOT NULL,
  `OrderSourceId` int(10) NOT NULL,
  PRIMARY KEY  (`TrackingPhoneId`,`OrderSourceId`),
  KEY `FK_TrackingPhoneOrderSource_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_TrackingPhoneOrderSource_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneOrderSource_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `transaction`
--

DROP TABLE IF EXISTS `transaction`;
CREATE TABLE `transaction` (
  `ID` bigint(19) NOT NULL auto_increment,
  `ServmanTransactionId` varchar(7) NOT NULL,
  `ServmanWorkflowId` varchar(7) NOT NULL,
  `ManualRecordedCallerId` varchar(50) NOT NULL,
  `TicketNumber` varchar(6) default NULL,
  `CustomerId` int(10) default NULL,
  `CustomerName` varchar(40) NOT NULL,
  `ServiceType` int(10) NOT NULL,
  `TransactionCode` varchar(4) NOT NULL,
  `UserName` varchar(40) NOT NULL,
  `Extension` varchar(3) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `DigiumLogItemId` bigint(19) default NULL,
  `MatchCriteria` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `IX_ServmanTransactionId` (`ServmanTransactionId`),
  KEY `FK_Transaction_DigiumLogItem` (`DigiumLogItemId`),
  KEY `FK_Transaction_Customer` (`CustomerId`),
  KEY `FK_Transaction_Order` (`TicketNumber`),
  CONSTRAINT `FK_Transaction_DigiumLogItem` FOREIGN KEY (`DigiumLogItemId`) REFERENCES `digiumlogitem` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Transaction_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Transaction_Order` FOREIGN KEY (`TicketNumber`) REFERENCES `order` (`TicketNumber`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `van`
--

DROP TABLE IF EXISTS `van`;
CREATE TABLE `van` (
  `ID` int(10) NOT NULL auto_increment,
  `ServmanTruckId` varchar(6) character set utf8 default NULL,
  `ServmanTruckNum` varchar(4) character set utf8 default NULL,
  `AreaId` tinyint(3) NOT NULL,
  `LicensePlateNumber` varchar(20) character set utf8 default NULL,
  `EngineNumber` varchar(50) character set utf8 default NULL,
  `BodyNumber` varchar(50) character set utf8 default NULL,
  `Color` varchar(50) character set utf8 default NULL,
  `OilChangeDue` varchar(50) character set utf8 default NULL,
  `PagerNumber` varchar(30) character set utf8 default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Van_Area` (`AreaId`),
  CONSTRAINT `FK_Van_Area` FOREIGN KEY (`AreaId`) REFERENCES `area` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=488 DEFAULT CHARSET=latin1;

--
-- Table structure for table `vandetail`
--

DROP TABLE IF EXISTS `vandetail`;
CREATE TABLE `vandetail` (
  `ID` int(10) NOT NULL auto_increment,
  `VanId` int(10) NOT NULL,
  `DateCreated` datetime default NULL,
  `OilChangeDue` decimal(18,0) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_VanDetail_Van` (`VanId`),
  CONSTRAINT `FK_VanDetail_Van` FOREIGN KEY (`VanId`) REFERENCES `van` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `visit`
--

DROP TABLE IF EXISTS `visit`;
CREATE TABLE `visit` (
  `ID` int(10) NOT NULL auto_increment,
  `VisitStatusId` int(10) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `ServiceDate` datetime default NULL,
  `DurationMin` int(10) default NULL,
  `PreferedTimeFrom` datetime default NULL,
  `PreferedTimeTo` datetime default NULL,
  `CustomerId` int(10) default NULL,
  `ServiceAddressId` int(10) default NULL,
  `Notes` varchar(1500) character set utf8 default NULL,
  `ConfirmDateTime` datetime default NULL,
  `ConfirmedFrameBegin` datetime default NULL,
  `ConfirmedFrameEnd` datetime default NULL,
  `ConfirmLeftMessage` tinyint(1) NOT NULL,
  `ConfirmBusy` tinyint(1) NOT NULL,
  `IsCallOnYourWay` tinyint(1) NOT NULL,
  `ClosedDollarAmount` decimal(19,4) NOT NULL,
  `SyncToolPrintDate` datetime default NULL,
  `IsWillCall` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Visit_VisitStatus` (`VisitStatusId`),
  KEY `FK_Visit_Address` (`ServiceAddressId`),
  KEY `FK_Visit_Customer` (`CustomerId`),
  CONSTRAINT `FK_Visit_VisitStatus` FOREIGN KEY (`VisitStatusId`) REFERENCES `visitstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Address` FOREIGN KEY (`ServiceAddressId`) REFERENCES `address` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Visit_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DELIMITER ;;
/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedVisitUpdate` BEFORE UPDATE ON `visit` FOR EACH ROW BEGIN
  IF NOT (NEW.VisitStatusId <=> OLD.VisitStatusId)
     OR NOT (NEW.ServiceDate <=> OLD.ServiceDate)
     OR NOT (NEW.PreferedTimeFrom <=> OLD.PreferedTimeFrom)
     OR NOT (NEW.PreferedTimeTo <=> OLD.PreferedTimeTo)
     OR NOT (NEW.ServiceAddressId <=> OLD.ServiceAddressId)
     OR NOT (NEW.Notes <=> OLD.Notes)
     OR NOT (NEW.ClosedDollarAmount <=> OLD.ClosedDollarAmount)
  THEN
    call MarkTaskModifiedByVisit(NEW.ID);
  END IF;
END */;;

DELIMITER ;
/*!50003 SET SESSION SQL_MODE=@OLD_SQL_MODE */;

--
-- Table structure for table `visitstatus`
--

DROP TABLE IF EXISTS `visitstatus`;
CREATE TABLE `visitstatus` (
  `ID` int(10) NOT NULL,
  `Status` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `visittask`
--

DROP TABLE IF EXISTS `visittask`;
CREATE TABLE `visittask` (
  `VisitId` int(10) NOT NULL,
  `TaskId` int(10) NOT NULL,
  PRIMARY KEY  (`VisitId`,`TaskId`),
  KEY `FK_VisitTask_Task` (`TaskId`),
  CONSTRAINT `FK_VisitTask_Visit` FOREIGN KEY (`VisitId`) REFERENCES `visit` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_VisitTask_Task` FOREIGN KEY (`TaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DELIMITER ;;
/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedVisitTaskInsert` BEFORE INSERT ON `visittask` FOR EACH ROW BEGIN
  call MarkTaskModified(NEW.TaskId);
END */;;

DELIMITER ;
/*!50003 SET SESSION SQL_MODE=@OLD_SQL_MODE */;

--
-- Table structure for table `weblog`
--

DROP TABLE IF EXISTS `weblog`;
CREATE TABLE `weblog` (
  `ID` int(10) NOT NULL auto_increment,
  `WebSiteId` int(10) NOT NULL,
  `SessionId` varchar(80) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `ReferrerHost` varchar(256) NOT NULL,
  `URL` varchar(256) NOT NULL,
  `Keyword` varchar(128) NOT NULL,
  `KeywordId` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WebLog_WebSite` (`WebSiteId`),
  CONSTRAINT `FK_WebLog_WebSite` FOREIGN KEY (`WebSiteId`) REFERENCES `website` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `website`
--

DROP TABLE IF EXISTS `website`;
CREATE TABLE `website` (
  `ID` int(10) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `websitearticle`
--

DROP TABLE IF EXISTS `websitearticle`;
CREATE TABLE `websitearticle` (
  `ID` int(10) NOT NULL auto_increment,
  `WebSiteArticleCategoryId` int(10) NOT NULL,
  `WebSiteArticleTypeId` int(10) NOT NULL,
  `Url` varchar(1024) NOT NULL,
  `MenuId` int(10) NOT NULL,
  `DatePublished` datetime default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WebSiteArticle_WebSiteArticleCategory` (`WebSiteArticleCategoryId`),
  CONSTRAINT `FK_WebSiteArticle_WebSiteArticleCategory` FOREIGN KEY (`WebSiteArticleCategoryId`) REFERENCES `websitearticlecategory` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `websitearticlecategory`
--

DROP TABLE IF EXISTS `websitearticlecategory`;
CREATE TABLE `websitearticlecategory` (
  `ID` int(10) NOT NULL auto_increment,
  `WebSiteId` int(10) NOT NULL,
  `ParentWebSiteArticleCategoryId` int(10) default NULL,
  `LandingPageWebSiteArticleId` int(10) NOT NULL,
  `Name` varchar(100) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WebSiteArticleCategory_WebSiteArticle` (`LandingPageWebSiteArticleId`),
  KEY `FK_WebSiteArticleCategory_WebSiteArticleCategory` (`ParentWebSiteArticleCategoryId`),
  CONSTRAINT `FK_WebSiteArticleCategory_WebSiteArticle` FOREIGN KEY (`LandingPageWebSiteArticleId`) REFERENCES `websitearticle` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WebSiteArticleCategory_WebSiteArticleCategory` FOREIGN KEY (`ParentWebSiteArticleCategoryId`) REFERENCES `websitearticlecategory` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `websitearticlepart`
--

DROP TABLE IF EXISTS `websitearticlepart`;
CREATE TABLE `websitearticlepart` (
  `WebSiteArticleId` int(10) NOT NULL,
  `WebSiteArticlePartTypeId` int(10) NOT NULL,
  `ContentText` longtext NOT NULL,
  PRIMARY KEY  (`WebSiteArticleId`,`WebSiteArticlePartTypeId`),
  KEY `FK_WebSiteArticlePart_WebSiteArticlePartType` (`WebSiteArticlePartTypeId`),
  CONSTRAINT `FK_WebSiteArticlePart_WebSiteArticlePartType` FOREIGN KEY (`WebSiteArticlePartTypeId`) REFERENCES `websitearticleparttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WebSiteArticlePart_WebSiteArticle` FOREIGN KEY (`WebSiteArticleId`) REFERENCES `websitearticle` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `websitearticleparttype`
--

DROP TABLE IF EXISTS `websitearticleparttype`;
CREATE TABLE `websitearticleparttype` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(100) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;

--
-- Table structure for table `webuser`
--

DROP TABLE IF EXISTS `webuser`;
CREATE TABLE `webuser` (
  `ID` int(10) NOT NULL auto_increment,
  `OrderSourceId` int(10) default NULL,
  `WebUserRoleId` int(10) NOT NULL,
  `Login` varchar(100) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `EmployeeId` int(10) default NULL,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  PRIMARY KEY  (`ID`),
  UNIQUE KEY `IX_Login` (`Login`),
  KEY `FK_WebUser_Employee` (`EmployeeId`),
  KEY `FK_WebUser_WebUserRole` (`WebUserRoleId`),
  KEY `FK_WebUser_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_WebUser_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WebUser_WebUserRole` FOREIGN KEY (`WebUserRoleId`) REFERENCES `webuserrole` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WebUser_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `webuserrole`
--

DROP TABLE IF EXISTS `webuserrole`;
CREATE TABLE `webuserrole` (
  `ID` int(10) NOT NULL,
  `Name` varchar(100) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `work`
--

DROP TABLE IF EXISTS `work`;
CREATE TABLE `work` (
  `ID` int(10) NOT NULL auto_increment,
  `DispatchEmployeeId` int(10) NOT NULL,
  `TechnicianEmployeeId` int(10) NOT NULL,
  `VanId` int(10) default NULL,
  `StartDate` datetime default NULL,
  `WorkStatusId` int(10) default NULL,
  `StartMessage` varchar(500) character set utf8 default NULL,
  `EndMessage` varchar(500) character set utf8 default NULL,
  `EquipmentNotes` varchar(500) character set utf8 default NULL,
  `IsSentToServman` tinyint(1) NOT NULL default '0',
  `CreateDate` datetime NOT NULL,
  `StartDayDate` datetime default NULL,
  `EndDayDate` datetime default NULL,
  `ClosedDollarAmount` decimal(19,4) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Work_Employee` (`DispatchEmployeeId`),
  KEY `FK_Work_Employee1` (`TechnicianEmployeeId`),
  KEY `FK_Work_Van` (`VanId`),
  KEY `FK_Work_WorkStatus` (`WorkStatusId`),
  CONSTRAINT `FK_Work_Employee` FOREIGN KEY (`DispatchEmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Work_Employee1` FOREIGN KEY (`TechnicianEmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Work_Van` FOREIGN KEY (`VanId`) REFERENCES `van` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Work_WorkStatus` FOREIGN KEY (`WorkStatusId`) REFERENCES `workstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DELIMITER ;;
/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedWorkUpdate` BEFORE UPDATE ON `work` FOR EACH ROW BEGIN
  IF NOT (NEW.VanId <=> OLD.VanId)
  THEN
    call MarkTaskModifiedByWork(NEW.ID);
  END IF;
END */;;

DELIMITER ;
/*!50003 SET SESSION SQL_MODE=@OLD_SQL_MODE */;

--
-- Table structure for table `workdetail`
--

DROP TABLE IF EXISTS `workdetail`;
CREATE TABLE `workdetail` (
  `ID` int(10) NOT NULL auto_increment,
  `WorkId` int(10) NOT NULL,
  `VisitId` int(10) NOT NULL,
  `TimeBegin` datetime NOT NULL,
  `TimeEnd` datetime NOT NULL,
  `Sequence` int(10) default NULL,
  `WorkDetailStatusId` int(10) default NULL,
  `TimeDispatch` datetime default NULL,
  `TimeArrive` datetime default NULL,
  `TimeComplete` datetime default NULL,
  `TimeBeginAssigned` datetime default NULL,
  `TimeEndAssigned` datetime default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WorkDetail_WorkDetailStatus` (`WorkDetailStatusId`),
  KEY `FK_WorkDetail_Visit` (`VisitId`),
  KEY `FK_WorkDetail_Work` (`WorkId`),
  CONSTRAINT `FK_WorkDetail_WorkDetailStatus` FOREIGN KEY (`WorkDetailStatusId`) REFERENCES `workdetailstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkDetail_Visit` FOREIGN KEY (`VisitId`) REFERENCES `visit` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkDetail_Work` FOREIGN KEY (`WorkId`) REFERENCES `work` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DELIMITER ;;
/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedWorkDetailUpdate` BEFORE UPDATE ON `workdetail` FOR EACH ROW BEGIN
  IF NOT (NEW.WorkId <=> OLD.WorkId)
     OR NOT (NEW.TimeBegin <=> OLD.TimeBegin)
     OR NOT (NEW.TimeEnd <=> OLD.TimeEnd)
     OR NOT (NEW.TimeDispatch <=> OLD.TimeDispatch)
     OR NOT (NEW.TimeArrive <=> OLD.TimeArrive)
     OR NOT (NEW.TimeComplete <=> OLD.TimeComplete)
  THEN
    call MarkTaskModifiedByVisit(OLD.VisitId);
    call MarkTaskModifiedByVisit(NEW.VisitId);
  END IF;
END */;;

DELIMITER ;
/*!50003 SET SESSION SQL_MODE=@OLD_SQL_MODE */;

--
-- Table structure for table `workdetailstatus`
--

DROP TABLE IF EXISTS `workdetailstatus`;
CREATE TABLE `workdetailstatus` (
  `ID` int(10) NOT NULL,
  `Status` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `workequipment`
--

DROP TABLE IF EXISTS `workequipment`;
CREATE TABLE `workequipment` (
  `ID` int(10) NOT NULL auto_increment,
  `WorkId` int(10) NOT NULL,
  `EquipmentTypeId` int(10) NOT NULL,
  `Quantity` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WorkEquipment_EquipmentType` (`EquipmentTypeId`),
  KEY `FK_WorkEquipment_Work` (`WorkId`),
  CONSTRAINT `FK_WorkEquipment_EquipmentType` FOREIGN KEY (`EquipmentTypeId`) REFERENCES `equipmenttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkEquipment_Work` FOREIGN KEY (`WorkId`) REFERENCES `work` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `workstatus`
--

DROP TABLE IF EXISTS `workstatus`;
CREATE TABLE `workstatus` (
  `ID` int(10) NOT NULL,
  `Status` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransaction`
--

DROP TABLE IF EXISTS `worktransaction`;
CREATE TABLE `worktransaction` (
  `ID` int(10) NOT NULL auto_increment,
  `WorkId` int(10) NOT NULL,
  `EmployeeId` int(10) NOT NULL,
  `VisitId` int(10) default NULL,
  `WorkTransactionTypeId` int(10) NOT NULL,
  `TransactionDate` datetime default NULL,
  `AmountCollected` decimal(19,4) default NULL,
  `IsSentToServman` tinyint(1) NOT NULL default '0',
  PRIMARY KEY  (`ID`),
  KEY `FK_WorkTransaction_Visit` (`VisitId`),
  KEY `FK_WorkTransaction_Employee` (`EmployeeId`),
  KEY `FK_WorkTransaction_WorkTransactionType` (`WorkTransactionTypeId`),
  KEY `FK_WorkTransaction_Work` (`WorkId`),
  CONSTRAINT `FK_WorkTransaction_Visit` FOREIGN KEY (`VisitId`) REFERENCES `visit` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransaction_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransaction_WorkTransactionType` FOREIGN KEY (`WorkTransactionTypeId`) REFERENCES `worktransactiontype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransaction_Work` FOREIGN KEY (`WorkId`) REFERENCES `work` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransactionetc`
--

DROP TABLE IF EXISTS `worktransactionetc`;
CREATE TABLE `worktransactionetc` (
  `WorkTransactionId` int(10) NOT NULL,
  `SaleAmount` decimal(19,4) default NULL,
  `Hours` int(10) default NULL,
  `Minutes` int(10) default NULL,
  `Notes` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`WorkTransactionId`),
  CONSTRAINT `FK_WorkTransactionEtc_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DELIMITER ;;
/*!50003 SET SESSION SQL_MODE="STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION" */;;
/*!50003 CREATE */ /*!50017 DEFINER=`root`@`localhost` */ /*!50003 TRIGGER `MarkTaskModifiedWorkTransactionEtcInsert` BEFORE INSERT ON `worktransactionetc` FOR EACH ROW BEGIN
	DECLARE visitIdVar int;
	SELECT VisitId INTO visitIdVar FROM WorkTransaction WHERE WorkTransaction.ID = NEW.WorkTransactionId;
	call MarkTaskModifiedByVisit(visitIdVar);
END */;;

DELIMITER ;
/*!50003 SET SESSION SQL_MODE=@OLD_SQL_MODE */;

--
-- Table structure for table `worktransactiongps`
--

DROP TABLE IF EXISTS `worktransactiongps`;
CREATE TABLE `worktransactiongps` (
  `WorkTransactionId` int(10) NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `GpsTime` datetime NOT NULL,
  PRIMARY KEY  (`WorkTransactionId`),
  CONSTRAINT `FK_WorkTransactionGps_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransactionpayment`
--

DROP TABLE IF EXISTS `worktransactionpayment`;
CREATE TABLE `worktransactionpayment` (
  `ID` int(10) NOT NULL auto_increment,
  `WorkTransactionId` int(10) NOT NULL,
  `WorkTransactionPaymentTypeId` int(10) NOT NULL,
  `PaymentAmount` decimal(19,4) NOT NULL,
  `FirstName` varchar(100) character set utf8 default NULL,
  `LastName` varchar(100) character set utf8 default NULL,
  `Address` varchar(200) character set utf8 default NULL,
  `City` varchar(100) character set utf8 default NULL,
  `State` varchar(30) character set utf8 default NULL,
  `Zip` varchar(10) character set utf8 default NULL,
  `CreditCardTypeId` int(10) default NULL,
  `CreditCardNumber` varchar(50) character set utf8 default NULL,
  `CreditCardExpirationDate` datetime default NULL,
  `CreditCardCVV2TypeId` int(10) default NULL,
  `CreditCardCVV2` varchar(10) character set utf8 default NULL,
  `BankCheckAccountTypeId` int(10) default NULL,
  `BankCheckNumber` varchar(50) character set utf8 default NULL,
  `BankRouteNumber` varchar(50) character set utf8 default NULL,
  `BankCheckCompany` varchar(100) character set utf8 default NULL,
  `BankCheckBankName` varchar(100) character set utf8 default NULL,
  `BankCheckAccountNumber` varchar(50) character set utf8 default NULL,
  `IsAccepted` tinyint(1) NOT NULL,
  `ServerResponse` varchar(200) character set utf8 NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WorkTransactionPayment_CreditCardType` (`CreditCardTypeId`),
  KEY `FK_WorkTransactionPayment_WorkTransactionPaymentType` (`WorkTransactionPaymentTypeId`),
  KEY `FK_WorkTransactionPayment_CreditCardCVV2Type` (`CreditCardCVV2TypeId`),
  KEY `FK_WorkTransactionPayment_BankCheckAccountType` (`BankCheckAccountTypeId`),
  KEY `FK_WorkTransactionPayment_WorkTransaction` (`WorkTransactionId`),
  CONSTRAINT `FK_WorkTransactionPayment_CreditCardType` FOREIGN KEY (`CreditCardTypeId`) REFERENCES `creditcardtype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransactionPayment_WorkTransactionPaymentType` FOREIGN KEY (`WorkTransactionPaymentTypeId`) REFERENCES `worktransactionpaymenttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransactionPayment_CreditCardCVV2Type` FOREIGN KEY (`CreditCardCVV2TypeId`) REFERENCES `creditcardcvv2type` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransactionPayment_BankCheckAccountType` FOREIGN KEY (`BankCheckAccountTypeId`) REFERENCES `bankcheckaccounttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransactionPayment_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransactionpaymenttype`
--

DROP TABLE IF EXISTS `worktransactionpaymenttype`;
CREATE TABLE `worktransactionpaymenttype` (
  `ID` int(10) NOT NULL,
  `PaymentType` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransactionproject`
--

DROP TABLE IF EXISTS `worktransactionproject`;
CREATE TABLE `worktransactionproject` (
  `WorkTransactionId` int(10) NOT NULL,
  `ProjectId` int(10) NOT NULL,
  `IsModified` tinyint(1) NOT NULL,
  `IsCreated` tinyint(1) NOT NULL,
  PRIMARY KEY  (`WorkTransactionId`,`ProjectId`),
  KEY `FK_WorkTransactionProject_Project` (`ProjectId`),
  CONSTRAINT `FK_WorkTransactionProject_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransactionProject_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransactionqbinvoice`
--

DROP TABLE IF EXISTS `worktransactionqbinvoice`;
CREATE TABLE `worktransactionqbinvoice` (
  `WorkTransactionId` int(10) NOT NULL,
  `QbInvoiceId` int(10) NOT NULL,
  `QbCustomerId` int(10) default NULL,
  `QbProjectId` int(10) default NULL,
  `IsModified` tinyint(1) NOT NULL,
  `IsCreated` tinyint(1) NOT NULL,
  PRIMARY KEY  (`WorkTransactionId`,`QbInvoiceId`),
  KEY `FK_WorkTransactionQbInvoice_QbInvoice` (`QbInvoiceId`),
  CONSTRAINT `FK_WorkTransactionQbInvoice_QbInvoice` FOREIGN KEY (`QbInvoiceId`) REFERENCES `qbinvoice` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransactionQbInvoice_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransactiontask`
--

DROP TABLE IF EXISTS `worktransactiontask`;
CREATE TABLE `worktransactiontask` (
  `WorkTransactionId` int(10) NOT NULL,
  `TaskId` int(10) NOT NULL,
  `IsModified` tinyint(1) NOT NULL,
  `IsCreated` tinyint(1) NOT NULL,
  `WorkTransactionTaskActionId` int(10) NOT NULL,
  PRIMARY KEY  (`WorkTransactionId`,`TaskId`),
  KEY `FK_WorkTransactionTask_WorkTransactionTaskAction` (`WorkTransactionTaskActionId`),
  KEY `FK_WorkTransactionTask_Task` (`TaskId`),
  CONSTRAINT `FK_WorkTransactionTask_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransactionTask_WorkTransactionTaskAction` FOREIGN KEY (`WorkTransactionTaskActionId`) REFERENCES `worktransactiontaskaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransactionTask_Task` FOREIGN KEY (`TaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransactiontaskaction`
--

DROP TABLE IF EXISTS `worktransactiontaskaction`;
CREATE TABLE `worktransactiontaskaction` (
  `ID` int(10) NOT NULL,
  `TaskAction` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransactiontype`
--

DROP TABLE IF EXISTS `worktransactiontype`;
CREATE TABLE `worktransactiontype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) character set utf8 default NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `worktransactionvancheck`
--

DROP TABLE IF EXISTS `worktransactionvancheck`;
CREATE TABLE `worktransactionvancheck` (
  `WorkTransactionId` int(10) NOT NULL,
  `OilChecked` tinyint(1) default NULL,
  `UnitClean` tinyint(1) default NULL,
  `VanClean` tinyint(1) default NULL,
  `SuppliesStocked` tinyint(1) default NULL,
  `OdometerReading` decimal(18,0) default NULL,
  `HobbsReading` decimal(18,0) default NULL,
  `SpecialNeeds` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`WorkTransactionId`),
  CONSTRAINT `FK_WorkTransactionVanCheck_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `zip`
--

DROP TABLE IF EXISTS `zip`;
CREATE TABLE `zip` (
  `ZipCode` varchar(5) character set utf8 NOT NULL,
  `AreaId` tinyint(3) NOT NULL,
  `City` varchar(25) character set utf8 NOT NULL,
  `State` varchar(2) character set utf8 NOT NULL,
  PRIMARY KEY  (`ZipCode`,`City`),
  KEY `FK_Zip_Area` (`AreaId`),
  CONSTRAINT `FK_Zip_Area` FOREIGN KEY (`AreaId`) REFERENCES `area` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2011-07-21  8:30:49
