alter table DOC_LEASE_EXT add LEASE_ID int null;

update DOC_LEASE_EXT set 
    LEASE_ID = (select ID from DOC_LEASE_CLAUSE where EXT_ID=DOC_LEASE_EXT.ID)
where (select ID from DOC_LEASE_CLAUSE where EXT_ID=DOC_LEASE_EXT.ID) is not null;

alter table DOC_LEASE_EXT alter column LEASE_ID set not null;

alter table DOC_LEASE_EXT add constraint FK_DLE_DOC_LEASE foreign key(LEASE_ID) 
    references DOC_LEASE(ID);

insert into sys_version (version) values ('0.0.48');
