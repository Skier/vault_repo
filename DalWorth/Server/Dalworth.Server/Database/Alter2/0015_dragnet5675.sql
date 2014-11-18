ALTER TABLE `Project` ADD COLUMN `LeadId` int(10) default NULL AFTER `ProjectStatusId`,
 ADD INDEX `FK_Project_Lead` USING BTREE(`LeadId`),
 ADD CONSTRAINT `FK_Project_Lead` FOREIGN KEY `FK_Project_Lead` (`LeadId`)
    REFERENCES `lead` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

update Project, Lead
set Project.LeadId = Lead.ID
where Project.ID = Lead.ProjectId;

ALTER TABLE `lead` DROP COLUMN `ProjectId`,
 DROP INDEX `FK_Lead_Project`,
 DROP FOREIGN KEY `FK_Lead_Project`;

ALTER TABLE `Lead` ADD COLUMN `EmployeeId` int(10) default NULL AFTER `ProjectTypeId`,
 ADD INDEX `FK_Lead_Employee` USING BTREE(`EmployeeId`),
 ADD CONSTRAINT `FK_Lead_Employee` FOREIGN KEY `FK_Lead_Employee` (`EmployeeId`)
    REFERENCES `employee` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

INSERT INTO `sysversion` (`Version`) VALUES 
(15);