ALTER TABLE `Lead`
ADD COLUMN  `DateLateNotificationSent` datetime,
ADD COLUMN  `DateFirstSetPending` datetime,
ADD COLUMN  `DateLastSetPending` datetime ,
ADD COLUMN  `FirstUpdateEmployeeId` int(10),
ADD COLUMN  `LastUpdateEmployeeId` char(10) default NULL;

insert into BusinessPartner (id, name) values (5, 'reminder');

alter table backgroundjobpending
Add column `ProjectFeedbackId` int(10) default NULL After `ProjectId`,
  Add  key FK_BackgroundJobPending_ProjectFeedback (`ProjectFeedbackId`),
  add CONSTRAINT `FK_BackgroundJobPending_ProjectFeedback` FOREIGN KEY (`ProjectFeedbackId`) REFERENCES `projectfeedback` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;


insert into backgroundjobtype( id, type, description) values (6, 'ProjectFeedbackProcessed', 'Notify that lead was processed');

INSERT INTO `sysversion` (`Version`) VALUES
(29);