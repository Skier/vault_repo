ALTER TABLE `Lead` CHANGE COLUMN `PreferedTimeFrom` `PreferedTime` varchar(30) NOT NULL;
ALTER TABLE `Lead` DROP COLUMN `PreferedTimeTo`;


INSERT INTO `sysversion` (`Version`) VALUES 
(9);