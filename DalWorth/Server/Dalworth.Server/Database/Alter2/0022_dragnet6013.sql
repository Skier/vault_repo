DROP TABLE IF EXISTS `website`;
CREATE TABLE `website` (
  `ID` int(10) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


DROP TABLE IF EXISTS `weblog`;
CREATE TABLE `webLog` (
  `ID` int(10) NOT NULL auto_increment,
  `WebSiteId` int(10) NOT NULL,
  `SessionId` varchar(80) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `ReferrerHost` varchar(256) NOT NULL,
  `URL` varchar(256) NOT NULL,
  `Keyword` varchar(128) NOT NULL,
  `KeywordId` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WebLog_WebSite` (`WebSiteId`),
  CONSTRAINT `FK_WebLog_WebSite` FOREIGN KEY (`WebSiteId`) REFERENCES `website` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `website`
--



ALTER TABLE `Lead` ADD COLUMN `WebLogId` int(10) default NULL AFTER `CustomerWebAccountId`,
 ADD KEY `FK_Lead_WebLog` (`WebLogId`),
 ADD CONSTRAINT `FK_Lead_WebLog` FOREIGN KEY (`WebLogId`)
	REFERENCES `WebLog` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;



INSERT INTO `website` (`ID`, `Name`) VALUES 
(1,'DalworthRugCleaning'),
(2,'Restoration');

insert into  backgroundjobtype (id, type, description) values (5, 'ProjectFeedbackReceived', 'Customer left feedback about the project') ;

INSERT INTO `sysversion` (`Version`) VALUES
(22);