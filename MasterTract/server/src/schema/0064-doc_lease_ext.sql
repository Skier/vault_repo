update DOC_LEASE_CLAUSE2 set
    TERM=(select TERM from DOC_LEASE_EXT where DOC_LEASE_EXT.LEASE_ID=DOC_LEASE_CLAUSE2.LEASE_ID),
    ROYALTY=(select ROYALTY from DOC_LEASE_EXT where DOC_LEASE_EXT.LEASE_ID=DOC_LEASE_CLAUSE2.LEASE_ID),
    BONUS_RATE=(select BONUS_RATE from DOC_LEASE_EXT where DOC_LEASE_EXT.LEASE_ID=DOC_LEASE_CLAUSE2.LEASE_ID),
    BONUS_AMT=(select BONUS_AMT from DOC_LEASE_EXT where DOC_LEASE_EXT.LEASE_ID=DOC_LEASE_CLAUSE2.LEASE_ID),
    DESCR=(select NOTE from DOC_LEASE_EXT where DOC_LEASE_EXT.LEASE_ID=DOC_LEASE_CLAUSE2.LEASE_ID)
where CODE='OPT_TO_EXT';

--alter table DOC_LEASE_EXT drop constraint FK_DLE_DOC_LEASE;

--drop table doc_lease_ext;

insert into sys_version (version) values ('0.0.64');
