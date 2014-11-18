--
-- Table structure for table `leadstatus`
--

DROP TABLE IF EXISTS `leadstatus`;
CREATE TABLE `leadstatus` (
  `Id` int(10) NOT NULL auto_increment,
  `StatusName` varchar(50) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


--
-- Table structure for table `phoneblacklist` 
--

DROP TABLE IF EXISTS `PhoneBlackList`;
CREATE TABLE `PhoneBlackList` (
  `Id` int(10) NOT NULL auto_increment,
  `PhoneNumber` varchar(50) NOT NULL,
  `Description` varchar(250) ,
  `PhoneDigits` varchar(50),
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


--
-- Table structure for table `businesspartner`
--

DROP TABLE IF EXISTS `businesspartner`;
CREATE TABLE `businesspartner` (
  `Id` int(10) NOT NULL auto_increment,
  `PartnerName` varchar(50) default NULL,
  `Email` varchar(150) default NULL,
  `Phone` varchar(50) default NULL,
  `PhoneDigits` varchar(50),
  `SalesRepId` int(10),
  `IsRemoved` tinyint(4) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `Address` varchar(500) ,        
  `IsExcludedFromReports` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `partnerphonenumber`
--

DROP TABLE IF EXISTS `partnerphonenumber`;
CREATE TABLE `PartnerPhoneNumber` (
  `Id` int(10) NOT NULL auto_increment,
  `BusinessPartnerId` int(10) NOT NULL,
  `PhoneNumber` varchar(50) NOT NULL,
  `Description` varchar(250) ,
  `PhoneDigits` varchar(10),
  PRIMARY KEY  (`Id`),
  KEY `FK_PartnerPhoneNumber_BusinessPartner` (`BusinessPartnerId`),
  CONSTRAINT `FK_PartnerPhoneNumber_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `BusinessPartner` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `Id` int(10) NOT NULL auto_increment,
  `DateCreated` datetime NOT NULL,
  `Email` varchar(150) default NULL,
  `FirstName` varchar(50) default NULL,
  `LastName` varchar(50) default NULL,
  `ScreenName` varchar(100) default NULL,
  `Phone` varchar(50) default NULL,
  `Address` varchar(250) default NULL,
  `QbUserId` varchar(50) default NULL,
  `QbRoleName` varchar(50) default NULL,
  `BusinessPartnerId` int(10) default NULL,
  `DateLastAccess` datetime default NULL,
  `IsActive` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_User_BusinessPartner` (`BusinessPartnerId`),
  CONSTRAINT `FK_User_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `businesspartner` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `activitylog`
--

DROP TABLE IF EXISTS `activitylog`;
CREATE TABLE `activitylog` (
  `Id` int(10) NOT NULL auto_increment,
  `DateCreated` datetime NOT NULL,
  `UserId` int(10) default NULL,
  `ActivityNotes` varchar(1024) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_ActivityLog_User` (`UserId`),
  CONSTRAINT `FK_ActivityLog_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `campaign`
--

DROP TABLE IF EXISTS `campaign`;
CREATE TABLE `campaign` (
  `Id` int(10) NOT NULL auto_increment,
  `CampaignName` varchar(250) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateStart` datetime NOT NULL,
  `DateEnd` datetime default NULL,
  `BusinessPartnerId` int(10) default NULL,
  `UserId` int(10) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Campaign_User` (`UserId`),
  KEY `FK_Campaign_BusinessPartner` (`BusinessPartnerId`),
  CONSTRAINT `FK_Campaign_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Campaign_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `businesspartner` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `trackingphone`
--

DROP TABLE IF EXISTS `trackingphone`;
CREATE TABLE `trackingphone` (
  `Id` int(10) NOT NULL auto_increment,
  `PhoneNumber` varchar(50) NOT NULL,
  `FriendlyNumber` varchar(50) default NULL,
  `Description` varchar(250) default NULL,
  `RedirectPhoneNumber` varchar(50) NOT NULL,
  `TwilioNumberId` varchar(50) default NULL,
  `DateCreated` datetime default NULL,
  `BusinessPartnerId` int(10) default NULL,
  `IsSuspended` tinyint(4) NOT NULL,
  `IsRemoved` tinyint(4) NOT NULL,
  `CallerIdLookup` tinyint(4) NOT NULL,
  `TranscribeCalls` tinyint(4) NOT NULL,
  `IsTollFree` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `compaigntrackingphone`
--

DROP TABLE IF EXISTS `compaigntrackingphone`;
CREATE TABLE `compaigntrackingphone` (
  `Id` int(10) NOT NULL auto_increment,
  `CampaignId` int(10) NOT NULL,
  `TrackingPhoneId` int(10) NOT NULL,
  `DateAssigned` datetime NOT NULL,
  `DateReleased` datetime default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_CompaignTrackingPhone_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_CompaignTrackingPhone_Campaign` (`CampaignId`),
  CONSTRAINT `FK_CompaignTrackingPhone_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_CompaignTrackingPhone_Campaign` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `trackingphonerotation`
--

DROP TABLE IF EXISTS `trackingphonerotation`;
CREATE TABLE `trackingphonerotation` (
  `Id` int(10) NOT NULL auto_increment,
  `CampaignId` int(10) NOT NULL,
  `TrackingPhoneId` int(10) NOT NULL,
  `ShowedPhoneNumbere` varchar(50) NOT NULL,
  `TimeRotation` datetime NOT NULL,
  `SessionUid` varchar(50) NOT NULL,
  `UserHosAddress` varchar(150) NOT NULL,
  `RotationPageUri` varchar(1024) NOT NULL,
  `ReferralUri` varchar(1024) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_TrackingPhoneRotation_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_TrackingPhoneRotation_Campaign` (`CampaignId`),
  CONSTRAINT `FK_TrackingPhoneRotation_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneRotation_Campaign` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `phonecall`
--

DROP TABLE IF EXISTS `phonecall`;
CREATE TABLE `phonecall` (
  `Id` int(10) NOT NULL auto_increment,
  `TrackingPhoneId` int(10) NOT NULL,
  `TrackingPhoneNumber` varchar(50) default NULL,
  `CallDuration` int(10) default NULL,
  `RecordingUrl` varchar(250) default NULL,
  `DateCreated` datetime NOT NULL,
  `Status` varchar(50) default NULL,
  `CampaignId` int(10) default NULL,
  `CallerName` varchar(150) default NULL,
  `FromPhone` varchar(50) default NULL,
  `FromCity` varchar(150) default NULL,
  `FromState` varchar(150) default NULL,
  `FromZip` varchar(50) default NULL,
  `FromCountry` varchar(150) default NULL,
  `TwilioCallId` varchar(50) default NULL,
  `TrackingPhoneRotationId` int(10) default NULL,
  `TwilioRecordingUrl` varchar(250),
  `IsProcessed` tinyint(4),                 
  `PhoneBlackListId` int(10),                   
  `Notes` varchar(250),
  PRIMARY KEY  (`Id`),
  KEY `FK_PhoneCall_TrackingPhoneRotation` (`TrackingPhoneRotationId`),
  KEY `FK_PhoneCall_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_PhoneCall_Campaign` (`CampaignId`),
  KEY `FK_PhoneCall_PhoneBlackList` (`PhoneBlackListId`),
  CONSTRAINT `FK_PhoneCall_TrackingPhoneRotation` FOREIGN KEY (`TrackingPhoneRotationId`) REFERENCES `trackingphonerotation` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneCall_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneCall_Campaign` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneCall_PhoneBlackList` FOREIGN KEY (`PhoneBlackListId`) REFERENCES `PhoneBlackList` (`Id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `phonesms`
--

DROP TABLE IF EXISTS `phonesms`;
CREATE TABLE `phonesms` (
  `Id` int(10) NOT NULL auto_increment,
  `TrackingPhoneId` int(10) NOT NULL,
  `TrackingPhoneNumber` varchar(50) default NULL,
  `Message` varchar(1024) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `CampaignId` int(10) default NULL,
  `FromPhone` varchar(50) default NULL,
  `Status` varchar(50) default NULL,
  `TwilioSmsId` varchar(50) default NULL,
  `TrackingPhoneRotationId` int(10) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_PhoneSms_TrackingPhoneRotation` (`TrackingPhoneRotationId`),
  KEY `FK_PhoneSms_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_PhoneSms_Campaign` (`CampaignId`),
  CONSTRAINT `FK_PhoneSms_TrackingPhoneRotation` FOREIGN KEY (`TrackingPhoneRotationId`) REFERENCES `trackingphonerotation` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneSms_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneSms_Campaign` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `webform`
--

DROP TABLE IF EXISTS `webform`;
CREATE TABLE `webform` (
  `Id` int(10) NOT NULL auto_increment,
  `CampagnId` int(10) default NULL,
  `DateCreated` datetime NOT NULL,
  `FirstName` varchar(150) NOT NULL,
  `LastName` varchar(150) NOT NULL,
  `Phone` varchar(150) NOT NULL,
  `Message` varchar(150) NOT NULL,
  `WebPageUri` varchar(1024) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_WebForm_Campaign` (`CampagnId`),
  CONSTRAINT `FK_WebForm_Campaign` FOREIGN KEY (`CampagnId`) REFERENCES `campaign` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `source`
--

DROP TABLE IF EXISTS `source`;
CREATE TABLE `source` (
  `Id` int(10) NOT NULL auto_increment,
  `PhoneCallId` int(10) default NULL,
  `PhoneSmsId` int(10) default NULL,
  `WebFormId` int(10) default NULL,
  `UserId` int(10) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Source_User` (`UserId`),
  KEY `FK_Source_PhoneCall` (`PhoneCallId`),
  KEY `FK_Source_PhoneSms` (`PhoneSmsId`),
  KEY `FK_Source_WebForm` (`WebFormId`),
  CONSTRAINT `FK_Source_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Source_PhoneCall` FOREIGN KEY (`PhoneCallId`) REFERENCES `phonecall` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Source_PhoneSms` FOREIGN KEY (`PhoneSmsId`) REFERENCES `phonesms` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Source_WebForm` FOREIGN KEY (`WebFormId`) REFERENCES `webform` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `lead`
--

DROP TABLE IF EXISTS `lead`;
CREATE TABLE `lead` (
  `Id` int(10) NOT NULL auto_increment,
  `LeadStatusId` int(10) NOT NULL,
  `CampaignId` int(10) default NULL,
  `BusinessPartnerId` int(10),
  `DateCreated` datetime NOT NULL,
  `FirstName` varchar(150) ,
  `LastName` varchar(150) ,
  `Phone` varchar(50) ,
  `Address` varchar(250) ,
  `CustomerNotes` varchar(1024) ,
  `SourceId` int(10) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Lead_LeadStatus` (`LeadStatusId`),
  KEY `FK_Lead_Source` (`SourceId`),
  KEY `FK_Lead_Campaign` (`CampaignId`),
  KEY `FK_Lead_BusinessPartner` (`BusinessPartnerId`),
  CONSTRAINT `FK_Lead_LeadStatus` FOREIGN KEY (`LeadStatusId`) REFERENCES `leadstatus` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_Source` FOREIGN KEY (`SourceId`) REFERENCES `source` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_Campaign` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `businesspartner` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbinvoice`
--

DROP TABLE IF EXISTS `qbinvoice`;
CREATE TABLE `qbinvoice` (
  `Id` int(10) NOT NULL auto_increment,
  `LeadId` int(10) NOT NULL,
  `QbInvoiceId` varchar(50) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_QbInvoice_Lead` (`LeadId`),
  CONSTRAINT `FK_QbInvoice_Lead` FOREIGN KEY (`LeadId`) REFERENCES `lead` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `transactiontype`
--

DROP TABLE IF EXISTS `transactiontype`;
CREATE TABLE `transactiontype` (
  `Id` int(10) NOT NULL auto_increment,
  `TypeName` varchar(50) NOT NULL,
  `BaseCost` decimal(19,4) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `transaction`
--

DROP TABLE IF EXISTS `transaction`;
CREATE TABLE `transaction` (
  `Id` int(10) NOT NULL auto_increment,
  `TransactionTypeId` int(10) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `SourceId` int(10) default NULL,
  `Description` varchar(500) ,
  `Cost` decimal(19,4) NOT NULL,
  `Quantity` decimal(18,3) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  `CurrentBalance` decimal(19,4) NOT NULL,
  `QbmsTransactionId` int(10) default NULL,
  `CampaignId` int(10) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Transaction_TransactionType` (`TransactionTypeId`),
  KEY `FK_Transaction_Source` (`SourceId`),
  KEY `FK_Transaction_Campaign` (`CampaignId`),
  CONSTRAINT `FK_Transaction_TransactionType` FOREIGN KEY (`TransactionTypeId`) REFERENCES `transactiontype` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Transaction_Source` FOREIGN KEY (`SourceId`) REFERENCES `source` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Transaction_Campaign` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


--
-- Insert start data
--
INSERT INTO `leadstatus` (`Id`, `StatusName`) VALUES
(1,'NEW'),
(2,'PENDING'),
(3,'CANCELLED'),
(4,'CONVERTED');

INSERT INTO `transactiontype` (`Id`, `TypeName`, `BaseCost`) VALUES
(1,'IncomeCall',-0.02),
(2,'OutcomeCall',-0.04),
(3,'IncomeSms',-0.06),
(4,'OutcomeSms',-0.06),
(5,'CallerIdLookup',-0.02),
(6,'VoiceTranscribe',-0.10),
(7,'ApplicationCharge',-20.00),
(8,'PhoneNumberCharge',-2.00),
(9,'RecurringPayment',0.00),
(10,'ExtraPayment',0.00),
(11,'IncomeTollFreeCall',-0.04);

--
-- Alter Table structure for table `businesspartner` Add SalesRep foreign key
--
ALTER TABLE `businesspartner`
 ADD CONSTRAINT `FK_BusinessPartner_User` FOREIGN KEY (`SalesRepId`)
    REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

DROP TABLE IF EXISTS `notificationtype`;
CREATE TABLE  `notificationtype` (
  `Id` int(10) NOT NULL auto_increment,
  `TypeName` varchar(50) NOT NULL,
  `SendToAdmin` tinyint(4) NOT NULL,
  `SendToPartner` tinyint(4) NOT NULL,
  `SendToPartnerUsers` tinyint(4) NOT NULL,
  `SendToStaff` tinyint(4) NOT NULL,
  `SendToSalesRep` tinyint(4) NOT NULL,
  `SendToAccountant` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


DROP TABLE IF EXISTS `notification`;
CREATE TABLE  `notification` (
  `Id` int(10) NOT NULL auto_increment,
  `NotificationTypeId` int(10) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateProcessed` datetime default NULL,
  `IsProcessed` tinyint(4) NOT NULL,
  `FromEmail` varchar(150) NOT NULL,
  `ToEmail` varchar(150) NOT NULL,
  `Message` varchar(5000) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Notification_NotificationType` (`NotificationTypeId`),
  CONSTRAINT `FK_Notification_NotificationType` FOREIGN KEY (`NotificationTypeId`) REFERENCES `notificationtype` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `notificationtype` (`Id`,`TypeName`,`SendToAdmin`,`SendToPartner`,`SendToPartnerUsers`,`SendToStaff`,`SendToSalesRep`,`SendToAccountant`) VALUES
(1,'Create Lead',1,1,1,1,1,0),
(2,'Cancel Lead',1,1,0,0,1,0),
(3,'Set Lead Pending',1,1,0,1,1,0),
(4,'Complete Lead',1,1,1,0,1,0),
(5,'Match Lead to QB Invoice',1,1,0,0,1,1),
(6,'Low Balance',1,0,0,0,0,0),
(7,'Empty Balance',1,0,0,0,0,0),
(8,'Reject Call',1,0,0,1,1,0),
(9,'Purchase Phone Number',1,0,0,0,0,1),
(10,'Weekly Summary',1,0,0,0,0,1);
