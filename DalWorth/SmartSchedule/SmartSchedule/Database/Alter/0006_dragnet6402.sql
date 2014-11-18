ALTER TABLE `Technician` ADD COLUMN `IsContractor` TINYINT(1) NOT NULL AFTER `DriveTimeMinutes`;
ALTER TABLE `TechnicianDefault` ADD COLUMN `IsContractor` TINYINT(1) NOT NULL AFTER `DriveTimeMinutes`;

INSERT INTO `sysversion` (`Version`) VALUES 
(6);