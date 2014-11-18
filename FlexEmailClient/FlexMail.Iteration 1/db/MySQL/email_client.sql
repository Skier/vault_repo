-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version       5.0.26-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema email_client
--

CREATE DATABASE IF NOT EXISTS email_client;
USE email_client;

--
-- Definition of table `account`
--

DROP TABLE IF EXISTS `account`;
CREATE TABLE `account` (
  `AccountId` int(10) unsigned NOT NULL auto_increment,
  `Email` varchar(50) NOT NULL,
  `Pop3SettingsId` int(10) unsigned NOT NULL,
  `SmtpSettingsId` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`AccountId`),
  KEY `FK_pop3_settings` (`Pop3SettingsId`),
  KEY `FK_smtp_settings` (`SmtpSettingsId`),
  CONSTRAINT `FK_pop3_settings` FOREIGN KEY (`Pop3SettingsId`) REFERENCES `serversettings` (`ServerSettingsId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_smtp_settings` FOREIGN KEY (`SmtpSettingsId`) REFERENCES `serversettings` (`ServerSettingsId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `account`
--

--
-- Definition of table `address`
--

DROP TABLE IF EXISTS `address`;
CREATE TABLE `address` (
  `AddressId` int(10) unsigned NOT NULL auto_increment,
  `AccountId` int(10) unsigned NOT NULL,
  `Email` varchar(50) NOT NULL,
  PRIMARY KEY  (`AddressId`),
  KEY `FK_account` (`AccountId`),
  CONSTRAINT `FK_account` FOREIGN KEY (`AccountId`) REFERENCES `account` (`AccountId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `address`
--

--
-- Definition of table `serversettings`
--

DROP TABLE IF EXISTS `serversettings`;
CREATE TABLE `serversettings` (
  `ServerSettingsId` int(10) unsigned NOT NULL auto_increment,
  `Host` varchar(50) NOT NULL,
  `Port` int(10) unsigned NOT NULL,
  `UserName` varchar(50) NOT NULL,
  `UserPassword` varchar(50) NOT NULL,
  PRIMARY KEY  (`ServerSettingsId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `serversettings`
--


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
