ALTER TABLE `ApplicationSetting` ADD COLUMN `Note` VARCHAR(50) NOT NULL AFTER `ImportDate`;

INSERT INTO `sysversion` (`Version`) VALUES 
(9);