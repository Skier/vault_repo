--insert into LL_Company (UserId, Name, Description) values (1, 'Love Land', 'test company for old data');

--alter table LL_Client add CompanyId int null;
alter table LL_Client add CompanyId int not null;

--update LL_Client set CompanyId = 1;

alter table LL_Client add
  constraint FK_LL_Client_LL_Company foreign key(CompanyId) references LL_Company(CompanyId);

--alter table LL_Client alter column CompanyId set not null;
