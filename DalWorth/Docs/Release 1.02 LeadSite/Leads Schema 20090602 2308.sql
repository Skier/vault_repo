-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.67-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema dalworth_server_dbo
--

CREATE DATABASE IF NOT EXISTS dalworth_server_dbo;
USE dalworth_server_dbo;

--
-- Definition of table `businesspartner`
--

DROP TABLE IF EXISTS `businesspartner`;
CREATE TABLE `businesspartner` (
  `ID` int(10) unsigned NOT NULL auto_increment,
  `Name` varchar(45) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `businesspartner`
--

/*!40000 ALTER TABLE `businesspartner` DISABLE KEYS */;
INSERT INTO `businesspartner` (`ID`,`Name`) VALUES 
 (1,'dalworthrugcleaning.com');
/*!40000 ALTER TABLE `businesspartner` ENABLE KEYS */;


--
-- Definition of table `customerwebaccount`
--

DROP TABLE IF EXISTS `customerwebaccount`;
CREATE TABLE `customerwebaccount` (
  `ID` int(10) unsigned NOT NULL auto_increment,
  `CustomerId` int(10) unsigned default NULL,
  `BusinessPartnerId` int(10) unsigned default NULL,
  `Company` varchar(40) default NULL,
  `FirstName` varchar(40) NOT NULL,
  `LastName` varchar(40) NOT NULL,
  `Address1` varchar(40) NOT NULL,
  `Address2` varchar(40) default NULL,
  `City` varchar(24) NOT NULL,
  `State` varchar(2) NOT NULL,
  `Zip` int (10) NOT NULL,
  `Phone`  varchar(10) NOT NULL,
  `LastModifiedDate` datetime NOT NULL,
  `Password` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;



--
-- Definition of table `lead`
--

DROP TABLE IF EXISTS `lead`;
CREATE TABLE `lead` (
  `ID` int(10) unsigned NOT NULL auto_increment,
  `ProjectTypeId` int(10) unsigned NOT NULL,
  `BusinessPartnerId` int(10) unsigned NOT NULL,
  `CustomerWebAccountId` int(10) unsigned default NULL,
  `Company` varchar(40) default NULL,
  `FirstName` varchar(40) NOT NULL,
  `Last Name` varchar(40) NOT NULL,
  `Address1` varchar(40) NOT NULL,
  `Address2` varchar(40) default NULL,
  `City` varchar(24) NOT NULL,
  `State` varchar(2) NOT NULL,
  `Zip` int(10) NOT NULL,
  `Phone`  varchar(10) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Description` varchar(256) default NULL,
  `DesiredServiceDate` datetime default NULL,
  `PreferedTimeFrom` datetime default NULL,
  `PreferedTimeTo` datetime default NULL,
  `LeadStatusId` int(10) unsigned NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateCancelled` datetime default NULL,
  `ProjectId` int(10) unsigned default NULL,
  `Notes` varchar(256) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;


--
-- Definition of table `leadstatus`
--

DROP TABLE IF EXISTS `leadstatus`;
CREATE TABLE `leadstatus` (
  `ID` int(10) unsigned NOT NULL auto_increment,
  `TYPE` varchar(50) NOT NULL,
  `Description` varchar(200) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leadstatus`
--

/*!40000 ALTER TABLE `leadstatus` DISABLE KEYS */;
INSERT INTO `leadstatus` (`ID`,`TYPE`,`Description`) VALUES
 (1,'NEW','JUST CREATED'),
 (2,'PENDING','CUSTOMER CONTACTED BUT PROJECT NOT CREATED YET'),
 (3,'CONVERTED','LEAD IS CONVERTED TO PROJECT'),
 (4,'CANCELLED','LEAD IS CANCELLED');
/*!40000 ALTER TABLE `leadstatus` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;