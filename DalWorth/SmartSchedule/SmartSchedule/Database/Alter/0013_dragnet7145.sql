ALTER TABLE `Visit` ADD COLUMN `IsTechnicianEmailSent` tinyint(1) NOT NULL AFTER `IsBlockout`;
ALTER TABLE `TechnicianDefault` ADD COLUMN `Email` varchar(100) NOT NULL AFTER `MaxNonExclusiveVisitsCount`;

INSERT INTO `sysversion` (`Version`) VALUES 
(13);