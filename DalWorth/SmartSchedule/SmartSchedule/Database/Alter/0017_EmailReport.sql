ALTER TABLE `Visit` ADD COLUMN `TechnicianEmailSentDate` datetime DEFAULT NULL AFTER `IsTechnicianEmailSent`;
ALTER TABLE `Visit` ADD COLUMN `IsSecondaryEmailSent` tinyint(1) NOT NULL AFTER `TechnicianEmailSentDate`;
update visit set TechnicianEmailSentDate = Now(), IsSecondaryEmailSent = true where IsTechnicianEmailSent = 1;
ALTER TABLE `Visit` DROP COLUMN `IsTechnicianEmailSent`;
ALTER TABLE `ApplicationSetting` ADD COLUMN `LastEmailReportDate` datetime NOT NULL AFTER `Note`;

INSERT INTO `sysversion` (`Version`) VALUES 
(17);