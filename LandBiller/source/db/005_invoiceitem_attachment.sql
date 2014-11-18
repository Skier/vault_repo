create sequence LL_InvoiceItemAttachment_Sqc;
create table LL_InvoiceItemAttachment(
  InvoiceItemAttachmentId int  not null DEFAULT nextval('LL_InvoiceItemAttachment_Sqc'::regclass),
  InvoiceItemId           int  not null,
  FileId                  int  not null
);

alter table LL_InvoiceItemAttachment
   add constraint PK_LL_InvoiceItemAttachment primary key (InvoiceItemAttachmentId);

alter table LL_InvoiceItemAttachment add
  constraint FK_LL_InvoiceItemAttachment_LL_InvoiceItem foreign key(InvoiceItemId) references LL_InvoiceItem(InvoiceItemId);

alter table LL_InvoiceItemAttachment add
  constraint FK_LL_InvoiceItemAttachment_LL_File foreign key(FileId) references LL_File(FileId);

