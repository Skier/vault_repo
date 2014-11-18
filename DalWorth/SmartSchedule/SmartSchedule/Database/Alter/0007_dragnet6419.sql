ALTER TABLE `Technician` ADD COLUMN `MinRevenuePerMile` DECIMAL(19,4) NOT NULL AFTER `IsContractor`;
ALTER TABLE `TechnicianDefault` ADD COLUMN `MinRevenuePerMile` DECIMAL(19,4) NOT NULL AFTER `IsContractor`;

INSERT INTO `sysversion` (`Version`) VALUES 
(7);