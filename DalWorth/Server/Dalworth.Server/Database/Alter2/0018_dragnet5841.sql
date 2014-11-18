ALTER TABLE `businesspartner` DROP COLUMN `Login`;
ALTER TABLE `businesspartner` DROP COLUMN `PasswordHash`;
ALTER TABLE `lead` ADD COLUMN `AdvertisingSourceAcronym` VARCHAR(6) default NULL AFTER `KeywordId`;

DROP TABLE IF EXISTS `webuser`;
CREATE TABLE `webuser` (
  `ID` int(10) NOT NULL auto_increment,
  `Login` varchar(50) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `EmployeeId` int(10) default NULL,
  `BusinessPartnerId` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  UNIQUE KEY `IX_Login` (`Login`),
  KEY `FK_WebUser_Employee` (`EmployeeId`),
  KEY `FK_WebUser_BusinessPartner` (`BusinessPartnerId`),
  CONSTRAINT `FK_WebUser_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WebUser_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `businesspartner` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


INSERT INTO `sysversion` (`Version`) VALUES
(18);