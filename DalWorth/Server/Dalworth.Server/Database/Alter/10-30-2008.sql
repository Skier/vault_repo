ALTER TABLE `MonitoringDetail` ADD COLUMN `IsNoReadings` TINYINT(1) default NULL AFTER `WallSurface`;
ALTER TABLE `Visit` ADD COLUMN `ConfirmBusy` TINYINT(1) NOT NULL AFTER `ConfirmLeftMessage`;