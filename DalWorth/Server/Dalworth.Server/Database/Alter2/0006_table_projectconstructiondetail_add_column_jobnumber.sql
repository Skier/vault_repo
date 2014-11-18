ALTER TABLE `ProjectConstructionDetail` ADD COLUMN `JobNumber` varchar(50) NULL AFTER `LastModifiedDate`;
ALTER TABLE `ProjectConstructionDetail` ADD UNIQUE KEY `IX_ProjectConstructionDetail_JobNumber` USING BTREE (`JobNumber`);

INSERT INTO `sysversion` (`Version`) VALUES 
(6);
