alter table LL_Invoice add Status varchar(256) null;

update LL_Invoice set Status = 'NEW';

alter table LL_Invoice alter column Status set not null;