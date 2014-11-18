ALTER TABLE `DigiumLogItem` ADD COLUMN `IsIncoming` TINYINT(1) NOT NULL AFTER `CallId`;
update digiumlogitem set IsIncoming = 1;

INSERT INTO `sysversion` (`Version`) VALUES (42);