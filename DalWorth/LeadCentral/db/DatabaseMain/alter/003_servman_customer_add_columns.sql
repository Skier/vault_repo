ALTER TABLE `servmancustomer` 
 ADD COLUMN `IsLeadSourcesInited` TINYINT AFTER `AppDbId`,
 ADD COLUMN `IsOAuthInited` TINYINT AFTER `IsLeadSourcesInited`,
 ADD COLUMN `IsWorkflowsInited` TINYINT AFTER `IsOAuthInited`;