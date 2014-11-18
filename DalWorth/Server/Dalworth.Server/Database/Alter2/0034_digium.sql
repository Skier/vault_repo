DROP TABLE IF EXISTS `transaction`;
DROP TABLE IF EXISTS `digiumlogitem`;
DROP TABLE IF EXISTS `ordersourceownphone`;
DROP TABLE IF EXISTS `trackingphoneordersource`;
DROP TABLE IF EXISTS `trackingphone`;
DROP TABLE IF EXISTS `order`;
DROP TABLE IF EXISTS `partnerinvitation`;
DROP TABLE IF EXISTS `ordersource`;
DROP TABLE IF EXISTS `webuser`;


CREATE TABLE `ordersource` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `ParentOrderSourceId` int(10) DEFAULT NULL,
  `Name` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_OrderSource_OrderSource` (`ParentOrderSourceId`),
  CONSTRAINT `FK_OrderSource_OrderSource` FOREIGN KEY (`ParentOrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `webuser`
--

CREATE TABLE `webuser` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `OrderSourceId` int(10) DEFAULT NULL,
  `Login` varchar(100) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `EmployeeId` int(10) DEFAULT NULL,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `IsOwner` tinyint(1) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `IX_Login` (`Login`),
  KEY `FK_WebUser_Employee` (`EmployeeId`),
  KEY `FK_WebUser_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_WebUser_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WebUser_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


--
-- Table structure for table `ordersource`
--

CREATE TABLE `digiumlogitem` (
  `ID` bigint(19) NOT NULL AUTO_INCREMENT,
  `CallId` varchar(30) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeTalkStarted` datetime NOT NULL,
  `DurationSec` int(10) NOT NULL,
  `CallerIdNumber` varchar(15) NOT NULL,
  `CallerName` varchar(200) NOT NULL,
  `Extension` varchar(5) NOT NULL,
  `ExtensionType` varchar(20) NOT NULL,
  `IncomingDid` varchar(15) NOT NULL,
  `IsIntermediateCall` tinyint(1) NOT NULL,
  `CallSourceId` int(10) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_DigiumCallId` (`CallId`),
  KEY `IX_DigiumLogItem_TimeCreated` (`TimeCreated`),
  KEY `FK_DigiumLogItem_OrderSource` (`CallSourceId`),
  CONSTRAINT `FK_DigiumLogItem_OrderSource` FOREIGN KEY (`CallSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `order`
--

CREATE TABLE `order` (
  `TicketNumber` varchar(6) NOT NULL,
  `CustomerId` int(10) NOT NULL,
  `OrderSourceId` int(10) DEFAULT NULL,
  `ScheduleDate` datetime NOT NULL,
  `ServiceType` int(10) NOT NULL,
  `TransactionType` int(10) NOT NULL,
  `TransactionStatus` int(10) NOT NULL,
  `CompletionType` int(10) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  PRIMARY KEY (`TicketNumber`),
  KEY `FK_Order_Customer` (`CustomerId`),
  KEY `FK_Order_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_Order_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Order_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


CREATE TABLE `transaction` (
  `ID` bigint(19) NOT NULL AUTO_INCREMENT,
  `ServmanTransactionId` varchar(7) NOT NULL,
  `ServmanWorkflowId` varchar(7) NOT NULL,
  `TicketNumber` varchar(6) DEFAULT NULL,
  `CustomerId` int(10) DEFAULT NULL,
  `CustomerName` varchar(40) NOT NULL,
  `ServiceType` int(10) NOT NULL,
  `TransactionCode` varchar(4) NOT NULL,
  `UserName` varchar(40) NOT NULL,
  `Extension` varchar(3) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `DigiumLogItemId` bigint(19) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_ServmanTransactionId` (`ServmanTransactionId`),
  KEY `FK_Transaction_Customer` (`CustomerId`),
  KEY `FK_Transaction_DigiumLogItem` (`DigiumLogItemId`),
  KEY `FK_Transaction_Order` (`TicketNumber`),
  CONSTRAINT `FK_Transaction_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Transaction_DigiumLogItem` FOREIGN KEY (`DigiumLogItemId`) REFERENCES `digiumlogitem` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Transaction_Order` FOREIGN KEY (`TicketNumber`) REFERENCES `order` (`TicketNumber`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `ordersourceownphone`
--

CREATE TABLE `ordersourceownphone` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `OrderSourceId` int(10) NOT NULL,
  `Number` varchar(20) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_OrderSourceOwnPhone_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_OrderSourceOwnPhone_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `partnerinvitation`
--

CREATE TABLE `partnerinvitation` (
  `InvitationKey` varchar(255) NOT NULL,
  `OrderSourceId` int(10) DEFAULT NULL,
  `WebUserId` int(10) DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  `ExpirationDate` datetime NOT NULL,
  `IsInvitationSent` tinyint(1) NOT NULL,
  PRIMARY KEY (`InvitationKey`),
  KEY `FK_PartnerInvitation_OrderSource` (`OrderSourceId`),
  KEY `FK_PartnerInvitation_WebUser` (`WebUserId`),
  CONSTRAINT `FK_PartnerInvitation_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_PartnerInvitation_WebUser` FOREIGN KEY (`WebUserId`) REFERENCES `webuser` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `trackingphone`
--

CREATE TABLE `trackingphone` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `Number` varchar(20) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `trackingphoneordersource`
--


CREATE TABLE `trackingphoneordersource` (
  `TrackingPhoneId` int(10) NOT NULL,
  `OrderSourceId` int(10) NOT NULL,
  PRIMARY KEY (`TrackingPhoneId`,`OrderSourceId`),
  KEY `FK_TrackingPhoneOrderSource_OrderSource` (`OrderSourceId`),
  CONSTRAINT `FK_TrackingPhoneOrderSource_TrackingPhone` FOREIGN KEY (`TrackingPhoneId`) REFERENCES `trackingphone` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrackingPhoneOrderSource_OrderSource` FOREIGN KEY (`OrderSourceId`) REFERENCES `ordersource` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


insert into BackgroundJobType(id, type, description) 
values (7, 'PartnerSiteInvitation', 'Send invitation to Partner in profit program');

insert into BackgroundJobType(id, type, description) 
values (8, 'PartnerSitePasswordReminder', 'Send password reset instructions');

ALTER TABLE `backgroundjobpending` ADD COLUMN `PartnerInvitationKey` VARCHAR(255) AFTER `ProjectFeedbackId`,
 ADD CONSTRAINT `FK_BackgroundJobPending_PartnerInvitation` FOREIGN KEY `FK_BackgroundJobPending_PartnerInvitation` (`PartnerInvitationKey`)
    REFERENCES `partnerinvitation` (`InvitationKey`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

INSERT INTO `sysversion` (`Version`) VALUES
(34);