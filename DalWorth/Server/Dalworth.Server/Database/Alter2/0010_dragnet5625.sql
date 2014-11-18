ALTER TABLE `Lead` CHANGE COLUMN `PreferedServiceDate` `PreferredServiceDate` datetime default NULL;
ALTER TABLE `Lead` CHANGE COLUMN `PreferedTime` `PreferredTime` varchar(30) NOT NULL;

INSERT INTO `sysversion` (`Version`) VALUES 
(10);