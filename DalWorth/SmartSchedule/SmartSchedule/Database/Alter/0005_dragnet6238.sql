ALTER TABLE `ChangeRecord` MODIFY COLUMN `ChangeText` VARCHAR(500) NOT NULL;

INSERT INTO `sysversion` (`Version`) VALUES 
(5);