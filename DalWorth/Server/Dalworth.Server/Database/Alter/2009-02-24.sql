Update Project
set Description = ''
where Description is null;

ALTER TABLE Project MODIFY COLUMN `Description` VARCHAR(2000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

INSERT INTO `ProjectType` (`ID`, `Type`, `Description`) VALUES 
(4,'Construction',NULL),
(5,'Content',NULL);

CREATE TABLE `ConstructionDamageType` (
  `ID` int(10) NOT NULL,
  `DamageType` varchar(50) NOT NULL,
  `Description` varchar(200) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `ConstructionDamageType` (`ID`, `DamageType`, `Description`) VALUES 
(1,'Smoke',NULL),
(2,'Fire',NULL),
(3,'Water',NULL),
(4,'Hail',NULL);

CREATE TABLE `ProjectConstructionDetail` (
  `ProjectId` int(10) NOT NULL,
  `ProjectManagerEmployeeId` int(10) default NULL,
  `ScopeDate` datetime default NULL,
  `SignUpDate` datetime default NULL,
  `DeclineDate` datetime default NULL,
  `EstimatedAmount` decimal(19,4) NOT NULL,
  `ActualStartDate` datetime default NULL,
  `ActualCompletionDate` datetime default NULL,
  `SignOffDate` datetime default NULL,
  `BillingDate` datetime default NULL,
  `PaymentDate` datetime default NULL,
  `IsSelfGeneratedLead` tinyint(1) NOT NULL,
  `JobCost` decimal(19,4) NOT NULL,
  `ConstructionDamageTypeId` int(10) default NULL,
  `DamageTypeText` varchar(40) NOT NULL,
  `DamageOrigin` varchar(80) NOT NULL,
  `LossDate` datetime default NULL,
  `LossDescription` varchar(300) NOT NULL,
  PRIMARY KEY  (`ProjectId`),
  KEY `FK_ProjectConstructionDetail_Employee` (`ProjectManagerEmployeeId`),
  KEY `FK_ProjectConstructionDetail_ConstructionDamageType` (`ConstructionDamageTypeId`),
  CONSTRAINT `FK_ProjectConstructionDetail_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionDetail_Employee` FOREIGN KEY (`ProjectManagerEmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectConstructionDetail_ConstructionDamageType` FOREIGN KEY (`ConstructionDamageTypeId`) REFERENCES `constructiondamagetype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


INSERT INTO `EmployeeType` (`ID`, `Type`, `Description`) VALUES 
(3,'ProjectManager',NULL);

INSERT INTO `Employee` (`EmployeeTypeId`, `ServmanUserId`, `ServmanTechId`, `AddressId`, `Login`, `FirstName`, `LastName`, `HireDate`, `Phone1`, `Phone2`, `Password`, `IsActive`, `IsRestoration`, `IsUnknown`, `DefaultVanId`) VALUES 
(3,NULL,NULL,NULL,NULL,'Ken','Archer',NULL,NULL,NULL,'',1,0,0,NULL),
(3,NULL,NULL,NULL,NULL,'Josh','Hobbs',NULL,NULL,NULL,'',1,0,0,NULL),
(3,NULL,NULL,NULL,NULL,'Mike','Grubbs',NULL,NULL,NULL,'',1,0,0,NULL),
(3,NULL,NULL,NULL,NULL,'Vicki','Plemmons',NULL,NULL,NULL,'',1,0,0,NULL),
(3,NULL,NULL,NULL,NULL,'Andy','Rials',NULL,NULL,NULL,'',1,0,0,NULL);

update Project p
set p.ProjectStatusId = 1
where (
select count(*) from Task t where
t.ProjectId = p.ID and
t.TaskStatusId != 2 and
(t.TaskFailTypeId is null or (t.TaskFailTypeId is not null and t.TaskFailTypeId = 1))
) > 0;

update Project p
set p.ProjectStatusId = 2
where (
select count(*) from Task t where
t.ProjectId = p.ID and
t.TaskStatusId != 2 and
(t.TaskFailTypeId is null or (t.TaskFailTypeId is not null and t.TaskFailTypeId = 1))
) = 0;