create table DocumentAttachment (
  DocumentAttachmentId int          not null identity constraint PK_DocumentAttachment primary key,
  DocId                int          not null,
  FileName             varchar(350) not null,
  OriginalFileName     varchar(255) not null
)

alter table DocumentAttachment add
  constraint FK_DocumentAttachment_Document foreign key(DocId) references Document(DocId) on update cascade on delete cascade

