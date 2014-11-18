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
-- Table structure for table `callworkflow`
--

DROP TABLE IF EXISTS `callworkflow`;
CREATE TABLE `callworkflow` (
  `Id` int(10) NOT NULL auto_increment,
  `Description` varchar(50) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
-- Table structure for table `leadstatus`
--

DROP TABLE IF EXISTS `leadstatus`;
CREATE TABLE `leadstatus` (
  `Id` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
-- Table structure for table `leadsource`
--

DROP TABLE IF EXISTS `leadsource`;
CREATE TABLE `leadsource` (
  `Id` int(10) NOT NULL auto_increment,
  `Name` varchar(250) NOT NULL,
  `UserId` int(10) default NULL,
  `IsActive` tinyint(4) NOT NULL,
  `OwnedByUserId` int(10) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_LeadSource_User` (`UserId`),
  KEY `FK_LeadSource_User1` (`OwnedByUserId`),
  CONSTRAINT `FK_LeadSource_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_LeadSource_User1` FOREIGN KEY (`OwnedByUserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
-- Table structure for table `leadsourcetrackingphone`
--

DROP TABLE IF EXISTS `leadsourcetrackingphone`;
CREATE TABLE `leadsourcetrackingphone` (
  `LeadSourceId` int(10) NOT NULL,
  `TrackingPhoneId` int(10) NOT NULL,
  `Notes` varchar(500) default NULL,
  PRIMARY KEY  (`LeadSourceId`,`TrackingPhoneId`),
  KEY `FK_LeadSourceTrackingPhone_TrackingPhone` (`TrackingPhoneId`),
  CONSTRAINT `FK_LeadSourceTrackingPhone_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_LeadSourceTrackingPhone_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  KEY `FK_PhoneCall_User` (`AnsweredByUserId`),
  KEY `FK_PhoneCall_TrackingPhone` (`TrackingPhoneId`),
  CONSTRAINT `FK_PhoneCall_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneCall_User` FOREIGN KEY (`AnsweredByUserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneCall_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  KEY `FK_PhoneSms_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_PhoneSms_LeadSource` (`LeadSourceId`),
  CONSTRAINT `FK_PhoneSms_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneSms_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  KEY `FK_TrackingPhoneRotation_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_TrackingPhoneRotation_PhoneCall` (`PhoneCallId`),
  KEY `FK_TrackingPhoneRotation_LeadSource` (`LeadSourceId`),
  KEY `FK_TrackingPhoneRotation_PhoneSms` (`PhoneSmsId`),
  KEY `FK_TrackingPhoneRotation_LeadForm` (`LeadFormId`),
  CONSTRAINT `FK_TrackingPhoneRotation_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneRotation_PhoneCall` FOREIGN KEY (`PhoneCallId`) REFERENCES `phonecall` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneRotation_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneRotation_PhoneSms` FOREIGN KEY (`PhoneSmsId`) REFERENCES `phonesms` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneRotation_LeadForm` FOREIGN KEY (`LeadFormId`) REFERENCES `leadform` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  KEY `FK_PhoneCallWorkflow_TrackingPhone` (`TrackingPhoneId`),
  KEY `FK_PhoneCallWorkflow_CallWorkflow` (`CallWorkflowId`),
  CONSTRAINT `FK_PhoneCallWorkflow_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PhoneCallWorkflow_CallWorkflow` FOREIGN KEY (`CallWorkflowId`) REFERENCES `callworkflow` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  `DateLastUpdated` datetime default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Lead_LeadSource` (`LeadSourceId`),
  KEY `FK_Lead_LeadStatus` (`LeadStatusId`),
  KEY `FK_Lead_PhoneCall` (`PhoneCallId`),
  KEY `FK_Lead_User` (`CreatedByUserId`),
  KEY `FK_Lead_User1` (`AssignedToUser`),
  KEY `FK_Lead_LeadForm` (`WebFormId`),
  KEY `FK_Lead_PhoneSms` (`PhoneSmsId`),
  CONSTRAINT `FK_Lead_LeadSource` FOREIGN KEY (`LeadSourceId`) REFERENCES `leadsource` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_LeadStatus` FOREIGN KEY (`LeadStatusId`) REFERENCES `leadstatus` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_PhoneCall` FOREIGN KEY (`PhoneCallId`) REFERENCES `phonecall` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_User` FOREIGN KEY (`CreatedByUserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_User1` FOREIGN KEY (`AssignedToUser`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_LeadForm` FOREIGN KEY (`WebFormId`) REFERENCES `leadform` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_PhoneSms` FOREIGN KEY (`PhoneSmsId`) REFERENCES `phonesms` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
-- Insert start data
--
INSERT INTO `leadstatus` (`Id`, `Name`) VALUES
(1,'NEW'),
(2,'PENDING'),
(3,'CANCELLED'),
(4,'CONVERTED');

INSERT INTO `transactiontype` (`Id`, `Name`, `Cost`) VALUES
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

INSERT INTO `callworkflow` (`Id`, `Description`) VALUES (1,'Voice mail'),(2,'Redirect call');

INSERT INTO `workflowdetail` (`Id`, `CallWorkflowId`, `PropertyName`, `PropertyValue`) VALUES
(1,1,'Welcome message', 'Thank you for calling.'),
(2,1,'Message', 'Please leave your message after the tone.'),
(3,2,'Redirect phone number', '');
