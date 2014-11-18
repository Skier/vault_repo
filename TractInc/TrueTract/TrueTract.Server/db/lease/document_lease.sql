create table TT_DocumentLease (
    DocLeaseID      int          not null identity constraint PK_DocumentLease primary key,
    DocId           int          not null,
    LCN             varchar(36)  null,
    Term            int          null,
    Royalty         decimal(32,16) null,
    EffectiveDate   datetime null,
    Acreage         decimal(18,3) null,
    AliasGrantee    varchar(50)  null,
    ExpirationDate  datetime null,
    HBP             bit null
)

alter table TT_DocumentLease add
  constraint FK_DocumentLease_Document foreign key(DocId) references [TT_Document](DocId) on delete cascade
