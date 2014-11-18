ALTER TABLE `Visit` ADD COLUMN `DurationCost` decimal(19,4) NOT NULL AFTER `TaxPercent`;

INSERT INTO `sysversion` (`Version`) VALUES 
(3);