-- delete from DOC_LEASE_CLAUSE2;

insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'DEPTH',
    ''
from DOC_LEASE_CLAUSE
where DEPTH is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'DAMAGE',
    ''
from DOC_LEASE_CLAUSE
where DAMAGE is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'PUGH',
    ''
from DOC_LEASE_CLAUSE
where PUGH is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'SHUT_IN_GAS',
    ''
from DOC_LEASE_CLAUSE
where SHUT_IN_GAS is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'TAKE_GAS_ROY',
    ''
from DOC_LEASE_CLAUSE
where TAKE_GAS_ROY is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'SURFACE',
    ''
from DOC_LEASE_CLAUSE
where SURFACE is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'CONT_DRILL',
    ''
from DOC_LEASE_CLAUSE
where CONT_DRILL is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'FAV_NAT',
    ''
from DOC_LEASE_CLAUSE
where FAV_NAT is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'OPT_TO_EXT',
    ''
from DOC_LEASE_CLAUSE
where OPT_TO_EXT is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'ASSIGNMENT',
    ''
from DOC_LEASE_CLAUSE
where ASSIGNMENT is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'PROD_PAYM',
    ''
from DOC_LEASE_CLAUSE
where PROD_PAYM is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'POOL_PROV',
    ''
from DOC_LEASE_CLAUSE
where POOL_PROV is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'MIN_ROY_PAY',
    ''
from DOC_LEASE_CLAUSE
where MIN_ROY_PAY is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'RENEWAL_OPT',
    ''
from DOC_LEASE_CLAUSE
where RENEWAL_OPT is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'HBP',
    ''
from DOC_LEASE_CLAUSE
where HBP is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'SPC_PROV',
    ''
from DOC_LEASE_CLAUSE
where SPC_PROV is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'LESSER_INT',
    ''
from DOC_LEASE_CLAUSE
where LESSER_INT is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'REWORK_DAYS',
    ''
from DOC_LEASE_CLAUSE
where REWORK_DAYS is true;
    
insert into DOC_LEASE_CLAUSE2 (
    ID,
    LEASE_ID,
    CODE,
    NAME
) select
    nextval('DOC_LEASE_CLAUSE_SQC'),
    ID,
    'OTHER',
    ''
from DOC_LEASE_CLAUSE
where OTHER is true;

drop table DOC_LEASE_CLAUSE;
    
insert into sys_version (version) values ('0.0.49');
