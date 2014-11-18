create sequence LL_File_Sqc;
create table LL_File(
  FileId       int          not null DEFAULT nextval('LL_File_Sqc'::regclass),
  OrigFilename text         not null,
  StorageKey   text         not null,
  UserId       int          not null,
  Note         text
);

alter table LL_File
   add constraint PK_LL_File primary key (FileId);

alter table LL_File add
  constraint FK_LL_File_LL_User foreign key(UserId) references LL_User(UserId);

