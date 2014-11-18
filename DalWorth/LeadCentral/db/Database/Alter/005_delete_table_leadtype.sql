ALTER TABLE `lead`
  DROP FOREIGN KEY `FK_Lead_LeadType`;

ALTER TABLE `lead`
  DROP INDEX `FK_Lead_LeadType`;

ALTER TABLE `lead`
  DROP COLUMN `LeadTypeId`;

DROP TABLE `leadtype`;
