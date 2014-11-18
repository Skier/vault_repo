update Task set TaskFailTypeId = 3 where TaskFailTypeId = 2;
update WorkTransactionTask set WorkTransactionTaskActionId = 5 where WorkTransactionTaskActionId = 4;
delete from TaskFailType where ID = 2;
delete from WorkTransactionTaskAction where ID = 4;