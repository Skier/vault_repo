ALTER TABLE `Item` ADD COLUMN `CleaningRate` DECIMAL(19,4) NULL AFTER `TotalCost`;
ALTER TABLE `Task` ADD COLUMN `DiscountPercentage` INT(10) NOT NULL AFTER `IsRugCleaningDepartment`;