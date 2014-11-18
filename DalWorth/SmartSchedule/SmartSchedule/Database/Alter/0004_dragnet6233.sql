ALTER TABLE `Visit` ADD COLUMN `SnapshotDate` datetime default NULL AFTER `DurationCost`;

INSERT INTO `sysversion` (`Version`) VALUES 
(4);