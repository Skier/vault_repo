ALTER TABLE `websitearticle` DROP COLUMN `Title`;
ALTER TABLE `websitearticle` DROP COLUMN `Description`;
ALTER TABLE `websitearticle` DROP COLUMN `Keywords`;
ALTER TABLE `websitearticle` DROP COLUMN `BreadCrum`;
ALTER TABLE `websitearticle` DROP COLUMN `LinksText`;
ALTER TABLE `websitearticle` DROP COLUMN `ServicesText`;
ALTER TABLE `websitearticle` DROP COLUMN `ArticlePart1`;
ALTER TABLE `websitearticle` DROP COLUMN `ArticlePart2`;
ALTER TABLE `websitearticle` DROP COLUMN `ArticlePart3`;


DROP TABLE IF EXISTS `websitearticleparttype`;
CREATE TABLE `websitearticleparttype` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(100) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;


INSERT INTO `websitearticleparttype` (`ID`, `Name`, `Description`) VALUES 
(1,'Title',''),
(2,'Description',''),
(4,'Keywords',''),
(5,'BreadCrum',''),
(6,'Links',''),
(7,'Services',''),
(8,'Custom1',''),
(9,'Custom2',''),
(10,'Custom3',''),
(11,'Custom4',''),
(12,'Custom5',''),
(13,'Custom6','');


DROP TABLE IF EXISTS `websitearticlepart`;
CREATE TABLE `websitearticlepart` (
  `WebSiteArticleId` int(10) NOT NULL,
  `WebSiteArticlePartTypeId` int(10) NOT NULL,
  `ContentText` longtext NOT NULL,
  PRIMARY KEY  (`WebSiteArticleId`,`WebSiteArticlePartTypeId`),
  KEY `FK_WebSiteArticlePart_WebSiteArticlePartType` (`WebSiteArticlePartTypeId`),
  CONSTRAINT `FK_WebSiteArticlePart_WebSiteArticle` FOREIGN KEY (`WebSiteArticleId`) REFERENCES `websitearticle` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WebSiteArticlePart_WebSiteArticlePartType` FOREIGN KEY (`WebSiteArticlePartTypeId`) REFERENCES `websitearticleparttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


INSERT INTO `sysversion` (`Version`) VALUES
(26);