ALTER TABLE `AdvertisingSource` ADD COLUMN `TrackingUrl` varchar(1024) NOT NULL AFTER `IsRestoration`;
ALTER TABLE `SyncToolInfo` CHANGE COLUMN `LastImportedAdSourceId` `LastImportAdSourceDate` DATETIME NOT NULL;


INSERT INTO `sysversion` (`Version`) VALUES (46);