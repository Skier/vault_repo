alter table projecttype
add column `QbClassListId` varchar(50) default NULL,
add CONSTRAINT `FK_ProjectType_QbClass` FOREIGN KEY (`QbClassListId`) REFERENCES `qbclass` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
add KEY `FK_ProjectType_QbClass` (`QbClassListId`);

alter table qbinvoice drop foreign key FK_QbInvoice_QbSalesRep;
alter table  qbinvoice change `SalesRepRefListId`  `QbSalesRepRefListId` varchar(50) default NULL;
alter table qbinvoice
add CONSTRAINT `FK_QbInvoice_QbSalesRep` FOREIGN KEY (`QbSalesRepRefListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION;


DROP TABLE IF EXISTS `qbcustomertype`;
CREATE TABLE `qbcustomertype` (
  `ListId` varchar(50) NOT NULL,
  `TimeCreated` datetime NOT NULL,
  `TimeModified` datetime NOT NULL,
  `EditSequence` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `FullName` varchar(256) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `ParentRefListId` varchar(50) default NULL,
  `SubLevel` int(10) NOT NULL,
  `RequiredQbSalesRepListId` varchar(50) default NULL,
  PRIMARY KEY  (`ListId`),
  KEY `FK_QbCustomerType_QbSalesRep` (`RequiredQbSalesRepListId`),
  CONSTRAINT `FK_QbCustomerType_QbSalesRep` FOREIGN KEY (`RequiredQbSalesRepListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

alter table businesspartner
  ADD column `QbCustomerTypeListId` varchar(50) default NULL,
  ADD CONSTRAINT `FK_BusinessPartner_QbCustomerType` FOREIGN KEY (`QbCustomerTypeListId`) REFERENCES `qbcustomertype` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD KEY `FK_BusinessPartner_QbCustomerType` (`QbCustomerTypeListId`);

alter table project
ADD column   `QbCustomerTypeListId` varchar(50) default NULL,
ADD column   `QbSalesRepListId` varchar(50) default NULL,
add CONSTRAINT `FK_Project_QbCustomerType` FOREIGN KEY (`QbCustomerTypeListId`) REFERENCES `qbcustomertype` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
add CONSTRAINT `FK_Project_QbSalesRep` FOREIGN KEY (`QbSalesRepListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
add KEY `FK_Project_QbCustomerType` (`QbCustomerTypeListId`),
add KEY `FK_Project_QbSalesRep` (`QbSalesRepListId`);

DROP TABLE IF EXISTS `qbsalesreptype`;
CREATE TABLE `qbsalesreptype` (
  `ID` int(10) NOT NULL,
  `Name` varchar(50) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

insert into qbsalesreptype (id, name)
values (0, "Unknown"), (1, "Employee"), (2, "Vendor"), (3, "Other");


alter table qbsalesrep
add column `EmployeeId` int(10) default NULL,
add column  `QbCustomerTypeListId` varchar(50) default NULL,
add column  `QbSalesRepTypeId` int(10) NOT NULL,
add column  `FirstName` varchar(25) default NULL,
add column  `LastName` varchar(25) default NULL,
add CONSTRAINT `FK_QbSalesRep_Employee` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
add CONSTRAINT `FK_QbSalesRep_QbCustomerType` FOREIGN KEY (`QbCustomerTypeListId`) REFERENCES `qbcustomertype` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
add CONSTRAINT `FK_QbSalesRep_QbSalesRepType` FOREIGN KEY (`QbSalesRepTypeId`) REFERENCES `qbsalesreptype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
add KEY `FK_QbSalesRep_Employee` (`EmployeeId`),
add KEY `FK_QbSalesRep_QbCustomerType` (`QbCustomerTypeListId`),
add KEY `FK_QbSalesRep_QbSalesRepType` (`QbSalesRepTypeId`);

alter table qbsalesrep
add CONSTRAINT `FK_QbSalesRep_QbSalesRepType` FOREIGN KEY (`QbSalesRepTypeId`) REFERENCES `qbsalesreptype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
add KEY `FK_QbSalesRep_QbSalesRepType` (`QbSalesRepTypeId`);

update qbsalesrep set editsequence = 1;

alter table qbcustomer
ADD COLUMN `QbCustomerTypeListId` varchar(50) default NULL,
ADD COLUMN `QbSalesRepListId` varchar(50) default NULL,
add CONSTRAINT `FK_QbCustomer_QbCustomerType` FOREIGN KEY (`QbCustomerTypeListId`) REFERENCES `qbcustomertype` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
add CONSTRAINT `FK_QbCustomer_QbSalesRep` FOREIGN KEY (`QbSalesRepListId`) REFERENCES `qbsalesrep` (`ListId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
add KEY `FK_QbCustomer_QbCustomerType` (`QbCustomerTypeListId`),
add KEY `FK_QbCustomer_QbSalesRep` (`QbSalesRepListId`);




insert into projecttype (id, type) values (6, 'Basement Systems');

INSERT INTO `sysversion` (`Version`) VALUES
(35);
