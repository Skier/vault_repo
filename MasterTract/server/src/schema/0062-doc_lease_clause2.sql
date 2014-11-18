alter table doc_lease_clause2 add CREATED     date not null default now();
alter table doc_lease_clause2 add MODIFIED    date not null default now();
alter table doc_lease_clause2 add IS_ACTIVE   bool not null default true;
alter table doc_lease_clause2 add DETAILS     text null;
alter table doc_lease_clause2 add TERM        int           null;
alter table doc_lease_clause2 add ROYALTY     decimal(17,5) null;
alter table doc_lease_clause2 add BONUS_RATE  decimal(17,5) null;
alter table doc_lease_clause2 add BONUS_AMT   decimal(17,5) null;

insert into sys_version (version) values ('0.0.62');
