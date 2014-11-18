--
-- Table structure for table `servmancustomer`
--

DROP TABLE IF EXISTS `servmancustomer`;
CREATE TABLE `servmancustomer` (
  `Id` int(10) NOT NULL auto_increment,
  `RealmId` varchar(50) NOT NULL,
  `CreationDate` datetime NOT NULL,
  `LastLoginDate` datetime NOT NULL,
  `DbName` varchar(50) NOT NULL,
  `Login` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Name` varchar(50) default NULL,
  `Email` varchar(50) default NULL,
  `Phone` varchar(50) default NULL,
  `Description` varchar(500) default NULL,
  `AppDbId` varchar(50) default NULL,
  `IsLeadSourcesInited` tinyint(4) NOT NULL,
  `IsOAuthInited` tinyint(4) NOT NULL,
  `IsWorkflowsInited` tinyint(4) NOT NULL,
  `IsQBO` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `applicationstatus`
--

DROP TABLE IF EXISTS `applicationstatus`;
CREATE TABLE `applicationstatus` (
  `Id` int(10) NOT NULL auto_increment,
  `ServmanCustomerId` int(10) NOT NULL,
  `BillingStatus` varchar(50) NOT NULL,
  `LastPaymentDate` datetime NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_ApplicationStatus_ServmanCustomer` (`ServmanCustomerId`),
  CONSTRAINT `FK_ApplicationStatus_ServmanCustomer` FOREIGN KEY (`ServmanCustomerId`) REFERENCES `servmancustomer` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `oauthconnection`
--

DROP TABLE IF EXISTS `oauthconnection`;
CREATE TABLE `oauthconnection` (
  `Id` int(10) NOT NULL auto_increment,
  `ServmanCustomerId` int(10) NOT NULL,
  `ParentConsumerKey` varchar(50) NOT NULL,
  `RequestTokenUrl` varchar(250) NOT NULL,
  `DynamicKeyRetrievalUrl` varchar(250) NOT NULL,
  `AccessTokenUrl` varchar(250) NOT NULL,
  `AuthorizeRequestUrl` varchar(250) NOT NULL,
  `ConsumerKey` varchar(50) NOT NULL,
  `ConsumerSecret` varchar(50) NOT NULL,
  `AccessToken` varchar(150) NOT NULL,
  `AccessTokenSecret` varchar(150) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `IsActive` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_OAuthConnection_ServmanCustomer1` (`ServmanCustomerId`),
  CONSTRAINT `FK_OAuthConnection_ServmanCustomer1` FOREIGN KEY (`ServmanCustomerId`) REFERENCES `servmancustomer` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `qbmstransaction`
--

DROP TABLE IF EXISTS `qbmstransaction`;
CREATE TABLE `qbmstransaction` (
  `Id` int(10) NOT NULL auto_increment,
  `ServmanCustomerId` int(10) NOT NULL,
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
  PRIMARY KEY  (`Id`),
  KEY `FK_QbmsTransaction_ServmanCustomer` (`ServmanCustomerId`),
  CONSTRAINT `FK_QbmsTransaction_ServmanCustomer` FOREIGN KEY (`ServmanCustomerId`) REFERENCES `servmancustomer` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `servmansession`
--

DROP TABLE IF EXISTS `servmansession`;
CREATE TABLE `servmansession` (
  `Id` int(10) NOT NULL auto_increment,
  `ServmanCustomerId` int(10) NOT NULL,
  `QbUserId` varchar(50) NOT NULL,
  `Ticket` varchar(50) NOT NULL,
  `AppToken` varchar(50) NOT NULL,
  `IntuitTicket` varchar(250) NOT NULL,
  `SessionStart` datetime NOT NULL,
  `IsActive` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_ServmanSession_ServmanCustomer1` (`ServmanCustomerId`),
  CONSTRAINT `FK_ServmanSession_ServmanCustomer1` FOREIGN KEY (`ServmanCustomerId`) REFERENCES `servmancustomer` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
