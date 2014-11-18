DROP TABLE IF EXISTS `websitearticlecategory`;
CREATE TABLE `websitearticlecategory` (
  `ID` int(10) NOT NULL auto_increment,
  `WebSiteId` int(10) NOT NULL,
  `ParentWebSiteArticleCategoryId` int(10) default NULL,
  `LandingPageWebSiteArticleId` int(10) NOT NULL,
  `Name` varchar(100) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_WebSiteArticleCategory_WebSiteArticle` (`LandingPageWebSiteArticleId`),
  KEY `FK_WebSiteArticleCategory_WebSiteArticleCategory` (`ParentWebSiteArticleCategoryId`),
  CONSTRAINT `FK_WebSiteArticleCategory_WebSiteArticle` FOREIGN KEY (`LandingPageWebSiteArticleId`) REFERENCES `websitearticle` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WebSiteArticleCategory_WebSiteArticleCategory` FOREIGN KEY (`ParentWebSiteArticleCategoryId`) REFERENCES `websitearticlecategory` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE `websitearticle` DROP COLUMN `WebSiteId`;
ALTER TABLE `websitearticle` ADD COLUMN `WebSiteArticleCategoryId` int(10) NOT NULL AFTER `ID`,
  ADD KEY `FK_WebSiteArticle_WebSiteArticleCategory` (`WebSiteArticleCategoryId`),
  ADD CONSTRAINT `FK_WebSiteArticle_WebSiteArticleCategory` FOREIGN KEY (`WebSiteArticleCategoryId`) 
	REFERENCES `websitearticlecategory` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE `websitearticle` ADD COLUMN `DatePublished` datetime default NULL AFTER `MenuId`;


INSERT INTO `sysversion` (`Version`) VALUES
(27);