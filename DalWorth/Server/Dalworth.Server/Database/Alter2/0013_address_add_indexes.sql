ALTER TABLE `Address` 
 ADD INDEX `IX_Zip` USING BTREE(`Zip`),
 ADD INDEX `IX_Block` USING BTREE(`Block`),
 ADD INDEX `IX_City` USING BTREE(`City`),
 ADD INDEX `IX_Street` USING BTREE(`Street`);

INSERT INTO `sysversion` (`Version`) VALUES 
(13);