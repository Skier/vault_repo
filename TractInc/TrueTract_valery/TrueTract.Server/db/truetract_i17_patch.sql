/*
-- =============================================
-- Author:      Vitaly Vengrov
-- Create date: 2007/09/05
-- Description: Migration script for production database
-- =============================================

List of changes:

1. Deleted tables [TractBackup], [TractCallsBackup], [TractTextObjectsBackup], [UserTractHistory]
2. Added table [DocumentAttachmentType]. Filled by initial data
3. For FKs of tables [Permission], [PermissionAssignment], [UserRole] removed "ONUPDATE cascade"
4. Removed timestamp column in Tract table
5. [TracCalls] table renamed to [TractCall]
6. [TracTextObjects] table renamed to [TractTextObject]
7. Added column AcresRate into table Unit. Filed by initial data
8. Added tables [Group], [GroupItem], [GroupUser]
9. Added IX_AsNamed index for [Participant] table
10. [Document] table. Deleted columns DateFiledYear, DateFiledMonth, DateFiledDay, DateSignedYear, DateSignedMonth, DateSignedDay.
11. [Document] table. Added columns IsActive, DocBranchUid, Filed, Signed, Created, CreatedBy.
12. [Document] table. Deleted ck_documentKeyFields.
13. [Document] table. Added FK_Document_User.
14. Modified [DocumentAttachment] table.
15. Attachment files moved from "#attachmentDir#" folder to #attachmentDir#/#DocId#/ folder

*/

/*-- Delete tables [TractCallsBackup], [TractTextObjectsBackup], [TractBackup], [UserTractHistory] -- */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TractCallsBackup]') AND type in (N'U'))
    DROP TABLE [dbo].[TractCallsBackup]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TractTextObjectsBackup]') AND type in (N'U'))
    DROP TABLE [dbo].[TractTextObjectsBackup]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TractBackup]') AND type in (N'U'))
    DROP TABLE [dbo].[TractBackup]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTractHistory]') AND type in (N'U'))
    DROP TABLE [dbo].[UserTractHistory]
--

/*-- Create table DocumentAttachmentType */
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].DocumentAttachmentType') AND type in (N'U'))
BEGIN
    
    create table DocumentAttachmentType(
      DocumentAttachmentTypeId int         not null identity constraint PK_DocumentAttachmentType primary key,
      Name                     varchar(50) not null
    )

    set identity_insert dbo.DocumentAttachmentType on
        INSERT INTO dbo.DocumentAttachmentType (DocumentAttachmentTypeId, Name) VALUES (1, N'Document PDF Copy')
        INSERT INTO dbo.DocumentAttachmentType (DocumentAttachmentTypeId, Name) VALUES (2, N'Other')
    set identity_insert dbo.DocumentAttachmentType off
END
--

/*-- Alter table Document */

alter table Document add
  IsActive bit not null default 1,
  DocBranchUid uniqueidentifier not null default NEWID(),
  Filed datetime null,
  Signed datetime null,
  Created datetime null,
  CreatedBy int null

go

update document 
   set dateFiledYear = case when dateFiledYear < 1900 then 1900 else dateFiledYear end,
       dateSignedYear = case when dateSignedYear < 1900 then 1900 else dateSignedYear end
from document 

update Document set 
      Signed = cast(cast(DateSignedYear as varchar) + '-' + 
               cast(DateSignedMonth as varchar) + '-' + 
               cast(DateSignedDay as varchar) as datetime),
       Filed = cast(cast(DateFiledYear as varchar) + '-' + 
               cast(DateFiledMonth as varchar) + '-' + 
               cast(DateFiledDay as varchar) as datetime),
   CreatedBy = (select top 1 createdBy from tract where tract.docId = [Document].DocId),
     Created = GetDate()
from [Document]

delete from Document where createdBy is null

alter table Document alter column
  IsActive bit not null

alter table Document alter column
  DocBranchUid uniqueidentifier not null

alter table Document alter column
  Filed datetime not null

alter table Document alter column
  Signed datetime not null

alter table Document alter column
  Created datetime not null

