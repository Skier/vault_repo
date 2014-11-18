ALTER TABLE `AdvertisingSource` ADD COLUMN `IsRestoration` tinyint(1) NOT NULL AFTER `Acronym`;

INSERT INTO `sysversion` (`Version`) VALUES 
(1);