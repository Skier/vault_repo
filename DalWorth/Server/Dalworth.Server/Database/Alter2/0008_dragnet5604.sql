DROP TABLE IF EXISTS `businesspartner`;
CREATE TABLE `businesspartner` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;


INSERT INTO `businesspartner` (`ID`, `Name`) VALUES (1,'dalworthrugcleaning.com');


DROP TABLE IF EXISTS `leadstatus`;
CREATE TABLE `leadstatus` (
  `ID` int(10) NOT NULL,
  `Status` varchar(20) NOT NULL,
  `Description` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


INSERT INTO `leadstatus` (`ID`, `Status`, `Description`) VALUES 
(1,'New','Newly created lead'),
(2,'Pending','Lead in process, waiting for conversion or cancel'),
(3,'Converted','Project got created'),
(4,'Cancelled','Lead has been cancelled');



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



DROP TABLE IF EXISTS `lead`;
CREATE TABLE `lead` (
  `ID` int(10) NOT NULL auto_increment,
  `LeadStatusId` int(10) NOT NULL,
  `ProjectTypeId` int(10) NOT NULL,
  `ProjectId` int(10) default NULL,
  `CustomerWebAccountId` int(10) default NULL,
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
  `PreferedServiceDate` datetime default NULL,
  `PreferedTimeFrom` datetime default NULL,
  `PreferedTimeTo` datetime default NULL,
  `DateCreated` datetime NOT NULL,
  `DateCancelled` datetime default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_Lead_LeadStatus` (`LeadStatusId`),
  KEY `FK_Lead_ProjectType` (`ProjectTypeId`),
  KEY `FK_Lead_Project` (`ProjectId`),
  KEY `FK_Lead_CustomerWebAccount` (`CustomerWebAccountId`),
  KEY `FK_Lead_BusinessPartner` (`BusinessPartnerId`),
  CONSTRAINT `FK_Lead_LeadStatus` FOREIGN KEY (`LeadStatusId`) REFERENCES `leadstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_ProjectType` FOREIGN KEY (`ProjectTypeId`) REFERENCES `projecttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_CustomerWebAccount` FOREIGN KEY (`CustomerWebAccountId`) REFERENCES `customerwebaccount` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Lead_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `businesspartner` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;




INSERT INTO `sysversion` (`Version`) VALUES 
(8);