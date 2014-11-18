CREATE TABLE `CallbackInquireTransaction` (
  `ID` int(10) NOT NULL auto_increment,
  `CustomerId` int(10) NOT NULL,
  `VisitId` int(10) default NULL,
  `CallbackInquireDate` datetime NOT NULL,
  `CallbackDaysInterval` int(10) default NULL,
  `CallbackExactDate` datetime default NULL,
  `DoNotCall` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_CallbackInquireTransaction_Visit` (`VisitId`),
  KEY `FK_CallbackInquireTransaction_Customer` (`CustomerId`),
  CONSTRAINT `FK_CallbackInquireTransaction_Visit` FOREIGN KEY (`VisitId`) REFERENCES `visit` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_CallbackInquireTransaction_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE `Customer` ADD COLUMN `CallbackInquireDate` datetime NULL AFTER `LastSyncDate`;
ALTER TABLE `Customer` ADD COLUMN `CallbackDaysInterval` int(10) NULL AFTER `CallbackInquireDate`;
ALTER TABLE `Customer` ADD COLUMN `CallbackExactDate` datetime NULL AFTER `CallbackDaysInterval`;
ALTER TABLE `Customer` ADD COLUMN `CallbackDoNotCall` TINYINT(1) NOT NULL AFTER `CallbackExactDate`;