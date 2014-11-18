ALTER TABLE `Visit` ADD COLUMN `IsBlockout` tinyint(1) NOT NULL AFTER `SnapshotDate`;

INSERT INTO `sysversion` (`Version`) VALUES 
(12);