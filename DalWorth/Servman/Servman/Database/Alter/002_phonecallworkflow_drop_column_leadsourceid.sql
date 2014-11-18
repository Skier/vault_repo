ALTER TABLE `phonecallworkflow`
  DROP FOREIGN KEY `FK_PhoneCallWorkflow_LeadSourcePhone`;

ALTER TABLE `phonecallworkflow`
  DROP INDEX `FK_PhoneCallWorkflow_LeadSourcePhone`;

ALTER TABLE `phonecallworkflow`
  DROP COLUMN `LeadSourcePhoneId`;
