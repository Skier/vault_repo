alter table transaction
add column TimeSyncCompleted datetime default NULL,
add column SyncCount int(10) NOT NULL;

ALTER TABLE transaction DROP FOREIGN KEY FK_Transaction_Order;
ALTER TABLE transaction DROP FOREIGN KEY FK_Transaction_Customer;

update `transaction` set TimeSyncCompleted = now();

CREATE INDEX idx_transaction_servmantransactionid using BTREE ON `transaction` (servmantransactionid)  

create table transactiontemp
(
  `ID` bigint(19) NOT NULL
)

insert into transactiontemp (id)
select t.id
from transaction t
left join `order` on t.ticketnumber = `order`.ticketnumber
where t.ticketNumber is not null and `order`.ticketnumber is null

INSERT INTO `sysversion` (`Version`) VALUES (50);