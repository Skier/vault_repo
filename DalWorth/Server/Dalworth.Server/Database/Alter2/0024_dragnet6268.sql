DROP TABLE IF EXISTS `websitearticle`;
CREATE TABLE `websitearticle` (
  `ID` int(10) NOT NULL auto_increment,
  `WebSiteId` int(10) NOT NULL,
  `WebSiteArticleTypeId` int(10) NOT NULL,
  `Url` varchar(1024) NOT NULL,
  `Title` varchar(256) NOT NULL,
  `Description` varchar(1024) NOT NULL,
  `Keywords` varchar(2048) NOT NULL,
  `BreadCrum` varchar(1024) NOT NULL,
  `MenuId` int(10) NOT NULL,
  `LinksText` longtext NOT NULL,
  `ServicesText` longtext NOT NULL,
  `Article` longtext NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WebSiteArticle_WebSite` (`WebSiteId`),
  CONSTRAINT `FK_WebSiteArticle_WebSite` FOREIGN KEY (`WebSiteId`) REFERENCES `website` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `sysversion` (`Version`) VALUES
(24);