if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Participant_Address]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Participant] DROP CONSTRAINT FK_Participant_Address
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Participant_Address1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Participant] DROP CONSTRAINT FK_Participant_Address1
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Document_County]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Document] DROP CONSTRAINT FK_Document_County
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Participant_Document]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Participant] DROP CONSTRAINT FK_Participant_Document
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Tract_Document]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Tract] DROP CONSTRAINT FK_Tract_Document
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Document_DocumentType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Document] DROP CONSTRAINT FK_Document_DocumentType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ParticipantEntityParty_Participant]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ParticipantEntityParty] DROP CONSTRAINT FK_ParticipantEntityParty_Participant
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ParticipantReservation_Participant]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ParticipantReservation] DROP CONSTRAINT FK_ParticipantReservation_Participant
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Participant_ParticipantType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Participant] DROP CONSTRAINT FK_Participant_ParticipantType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_County_State]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[County] DROP CONSTRAINT FK_County_State
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Document_State]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Document] DROP CONSTRAINT FK_Document_State
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TractException_Tract]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[TractException] DROP CONSTRAINT FK_TractException_Tract
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Tract_Unit]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Tract] DROP CONSTRAINT FK_Tract_Unit
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Address]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Address]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[County]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[County]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Document]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Document]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DocumentType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DocumentType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Participant]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Participant]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ParticipantEntityParty]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ParticipantEntityParty]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ParticipantReservation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ParticipantReservation]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ParticipantType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ParticipantType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[State]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[State]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Tract]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Tract]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TractException]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TractException]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Unit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Unit]
GO

