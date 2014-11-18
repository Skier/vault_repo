ALTER TABLE `servmansession` 
 DROP COLUMN `DbId`;
ALTER TABLE `servmansession`
 CHANGE COLUMN `Ticket` `IntuitTicket` VARCHAR(250) NOT NULL;
ALTER TABLE `servmansession`
 CHANGE COLUMN `SessionId` `Ticket` VARCHAR(50) NOT NULL;
