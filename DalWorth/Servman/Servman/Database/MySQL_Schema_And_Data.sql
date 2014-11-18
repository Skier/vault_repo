-- MySQL dump 10.10
--
-- Host: localhost    Database: servman_customer_dbo
-- ------------------------------------------------------
-- Server version	5.0.26-community-nt

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
-- Current Database: `servman_customer_dbo`
--

USE `servman_customer_dbo`;

--
-- Table structure for table `callworkflow`
--

DROP TABLE IF EXISTS `callworkflow`;
CREATE TABLE `callworkflow` (
  `Id` int(10) NOT NULL auto_increment,
  `Description` varchar(50) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `callworkflow`
--

/*!40000 ALTER TABLE `callworkflow` DISABLE KEYS */;
/*!40000 ALTER TABLE `callworkflow` ENABLE KEYS */;

--
-- Table structure for table `file`
--

DROP TABLE IF EXISTS `file`;
CREATE TABLE `file` (
  `Id` int(10) NOT NULL auto_increment,
  `StorageKey` varchar(50) NOT NULL,
  `OriginalFileName` varchar(150) NOT NULL,
  `FileType` varchar(50) NOT NULL,
  `FileSize` decimal(18,0) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `file`
--

/*!40000 ALTER TABLE `file` DISABLE KEYS */;
/*!40000 ALTER TABLE `file` ENABLE KEYS */;

--
-- Table structure for table `job`
--

DROP TABLE IF EXISTS `job`;
CREATE TABLE `job` (
  `Id` int(10) NOT NULL auto_increment,
  `LeadId` int(10) NOT NULL,
  `QbJobRecordId` varchar(50) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Job_Lead` (`LeadId`),
  CONSTRAINT `FK_Job_Lead` FOREIGN KEY (`LeadId`) REFERENCES `lead` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `job`
--

/*!40000 ALTER TABLE `job` DISABLE KEYS */;
/*!40000 ALTER TABLE `job` ENABLE KEYS */;

--
-- Table structure for table `lead`
--

DROP TABLE IF EXISTS `lead`;
CREATE TABLE `lead` (
  `Id` int(10) NOT NULL auto_increment,
  `LeadStatusId` int(10) NOT NULL,
  `LeadSourceId` int(10) default NULL,
  `AssignedToUser` int(10) default NULL,
  `FirstName` varchar(150) default NULL,
  `LastName` varchar(150) default NULL,
  `Phone` varchar(50) default NULL,
  `Address` varchar(250) default NULL,
  `CustomerNotes` varchar(500) default NULL,
  `CreatedByUserId` int(10) default NULL,
  `DateCreated` datetime NOT NULL,
  `DateContacted` datetime default NULL,
  `IsImportant` tinyint(4) default NULL,
  `PhoneCallId` int(10) default NULL,
  `PhoneSmsId` int(10) default NULL,
  `WebFormId` int(10) default NULL,
  `LeadTypeId` int(10) default NULL,
  `DateLastUpdated` datetime default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Lead_LeadSource` (`LeadSourceId`),
  KEY `FK_Lead_LeadStatus` (`LeadStatusId`),
  KEY `FK_Lead_PhoneCall` (`PhoneCallId`),
  KEY `FK_Lead_LeadType` (`LeadTypeId`),
  KEY `FK_Lead_User` (`CreatedByUserId`),
  KEY `FK_Lead_User1` (`AssignedToUser`),
  KEY `FK_Lead_LeadForm` (`WebFormId`),
  KEY `FK_Lead_PhoneSms` (`PhoneSmsId`),
  CONSTRAINT `FK_Lead_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_LeadStatus` FOREIGN KEY (`LeadStatusId`) REFERENCES `leadstatus` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_PhoneCall` FOREIGN KEY (`PhoneCallId`) REFERENCES `phonecall` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_LeadType` FOREIGN KEY (`LeadTypeId`) REFERENCES `leadtype` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_User` FOREIGN KEY (`CreatedByUserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_User1` FOREIGN KEY (`AssignedToUser`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_LeadForm` FOREIGN KEY (`WebFormId`) REFERENCES `leadform` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_PhoneSms` FOREIGN KEY (`PhoneSmsId`) REFERENCES `phonesms` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `lead`
--

/*!40000 ALTER TABLE `lead` DISABLE KEYS */;
/*!40000 ALTER TABLE `lead` ENABLE KEYS */;

--
-- Table structure for table `leadaction`
--

DROP TABLE IF EXISTS `leadaction`;
CREATE TABLE `leadaction` (
  `Sequence` int(10) NOT NULL,
  `FromLeadStatusId` int(10) NOT NULL,
  `ToLeadStatusId` int(10) default NULL,
  `Message` varchar(50) default NULL,
  PRIMARY KEY  (`Sequence`,`FromLeadStatusId`),
  KEY `FK_LeadAction_LeadStatus` (`FromLeadStatusId`),
  KEY `FK_LeadAction_LeadStatus1` (`ToLeadStatusId`),
  CONSTRAINT `FK_LeadAction_LeadStatus` FOREIGN KEY (`FromLeadStatusId`) REFERENCES `leadstatus` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_LeadAction_LeadStatus1` FOREIGN KEY (`ToLeadStatusId`) REFERENCES `leadstatus` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leadaction`
--

/*!40000 ALTER TABLE `leadaction` DISABLE KEYS */;
/*!40000 ALTER TABLE `leadaction` ENABLE KEYS */;

--
-- Table structure for table `leadchangehistory`
--

DROP TABLE IF EXISTS `leadchangehistory`;
CREATE TABLE `leadchangehistory` (
  `Id` int(10) NOT NULL auto_increment,
  `LeadId` int(10) NOT NULL,
  `LeadStatusId` int(10) default NULL,
  `DateChanged` datetime NOT NULL,
  `UserId` int(10) NOT NULL,
  `Action` varchar(50) NOT NULL,
  `Description` varchar(250) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_LeadChangeHistory_LeadStatus` (`LeadStatusId`),
  KEY `FK_LeadChangeHistory_Lead` (`LeadId`),
  KEY `FK_LeadChangeHistory_User` (`UserId`),
  CONSTRAINT `FK_LeadChangeHistory_LeadStatus` FOREIGN KEY (`LeadStatusId`) REFERENCES `leadstatus` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_LeadChangeHistory_Lead` FOREIGN KEY (`LeadId`) REFERENCES `lead` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_LeadChangeHistory_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leadchangehistory`
--

/*!40000 ALTER TABLE `leadchangehistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `leadchangehistory` ENABLE KEYS */;

--
-- Table structure for table `leadform`
--

DROP TABLE IF EXISTS `leadform`;
CREATE TABLE `leadform` (
  `Id` int(10) NOT NULL auto_increment,
  `LeadSourceId` int(10) NOT NULL,
  `FirstName` varchar(50) default NULL,
  `LastName` varchar(50) default NULL,
  `Phone` varchar(50) default NULL,
  `Message` varchar(1024) default NULL,
  `DateCreated` datetime NOT NULL,
  `ReferralUri` varchar(1024) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_LeadForm_LeadSource` (`LeadSourceId`),
  CONSTRAINT `FK_LeadForm_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leadform`
--

/*!40000 ALTER TABLE `leadform` DISABLE KEYS */;
/*!40000 ALTER TABLE `leadform` ENABLE KEYS */;

--
-- Table structure for table `leadsource`
--

DROP TABLE IF EXISTS `leadsource`;
CREATE TABLE `leadsource` (
  `Id` int(10) NOT NULL auto_increment,
  `Name` varchar(250) NOT NULL,
  `ParentLeadSourceId` int(10) default NULL,
  `UserId` int(10) default NULL,
  `QbCustomerTypeId` varchar(50) default NULL,
  `QbEmployeeRecordId` varchar(50) default NULL,
  `QbVendorRecordId` varchar(50) default NULL,
  `QbSalesRepRecordId` varchar(50) default NULL,
  `IsActive` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_LeadSource_LeadSource` (`ParentLeadSourceId`),
  KEY `FK_LeadSource_User` (`UserId`),
  CONSTRAINT `FK_LeadSource_LeadSource` FOREIGN KEY (`ParentLeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_LeadSource_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leadsource`
--

/*!40000 ALTER TABLE `leadsource` DISABLE KEYS */;
/*!40000 ALTER TABLE `leadsource` ENABLE KEYS */;

--
-- Table structure for table `leadsourcephone`
--

DROP TABLE IF EXISTS `leadsourcephone`;
CREATE TABLE `leadsourcephone` (
  `Id` int(10) NOT NULL auto_increment,
  `LeadSourceId` int(10) NOT NULL,
  `PhoneNumber` varchar(50) NOT NULL,
  `SimplePhoneNumber` varchar(10) NOT NULL,
  `Description` varchar(250) default NULL,
  `IsRemoved` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_LeadSourcePhone_LeadSource` (`LeadSourceId`),
  CONSTRAINT `FK_LeadSourcePhone_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leadsourcephone`
--

/*!40000 ALTER TABLE `leadsourcephone` DISABLE KEYS */;
/*!40000 ALTER TABLE `leadsourcephone` ENABLE KEYS */;

--
-- Table structure for table `leadsourcetrackingphone`
--

DROP TABLE IF EXISTS `leadsourcetrackingphone`;
CREATE TABLE `leadsourcetrackingphone` (
  `LeadSourceId` int(10) NOT NULL,
  `TrackingPhoneId` int(10) NOT NULL,
  `Notes` varchar(500) default NULL,
  PRIMARY KEY  (`LeadSourceId`,`TrackingPhoneId`),
  KEY `FK_LeadSourceTrackingPhone_TrackingPhone` (`TrackingPhoneId`),
  CONSTRAINT `FK_LeadSourceTrackingPhone_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_LeadSourceTrackingPhone_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leadsourcetrackingphone`
--

/*!40000 ALTER TABLE `leadsourcetrackingphone` DISABLE KEYS */;
/*!40000 ALTER TABLE `leadsourcetrackingphone` ENABLE KEYS */;

--
-- Table structure for table `leadstatus`
--

DROP TABLE IF EXISTS `leadstatus`;
CREATE TABLE `leadstatus` (
  `Id` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leadstatus`
--

/*!40000 ALTER TABLE `leadstatus` DISABLE KEYS */;
/*!40000 ALTER TABLE `leadstatus` ENABLE KEYS */;

--
-- Table structure for table `leadtype`
--

DROP TABLE IF EXISTS `leadtype`;
CREATE TABLE `leadtype` (
  `Id` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `QbJobTypeRecordId` varchar(50) default NULL,
  `IsActive` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leadtype`
--

/*!40000 ALTER TABLE `leadtype` DISABLE KEYS */;
/*!40000 ALTER TABLE `leadtype` ENABLE KEYS */;

--
-- Table structure for table `phonecall`
--

DROP TABLE IF EXISTS `phonecall`;
CREATE TABLE `phonecall` (
  `Id` int(10) NOT NULL auto_increment,
  `TrackingPhoneId` int(10) NOT NULL,
  `IsAnsweredByUser` tinyint(4) default NULL,
  `AnsweredByUserId` int(10) default NULL,
  `CallSid` varchar(50) NOT NULL,
  `AccountSid` varchar(50) NOT NULL,
  `PhoneFrom` varchar(50) NOT NULL,
  `PhoneTo` varchar(50) NOT NULL,
  `CallStatus` varchar(50) NOT NULL,
  `ApiVersion` varchar(50) NOT NULL,
  `Direction` varchar(50) NOT NULL,
  `ForwardedFrom` varchar(50) default NULL,
  `FromCity` varchar(150) default NULL,
  `FromState` varchar(50) default NULL,
  `FromZip` varchar(50) default NULL,
  `FromCountry` varchar(150) default NULL,
  `ToCity` varchar(150) default NULL,
  `ToState` varchar(50) default NULL,
  `ToZip` varchar(50) default NULL,
  `ToCountry` varchar(150) default NULL,
  `CallDuration` varchar(50) default NULL,
  `RecordingUrl` varchar(250) default NULL,
  `CallerName` varchar(150) default NULL,
  `LeadSourceId` int(10) default NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_PhoneCall_LeadSource` (`LeadSourceId`),
  KEY `FK_PhoneCall_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_PhoneCall_User` (`AnsweredByUserId`),
  CONSTRAINT `FK_PhoneCall_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneCall_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneCall_User` FOREIGN KEY (`AnsweredByUserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `phonecall`
--

/*!40000 ALTER TABLE `phonecall` DISABLE KEYS */;
/*!40000 ALTER TABLE `phonecall` ENABLE KEYS */;

--
-- Table structure for table `phonecallworkflow`
--

DROP TABLE IF EXISTS `phonecallworkflow`;
CREATE TABLE `phonecallworkflow` (
  `Id` int(10) NOT NULL auto_increment,
  `TrackingPhoneId` int(10) NOT NULL,
  `CallWorkflowId` int(10) NOT NULL,
  `FromPhoneNumber` varchar(50) default NULL,
  `FromWeekDay` int(10) default NULL,
  `ToWeekDay` int(10) default NULL,
  `FromTime` varchar(5) default NULL,
  `ToTime` varchar(5) default NULL,
  `Priority` int(10) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_PhoneCallWorkflow_CallWorkflow` (`CallWorkflowId`),
  KEY `FK_PhoneCallWorkflow_TrackingPhone` (`TrackingPhoneId`),
  CONSTRAINT `FK_PhoneCallWorkflow_CallWorkflow` FOREIGN KEY (`CallWorkflowId`) REFERENCES `callworkflow` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneCallWorkflow_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `phonecallworkflow`
--

/*!40000 ALTER TABLE `phonecallworkflow` DISABLE KEYS */;
/*!40000 ALTER TABLE `phonecallworkflow` ENABLE KEYS */;

--
-- Table structure for table `phonesms`
--

DROP TABLE IF EXISTS `phonesms`;
CREATE TABLE `phonesms` (
  `Id` int(10) NOT NULL auto_increment,
  `TrackingPhoneId` int(10) NOT NULL,
  `LeadSourceId` int(10) default NULL,
  `SmsSid` varchar(50) NOT NULL,
  `AccountSid` varchar(50) NOT NULL,
  `Message` varchar(512) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `PhoneFrom` varchar(50) NOT NULL,
  `PhoneTo` varchar(50) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_PhoneSms_LeadSource` (`LeadSourceId`),
  KEY `FK_PhoneSms_TrackingPhone` (`TrackingPhoneId`),
  CONSTRAINT `FK_PhoneSms_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneSms_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `phonesms`
--

/*!40000 ALTER TABLE `phonesms` DISABLE KEYS */;
/*!40000 ALTER TABLE `phonesms` ENABLE KEYS */;

--
-- Table structure for table `qbinvoice`
--

DROP TABLE IF EXISTS `qbinvoice`;
CREATE TABLE `qbinvoice` (
  `Id` int(10) NOT NULL auto_increment,
  `LeadId` int(10) NOT NULL,
  `QbInvoiceId` varchar(50) NOT NULL,
  `Amount` decimal(19,4) default NULL,
  `TaxAmount` decimal(19,4) default NULL,
  `TotalAmount` decimal(19,4) default NULL,
  `Status` varchar(50) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_QbInvoice_Lead` (`LeadId`),
  CONSTRAINT `FK_QbInvoice_Lead` FOREIGN KEY (`LeadId`) REFERENCES `lead` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `qbinvoice`
--

/*!40000 ALTER TABLE `qbinvoice` DISABLE KEYS */;
/*!40000 ALTER TABLE `qbinvoice` ENABLE KEYS */;

--
-- Table structure for table `qbmstransaction`
--

DROP TABLE IF EXISTS `qbmstransaction`;
CREATE TABLE `qbmstransaction` (
  `Id` int(10) NOT NULL auto_increment,
  `Ticket` varchar(150) NOT NULL,
  `OpId` varchar(50) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  `OpType` varchar(50) default NULL,
  `Status` varchar(50) default NULL,
  `StatusCode` varchar(50) default NULL,
  `StatusMessage` varchar(250) default NULL,
  `TxnType` varchar(50) default NULL,
  `TxnTimestamp` varchar(50) default NULL,
  `MaskedCCN` varchar(50) default NULL,
  `AuthCode` varchar(50) default NULL,
  `TxnId` varchar(50) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `qbmstransaction`
--

/*!40000 ALTER TABLE `qbmstransaction` DISABLE KEYS */;
/*!40000 ALTER TABLE `qbmstransaction` ENABLE KEYS */;

--
-- Table structure for table `session`
--

DROP TABLE IF EXISTS `session`;
CREATE TABLE `session` (
  `Id` int(10) NOT NULL auto_increment,
  `UserId` int(10) NOT NULL,
  `SessionKey` varchar(50) NOT NULL,
  `Ticket` varchar(150) NOT NULL,
  `SessionStart` datetime NOT NULL,
  `SessionEnd` datetime default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Session_User` (`UserId`),
  CONSTRAINT `FK_Session_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `session`
--

/*!40000 ALTER TABLE `session` DISABLE KEYS */;
/*!40000 ALTER TABLE `session` ENABLE KEYS */;

--
-- Table structure for table `sessionlog`
--

DROP TABLE IF EXISTS `sessionlog`;
CREATE TABLE `sessionlog` (
  `Id` int(10) NOT NULL auto_increment,
  `SessionId` int(10) NOT NULL,
  `DateLog` datetime NOT NULL,
  `Description` varchar(500) default NULL,
  `UserAgent` varchar(250) default NULL,
  `RemoteAddress` varchar(150) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_SessionLog_Session` (`SessionId`),
  CONSTRAINT `FK_SessionLog_Session` FOREIGN KEY (`SessionId`) REFERENCES `session` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sessionlog`
--

/*!40000 ALTER TABLE `sessionlog` DISABLE KEYS */;
/*!40000 ALTER TABLE `sessionlog` ENABLE KEYS */;

--
-- Table structure for table `trackingphone`
--

DROP TABLE IF EXISTS `trackingphone`;
CREATE TABLE `trackingphone` (
  `Id` int(10) NOT NULL auto_increment,
  `Number` varchar(50) NOT NULL,
  `TwilioId` varchar(50) default NULL,
  `Description` varchar(250) default NULL,
  `IsTollFree` tinyint(4) NOT NULL,
  `IsSuspended` tinyint(4) NOT NULL,
  `IsRemoved` tinyint(4) NOT NULL,
  `ScreenNumber` varchar(50) default NULL,
  `TimeLastDisplay` datetime default NULL,
  `SmsResponse` varchar(250) default NULL,
  `DenyTranscription` tinyint(4) NOT NULL,
  `DenyCallerId` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `trackingphone`
--

/*!40000 ALTER TABLE `trackingphone` DISABLE KEYS */;
/*!40000 ALTER TABLE `trackingphone` ENABLE KEYS */;

--
-- Table structure for table `trackingphonerotation`
--

DROP TABLE IF EXISTS `trackingphonerotation`;
CREATE TABLE `trackingphonerotation` (
  `Id` int(10) NOT NULL auto_increment,
  `TimeDisplay` datetime NOT NULL,
  `UserHostAddress` varchar(50) NOT NULL,
  `ParentReferralUri` varchar(1024) NOT NULL,
  `ReferralUri` varchar(1024) NOT NULL,
  `SessionIdUid` varchar(50) NOT NULL,
  `TrackingPhoneId` int(10) default NULL,
  `PhoneCallId` int(10) default NULL,
  `PhoneSmsId` int(10) default NULL,
  `LeadFormId` int(10) default NULL,
  `LeadSourceId` int(10) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_TrackingPhoneRotation_LeadSource` (`LeadSourceId`),
  KEY `FK_TrackingPhoneRotation_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_TrackingPhoneRotation_PhoneCall` (`PhoneCallId`),
  KEY `FK_TrackingPhoneRotation_PhoneSms` (`PhoneSmsId`),
  KEY `FK_TrackingPhoneRotation_LeadForm` (`LeadFormId`),
  CONSTRAINT `FK_TrackingPhoneRotation_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneRotation_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneRotation_PhoneCall` FOREIGN KEY (`PhoneCallId`) REFERENCES `phonecall` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneRotation_PhoneSms` FOREIGN KEY (`PhoneSmsId`) REFERENCES `phonesms` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneRotation_LeadForm` FOREIGN KEY (`LeadFormId`) REFERENCES `leadform` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `trackingphonerotation`
--

/*!40000 ALTER TABLE `trackingphonerotation` DISABLE KEYS */;
/*!40000 ALTER TABLE `trackingphonerotation` ENABLE KEYS */;

--
-- Table structure for table `transaction`
--

DROP TABLE IF EXISTS `transaction`;
CREATE TABLE `transaction` (
  `Id` int(10) NOT NULL auto_increment,
  `TransactionDate` datetime NOT NULL,
  `TransactionTypeId` int(10) NOT NULL,
  `TrackingPhoneId` int(10) default NULL,
  `PhoneCallId` int(10) default NULL,
  `PhoneSmsId` int(10) default NULL,
  `Quantity` decimal(18,3) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  `CurrentBalance` decimal(19,4) NOT NULL,
  `QbmsTransactionId` int(10) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Transaction_TransactionType` (`TransactionTypeId`),
  KEY `FK_Transaction_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_Transaction_PhoneCall` (`PhoneCallId`),
  KEY `FK_Transaction_PhoneSms` (`PhoneSmsId`),
  CONSTRAINT `FK_Transaction_TransactionType` FOREIGN KEY (`TransactionTypeId`) REFERENCES `transactiontype` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Transaction_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Transaction_PhoneCall` FOREIGN KEY (`PhoneCallId`) REFERENCES `phonecall` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Transaction_PhoneSms` FOREIGN KEY (`PhoneSmsId`) REFERENCES `phonesms` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `transaction`
--

/*!40000 ALTER TABLE `transaction` DISABLE KEYS */;
/*!40000 ALTER TABLE `transaction` ENABLE KEYS */;

--
-- Table structure for table `transactiontype`
--

DROP TABLE IF EXISTS `transactiontype`;
CREATE TABLE `transactiontype` (
  `Id` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `Cost` decimal(19,4) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `transactiontype`
--

/*!40000 ALTER TABLE `transactiontype` DISABLE KEYS */;
/*!40000 ALTER TABLE `transactiontype` ENABLE KEYS */;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `Id` int(10) NOT NULL auto_increment,
  `QbUserId` varchar(50) NOT NULL,
  `Email` varchar(150) NOT NULL,
  `Name` varchar(50) default NULL,
  `FirstName` varchar(50) default NULL,
  `LastName` varchar(50) default NULL,
  `Phone` varchar(50) default NULL,
  `Address` varchar(250) default NULL,
  `PhotoFileId` int(10) default NULL,
  `IsActive` tinyint(4) NOT NULL,
  `QbEmployeeRecordId` varchar(50) default NULL,
  `QbVendorRecordId` varchar(50) default NULL,
  `QbSalesRepRecordId` varchar(50) default NULL,
  `RoleName` varchar(50) NOT NULL,
  `DateLastAccess` datetime default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_User_File` (`PhotoFileId`),
  CONSTRAINT `FK_User_File` FOREIGN KEY (`PhotoFileId`) REFERENCES `file` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

--
-- Table structure for table `workflowdetail`
--

DROP TABLE IF EXISTS `workflowdetail`;
CREATE TABLE `workflowdetail` (
  `Id` int(10) NOT NULL auto_increment,
  `CallWorkflowId` int(10) NOT NULL,
  `PropertyName` varchar(50) NOT NULL,
  `PropertyValue` varchar(250) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_WorkflowDetail_CallWorkflow` (`CallWorkflowId`),
  CONSTRAINT `FK_WorkflowDetail_CallWorkflow` FOREIGN KEY (`CallWorkflowId`) REFERENCES `callworkflow` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `workflowdetail`
--

/*!40000 ALTER TABLE `workflowdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `workflowdetail` ENABLE KEYS */;

--
-- Dumping routines for database 'servman_customer_dbo'
--
DELIMITER ;;
DELIMITER ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2011-02-04 13:26:07
