ALTER TABLE `BusinessPartner` 
 ADD COLUMN `Address` varchar(500) AFTER `DateCreated`,
 ADD COLUMN `IsExcludedFromReports` tinyint(4) AFTER `Address`,
 CHANGE COLUMN `IsActive` `IsRemoved` TINYINT(4) NOT NULL ;

UPDATE `BusinessPartner` SET `IsExcludedFromReports` = 0;

ALTER TABLE `BusinessPartner` 
 MODIFY COLUMN `IsExcludedFromReports` TINYINT(4) NOT NULL ;
