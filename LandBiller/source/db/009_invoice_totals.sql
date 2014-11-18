alter table LL_Invoice add Amount numeric(18,3) null;
alter table LL_Invoice add Adjustment numeric(18,3) null;
alter table LL_Invoice add Total numeric(18,3) null;

update LL_Invoice set Amount = 0;
update LL_Invoice set Adjustment = 0;
update LL_Invoice set Total = 0;

alter table LL_Invoice alter column Amount set not null;
alter table LL_Invoice alter column Adjustment set not null;
alter table LL_Invoice alter column Total set not null;
