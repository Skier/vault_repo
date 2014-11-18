DROP TABLE IF EXISTS `qbcreditmemoline`;
DROP TABLE IF EXISTS `qbcreditmemo`;
DROP table if exists `qbpayment`;
DROP TABLE IF EXISTS `tasktypeqbitem`;
DROP TABLE IF EXISTS `projecttypeqbitem`;
DROP TABLE IF EXISTS `projecttypeqbtemplate`;
DROP TABLE IF EXISTS `worktransactionqbinvoice`;
DROP TABLE IF EXISTS `qbinvoiceline`;
DROP TABLE IF EXISTS `qbinvoice`;
DROP TABLE IF EXISTS `qbitem`;
DROP TABLE IF EXISTS `qbitemtype`;
DROP TABLE IF EXISTS `qbcustomer`;
DROP TABLE IF EXISTS `qbsalesrep`;
DROP TABLE IF EXISTS `qbclass`;
DROP TABLE IF EXISTS `qbtemplate`;
DROP TABLE IF EXISTS `projecttypeqbaccount`;
DROP TABLE IF EXISTS `qbaccount`;
DROP TABLE IF EXISTS `qbinvoiceterm`;
DROP TABLE IF EXISTS `qbsalestaxcode`;
DROP TABLE IF EXISTS `qbpaymentmethod`;
DROP TABLE IF EXISTS `qbsynclogdetail`;
DROP TABLE IF EXISTS `qbsynclog`;
DROP TABLE IF EXISTS `qbsyncrequest`;
DROP TABLE IF EXISTS `qbsyncaction`;

