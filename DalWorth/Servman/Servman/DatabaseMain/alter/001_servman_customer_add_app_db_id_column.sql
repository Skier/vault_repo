ALTER TABLE `servmancustomer` 
 ADD COLUMN `AppDbId` VARCHAR(50) AFTER `Description`;

UPDATE `servmancustomer` SET `AppDbId` = 'bfit8895q';
