ALTER TABLE `Partnersummaryreportitem` CHANGE COLUMN `NonBookCount` `ShopperCount` INT(10) NOT NULL;

INSERT INTO `sysversion` (`Version`) VALUES (52);