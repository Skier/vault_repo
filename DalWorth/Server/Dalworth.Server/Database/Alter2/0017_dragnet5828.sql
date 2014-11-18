ALTER TABLE `businesspartner` ADD COLUMN `Login` VARCHAR(50) default NULL AFTER `Name`;
ALTER TABLE `businesspartner` ADD COLUMN `PasswordHash` VARCHAR(255) default NULL AFTER `Login`;

INSERT INTO `sysversion` (`Version`) VALUES
(17);