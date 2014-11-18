update DOC_LEASE_CLAUSE2 set 
    DESCR=(select OTHER_DESC
             from DOC_LEASE_CLAUSE 
            where ID=DOC_LEASE_CLAUSE2.LEASE_ID)
where CODE='OTHER';

insert into sys_version (version) values ('0.0.65');
