-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.26-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema (null)
--

CREATE DATABASE IF NOT EXISTS (null);
USE (null);

--
-- Definition of table `addresstype`
--

DROP TABLE IF EXISTS `addresstype`;
CREATE TABLE `addresstype` (
  `AddressTypeID` int(10) unsigned NOT NULL auto_increment,
  `Types` varchar(50) default NULL,
  PRIMARY KEY  (`AddressTypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `addresstype`
--

/*!40000 ALTER TABLE `addresstype` DISABLE KEYS */;
/*!40000 ALTER TABLE `addresstype` ENABLE KEYS */;


--
-- Definition of table `document`
--

DROP TABLE IF EXISTS `document`;
CREATE TABLE `document` (
  `DocID` int(10) unsigned NOT NULL auto_increment,
  `IsPublic` bit(1) NOT NULL,
  `DocTypeId` int(10) unsigned default NULL,
  `Vol` varchar(50) default NULL,
  `Pg` varchar(50) default NULL,
  `DocumentNo` varchar(50) default NULL,
  `County` varchar(50) default NULL,
  `State` varchar(50) default NULL,
  `DateFiled` datetime default NULL,
  `DateSigned` datetime default NULL,
  `ResearchNote` text,
  `ImageLink` text,
  PRIMARY KEY  (`DocID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `document`
--

/*!40000 ALTER TABLE `document` DISABLE KEYS */;
INSERT INTO `document` (`DocID`,`IsPublic`,`DocTypeId`,`Vol`,`Pg`,`DocumentNo`,`County`,`State`,`DateFiled`,`DateSigned`,`ResearchNote`,`ImageLink`) VALUES 
 (18,'\0',4,'34563456','34563456','234562365','County5','Arkansas','2007-02-14 00:00:00','2007-02-15 00:00:00','','http://localhost/DocumentStorage/c663c680-44cd-40c7-8bf9-20beca712553/OnlineBillView.zip'),
 (21,'',4,'iuh ','weoiugfwheori u','tg jwehrlgah volui h','County5','Arkansas','2007-02-06 00:00:00','2007-02-14 00:00:00','','eryteryteryt'),
 (29,'\0',5,'4235235','23452345','23423','County2','Texas','2007-02-14 00:00:00','2007-02-23 00:00:00','e rfkhwjeroiphgwjotip hwphjw rpjk p\\oipwejr t[woipjethop \rerth jpeor hj[per\reryjk esrn;\rWkt h\r[apekfb\rqapeirh]\rpei hoi[etph\rthoi wer[ohtipoer htpoweuirhtp wsotphu qaworpejg\rWROUJG AWRIPU GA\'OPWIRY GA;WOIRHGA[WOR G\rAWROUGA \'[PWERU GOPAWIHR G[','23452345'),
 (33,'\0',1,'yeryujhre','ytrtyurtyu','erythujre','County4','Arkansas','2007-02-04 00:00:00','2007-02-21 00:00:00','est hwt hwret hjer hjteryj eryj eryhj','rtyurtyurtikurokry uik'),
 (39,'\0',23,'090','008','Doc Number 001','County5','Texas','2007-02-21 00:00:00','2007-02-22 00:00:00','','987987987');
INSERT INTO `document` (`DocID`,`IsPublic`,`DocTypeId`,`Vol`,`Pg`,`DocumentNo`,`County`,`State`,`DateFiled`,`DateSigned`,`ResearchNote`,`ImageLink`) VALUES 
 (65,'\0',4,'235423','2345','1241234','County5','Arkansas','2007-02-15 00:00:00',NULL,' jherojtihoperit ujhoperijt hpoerijthopier jtpoierjthp oiertpoih jerpotihjeportihobewrt','da22f4f4-c211-4338-a8c0-dc78499d34f0/61244930.png'),
 (66,'\0',4,'','','123123','County4','Arkansas','2007-02-14 00:00:00',NULL,'',''),
 (67,'\0',4,'123414','234','','County5','Arkansas','2007-02-15 00:00:00',NULL,'',''),
 (68,'\0',4,'987','tyutyu','9879879','County5','Arkansas','2007-02-15 00:00:00',NULL,'','tyuty2234'),
 (69,'\0',1,'1111','1111','1111','County1','Alabama','2007-02-02 00:00:00','2007-02-07 00:00:00','2312312412431\r24125453245635324523\rretkj 3r;kgjwoirjg wijghoiw4tj ho4iwjoth','1111'),
 (70,'\0',1,'1111','1111','11111','County1','Alabama','2007-02-02 00:00:00',NULL,'',''),
 (71,'\0',1,'1111','1111','111111','County1','Alabama','2007-02-02 00:00:00',NULL,'',''),
 (72,'\0',1,'11111','1111','111','County1','Alabama','2007-02-13 00:00:00',NULL,'','');
INSERT INTO `document` (`DocID`,`IsPublic`,`DocTypeId`,`Vol`,`Pg`,`DocumentNo`,`County`,`State`,`DateFiled`,`DateSigned`,`ResearchNote`,`ImageLink`) VALUES 
 (73,'\0',1,'123','123','123','County2','Alabama','2007-02-06 00:00:00','2007-02-14 00:00:00','123123 rjghoerhgwiu rhgwuioe hgwiuehgwiue hgiwouehoiuwehoiuwehogiuwheoiguwheriugwheirg\r\r\rwkerj iwjthopiwp\rlekr jthwjtrh oipw\rjhe rjkhweuti hg\rljkehr tlkjertho erohlt\rejr thoiwjetpo hiweoirjgt wpogtr\rwljkihre g;qwlihjreg pwoihrg','23123123123'),
 (74,'\0',1,'','','123','County1','Alabama','2007-02-01 00:00:00',NULL,'',''),
 (75,'\0',1,'1223','123123','123123','County1','Alabama','2007-02-01 00:00:00','2007-01-17 00:00:00','122333123','123123');
/*!40000 ALTER TABLE `document` ENABLE KEYS */;


--
-- Definition of table `documenttype`
--

DROP TABLE IF EXISTS `documenttype`;
CREATE TABLE `documenttype` (
  `DocTypeID` int(10) unsigned NOT NULL auto_increment,
  `Name` varchar(50) default NULL,
  PRIMARY KEY  (`DocTypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `documenttype`
--

/*!40000 ALTER TABLE `documenttype` DISABLE KEYS */;
INSERT INTO `documenttype` (`DocTypeID`,`Name`) VALUES 
 (1,'Company'),
 (2,'Individual'),
 (3,'Affidavit of Heirship'),
 (4,'Agreement'),
 (5,'Correction Warranty Deed'),
 (6,'Deed of Trust'),
 (7,'Designation of Unit'),
 (8,'Easement'),
 (9,'Executors Deed'),
 (10,'Lease Memo'),
 (11,'License'),
 (12,'Lien'),
 (13,'Marshals Deed'),
 (14,'Mechanics Lien'),
 (15,'Mineral Deed'),
 (16,'Oil Gas Mineral Lease'),
 (17,'Option and Agreement'),
 (18,'Probate'),
 (19,'Quit Claim Deed'),
 (20,'Release of Lien'),
 (21,'Right of Way'),
 (22,'Sheriffs Deed'),
 (23,'Special Warranty Deed'),
 (24,'Warranty Deed');
/*!40000 ALTER TABLE `documenttype` ENABLE KEYS */;


--
-- Definition of table `participant`
--

DROP TABLE IF EXISTS `participant`;
CREATE TABLE `participant` (
  `ParticipantID` int(10) unsigned NOT NULL auto_increment,
  `DocID` int(10) unsigned default NULL,
  `DocRoleID` int(10) unsigned default NULL,
  `AsNamed` text,
  `PhoneHome` int(11) default NULL,
  `PhoneOffice` int(11) default NULL,
  `PhoneCell` int(11) default NULL,
  `PhoneAlt` int(11) default NULL,
  `EntityName` varchar(50) default NULL,
  `FirstName` varchar(50) default NULL,
  `MiddleName` varchar(50) default NULL,
  `LastName` varchar(50) default NULL,
  `ContactPosition` varchar(50) default NULL,
  `TAXID` varchar(50) default NULL,
  `SSN` varchar(50) default NULL,
  `ParentID` int(10) unsigned NOT NULL,
  `TypeID` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`ParticipantID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `participant`
--

/*!40000 ALTER TABLE `participant` DISABLE KEYS */;
/*!40000 ALTER TABLE `participant` ENABLE KEYS */;


--
-- Definition of table `participantaddress`
--

DROP TABLE IF EXISTS `participantaddress`;
CREATE TABLE `participantaddress` (
  `AddressID` int(10) unsigned NOT NULL auto_increment,
  `ParticipantlID` int(10) unsigned default NULL,
  `AddressTypeID` int(10) unsigned default NULL,
  `Line1` varchar(50) default NULL,
  `Line2` varchar(50) default NULL,
  `City` varchar(50) default NULL,
  `State` varchar(50) default NULL,
  `Zip` varchar(50) default NULL,
  `Incareof` varchar(50) default NULL,
  PRIMARY KEY  (`AddressID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `participantaddress`
--

/*!40000 ALTER TABLE `participantaddress` DISABLE KEYS */;
/*!40000 ALTER TABLE `participantaddress` ENABLE KEYS */;


--
-- Definition of table `participantentityparty`
--

DROP TABLE IF EXISTS `participantentityparty`;
CREATE TABLE `participantentityparty` (
  `ParticipantEntityPartyID` int(10) unsigned NOT NULL auto_increment,
  `ParticipantID` int(10) unsigned default NULL,
  `fName` varchar(50) default NULL,
  `mName` varchar(50) default NULL,
  `lName` varchar(50) default NULL,
  `SSN` varchar(50) default NULL,
  PRIMARY KEY  (`ParticipantEntityPartyID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `participantentityparty`
--

/*!40000 ALTER TABLE `participantentityparty` DISABLE KEYS */;
/*!40000 ALTER TABLE `participantentityparty` ENABLE KEYS */;


--
-- Definition of table `participantreservation`
--

DROP TABLE IF EXISTS `participantreservation`;
CREATE TABLE `participantreservation` (
  `DocReservationID` int(10) unsigned NOT NULL auto_increment,
  `ParticipantID` int(10) unsigned default NULL,
  `Details` text,
  PRIMARY KEY  (`DocReservationID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `participantreservation`
--

/*!40000 ALTER TABLE `participantreservation` DISABLE KEYS */;
/*!40000 ALTER TABLE `participantreservation` ENABLE KEYS */;


--
-- Definition of table `participantrole`
--

DROP TABLE IF EXISTS `participantrole`;
CREATE TABLE `participantrole` (
  `DocRoleID` int(10) unsigned NOT NULL auto_increment,
  `RoleName` varchar(50) default NULL,
  PRIMARY KEY  (`DocRoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `participantrole`
--

/*!40000 ALTER TABLE `participantrole` DISABLE KEYS */;
/*!40000 ALTER TABLE `participantrole` ENABLE KEYS */;


--
-- Definition of table `participanttype`
--

DROP TABLE IF EXISTS `participanttype`;
CREATE TABLE `participanttype` (
  `TypeID` int(10) unsigned NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`TypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `participanttype`
--

/*!40000 ALTER TABLE `participanttype` DISABLE KEYS */;
/*!40000 ALTER TABLE `participanttype` ENABLE KEYS */;


--
-- Definition of table `tract`
--

DROP TABLE IF EXISTS `tract`;
CREATE TABLE `tract` (
  `TractID` int(10) unsigned NOT NULL auto_increment,
  `DocID` int(10) unsigned default NULL,
  `RefName` varchar(50) default NULL,
  `CalledAC` decimal(10,0) default NULL,
  `ScopePlotUrl` text,
  PRIMARY KEY  (`TractID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tract`
--

/*!40000 ALTER TABLE `tract` DISABLE KEYS */;
/*!40000 ALTER TABLE `tract` ENABLE KEYS */;


--
-- Definition of table `tractexception`
--

DROP TABLE IF EXISTS `tractexception`;
CREATE TABLE `tractexception` (
  `TractExceptionsID` int(10) unsigned NOT NULL auto_increment,
  `TractID` int(10) unsigned default NULL,
  `RefName` varchar(50) default NULL,
  `CalledAC` varchar(50) default NULL,
  PRIMARY KEY  (`TractExceptionsID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tractexception`
--

/*!40000 ALTER TABLE `tractexception` DISABLE KEYS */;
/*!40000 ALTER TABLE `tractexception` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