alter table Document alter column
  CreatedBy int not null

alter table Document drop
  column DateFiledYear,
  column DateFiledMonth,
  column DateFiledDay,
  column DateSignedYear,
  column DateSignedMonth,
  column DateSignedDay

create index IX_DocBranchUid on Document(DocBranchUid)

alter table Document drop
  constraint FK_Document_DocumentType,
  constraint ck_documentKeyFields

alter table Document add
  constraint FK_Document_DocumentType foreign key(DocTypeId) references DocumentType(DocTypeID),
  constraint FK_Document_User foreign key(CreatedBy) references [User](UserId)

-------
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_Module1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
    ALTER TABLE [dbo].[Permission] DROP CONSTRAINT [FK_Permission_Module1]

ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Module1] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([ModuleId])
ON DELETE CASCADE
----
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PermissionAssignment_Permission]') AND parent_object_id = OBJECT_ID(N'[dbo].[PermissionAssignment]'))
    ALTER TABLE [dbo].[PermissionAssignment] DROP CONSTRAINT [FK_PermissionAssignment_Permission]

ALTER TABLE [dbo].[PermissionAssignment]  WITH CHECK ADD  CONSTRAINT [FK_PermissionAssignment_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permission] ([PermissionId])
ON DELETE CASCADE
----
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PermissionAssignment_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[PermissionAssignment]'))
    ALTER TABLE [dbo].[PermissionAssignment] DROP CONSTRAINT [FK_PermissionAssignment_Role]

