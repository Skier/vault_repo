ALTER TABLE `Customer` 
 ADD INDEX `IX_LastName` USING BTREE(`LastName`),
 ADD INDEX `IX_FirstName` USING BTREE(`FirstName`);

INSERT INTO `sysversion` (`Version`) VALUES 
(12);