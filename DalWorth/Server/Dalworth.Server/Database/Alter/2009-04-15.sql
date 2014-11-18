DROP TABLE IF EXISTS `projectconstructionbillpaytype`;
CREATE TABLE `projectconstructionbillpaytype` (
  `ID` int(10) NOT NULL,
  `BillPayType` varchar(20) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `projectconstructionbillpaytype` (`ID`, `BillPayType`) VALUES 
(1,'Invoice'),
(2,'Payment'),
(3,'Credit');

DROP TABLE IF EXISTS `projectconstructionbillpay`;
CREATE TABLE `projectconstructionbillpay` (
  `ID` int(10) NOT NULL auto_increment,
  `ProjectId` int(10) NOT NULL,
  `ProjectConstructionBillPayTypeId` int(10) NOT NULL,
  `IssueDate` datetime NOT NULL,
  `Number` varchar(50) NOT NULL,
  `IsVoided` tinyint(1) NOT NULL,
  `Notes` varchar(200) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_ProjectConstructionBillPay_Project` (`ProjectId`),
  KEY `FK_ProjectConstructionBillPay_ProjectConstructionBillPayType` (`ProjectConstructionBillPayTypeId`),
  CONSTRAINT `FK_ProjectConstructionBillPay_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionBillPay_ProjectConstructionBillPayType` FOREIGN KEY (`ProjectConstructionBillPayTypeId`) REFERENCES `projectconstructionbillpaytype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE `ProjectConstructionDetail` CHANGE COLUMN `BillingDate` `LastBillingDate` DATETIME DEFAULT NULL;
ALTER TABLE `ProjectConstructionDetail` CHANGE COLUMN `PaymentDate` `LastPaymentDate` DATETIME DEFAULT NULL;
ALTER TABLE `ProjectConstructionDetail` ADD COLUMN `ScopeAmount` decimal(19,4) default NULL AFTER `LossDescription`;
ALTER TABLE `ProjectConstructionDetail` DROP COLUMN `LossDescription`;

DROP TABLE IF EXISTS `projectconstructionscopetype`;
CREATE TABLE `projectconstructionscopetype` (
  `ID` int(10) NOT NULL,
  `ScopeType` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `projectconstructionscopetype` (`ID`, `ScopeType`) VALUES 
(1,'Scope'),
(2,'ScopeEstimate'),
(3,'ChangeOrder');

DROP TABLE IF EXISTS `projectconstructionscope`;
CREATE TABLE `projectconstructionscope` (
  `ID` int(10) NOT NULL auto_increment,
  `ProjectId` int(10) NOT NULL,
  `ProjectConstructionScopeTypeId` int(10) NOT NULL,
  `ScopeDate` datetime NOT NULL,
  `JobType` varchar(50) NOT NULL,
  `IsVoided` tinyint(1) NOT NULL,
  `Notes` varchar(200) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_ProjectConstructionScope_ProjectConstructionScopeType` (`ProjectConstructionScopeTypeId`),
  KEY `FK_ProjectConstructionScope_Project` (`ProjectId`),
  CONSTRAINT `FK_ProjectConstructionScope_ProjectConstructionScopeType` FOREIGN KEY (`ProjectConstructionScopeTypeId`) REFERENCES `projectconstructionscopetype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionScope_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `sysversion`;
CREATE TABLE `sysversion` (
  `Version` int(10) NOT NULL,
  PRIMARY KEY  (`Version`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


-- data migration
-- create scopes for construction projects
insert into `projectconstructionscope` (`ProjectId`, `ProjectConstructionScopeTypeId`, `ScopeDate`,
                                        `JobType`, `IsVoided`, `Notes`, `Amount`)
select p.ID, 1, pcd.LastBillingDate, 'scope', false, '', p.ClosedAmount
  from Project p
       inner join ProjectConstructionDetail pcd on p.ID = pcd.ProjectId
 where p.ClosedAmount != 0
   and pcd.LastBillingDate is not null;

-- create invoices for construction projects
insert into `projectconstructionbillpay` (`ProjectId`, `ProjectConstructionBillPayTypeId`, `IssueDate`,
                                        `Number`, `IsVoided`, `Notes`, `Amount`)
select p.ID, 1, pcd.LastBillingDate, '', false, '', p.ClosedAmount
  from Project p
       inner join ProjectConstructionDetail pcd on p.ID = pcd.ProjectId
 where p.ClosedAmount != 0
   and pcd.LastBillingDate is not null;

-- create paiments for construction projects
insert into `projectconstructionbillpay` (`ProjectId`, `ProjectConstructionBillPayTypeId`, `IssueDate`,
                                        `Number`, `IsVoided`, `Notes`, `Amount`)
select p.ID, 2, pcd.LastPaymentDate, '', false, '', p.PaidAmount
  from Project p
       inner join ProjectConstructionDetail pcd on p.ID = pcd.ProjectId
 where p.PaidAmount != 0
   and pcd.LastPaymentDate is not null;

INSERT INTO `sysversion` (`Version`) VALUES 
(0);