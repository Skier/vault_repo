alter table lead
modify `ProjectTypeId` int(10) default NULL;

DROP TABLE IF EXISTS `websitephone`;
CREATE TABLE  `websitephone` (
  `ID` int(10) NOT NULL auto_increment,
  `WebSiteId` int(10) NOT NULL,
  `PhoneNumber` varchar(64) NOT NULL,
  `PhoneKey` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WebSitePhone_WebSite` (`WebSiteId`),
  CONSTRAINT `FK_WebSitePhone_WebSite` FOREIGN KEY (`WebSiteId`) REFERENCES `website` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `sysversion` (`Version`) VALUES (38);