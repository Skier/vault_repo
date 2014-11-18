ALTER TABLE `Technician` ADD COLUMN `MaxVisitsCount` int(10) NOT NULL AFTER `MinRevenuePerMile`;
ALTER TABLE `Technician` ADD COLUMN `MaxNonExclusiveVisitsCount` int(10) NOT NULL AFTER `MaxVisitsCount`;
ALTER TABLE `TechnicianDefault` ADD COLUMN `MaxVisitsCount` int(10) NOT NULL AFTER `MinRevenuePerMile`;
ALTER TABLE `TechnicianDefault` ADD COLUMN `MaxNonExclusiveVisitsCount` int(10) NOT NULL AFTER `MaxVisitsCount`;

update Technician set MaxVisitsCount = 10;
update Technician set MaxNonExclusiveVisitsCount = 10;

update TechnicianDefault set MaxVisitsCount = 10;
update TechnicianDefault set MaxNonExclusiveVisitsCount = 10;


INSERT INTO `sysversion` (`Version`) VALUES 
(8);