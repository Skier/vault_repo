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



