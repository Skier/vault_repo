-- MySQL dump 10.10
--
-- Host: localhost    Database: servman_main_dbo
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
-- Current Database: `servman_main_dbo`
--

USE `servman_main_dbo`;

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
-- Dumping data for table `applicationstatus`
--

/*!40000 ALTER TABLE `applicationstatus` DISABLE KEYS */;
/*!40000 ALTER TABLE `applicationstatus` ENABLE KEYS */;

--
-- Table structure for table `oauthconnection`
--

DROP TABLE IF EXISTS `oauthconnection`;
CREATE TABLE `oauthconnection` (
  `Id` int(10) NOT NULL,
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
-- Dumping data for table `oauthconnection`
--

/*!40000 ALTER TABLE `oauthconnection` DISABLE KEYS */;
/*!40000 ALTER TABLE `oauthconnection` ENABLE KEYS */;

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
-- Dumping data for table `qbmstransaction`
--

/*!40000 ALTER TABLE `qbmstransaction` DISABLE KEYS */;
/*!40000 ALTER TABLE `qbmstransaction` ENABLE KEYS */;

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
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `servmancustomer`
--

/*!40000 ALTER TABLE `servmancustomer` DISABLE KEYS */;
/*!40000 ALTER TABLE `servmancustomer` ENABLE KEYS */;

--
-- Table structure for table `servmansession`
--

DROP TABLE IF EXISTS `servmansession`;
CREATE TABLE `servmansession` (
  `Id` int(10) NOT NULL,
  `ServmanCustomerId` int(10) NOT NULL,
  `QbUserId` varchar(50) NOT NULL,
  `SessionId` varchar(50) NOT NULL,
  `DbId` varchar(50) NOT NULL,
  `AppToken` varchar(50) NOT NULL,
  `Ticket` varchar(250) NOT NULL,
  `SessionStart` datetime NOT NULL,
  `IsActive` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_ServmanSession_ServmanCustomer1` (`ServmanCustomerId`),
  CONSTRAINT `FK_ServmanSession_ServmanCustomer1` FOREIGN KEY (`ServmanCustomerId`) REFERENCES `servmancustomer` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `servmansession`
--

/*!40000 ALTER TABLE `servmansession` DISABLE KEYS */;
/*!40000 ALTER TABLE `servmansession` ENABLE KEYS */;

--
-- Dumping routines for database 'servman_main_dbo'
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

-- Dump completed on 2011-01-25 21:07:30
