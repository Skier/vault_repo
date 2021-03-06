USE [doc-capture]
GO

/****** Object:  Table [dbo].[Addresstype]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Addresstype](
	[AddressTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Types] [varchar](50) NULL,
 CONSTRAINT [PK_addresstype] PRIMARY KEY CLUSTERED 
(
	[AddressTypeID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Countys]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countys](
	[OBJECTID] [int] NOT NULL,
	[STATE_ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[STATE_NAME] [nvarchar](50) NULL,
	[STATE_FIPS] [nvarchar](3) NULL,
	[CNTY_FIPS] [nvarchar](5) NULL,
	[FIPS] [nvarchar](8) NULL,
	[POP2000] [decimal](18, 0) NULL,
	[POP2004] [decimal](18, 0) NULL,
	[POP00_SQMI] [decimal](18, 0) NULL,
	[POP04_SQMI] [decimal](18, 0) NULL,
	[WHITE] [decimal](18, 0) NULL,
	[BLACK] [decimal](18, 0) NULL,
	[AMERI_ES] [decimal](18, 0) NULL,
	[ASIAN] [decimal](18, 0) NULL,
	[HAWN_PI] [decimal](18, 0) NULL,
	[OTHER] [decimal](18, 0) NULL,
	[MULT_RACE] [decimal](18, 0) NULL,
	[HISPANIC] [decimal](18, 0) NULL,
	[MALES] [decimal](18, 0) NULL,
	[FEMALES] [decimal](18, 0) NULL,
	[AGE_UNDER5] [decimal](18, 0) NULL,
	[AGE_5_17] [decimal](18, 0) NULL,
	[AGE_18_21] [decimal](18, 0) NULL,
	[AGE_22_29] [decimal](18, 0) NULL,
	[AGE_30_39] [decimal](18, 0) NULL,
	[AGE_40_49] [decimal](18, 0) NULL,
	[AGE_50_64] [decimal](18, 0) NULL,
	[AGE_65_UP] [decimal](18, 0) NULL,
	[MED_AGE] [decimal](18, 0) NULL,
	[MED_AGE_M] [decimal](18, 0) NULL,
	[MED_AGE_F] [decimal](18, 0) NULL,
	[HOUSEHOLDS] [decimal](18, 0) NULL,
	[AVE_HH_SZ] [decimal](18, 0) NULL,
	[HSEHLD_1_M] [decimal](18, 0) NULL,
	[HSEHLD_1_F] [decimal](18, 0) NULL,
	[MARHH_CHD] [decimal](18, 0) NULL,
	[MARHH_NO_C] [decimal](18, 0) NULL,
	[MHH_CHILD] [decimal](18, 0) NULL,
	[FHH_CHILD] [decimal](18, 0) NULL,
	[FAMILIES] [decimal](18, 0) NULL,
	[AVE_FAM_SZ] [decimal](18, 0) NULL,
	[HSE_UNITS] [decimal](18, 0) NULL,
	[VACANT] [decimal](18, 0) NULL,
	[OWNER_OCC] [decimal](18, 0) NULL,
	[RENTER_OCC] [decimal](18, 0) NULL,
	[NO_FARMS97] [decimal](18, 0) NULL,
	[AVG_SIZE97] [decimal](18, 0) NULL,
	[CROP_ACR97] [decimal](18, 0) NULL,
	[AVG_SALE97] [decimal](18, 0) NULL,
	[SQMI] [decimal](18, 0) NULL,
	[Shape_Leng] [decimal](18, 0) NULL,
	[Shape_Area] [decimal](18, 0) NULL,
	[IcoMapAttr] [nvarchar](255) NULL,
 CONSTRAINT [PK_Countys] PRIMARY KEY CLUSTERED 
(
	[OBJECTID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Document]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Document](
	[DocID] [int] IDENTITY(1,1) NOT NULL,
	[IsPublic] [bit] NOT NULL,
	[DocTypeId] [int] NULL,
	[Vol] [varchar](50) NULL,
	[Pg] [varchar](50) NULL,
	[DocumentNo] [varchar](50) NULL ,
	[County] [varchar](50) NULL ,
	[State] [varchar](50) NULL ,
	[DateFiled] [datetime] NULL ,
	[DateSigned] [datetime] NULL ,
	[ResearchNote] [varchar](350) NULL,
	[ImageLink] [varchar](350) NULL,
 CONSTRAINT [PK_document] PRIMARY KEY CLUSTERED 
(
	[DocID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Documenttype]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Documenttype](
	[DocTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[SellerRole] [varchar](50) NULL,
	[BuyerRole] [varchar](50) NULL,
 CONSTRAINT [PK_documenttype] PRIMARY KEY CLUSTERED 
(
	[DocTypeID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Participant]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Participant](
	[ParticipantID] [int] IDENTITY(1,1) NOT NULL,
	[DocID] [int] NULL,
	[DocRoleID] [int] NULL,
	[AsNamed] [varchar](350) NULL,
	[PhoneHome] [varchar](10) NULL,
	[PhoneOffice] [varchar](10) NULL,
	[PhoneCell] [varchar](10) NULL,
	[PhoneAlt] [varchar](10) NULL,
	[EntityName] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[ContactPosition] [varchar](50) NULL,
	[TAXID] [varchar](12) NULL,
	[SSN] [varchar](12) NULL,
	[ParentID] [int] NOT NULL,
	[TypeId] [int] NULL,
 CONSTRAINT [PK_participant] PRIMARY KEY CLUSTERED 
(
	[ParticipantID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Participantaddress]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Participantaddress](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[ParticipantlID] [int] NULL,
	[AddressTypeID] [int] NULL,
	[Line1] [varchar](50) NULL,
	[Line2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Zip] [varchar](50) NULL,
	[Incareof] [varchar](50) NULL,
 CONSTRAINT [PK_participantaddress] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Participantentityparty]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Participantentityparty](
	[ParticipantEntityPartyID] [int] IDENTITY(1,1) NOT NULL,
	[ParticipantID] [int] NULL,
	[fName] [varchar](50) NULL,
	[mName] [varchar](50) NULL,
	[lName] [varchar](50) NULL,
	[SSN] [varchar](50) NULL,
 CONSTRAINT [PK_participantentityparty] PRIMARY KEY CLUSTERED 
(
	[ParticipantEntityPartyID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Participantreservation]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Participantreservation](
	[DocReservationID] [int] IDENTITY(1,1) NOT NULL,
	[ParticipantID] [int] NULL,
	[Details] [varchar](350) NULL,
 CONSTRAINT [PK_participantreservation] PRIMARY KEY CLUSTERED 
(
	[DocReservationID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Participantrole]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Participantrole](
	[DocRoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NULL,
	[DocTypeID] [int] NULL,
	[IsSeller] [bit] NOT NULL,
 CONSTRAINT [PK_participantrole] PRIMARY KEY CLUSTERED 
(
	[DocRoleID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Participanttype]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Participanttype](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_participanttype] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[States]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[OBJECTID] [int] NULL,
	[STATE_ID] [int] NOT NULL,
	[STATE_NAME] [nvarchar](50) NULL,
	[STATE_FIPS] [nvarchar](3) NULL,
	[SUB_REGION] [nvarchar](50) NULL,
	[STATE_ABBR] [nvarchar](2) NULL,
	[POP2000] [decimal](18, 0) NULL,
	[POP2004] [decimal](18, 0) NULL,
	[POP00_SQMI] [decimal](18, 0) NULL,
	[POP04_SQMI] [decimal](18, 0) NULL,
	[WHITE] [decimal](18, 0) NULL,
	[BLACK] [decimal](18, 0) NULL,
	[AMERI_ES] [decimal](18, 0) NULL,
	[ASIAN] [decimal](18, 0) NULL,
	[HAWN_PI] [decimal](18, 0) NULL,
	[OTHER] [decimal](18, 0) NULL,
	[MULT_RACE] [decimal](18, 0) NULL,
	[HISPANIC] [decimal](18, 0) NULL,
	[MALES] [decimal](18, 0) NULL,
	[FEMALES] [decimal](18, 0) NULL,
	[AGE_UNDER5] [decimal](18, 0) NULL,
	[AGE_5_17] [decimal](18, 0) NULL,
	[AGE_18_21] [decimal](18, 0) NULL,
	[AGE_22_29] [decimal](18, 0) NULL,
	[AGE_30_39] [decimal](18, 0) NULL,
	[AGE_40_49] [decimal](18, 0) NULL,
	[AGE_50_64] [decimal](18, 0) NULL,
	[AGE_65_UP] [decimal](18, 0) NULL,
	[MED_AGE] [decimal](18, 0) NULL,
	[MED_AGE_M] [decimal](18, 0) NULL,
	[MED_AGE_F] [decimal](18, 0) NULL,
	[HOUSEHOLDS] [decimal](18, 0) NULL,
	[AVE_HH_SZ] [decimal](18, 0) NULL,
	[HSEHLD_1_M] [decimal](18, 0) NULL,
	[HSEHLD_1_F] [decimal](18, 0) NULL,
	[MARHH_CHD] [decimal](18, 0) NULL,
	[MARHH_NO_C] [decimal](18, 0) NULL,
	[MHH_CHILD] [decimal](18, 0) NULL,
	[FHH_CHILD] [decimal](18, 0) NULL,
	[FAMILIES] [decimal](18, 0) NULL,
	[AVE_FAM_SZ] [decimal](18, 0) NULL,
	[HSE_UNITS] [decimal](18, 0) NULL,
	[VACANT] [decimal](18, 0) NULL,
	[OWNER_OCC] [decimal](18, 0) NULL,
	[RENTER_OCC] [decimal](18, 0) NULL,
	[NO_FARMS97] [decimal](18, 0) NULL,
	[AVG_SIZE97] [decimal](18, 0) NULL,
	[CROP_ACR97] [decimal](18, 0) NULL,
	[AVG_SALE97] [decimal](18, 0) NULL,
	[SQMI] [decimal](18, 0) NULL,
	[Shape_Leng] [decimal](18, 0) NULL,
	[Shape_Area] [decimal](18, 0) NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[STATE_ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Tract]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tract](
	[TractID] [int] IDENTITY(1,1) NOT NULL,
	[DocID] [int] NULL,
	[RefName] [varchar](50) NULL,
	[CalledAC] [decimal](18, 0) NULL,
	[ScopePlotUrl] [varchar](50) NULL,
 CONSTRAINT [PK_tract] PRIMARY KEY CLUSTERED 
(
	[TractID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Tractexception]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tractexception](
	[TractExceptionsID] [int] IDENTITY(1,1) NOT NULL,
	[TractID] [int] NULL,
	[RefName] [varchar](50) NULL,
	[CalledAC] [varchar](50) NULL,
 CONSTRAINT [PK_tractexception] PRIMARY KEY CLUSTERED 
(
	[TractExceptionsID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Units]    Script Date: 02/25/2007 21:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Units](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [char](50) NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [doc-capture]
GO

ALTER TABLE [dbo].[Countys]  WITH CHECK ADD  CONSTRAINT [FK_Countys_States] FOREIGN KEY([STATE_ID])
REFERENCES [dbo].[States] ([STATE_ID])