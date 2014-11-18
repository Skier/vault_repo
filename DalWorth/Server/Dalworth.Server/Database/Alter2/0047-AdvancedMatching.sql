ALTER TABLE `Transaction` ADD COLUMN `ManualRecordedCallerId` varchar(50) NOT NULL AFTER `ServmanWorkflowId`;
ALTER TABLE `Transaction` ADD COLUMN `MatchCriteria` int(10) default NULL AFTER `DigiumLogItemId`;

INSERT INTO `sysversion` (`Version`) VALUES (47);