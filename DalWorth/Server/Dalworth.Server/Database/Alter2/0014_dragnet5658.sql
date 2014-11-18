DROP TABLE IF EXISTS `backgroundjobtype`;
CREATE TABLE `backgroundjobtype` (
  `ID` int(10) NOT NULL,
  `Type` varchar(50) NOT NULL,
  `Description` varchar(200) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `backgroundjobtype` (`ID`, `Type`, `Description`) VALUES 
(1,'LeadRecievedEmail',''),
(2,'LeadRecievedPrint','');


DROP TABLE IF EXISTS `backgroundjobpending`;
CREATE TABLE `backgroundjobpending` (
  `ID` int(10) NOT NULL auto_increment,
  `BackgroundJobTypeId` int(10) NOT NULL,
  `LeadId` int(10) default NULL,
  `ProjectId` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_BackgroundJobPending_BackgroundJobType` (`BackgroundJobTypeId`),
  KEY `FK_BackgroundJobPending_Lead` (`LeadId`),
  KEY `FK_BackgroundJobPending_Project` (`ProjectId`),
  CONSTRAINT `FK_BackgroundJobPending_BackgroundJobType` FOREIGN KEY (`BackgroundJobTypeId`) REFERENCES `backgroundjobtype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_BackgroundJobPending_Lead` FOREIGN KEY (`LeadId`) REFERENCES `lead` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_BackgroundJobPending_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



INSERT INTO `sysversion` (`Version`) VALUES 
(14);