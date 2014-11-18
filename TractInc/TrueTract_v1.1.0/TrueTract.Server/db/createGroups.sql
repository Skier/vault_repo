IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupItems]') AND type in (N'U'))
DROP TABLE [dbo].[GroupItems]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupUsers]') AND type in (N'U'))
DROP TABLE [dbo].[GroupUsers]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND type in (N'U'))
DROP TABLE [dbo].[Group]

CREATE TABLE [dbo].[Group](
        [GroupId] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Group] PRIMARY KEY,
        [GroupName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL
) ON [PRIMARY]


CREATE TABLE [dbo].[GroupUsers](
    [GroupUsersId] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_GroupUsers] PRIMARY KEY,
    [GroupId] [int] not null,
    [UserId] [int] not null

    constraint UK_GroupId_UserID unique ([GroupId], [UserId])
    constraint FK_UserGroups_User foreign key(UserId) references [User](UserId) on delete cascade,
    constraint FK_UserGroups_Group foreign key(GroupId) references [Group](GroupId) on delete cascade,

) ON [PRIMARY]


CREATE TABLE [dbo].[GroupItems](
    [GroupItemsId] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_GroupItems] PRIMARY KEY,
    [GroupId] [int] not null,
    [DocumentId] [int] null,
    [DrawingId] [int] null

    constraint UK_GroupId_DocumentID_DrawingId unique ([GroupId], [DocumentId], [DrawingId])
    constraint FK_GroupItems_Group foreign key(GroupId) references [Group](GroupId) on delete cascade,
    constraint FK_GroupItems_Document foreign key(DocumentId) references [Document](DocId) on delete cascade,
    constraint FK_GroupItems_Drawing foreign key(DrawingId) references [Tract](TractId) /* we can't use on delete cascade here because this will cause cycling*/

) ON [PRIMARY]