CREATE TABLE `qbsyncaction` (
  `ID` int(10) NOT NULL,
  `Description` varchar(64) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

insert into `qbsyncaction` (ID, Description) values 
(1, 'CustomerAdd'),
(2, 'JobAdd'),
(3, 'InvoiceAdd'),
(4, 'InvoiceImportFromQb'),
(5, 'CustomerMod'),
(6, 'JobMod'),
(7, 'InvoiceVoid'),
(8, 'PaymentAdd'),
(9, 'PaymentMod'),
(10, 'CreditMemoAdd'),
(11, 'CreditMemoMod');

CREATE TABLE `qbsyncrequest` (
  `ID` int(10) NOT NULL auto_increment,
  `RequestDate` datetime NOT NULL,
  `QbSyncActionId` int(10) NOT NULL,
  `QbCustomerId` int(10) default NULL,
  `QbInvoiceId` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbSyncRequest_QbSyncAction` (`QbSyncActionId`),
  CONSTRAINT `FK_QbSyncRequest_QbSyncAction` FOREIGN KEY (`QbSyncActionId`) REFERENCES `qbsyncaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE  `qbsynclog` (
  `ID` int(10) NOT NULL auto_increment,
  `LastRunDate` datetime NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbsynclogdetail` (
  `ID` int(10) NOT NULL auto_increment,
  `QbSyncLogId` int(10) NOT NULL,
  `CompletedDate` datetime NOT NULL,
  `QbSyncActionId` int(10) NOT NULL,
  `IsSuccess` tinyint(1) NOT NULL,
  `ErrorMessage` varchar(1024) default NULL,
  `QbCustomerId` int(10) default NULL,
  `QbInvoiceId` int(10) default NULL,
  `QbXmlRequest` longtext,
  `QbXmlResponse` longtext,
  `TxnID` varchar(50) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbSyncLogDetail_QBSyncLog` (`QbSyncLogId`),
  KEY `FK_QbSyncLogDetail_QbSyncAction` (`QbSyncActionId`),
  CONSTRAINT `FK_QbSyncLogDetail_QBSyncLog` FOREIGN KEY (`QbSyncLogId`) REFERENCES `qbsynclog` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbSyncLogDetail_QbSyncAction` FOREIGN KEY (`QbSyncActionId`) REFERENCES `qbsyncaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbclass` (
  `ListId` varchar(50) NOT NULL,
  `FullName` varchar(256) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `ParentClassListID` varchar(50) default NULL,
  `SubLevel` int(10) NOT NULL,
  PRIMARY KEY  (`ListId`),
  KEY `FK_QbClass_QbClass` (`ParentClassListID`),
  CONSTRAINT `FK_QbClass_QbClass` FOREIGN KEY (`ParentClassListID`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbtemplate` (
  `ListId` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbsalestaxcode` (
  `ListId` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(256) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsTaxable` tinyint(1) NOT NULL,
  `Description` varchar(256) NOT NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbaccount` (
  `ListId` varchar(50) NOT NULL,
  `FullName` varchar(256) NOT NULL,
  `AccountType` varchar(256) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE  `projecttypeqbaccount` (
  `ProjectTypeId` int(10) NOT NULL,
  `QbAccountListId` varchar(50) NOT NULL,
  `IsDefault` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ProjectTypeId`,`QbAccountListId`),
  KEY `FK_ProjectTypeQbAccount_QbAccount` (`QbAccountListId`),
  CONSTRAINT `FK_ProjectTypeQbAccount_ProjectType` FOREIGN KEY (`ProjectTypeId`) REFERENCES `projecttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectTypeQbAccount_QbAccount` FOREIGN KEY (`QbAccountListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbinvoiceterm` (
  `ListId` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsDateDriven` tinyint(1) NOT NULL,
  `StdDueDays` int(10) default NULL,
  `StdDiscountDays` int(10) default NULL,
  `DiscountPct` decimal(18,0) default NULL,
  `DayOfMonthDue` int(10) default NULL,
  `DueNextMonthDays` int(10) default NULL,
  `DiscountDayOfMonth` int(10) default NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbsalesrep` (
  `ListId` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Initial` varchar(256) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsEmployee` tinyint(1) NOT NULL,
  `RefListId` varchar(50) default NULL,
  `FullName` varchar(256) default NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbcustomer` (
  `ID` int(10) NOT NULL auto_increment,
  `CustomerId` int(10) NOT NULL,
  `ProjectId` int(10) default NULL,
  `ListId` varchar(50) default NULL,
  `EditSequence` varchar(50) default NULL,
  `TimeCreated` datetime default NULL,
  `TimeModified` datetime default NULL,
  `SubLevel` int(10) default NULL,
  `Name` varchar(256) NOT NULL,
  `FullName` varchar(256) default NULL,
  `IsActive` tinyint(1) NOT NULL,
  `CompanyName` varchar(50) character set utf8 default NULL,
  `FirstName` varchar(50) character set utf8 default NULL,
  `LastName` varchar(50) character set utf8 default NULL,
  `Phone1` varchar(10) character set utf8 default NULL,
  `Phone2` varchar(10) character set utf8 default NULL,
  `Email` varchar(50) character set utf8 default NULL,
  `BillingAddressAddr1` varchar(256) character set utf8 default NULL,
  `BillingAddressAddr2` varchar(256) character set utf8 default NULL,
  `BillingAddressCity` varchar(256) character set utf8 default NULL,
  `BillingAddressState` varchar(2) character set utf8 default NULL,
  `BillingAddressPostalCode` varchar(50) character set utf8 default NULL,
  `BillingAddressCountry` varchar(50) character set utf8 default NULL,
  `BillingAddressNote` varchar(256) character set utf8 default NULL,
  `ShippingAddressAddr1` varchar(256) character set utf8 default NULL,
  `ShippingAddressAddr2` varchar(256) character set utf8 default NULL,
  `ShippingAddressCity` varchar(256) character set utf8 default NULL,
  `ShippingAddressState` varchar(2) character set utf8 default NULL,
  `ShippingAddressPostalCode` varchar(50) character set utf8 default NULL,
  `ShippingAddressCountry` varchar(50) character set utf8 default NULL,
  `ShippingAddressNote` varchar(256) character set utf8 default NULL,
  `Balance` decimal(19,4) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbCustomer_Project` (`ProjectId`),
  KEY `FK_QbCustomer_Customer` (`CustomerId`),
  CONSTRAINT `FK_QbCustomer_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCustomer_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbitemtype` (
  `ID` int(10) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

insert into qbitemtype(id, name) values
(1, 'Service'),
(2, 'Tax'),
(3, 'Discount');

CREATE TABLE `qbitem` (
  `ListId` varchar(50) NOT NULL,
  `FullName` varchar(50) NOT NULL,
  `AccountRefListId` varchar(50) default NULL,
  `SalesTaxCodeRefListId` varchar(50) default NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `QbItemTypeId` int(10) NOT NULL,
  `TaxRate` decimal(19,4) default NULL,
  `Description` varchar(256) default NULL,
  `Price` decimal(19,4) default NULL,
  PRIMARY KEY  (`ListId`),
  KEY `FK_QbItem_QbItemType` (`QbItemTypeId`),
  KEY `FK_QbItemType_QbAccount` (`AccountRefListId`),
  CONSTRAINT `FK_QbItem_QbItemType` FOREIGN KEY (`QbItemTypeId`) REFERENCES `qbitemtype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbItemType_QbAccount` FOREIGN KEY (`AccountRefListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbinvoice` (
  `ID` int(10) NOT NULL auto_increment,
  `DumptedInvoiceId` int(10) default NULL,
  `DumpWorkTransactionId` int(10) default NULL,
  `QbCustomerId` int(10) NOT NULL,
  `CustomerRefListId` varchar(50) default NULL,
  `ProcessedDate` datetime default NULL,
  `ProcessedByEmployeeId` int(10) default NULL,
  `CreatedDate` datetime NOT NULL,
  `TxnID` varchar(50) default NULL,
  `TimeCreatedInQb` datetime default NULL,
  `TimeModifiedInQb` datetime default NULL,
  `TxnNumber` int(10) default NULL,
  `EditSequence` varchar(50) default NULL,
  `QbClassListId` varchar(50) default NULL,
  `QbAccountListId` varchar(50) default NULL,
  `QbTemplateListId` varchar(50) default NULL,
  `TxnDate` datetime default NULL,
  `RefNumber` varchar(50) default NULL,
  `BillingAddressAddr1` varchar(256) default NULL,
  `BillingAddressAddr2` varchar(256) default NULL,
  `BillingAddressAddr3` varchar(256) default NULL,
  `BillingAddressAddr4` varchar(256) default NULL,
  `BillingAddressAddr5` varchar(256) default NULL,
  `BillingAddressCity` varchar(50) default NULL,
  `BillingAddresState` varchar(2) default NULL,
  `BillingAddresPostalCode` varchar(10) default NULL,
  `BillingAddressCountry` varchar(50) default NULL,
  `BillingAddressNote` varchar(256) default NULL,
  `ShipAddressAddr1` varchar(256) default NULL,
  `ShipAddressAddr2` varchar(256) default NULL,
  `ShipAddressAddr3` varchar(256) default NULL,
  `ShipAddressAddr4` varchar(256) default NULL,
  `ShipAddressAddr5` varchar(256) default NULL,
  `ShipAddressCity` varchar(50) default NULL,
  `ShipAddressState` varchar(2) default NULL,
  `ShipAddressPostalCode` varchar(50) default NULL,
  `ShipAddressCountry` varchar(50) default NULL,
  `ShipAddressNote` varchar(256) default NULL,
  `QbInvoiceTermListId` varchar(50) default NULL,
  `Memo` varchar(256) default NULL,
  `ItemSalesTaxRef` varchar(50) default NULL,
  `SalesRepRefListId` varchar(50) default NULL,
  `SubTotalAmount` decimal(19,4) NOT NULL,
  `TaxAmount` decimal(19,4) NOT NULL,
  `TotalAmount` decimal(19,4) NOT NULL,
  `IsVoid` tinyint(1) default NULL,
  `IsPending` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbInvoice_QbClass` (`QbClassListId`),
  KEY `FK_QbInvoice_Employee` (`ProcessedByEmployeeId`),
  KEY `FK_QbInvoice_QbTemplate` (`QbTemplateListId`),
  KEY `FK_QbInvoice_QbInvoiceTerm` (`QbInvoiceTermListId`),
  KEY `FK_QbInvoice_QbAccount` (`QbAccountListId`),
  KEY `FK_QbInvoice_QbSalesRep` (`SalesRepRefListId`),
  KEY `FK_QbInvoice_QbInvoice` (`DumptedInvoiceId`),
  KEY `FK_QbInvoice_WorkTransaction` (`DumpWorkTransactionId`),
  KEY `FK_QbInvoice_QbItem1` (`ItemSalesTaxRef`),
  KEY `FK_QbInvoice_QbCustomer` (`QbCustomerId`),
  CONSTRAINT `FK_QbInvoice_QbClass` FOREIGN KEY (`QbClassListId`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_Employee` FOREIGN KEY (`ProcessedByEmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbTemplate` FOREIGN KEY (`QbTemplateListId`) REFERENCES `qbtemplate` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbInvoiceTerm` FOREIGN KEY (`QbInvoiceTermListId`) REFERENCES `qbinvoiceterm` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbAccount` FOREIGN KEY (`QbAccountListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbSalesRep` FOREIGN KEY (`SalesRepRefListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbInvoice` FOREIGN KEY (`DumptedInvoiceId`) REFERENCES `qbinvoice` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_WorkTransaction` FOREIGN KEY (`DumpWorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbItem1` FOREIGN KEY (`ItemSalesTaxRef`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoice_QbCustomer` FOREIGN KEY (`QbCustomerId`) REFERENCES `qbcustomer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE  `qbinvoiceline` (
  `ID` int(10) NOT NULL auto_increment,
  `QbItemListId` varchar(50) NOT NULL,
  `QbInvoiceId` int(10) NOT NULL,
  `TxnLineID` varchar(50) default NULL,
  `Description` varchar(256) NOT NULL,
  `Quantity` decimal(10,0) NOT NULL,
  `UnitOfMeasure` varchar(50) default NULL,
  `Rate` decimal(19,4) NOT NULL,
  `RatePercent` decimal(18,0) default NULL,
  `QbClassListId` varchar(50) default NULL,
  `Amount` decimal(19,4) NOT NULL,
  `QbSalesTaxCodeListId` varchar(50) NOT NULL,
  `TaskId` int(10) default NULL,
  `ItemId` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_QbInvoiceLine_Task` (`TaskId`),
  KEY `FK_QbInvoiceLine_Item` (`ItemId`),
  KEY `FK_QbInvoiceLine_QbClass` (`QbClassListId`),
  KEY `FK_QbInvoiceLine_QbSalesTaxCode` (`QbSalesTaxCodeListId`),
  KEY `FK_QbInvoiceLine_QbInvoice` (`QbInvoiceId`),
  KEY `FK_QbInvoiceLine_QbItem` (`QbItemListId`),
  CONSTRAINT `FK_QbInvoiceLine_Task` FOREIGN KEY (`TaskId`) REFERENCES `task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_Item` FOREIGN KEY (`ItemId`) REFERENCES `item` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_QbClass` FOREIGN KEY (`QbClassListId`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_QbSalesTaxCode` FOREIGN KEY (`QbSalesTaxCodeListId`) REFERENCES `qbsalestaxcode` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_QbInvoice` FOREIGN KEY (`QbInvoiceId`) REFERENCES `qbinvoice` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbInvoiceLine_QbItem` FOREIGN KEY (`QbItemListId`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `worktransactionqbinvoice` (
  `WorkTransactionId` int(10) NOT NULL,
  `QbInvoiceId` int(10) NOT NULL,
  `QbCustomerId` int(10) default NULL,
  `QbProjectId` int(10) default NULL,
  `IsModified` tinyint(1) NOT NULL,
  `IsCreated` tinyint(1) NOT NULL,
  PRIMARY KEY  (`WorkTransactionId`,`QbInvoiceId`),
  KEY `FK_WorkTransactionQbInvoice_QbInvoice` (`QbInvoiceId`),
  CONSTRAINT `FK_WorkTransactionQbInvoice_WorkTransaction` FOREIGN KEY (`WorkTransactionId`) REFERENCES `worktransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_WorkTransactionQbInvoice_QbInvoice` FOREIGN KEY (`QbInvoiceId`) REFERENCES `qbinvoice` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `projecttypeqbitem` (
  `ProjectTypeId` int(10) NOT NULL,
  `QbItemListId` varchar(50) NOT NULL,
  PRIMARY KEY  (`ProjectTypeId`,`QbItemListId`),
  KEY `FK_ProjectTypeQbItem_QbItem` (`QbItemListId`),
  CONSTRAINT `FK_ProjectTypeQbItem_ProjectType` FOREIGN KEY (`ProjectTypeId`) REFERENCES `projecttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectTypeQbItem_QbItem` FOREIGN KEY (`QbItemListId`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `tasktypeqbitem` (
  `TaskTypeId` int(10) NOT NULL,
  `QbItemListId` varchar(50) NOT NULL,
  PRIMARY KEY  (`TaskTypeId`,`QbItemListId`),
  KEY `FK_TaskTypeQbItem_QbItem` (`QbItemListId`),
  CONSTRAINT `FK_TaskTypeQbItem_TaskType` FOREIGN KEY (`TaskTypeId`) REFERENCES `tasktype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_TaskTypeQbItem_QbItem` FOREIGN KEY (`QbItemListId`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `projecttypeqbtemplate` (
  `ProjectTypeId` int(10) NOT NULL,
  `QbTemplateListId` varchar(50) NOT NULL,
  `IsDefault` tinyint(1) NOT NULL,
  PRIMARY KEY  (`ProjectTypeId`,`QbTemplateListId`),
  KEY `FK_ProjectTypeQbTemplate_QbTemplate` (`QbTemplateListId`),
  CONSTRAINT `FK_ProjectTypeQbTemplate_ProjectType` FOREIGN KEY (`ProjectTypeId`) REFERENCES `projecttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectTypeQbTemplate_QbTemplate` FOREIGN KEY (`QbTemplateListId`) REFERENCES `qbtemplate` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbpaymentmethod` (
  `ListId` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  PRIMARY KEY  (`ListId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbpayment` (
  `TxnID` varchar(50) NOT NULL,
  `QbCustomerId` int(10) NOT NULL,
  `TimeCreatedInQb` datetime default NULL,
  `TimeModifiedInQb` datetime default NULL,
  `EditSequence` varchar(50) default NULL,
  `QbAccountListId` varchar(50) default NULL,
  `TxnNumber` int(10) default NULL,
  `TxnDate` datetime default NULL,
  `RefNumber` varchar(50) default NULL,
  `TotalAmount` decimal(19,4) default NULL,
  `QbPaymentMethodListId` varchar(50) default NULL,
  `Memo` varchar(256) default NULL,
  `DepositToAccountListId` varchar(50) default NULL,
  PRIMARY KEY  (`TxnID`),
  KEY `FK_QbPayment_QbCustomer` (`QbCustomerId`),
  KEY `FK_QbPayment_QbPaymentMethod` (`QbPaymentMethodListId`),
  KEY `FK_QbPayment_QbAccount` (`DepositToAccountListId`),
  CONSTRAINT `FK_QbPayment_QbCustomer` FOREIGN KEY (`QbCustomerId`) REFERENCES `qbcustomer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbPayment_QbPaymentMethod` FOREIGN KEY (`QbPaymentMethodListId`) REFERENCES `qbpaymentmethod` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbPayment_QbAccount` FOREIGN KEY (`DepositToAccountListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbcreditmemo` (
  `TxnID` varchar(50) NOT NULL,
  `QbCustomerId` int(10) NOT NULL,
  `TimeCreatedInQb` datetime default NULL,
  `TimeModifiedInQb` datetime default NULL,
  `EditSequence` varchar(50) default NULL,
  `TxnNumber` int(10) default NULL,
  `QbClassListId` varchar(50) default NULL,
  `QbAccountListId` varchar(50) default NULL,
  `QbTemplateListId` varchar(50) default NULL,
  `TxnDate` datetime default NULL,
  `RefNumber` varchar(50) default NULL,
  `IsPending` tinyint(1) NOT NULL,
  `TermsRefListId` varchar(50) default NULL,
  `SalesRepRefListId` varchar(50) default NULL,
  `SubTotalAmount` decimal(19,4) default NULL,
  `ItemSalesTaxRef` varchar(50) default NULL,
  `SalesTaxPercentage` varchar(50) default NULL,
  `TaxAmount` decimal(19,4) default NULL,
  `TotalAmount` decimal(19,4) default NULL,
  `CreditRemaining` decimal(19,4) default NULL,
  `Memo` varchar(256) default NULL,
  PRIMARY KEY  (`TxnID`),
  KEY `FK_QbCreditMemo_QbCustomer` (`QbCustomerId`),
  KEY `FK_QbCreditMemo_QbClass` (`QbClassListId`),
  KEY `FK_QbCreditMemo_QbAccount` (`QbAccountListId`),
  KEY `FK_QbCreditMemo_QbTemplate` (`QbTemplateListId`),
  KEY `FK_QbCreditMemo_QbSalesRep` (`SalesRepRefListId`),
  KEY `FK_QbCreditMemo_QbItem` (`ItemSalesTaxRef`),
  CONSTRAINT `FK_QbCreditMemo_QbCustomer` FOREIGN KEY (`QbCustomerId`) REFERENCES `qbcustomer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbClass` FOREIGN KEY (`QbClassListId`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbAccount` FOREIGN KEY (`QbAccountListId`) REFERENCES `qbaccount` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbTemplate` FOREIGN KEY (`QbTemplateListId`) REFERENCES `qbtemplate` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbSalesRep` FOREIGN KEY (`SalesRepRefListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemo_QbItem` FOREIGN KEY (`ItemSalesTaxRef`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `qbcreditmemoline` (
  `TxnLineID` varchar(50) NOT NULL,
  `QbCreditMemoTxnID` varchar(50) NOT NULL,
  `QbItemListId` varchar(50) NOT NULL,
  `Description` varchar(256) default NULL,
  `Quantity` decimal(18,0) default NULL,
  `Rate` decimal(19,4) default NULL,
  `RatePercent` decimal(18,0) default NULL,
  `Amount` decimal(19,4) default NULL,
  `QbSalesTaxCodeListId` varchar(50) default NULL,
  PRIMARY KEY  (`TxnLineID`),
  KEY `FK_QbCreditMemoLine_QbItem` (`QbItemListId`),
  KEY `FK_QbCreditMemoLine_QbSalesTaxCode` (`QbSalesTaxCodeListId`),
  KEY `FK_QbCreditMemoLine_QbCreditMemo` (`QbCreditMemoTxnID`),
  CONSTRAINT `FK_QbCreditMemoLine_QbItem` FOREIGN KEY (`QbItemListId`) REFERENCES `qbitem` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemoLine_QbSalesTaxCode` FOREIGN KEY (`QbSalesTaxCodeListId`) REFERENCES `qbsalestaxcode` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_QbCreditMemoLine_QbCreditMemo` FOREIGN KEY (`QbCreditMemoTxnID`) REFERENCES `qbcreditmemo` (`TxnID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


drop PROCEDURE `MarkTaskModifiedByVisit`;
drop PROCEDURE `MarkTaskModifiedByWork`;

-- Update Tasks to new Modified date by VisitId

DELIMITER $$
CREATE PROCEDURE `MarkTaskModifiedByVisit`(IN visitId int)
BEGIN
    UPDATE Task t, (SELECT TaskId FROM VisitTask WHERE VisitTask.VisitId = visitId) t2
	SET t.Modified = NOW()
	WHERE t.ID = t2.TaskId;
END$$
DELIMITER ;

-- Update Tasks to new Modified date by Work

DELIMITER $$
CREATE PROCEDURE `MarkTaskModifiedByWork`(IN workIdInput int)
BEGIN
	UPDATE Task t, (SELECT vt.TaskId FROM WorkDetail wd 
		inner join VisitTask vt on vt.VisitId = wd.VisitId    
		where wd.WorkId = workIdInput) t2
	SET t.Modified = NOW()
	WHERE t.ID = t2.TaskId;
END$$
DELIMITER ;


INSERT INTO `sysversion` (`Version`) VALUES
(30);