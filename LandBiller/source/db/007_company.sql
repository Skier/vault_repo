create sequence LL_CompanySqc;
create table LL_Company(
  CompanyId   int          not null DEFAULT nextval('LL_CompanySqc'::regclass),
  UserId      int          not null,
  Name        varchar(250) not null,
  Description varchar(250)
);

alter table LL_Company
   add constraint PK_LL_Company primary key (CompanyId);

alter table LL_Company add
  constraint FK_LL_Company_LL_User foreign key(UserId) references LL_User(UserId);
