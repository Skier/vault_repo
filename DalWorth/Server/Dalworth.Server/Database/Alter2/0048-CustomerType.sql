alter table `qbcustomertype` add column `IsPending` tinyint(1) NOT NULL;
alter table `qbcustomertype` add column `EmployeeId` int(10) default NULL;
alter table qbsalesrep drop foreign key   FK_QbSalesRep_QbCustomerType;
alter table qbsalesrep drop column qbcustomertypelistid;

insert into employeeType values ('4', 'System', 'Not really employees. Ignore them');

INSERT INTO `sysversion` (`Version`) VALUES (47);

