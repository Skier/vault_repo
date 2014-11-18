ALTER TABLE `ProjectConstructionDetail` ADD COLUMN `LastModifiedDate` datetime NOT NULL AFTER `ScopeAmount`;

-- update LastModifiedDate

UPDATE Project p, ProjectConstructionDetail pcd
   SET pcd.LastModifiedDate =
       GREATEST(
           p.CreateDate,
           IFNULL(pcd.ScopeDate, p.CreateDate),
           IFNULL(pcd.SignUpDate, p.CreateDate),
           IFNULL(pcd.DeclineDate, p.CreateDate),
           IFNULL(pcd.ActualStartDate, p.CreateDate),
           IFNULL(pcd.ActualCompletionDate, p.CreateDate),
           IFNULL(pcd.SignOffDate, p.CreateDate),
           IFNULL(pcd.LastPaymentDate, p.CreateDate),
           IFNULL(pcd.LastBillingDate, p.CreateDate))
 WHERE p.id = pcd.ProjectId;

INSERT INTO `sysversion` (`Version`) VALUES 
(3);