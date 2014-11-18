alter table DOC_ATTACHMENT alter column FILE_ID drop not null;

alter table DOC_ATTACHMENT add constraint FK_DOC_A_DOC_RECORD foreign key(RECORD_ID) 
    references DOC_RECORD(ID);

insert into sys_version (version) values ('0.0.45');
