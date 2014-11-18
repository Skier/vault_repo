alter table TT_DocumentType add IsLease bit not null default 0
go

update TT_DocumentType set IsLease=1 where [name] like '%lease%'
go
