ALTER TABLE `Lead` ADD COLUMN `KeywordId` int(10) default NULL AFTER `DateCancelled`;

INSERT INTO `sysversion` (`Version`) VALUES 
(11);