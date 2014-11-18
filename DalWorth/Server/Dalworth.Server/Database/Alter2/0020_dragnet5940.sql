ALTER TABLE `Lead` ADD COLUMN `ServmanAdvertisingSource` VARCHAR(10) default NULL AFTER `AdvertisingSourceAcronym`;
ALTER TABLE `Lead` ADD COLUMN `ServmanTrackCode` VARCHAR(10) default NULL AFTER `ServmanAdvertisingSource`;

INSERT INTO `sysversion` (`Version`) VALUES
(20);