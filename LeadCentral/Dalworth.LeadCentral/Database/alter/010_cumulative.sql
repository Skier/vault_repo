--
-- Table structure for table `partnerphonenumber`
--

DROP TABLE IF EXISTS `partnerphonenumber`;
CREATE TABLE `PartnerPhoneNumber` (
  `Id` int(10) NOT NULL auto_increment,
  `BusinessPartnerId` int(10) NOT NULL,
  `PhoneNumber` varchar(50) NOT NULL,
  `Description` varchar(250) ,
  PRIMARY KEY  (`Id`),
  KEY `FK_PartnerPhoneNumber_BusinessPartner` (`BusinessPartnerId`),
  CONSTRAINT `FK_PartnerPhoneNumber_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `BusinessPartner` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Alter Table structure for table `businesspartner` Add SalesRep column
--

ALTER TABLE `Lead` 
 ADD COLUMN `BusinessPartnerId` int(10) AFTER `CampaignId`;

ALTER TABLE `Lead`
 ADD CONSTRAINT `FK_Lead_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`)
    REFERENCES `businesspartner` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;


UPDATE `lead` l
INNER JOIN `campaign` c ON c.Id = l.CampaignId
SET l.BusinessPartnerId = c.BusinessPartnerId;

ALTER TABLE `partnerphonenumber` 
 ADD COLUMN `PhoneDigits` varchar(10) AFTER `Description`;
ALTER TABLE `BusinessPartner` 
 ADD COLUMN `Address` varchar(500) AFTER `DateCreated`,
 ADD COLUMN `IsExcludedFromReports` tinyint(4) AFTER `Address`,
 CHANGE COLUMN `IsActive` `IsRemoved` TINYINT(4) NOT NULL ;

UPDATE `BusinessPartner` SET `IsExcludedFromReports` = 0;

ALTER TABLE `BusinessPartner` 
 MODIFY COLUMN `IsExcludedFromReports` TINYINT(4) NOT NULL ;
CREATE TABLE `PhoneBlackList` (
  `Id` int(10) NOT NULL auto_increment,
  `PhoneNumber` varchar(50) NOT NULL,
  `Description` varchar(250) ,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE `PhoneCall` 
 ADD COLUMN `TwilioRecordingUrl` varchar(250) AFTER `TrackingPhoneRotationId`,
 ADD COLUMN `IsProcessed` tinyint AFTER `TwilioRecordingUrl`,
 ADD COLUMN `PhoneBlackListId` int(10) AFTER `IsProcessed`,
 ADD CONSTRAINT `FK_PhoneCall_PhoneBlackList` FOREIGN KEY (`PhoneBlackListId`)
    REFERENCES `PhoneBlackList` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION ;

UPDATE `PhoneCall` SET `IsProcessed` = 1;

ALTER TABLE `PhoneCall` MODIFY COLUMN `IsProcessed` TINYINT(4) NOT NULL;



ALTER TABLE `phonecall` DROP Foreign Key `FK_PhoneCall_PhoneBlackList`;

ALTER TABLE `PhoneCall`
 ADD COLUMN `Notes` varchar(250) AFTER `PhoneBlackListId`,
 ADD CONSTRAINT `FK_PhoneCall_PhoneBlackList` FOREIGN KEY (`PhoneBlackListId`)
    REFERENCES `PhoneBlackList` (`Id`) ON DELETE SET NULL ON UPDATE SET NULL ;
ALTER TABLE `BusinessPartner`
 ADD COLUMN `PhoneDigits` varchar(50) AFTER `Phone`;
ALTER TABLE `PhoneBlackList`
 ADD COLUMN `PhoneDigits` varchar(50) AFTER `Description`;
