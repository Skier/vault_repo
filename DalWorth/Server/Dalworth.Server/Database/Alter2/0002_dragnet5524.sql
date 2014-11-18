CREATE TABLE `projectconstructionprogress` (
  `ID` int(10) NOT NULL,
  `Progress` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `projectconstructionprogress` (`ID`, `Progress`) VALUES 
(1,'Lead'),
(2,'Job'),
(3,'Declined'),
(4,'PaidInFull');

ALTER TABLE `ProjectConstructionDetail` ADD COLUMN `ProjectConstructionProgressId` int(10) NOT NULL AFTER `ProjectId`;

-- update process for projects

UPDATE ProjectConstructionDetail pcd
   SET pcd.ProjectConstructionProgressId =
       CASE
         WHEN pcd.SignUpDate is null
          AND pcd.DeclineDate is null
          AND pcd.LastBillingDate is null
          AND pcd.LastPaymentDate is null
         THEN 1

         WHEN pcd.DeclineDate is not null
          AND pcd.SignUpDate is null
          AND pcd.LastBillingDate is null
          AND pcd.LastPaymentDate is null
         THEN 3

         WHEN pcd.DeclineDate is null
          AND pcd.LastBillingDate is not null
          AND pcd.LastPaymentDate is not null
          AND pcd.SignOffDate is not null
         THEN 4

         ELSE 2
       END;

-- update statuses for projects

UPDATE Project p, ProjectConstructionDetail pcd
   SET p.ProjectStatusId =
       CASE
         WHEN pcd.ProjectConstructionProgressId = 3
           OR pcd.ProjectConstructionProgressId = 4
         THEN 2

         ELSE 1
       END
 WHERE p.id = pcd.ProjectId;



ALTER TABLE `ProjectConstructionDetail` ADD COLUMN `AccountManagerEmployeeId` int(10) default NULL AFTER `ProjectManagerEmployeeId`;

ALTER TABLE `ProjectConstructionDetail` ADD INDEX `FK_ProjectConstructionDetail_ProjectConstructionProgress` (`ProjectConstructionProgressId`),
    ADD CONSTRAINT `FK_ProjectConstructionDetail_ProjectConstructionProgress` FOREIGN KEY (`ProjectConstructionProgressId`) 
    REFERENCES `projectconstructionprogress` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;
    
ALTER TABLE `ProjectConstructionDetail` ADD INDEX `FK_ProjectConstructionDetail_Employee1` (`AccountManagerEmployeeId`),
    ADD CONSTRAINT `FK_ProjectConstructionDetail_Employee1` FOREIGN KEY (`AccountManagerEmployeeId`) 
    REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- update ScopeAmount

UPDATE Project p, ProjectConstructionDetail pcd
   SET pcd.ScopeAmount = p.ClosedAmount
 WHERE p.id = pcd.ProjectId;

INSERT INTO `sysversion` (`Version`) VALUES 
(2);