CREATE TABLE [dbo].[Address] (
    [AddressID] [int] IDENTITY (1, 1) NOT NULL ,
    [Line1] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [Line2] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [City] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [State] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [Zip] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [IncareOf] [varchar] (50) COLLATE Ukrainian_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[County] (
    [CountyId] [int] IDENTITY (1, 1) NOT NULL ,
    [Name] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [StateId] [int] NOT NULL ,
    [StateName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [StateFips] [varchar] (2) COLLATE Ukrainian_CI_AS NULL ,
    [CountyFips] [varchar] (3) COLLATE Ukrainian_CI_AS NULL ,
    [Fips] [varchar] (5) COLLATE Ukrainian_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Document] (
    [DocID] [int] IDENTITY (1, 1) NOT NULL ,
    [IsPublic] [bit] NOT NULL ,
    [DocTypeId] [int] NULL ,
    [Volume] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [Page] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [DocumentNo] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [County] [int] NULL ,
    [State] [int] NULL ,
    [DateFiledYear] [int] NULL ,
    [DateFiledMonth] [int] NULL ,
    [DateFiledDay] [int] NULL ,
    [DateSignedYear] [int] NULL ,
    [DateSignedMonth] [int] NULL ,
    [DateSignedDay] [int] NULL ,
    [ResearchNote] [varchar] (350) COLLATE Ukrainian_CI_AS NULL ,
    [ImageLink] [varchar] (350) COLLATE Ukrainian_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DocumentType] (
    [DocTypeID] [int] IDENTITY (1, 1) NOT NULL ,
    [Name] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [SellerRoleName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [BuyerRoleName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Participant] (
    [ParticipantID] [int] IDENTITY (1, 1) NOT NULL ,
    [DocID] [int] NULL ,
    [DocRoleID] [int] NULL ,
    [AsNamed] [varchar] (350) COLLATE Ukrainian_CI_AS NULL ,
    [PhoneHome] [varchar] (10) COLLATE Ukrainian_CI_AS NULL ,
    [PhoneOffice] [varchar] (10) COLLATE Ukrainian_CI_AS NULL ,
    [PhoneCell] [varchar] (10) COLLATE Ukrainian_CI_AS NULL ,
    [PhoneAlt] [varchar] (10) COLLATE Ukrainian_CI_AS NULL ,
    [EntityName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [FirstName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [MiddleName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [LastName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [ContactPosition] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [TaxID] [varchar] (12) COLLATE Ukrainian_CI_AS NULL ,
    [SSN] [varchar] (12) COLLATE Ukrainian_CI_AS NULL ,
    [MailingAddress] [int] NULL ,
    [PhisicalAddress] [int] NULL ,
    [ParentID] [int] NOT NULL ,
    [TypeId] [int] NULL ,
    [IsSeller] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ParticipantEntityParty] (
    [ParticipantEntityPartyID] [int] IDENTITY (1, 1) NOT NULL ,
    [ParticipantID] [int] NULL ,
    [FirstName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [MiddleName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [LastName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [SSN] [varchar] (50) COLLATE Ukrainian_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ParticipantReservation] (
    [DocReservationID] [int] IDENTITY (1, 1) NOT NULL ,
    [ParticipantID] [int] NULL ,
    [Details] [varchar] (350) COLLATE Ukrainian_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ParticipantType] (
    [TypeID] [int] IDENTITY (1, 1) NOT NULL ,
    [Name] [varchar] (50) COLLATE Ukrainian_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[State] (
    [StateId] [int] NOT NULL ,
    [Name] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [StateFips] [varchar] (2) COLLATE Ukrainian_CI_AS NULL ,
    [StateAbbr] [varchar] (2) COLLATE Ukrainian_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Tract] (
    [TractID] [int] IDENTITY (1, 1) NOT NULL ,
    [DocID] [int] NULL ,
    [RefName] [varchar] (50) COLLATE Ukrainian_CI_AS NULL ,
    [CalledAC] [decimal](18, 0) NULL ,
    [UnitId] [int] NULL ,
    [ScopePlotUrl] [varchar] (50) COLLATE Ukrainian_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TractException] (
    [TractExceptionID] [int] IDENTITY (1, 1) NOT NULL ,
    [TractID] [int] NULL ,
    [ExceptionID] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Unit] (
    [UnitId] [int] IDENTITY (1, 1) NOT NULL ,
    [Name] [varchar] (50) COLLATE Ukrainian_CI_AS NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Address] WITH NOCHECK ADD 
    CONSTRAINT [PK_Address] PRIMARY KEY  CLUSTERED 
    (
        [AddressID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[County] WITH NOCHECK ADD 
    CONSTRAINT [PK_County] PRIMARY KEY  CLUSTERED 
    (
        [CountyId]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Document] WITH NOCHECK ADD 
    CONSTRAINT [PK_Document] PRIMARY KEY  CLUSTERED 
    (
        [DocID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[DocumentType] WITH NOCHECK ADD 
    CONSTRAINT [PK_DocumentType] PRIMARY KEY  CLUSTERED 
    (
        [DocTypeID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Participant] WITH NOCHECK ADD 
    CONSTRAINT [PK_Participant] PRIMARY KEY  CLUSTERED 
    (
        [ParticipantID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ParticipantEntityParty] WITH NOCHECK ADD 
    CONSTRAINT [PK_ParticipantEntityParty] PRIMARY KEY  CLUSTERED 
    (
        [ParticipantEntityPartyID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ParticipantReservation] WITH NOCHECK ADD 
    CONSTRAINT [PK_ParticipantReservation] PRIMARY KEY  CLUSTERED 
    (
        [DocReservationID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ParticipantType] WITH NOCHECK ADD 
    CONSTRAINT [PK_ParticipantType] PRIMARY KEY  CLUSTERED 
    (
        [TypeID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[State] WITH NOCHECK ADD 
    CONSTRAINT [PK_State] PRIMARY KEY  CLUSTERED 
    (
        [StateId]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Tract] WITH NOCHECK ADD 
    CONSTRAINT [PK_Tract] PRIMARY KEY  CLUSTERED 
    (
        [TractID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TractException] WITH NOCHECK ADD 
    CONSTRAINT [PK_TractException] PRIMARY KEY  CLUSTERED 
    (
        [TractExceptionID]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Unit] WITH NOCHECK ADD 
    CONSTRAINT [PK_Unit] PRIMARY KEY  CLUSTERED 
    (
        [UnitId]
    )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[County] ADD 
    CONSTRAINT [FK_County_State] FOREIGN KEY 
    (
        [StateId]
    ) REFERENCES [dbo].[State] (
        [StateId]
    )
GO

ALTER TABLE [dbo].[Document] ADD 
    CONSTRAINT [FK_Document_County] FOREIGN KEY 
    (
        [County]
    ) REFERENCES [dbo].[County] (
        [CountyId]
    ),
    CONSTRAINT [FK_Document_DocumentType] FOREIGN KEY 
    (
        [DocTypeId]
    ) REFERENCES [dbo].[DocumentType] (
        [DocTypeID]
    ),
    CONSTRAINT [FK_Document_State] FOREIGN KEY 
    (
        [State]
    ) REFERENCES [dbo].[State] (
        [StateId]
    )
GO

ALTER TABLE [dbo].[Participant] ADD 
    CONSTRAINT [FK_Participant_Address] FOREIGN KEY 
    (
        [MailingAddress]
    ) REFERENCES [dbo].[Address] (
        [AddressID]
    ),
    CONSTRAINT [FK_Participant_Address1] FOREIGN KEY 
    (
        [PhisicalAddress]
    ) REFERENCES [dbo].[Address] (
        [AddressID]
    ),
    CONSTRAINT [FK_Participant_Document] FOREIGN KEY 
    (
        [DocID]
    ) REFERENCES [dbo].[Document] (
        [DocID]
    ),
    CONSTRAINT [FK_Participant_ParticipantType] FOREIGN KEY 
    (
        [TypeId]
    ) REFERENCES [dbo].[ParticipantType] (
        [TypeID]
    )
GO

ALTER TABLE [dbo].[ParticipantEntityParty] ADD 
    CONSTRAINT [FK_ParticipantEntityParty_Participant] FOREIGN KEY 
    (
        [ParticipantID]
    ) REFERENCES [dbo].[Participant] (
        [ParticipantID]
    )
GO

ALTER TABLE [dbo].[ParticipantReservation] ADD 
    CONSTRAINT [FK_ParticipantReservation_Participant] FOREIGN KEY 
    (
        [ParticipantID]
    ) REFERENCES [dbo].[Participant] (
        [ParticipantID]
    )
GO

ALTER TABLE [dbo].[Tract] ADD 
    CONSTRAINT [FK_Tract_Document] FOREIGN KEY 
    (
        [DocID]
    ) REFERENCES [dbo].[Document] (
        [DocID]
    ),
    CONSTRAINT [FK_Tract_Unit] FOREIGN KEY 
    (
        [UnitId]
    ) REFERENCES [dbo].[Unit] (
        [UnitId]
    )
GO

ALTER TABLE [dbo].[TractException] ADD 
    CONSTRAINT [FK_TractException_Tract] FOREIGN KEY 
    (
        [TractID]
    ) REFERENCES [dbo].[Tract] (
        [TractID]
    )
GO

