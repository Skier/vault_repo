ALTER TABLE `DigiumLogItem` ADD COLUMN `TimeCreatedOriginal` DATETIME NOT NULL AFTER `TimeCreated`;
ALTER TABLE `DigiumLogItem` ADD COLUMN `TimeTalkStartedOriginal` DATETIME NOT NULL AFTER `TimeTalkStarted`;
update digiumlogitem set TimeCreatedOriginal = TimeCreated;
update digiumlogitem set TimeTalkStartedOriginal = TimeTalkStarted;

INSERT INTO `sysversion` (`Version`) VALUES (41);