ALTER TABLE `ProjectConstructionDetail` CHANGE COLUMN `ScopeAmount` `BilledAmount` decimal(19,4) default NULL;

INSERT INTO `sysversion` (`Version`) VALUES 
(7);