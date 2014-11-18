ALTER TABLE `Order` ADD COLUMN `DateCompleted` DATETIME default NULL AFTER `Amount`;

INSERT INTO `sysversion` (`Version`) VALUES (44);