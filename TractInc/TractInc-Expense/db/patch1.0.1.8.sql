create table WorkLog(
    WorkLogId   int identity    not null,
    BillItemId  int             not null,
    LogMessage  varchar(1000)   not null,
    primary key(WorkLogId)
)

alter table WorkLog add
    constraint FK_WorkLog_BillItem foreign key(BillItemId) references BillItem(BillItemId)
        on update cascade
        on delete cascade

insert into WorkLog(BillItemId, LogMessage)
select BillItemId, '' from BillItem where BillItemTypeId = 1