ALTER TABLE [dbo].[PermissionAssignment]  WITH CHECK ADD  CONSTRAINT [FK_PermissionAssignment_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
ON DELETE CASCADE
----

alter table Tract drop
  column timestamp
----
EXEC sp_rename 'TractCalls', 'TractCall';
EXEC sp_rename 'TractCall.CallType', 'Type', 'COLUMN';
EXEC sp_rename 'TractCall.CallDBValue', 'Params', 'COLUMN';
EXEC sp_rename 'TractCall.CallOrder', 'Order', 'COLUMN';
EXEC sp_rename 'TractCall.PK_TractCalls', 'PK_TractCall', 'INDEX';
----
EXEC sp_rename 'TractTextObjects', 'TractTextObject';
EXEC sp_rename 'TractTextObject.PK_TractTextObjects', 'PK_TractTextObject', 'INDEX';
----
create index IX_AsNamed on Participant(AsNamed)
----

/*START ****** ADD GROUP, GROUPITEM AND GROUPUSER TABLES */
    create table [Group](
      GroupId   int         not null identity constraint PK_Group primary key,
      GroupName varchar(50) not null
    )
    go

    create table GroupItem(
      GroupItemId  int              not null identity constraint PK_GroupItem primary key,
      GroupId      int              not null,
      DocBranchUid uniqueidentifier not null
    )
    go

    alter table GroupItem add
      constraint FK_GroupItem_Group foreign key(GroupId) references [Group](GroupId) on delete cascade
    go

    create unique index UK_GroupId_DocBranchUid on GroupItem(GroupId,DocBranchUid)
    go

    create table GroupUser(
      GroupUserId int not null identity constraint PK_GroupUsers primary key,
      GroupId     int not null,
      UserId      int not null,

      constraint UK_GroupId_UserID unique(GroupId,UserId)
    )
    go

    alter table GroupUser add
      constraint FK_UserGroup_User foreign key(UserId) references [User](UserId) on delete cascade,
      constraint FK_UserGroup_Group foreign key(GroupId) references [Group](GroupId) on delete cascade
    go
/*END ****** ADD GROUP, GROUPITEM AND GROUPUSER TABLES */

/*START ****** ATTACHMENTS TABLES MODIFCATION */
    EXEC sp_rename 'PK_DocumentAttachment', 'PK_DocumentAttachment_Old';
    EXEC sp_rename 'FK_DocumentAttachment_Document', 'FK_DocumentAttachment_Document_Old';
    EXEC sp_rename 'DocumentAttachment', 'DocumentAttachment_Old';
    go

    create table DocumentAttachment(
      DocumentAttachmentId     int          not null identity constraint PK_DocumentAttachment primary key,
      DocumentAttachmentTypeId int          not null,
      DocumentId               int          not null,
      FileName                 varchar(250) not null,
      FileUrl                  varchar(250) not null,
      Description              varchar(250)
    )
    go

    alter table DocumentAttachment add
      constraint FK_DocumentAttachment_Document foreign key(DocumentId) references Document(DocID),
      constraint FK_DocumentAttachment_DocumentAttachmentType foreign key(DocumentAttachmentTypeId) references DocumentAttachmentType(DocumentAttachmentTypeId)
/*END ****** ATTACHMENTS TABLES MODIFCATION */

/*START ****** MOVE ATTACHMENT FILES TO NEW PLACE */

        EXEC sp_configure 'xp_cmdshell', 1
        GO
        RECONFIGURE
        GO

    declare @ATTACHMENTS_STORAGE_URL as varchar(500),
            @ATTACHMENTS_OLD_DIR as varchar(500),
            @ATTACHMENTS_NEW_DIR as varchar(500)

    declare @oldFileName as varchar(500), @newFileName as varchar(500), @docId int
    declare @cmd as varchar(1000)

    set @ATTACHMENTS_OLD_DIR = 'D:\public\test.affilia.com\weborb\truetract\attachments\'
    set @ATTACHMENTS_NEW_DIR = 'D:\public\test.affilia.com\weborb\truetract\attachments\'

    declare att_cursor cursor FORWARD_ONLY for (
            select 
                @ATTACHMENTS_OLD_DIR + [FileName], 
                @ATTACHMENTS_NEW_DIR + cast(DocId as varchar(10)) + '\' + OriginalFileName,
                                docId
              from DocumentAttachment_Old
    )

    open att_cursor
    FETCH NEXT FROM att_cursor into @oldFileName, @newFileName, @docId

    WHILE @@FETCH_STATUS = 0
    BEGIN
                set @cmd = 'mkdir ' + @ATTACHMENTS_NEW_DIR + cast(@docId as varchar(10))
                EXEC xp_cmdshell @cmd;

        set @cmd = 'copy "' + @oldFileName + '" "' + @newFileName + '" /Y'
        print @cmd
        EXEC xp_cmdshell @cmd;
        
        FETCH NEXT FROM att_cursor into @oldFileName, @newFileName, @docId
    END

    CLOSE att_cursor
    DEALLOCATE att_cursor
/*END ****** MOVE ATTACHMENT FILES TO NEW PLACE */

/*START ****** IMPORT ATTACHMENT RECORDS FROM OLD TABLE */
    --set @ATTACHMENTS_STORAGE_URL = 'http://www.scopemapping.com/weborb30/truetract/attachments/'
    set @ATTACHMENTS_STORAGE_URL = 'http://test.affilia.com/weborb/truetract/attachments/'

    insert into [DocumentAttachment] (DocumentAttachmentTypeId, DocumentId, FileName, FileUrl, Description)
    select 1, 
           DocId, 
           OriginalFileName, 
           @ATTACHMENTS_STORAGE_URL + cast(DocId as varchar) + '/' + OriginalFileName,
           ''
      from [DocumentAttachment_Old]
/*END ****** IMPORT ATTACHMENT RECORDS FROM OLD TABLE */

/*START ****** ADD ACRESRATE COLUMN TO TABLE UNIT */
        delete from Unit

    alter table Unit add
      AcresRate float not null
    go

    set identity_insert dbo.Unit on
        INSERT INTO dbo.Unit (UnitId, Name, AcresRate) VALUES (1, N'Square Feets', 43560)
        INSERT INTO dbo.Unit (UnitId, Name, AcresRate) VALUES (2, N'Acres', 1)
    set identity_insert dbo.Unit off

    alter table Tract add
      constraint FK_Tract_Unit foreign key(UnitId) references [Unit](UnitId)
/*END ****** ADD ACRESRATE COLUMN TO TABLE UNIT */
go