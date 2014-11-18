update Customer c
inner join
	(SELECT c.ID as CustomerId, max(w.StartDate) as LastDeliveryDate FROM Task t
	inner join WorkTransactionTask wtt on wtt.TaskId = t.ID
	inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
	inner join Work w on w.ID = wt.WorkId
	inner join Visit v on v.ID = wt.VisitId
	inner join Customer c on c.ID = v.CustomerId
	where TaskTypeId = 2 and TaskStatusId = 2 and DumpedTaskId is null
	  and wtt.WorkTransactionTaskActionId = 1
	group by c.ID) LastCompletedDeliveryTbl on LastCompletedDeliveryTbl.CustomerId = c.ID
set c.CallbackInquireDate = DATE(LastCompletedDeliveryTbl.LastDeliveryDate)
where c.CallbackInquireDate is null;

ALTER TABLE `Customer` ADD COLUMN `CallbackLastAttemptDate` DATETIME default NULL AFTER `CallbackBusyCount`;