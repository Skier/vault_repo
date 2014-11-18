ALTER TABLE `phonecall` DROP Foreign Key `FK_PhoneCall_PhoneBlackList`;

ALTER TABLE `PhoneCall`
 ADD COLUMN `Notes` varchar(250) AFTER `PhoneBlackListId`,
 ADD CONSTRAINT `FK_PhoneCall_PhoneBlackList` FOREIGN KEY (`PhoneBlackListId`)
    REFERENCES `PhoneBlackList` (`Id`) ON DELETE SET NULL ON UPDATE SET NULL ;
