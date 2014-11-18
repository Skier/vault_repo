ALTER TABLE `projectfeedback` DROP FOREIGN KEY `FK_ProjectFeedback_CallbackInquireTransaction`;
ALTER TABLE `projectfeedback` DROP COLUMN `CallbackInquireTransactionId`;

ALTER TABLE `projectfeedback` 
 ADD COLUMN `IsCallbackSelected` tinyint(1) NOT NULL AFTER `RateId`,
 ADD COLUMN `CallbackDaysInterval` int(10) default NULL AFTER `IsCallbackSelected`,
 ADD COLUMN `IsDoNotCallSelected` tinyint(1) NOT NULL AFTER `CallbackDaysInterval`,
 ADD COLUMN `ReminderEmailSentDate` datetime default NULL AFTER `IsDoNotCallSelected`,
 ADD COLUMN `ReminderPostCardSentDate` datetime default NULL AFTER `ReminderEmailSentDate`;


DROP TABLE `callbackinquiretransaction`;
DROP TABLE `callbackprocesstransaction`;
DROP TABLE `callbackprocesstransactionstatus`;


ALTER TABLE `customer` 
 DROP COLUMN `CallbackInquireDate`,
 DROP COLUMN `CallbackDaysInterval`,
 DROP COLUMN `CallbackExactDate`,
 DROP COLUMN `CallbackDoNotCall`,
 DROP COLUMN `CallbackLeftMessageCount`,
 DROP COLUMN `CallbackBusyCount`,
 DROP COLUMN `CallbackLastAttemptDate`;


INSERT INTO `sysversion` (`Version`) VALUES
(23);