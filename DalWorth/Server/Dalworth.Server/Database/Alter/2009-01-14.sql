ALTER TABLE `Task` ADD COLUMN `ReadyDate` DATETIME default NULL AFTER `DiscountPercentage`;

update Task
set ReadyDate = Modified
where TaskTypeId = 2 and TaskStatusId = 1 and IsReady = 1 and DumpedTaskId is null;