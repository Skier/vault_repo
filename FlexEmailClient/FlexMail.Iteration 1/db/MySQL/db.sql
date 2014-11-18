CREATE DATABASE `email_client` /*!40100 DEFAULT CHARACTER SET utf8 */;

DROP TABLE IF EXISTS `email_client`.`account`;
CREATE TABLE  `email_client`.`account` (
  `AccountId` int(10) unsigned NOT NULL auto_increment,
  `Email` varchar(50) NOT NULL,
  `Pop3SettingsId` int(10) unsigned NOT NULL,
  `SmtpSettingsId` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`AccountId`),
  KEY `FK_pop3_settings` (`Pop3SettingsId`),
  KEY `FK_smtp_settings` (`SmtpSettingsId`),
  CONSTRAINT `FK_pop3_settings` FOREIGN KEY (`Pop3SettingsId`) REFERENCES `serversettings` (`ServerSettingsId`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_smtp_settings` FOREIGN KEY (`SmtpSettingsId`) REFERENCES `serversettings` (`ServerSettingsId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `email_client`.`address`;
CREATE TABLE  `email_client`.`address` (
  `AddressId` int(10) unsigned NOT NULL auto_increment,
  `AccountId` int(10) unsigned NOT NULL,
  `Email` varchar(50) NOT NULL,
  PRIMARY KEY  (`AddressId`),
  KEY `FK_account` (`AccountId`),
  CONSTRAINT `FK_account` FOREIGN KEY (`AccountId`) REFERENCES `account` (`AccountId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `email_client`.`serversettings`;
CREATE TABLE  `email_client`.`serversettings` (
  `ServerSettingsId` int(10) unsigned NOT NULL auto_increment,
  `Host` varchar(50) NOT NULL,
  `Port` int(10) unsigned NOT NULL,
  `UserName` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  PRIMARY KEY  (`ServerSettingsId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
