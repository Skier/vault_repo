CREATE TABLE Client (
    ClientId int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    Name [varchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE ClientAccount (
    ClientAccountId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ClientId int NOT NULL,
    Code varchar(50) NOT NULL,
    Name varchar(50) NOT NULL,
    constraint FK_ClientAccount_Client foreign key(ClientId) 
        references [Client](ClientId) on delete cascade
) ON [PRIMARY]
GO

CREATE TABLE ProjectStatus (
    ProjectStatusId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name varchar(50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE Project (
    ProjectId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name varchar(50) NOT NULL,
    ShortName varchar(25) NOT NULL,
    ClientId int NOT NULL,
    ClientAccountId int NOT NULL,
    ProjectStatusId int NOT NULL,
    Description varchar(250) NOT NULL,
    constraint FK_Project_ProjectStatus foreign key(ProjectStatusId) 
        references [ProjectStatus](ProjectStatusId),
    constraint FK_Project_Client foreign key(ClientId) 
        references [Client](ClientId),
    constraint FK_Project_ClientAccount foreign key(ClientAccountId) 
        references [ClientAccount](ClientAccountId)
) ON [PRIMARY]
GO

CREATE TABLE [File] (
    FileId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    FileName varchar(250) NOT NULL,
    FileUrl varchar(250) NOT NULL,
    FilePath varchar(250) NOT NULL,
    Created datetime NOT NULL,
    CreatedBy int NOT NULL,
    Description varchar(250) NOT NULL,
    constraint FK_File_User foreign key(CreatedBy) references [User](UserId)
) ON [PRIMARY]
GO

CREATE TABLE ProjectAttachmentType (
    ProjectAttachmentTypeId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name varchar(50) NOT NULL,
    Description varchar(250) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE ProjectAttachment (
    ProjectAttachmentId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProjectId int NOT NULL,
    ProjectAttachmentTypeId int NOT NULL,
    FileId int NOT NULL,
    constraint FK_ProjectAttachment_Project foreign key(ProjectId) 
        references [Project](ProjectId) on delete cascade,
    constraint FK_ProjectAttachment_ProjectAttachmentType foreign key(ProjectAttachmentTypeId) 
        references [ProjectAttachmentType](ProjectAttachmentTypeId),
    constraint FK_ProjectAttachment_File foreign key(FileId) 
        references [File](FileId) on delete cascade
) ON [PRIMARY]
GO

CREATE TABLE [ProjectAttachmentInfo](
        [ProjectAttachmentInfoId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [ProjectAttachmentId] [int] NOT NULL,
        [Code] varchar(50) NOT NULL,
        [Value] varchar(250) NOT NULL,

    CONSTRAINT [FK_ProjectAttachmentInfo_ProjectAttachment] 
        FOREIGN KEY([ProjectAttachmentId]) REFERENCES [dbo].[ProjectAttachment] ([ProjectAttachmentId])

) ON [PRIMARY]
GO

CREATE TABLE ProjectTab (
    ProjectTabId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProjectId int NOT NULL,
    Name varchar(50) NOT NULL,
    Description varchar(250) NOT NULL,
    ContactName varchar(50) NOT NULL,
    ContactAddress varchar(250) NOT NULL,
    ContactState int NOT NULL,
    ContactCounty int NOT NULL,
    ContactZip varchar(20) NULL,
    ContactPhone varchar(30) NULL,

    constraint FK_ProjectTab_Project foreign key(ProjectId) 
        references [Project](ProjectId) on delete cascade,
) ON [PRIMARY]
GO

CREATE TABLE ProjectTabDocument (
    ProjectTabDocumentId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProjectTabId int NOT NULL,
    DocumentId int NOT NULL,
    Description varchar(250) NOT NULL,
    Remarks varchar(250) NOT NULL,
    IsActive bit NOT NULL,

    constraint UK_ProjectTabDocument_ProjectTabId_DocumentID unique (ProjectTabId, DocumentId),
    constraint FK_ProjectTabDocument_ProjectTab foreign key(ProjectTabId) 
        references [ProjectTab](ProjectTabId) on delete cascade,
    constraint FK_ProjectTabDocument_Document foreign key(DocumentId) 
        references [Document](DocId)
) ON [PRIMARY]
GO

CREATE TABLE Asset (
    AssetId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    AssetName varchar(250) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE AssetAssignment (
    AssetAssignmentId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    AssetId int NOT NULL,
    ProjectId int NOT NULL,
    constraint FK_AssetAssignment_Asset foreign key(AssetId) 
        references [Asset](AssetId),
    constraint FK_AssetAssignment_Project foreign key(ProjectId) 
        references [Project](ProjectId) on delete cascade,
) ON [PRIMARY]
GO

ALTER TABLE [User] ADD 
    AssetId int NULL constraint FK_User_Asset foreign key(AssetId) references [Asset] (AssetId)
GO

ALTER TABLE [DocumentAttachment] ADD 
    FileId int NULL constraint FK_DocumentAttachment_File foreign key(FileId) references [File] (FileId) on delete cascade
GO

ALTER TABLE [DocumentAttachment] DROP 
    COLUMN FileName,
    COLUMN FileUrl,
    COLUMN Description
GO

if not exists(select UserId from [User] where login = 'bot')
    INSERT INTO [User] (Login, FirstName, LastName, PhoneNumber, Password, Email, IsActive, HackingAttempts, DefaultSite)
    VALUES ('autobot', '', '', '', '', '', 0, 0, '')

declare @botUserId int
select @botUserId = UserId from [User] where login = 'autobot'
SELECT @botUserID

INSERT INTO [File] ([FileName], FileUrl, FilePath, Description, Created, CreatedBy)
SELECT [FileName], 
       FileUrl, 
       'c:\inetpub\wwwroot\weborb\truetract\attachments\' + cast(DocumentId as varchar) + '\' + FileName,
       Description,GetDate(), @botUserID
  FROM [DocumentAttachment] da
WHERE NOT Exists (select 1 from [file] where [file].FileUrl = da.FileUrl)

UPDATE [DocumentAttachment] set
    FileId = (select top 1 FileId from [File] where FileUrl = [DocumentAttachment].FileUrl)
FROM [DocumentAttachment]
GO

ALTER TABLE DocumentAttachment alter column FileId int NOT NULL


/*------- TEST DATA --------

    insert into asset (AssetName) values ('UserAsset')

    update [user] 
       set AssetId = (select AssetId from asset where AssetName = 'UserAsset')
     from [user] where [login] = 'user'

    insert into Client (Name) values ('XTO Energy')
    insert into Client (Name) values ('Anadarko')

    insert into ClientAccount (ClientId, Code, Name) select ClientId, Name, Name from Client

    insert into ProjectStatus (Name) values('Active')
    insert into Project (Name, ShortName, ClientId, ClientAccountId, ProjectStatusId, Description) 
        select 'TH Atkins 1 H', 'TH Atkins 1 H', Client.ClientId, ClientAccountId, 
            (select top 1 ProjectStatusId from ProjectStatus), '' 
          from Client
            join ClientAccount on ClientAccount.ClientId = Client.ClientId
         where Client.Name = 'XTO Energy'

    insert into Project (Name, ShortName, ClientId, ClientAccountId, ProjectStatusId, Description) 
        select 'Noname Project', 'Noname Project', Client.ClientId, ClientAccountId, 
            (select top 1 ProjectStatusId from ProjectStatus), '' 
          from Client
            join ClientAccount on ClientAccount.ClientId = Client.ClientId
         where Client.Name = 'Anadarko'

    insert into assetassignment (assetId, projectId)
    select
        (select AssetId from asset where AssetName = 'UserAsset'),
        ProjectId
    from project

    declare @projectId int

    select @projectId = projectId from Project where ShortName = 'TH Atkins 1 H'

    insert into ProjectTab (ProjectId, Name, Description)
    select @projectId, 'Leases', '' union
    select @projectId, 'Sub Tract 1', '' union
    select @projectId, 'Sub Tract 2', '' union
    select @projectId, 'Adj Tract 1', ''
-------------*/