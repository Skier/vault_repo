ALTER TABLE `Task` ADD COLUMN `FailCount` INT(10) NOT NULL AFTER `DumpWorkTransactionId`;
ALTER TABLE `Task` ADD COLUMN `LastFailDate` DATETIME default NULL AFTER `FailCount`;