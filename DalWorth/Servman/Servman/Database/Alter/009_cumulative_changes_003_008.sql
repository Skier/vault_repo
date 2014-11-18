--
-- Table structure for table `qbinvoice`
--

DROP TABLE IF EXISTS `qbinvoice`;
CREATE TABLE `qbinvoice` (
  `Id` int(10) NOT NULL auto_increment,
  `LeadId` int(10) NOT NULL,
  `QbInvoiceId` varchar(50) NOT NULL,
  `Amount` decimal(19,4) default NULL,
  `TaxAmount` decimal(19,4) default NULL,
  `TotalAmount` decimal(19,4) default NULL,
  `Status` varchar(50) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_QbInvoice_Lead` (`LeadId`),
  CONSTRAINT `FK_QbInvoice_Lead` FOREIGN KEY (`LeadId`) REFERENCES `lead` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE `leadsource` 
 DROP COLUMN `QbCustomerTypeId`,
 DROP COLUMN `QbEmployeeRecordId`,
 DROP COLUMN `QbVendorRecordId`,
 DROP COLUMN `QbSalesRepRecordId`;

ALTER TABLE `lead`
  DROP FOREIGN KEY `FK_Lead_LeadType`;

ALTER TABLE `lead`
  DROP INDEX `FK_Lead_LeadType`;

ALTER TABLE `lead`
  DROP COLUMN `LeadTypeId`;

DROP TABLE `leadtype`;
DROP TABLE `job`;
DROP TABLE `leadaction`;
DROP TABLE `leadchangehistory`;
