SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CreationDate] [datetime] NOT NULL,
    [RealmId] [varchar](50) NOT NULL,
    [AppDbId] [varchar](50) NOT NULL,
    [IsQBO] [bit] NOT NULL,
    [DbName] [varchar](50) NOT NULL,
    [DbLogin] [varchar](50) NOT NULL,
    [DbPassword] [varchar](50) NOT NULL,
    [Name] [varchar](50) NULL,
    [ContactPerson] [varchar](100) NULL,
    [Email] [varchar](50) NULL,
    [Phone] [varchar](50) NULL,
    [Description] [varchar](500) NULL,
    [IsTrackingPhonesInited] [bit] NOT NULL,
    [IsCampaignsInited] [bit] NOT NULL,
    [IsOAuthInited] [bit] NOT NULL,
    [IsCompanyProfileInited] [bit] NOT NULL,
    [BillingStatus] [varchar](50) NULL,
    [LastPaymentDate] [datetime] NULL,
 CONSTRAINT [PK_ServmanCustomer] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Session]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Session](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CustomerId] [int] NOT NULL,
    [QbUserId] [varchar](50) NOT NULL,
    [Ticket] [varchar](50) NOT NULL,
    [AppToken] [varchar](50) NOT NULL,
    [IntuitTicket] [varchar](250) NOT NULL,
    [SessionStart] [datetime] NOT NULL,
    [IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ServmanSession] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OAuthConnection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OAuthConnection](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CustomerId] [int] NOT NULL,
    [ParentConsumerKey] [varchar](50) NOT NULL,
    [RequestTokenUrl] [varchar](250) NOT NULL,
    [DynamicKeyRetrievalUrl] [varchar](250) NOT NULL,
    [AccessTokenUrl] [varchar](250) NOT NULL,
    [AuthorizeRequestUrl] [varchar](250) NOT NULL,
    [ConsumerKey] [varchar](50) NOT NULL,
    [ConsumerSecret] [varchar](50) NOT NULL,
    [AccessToken] [varchar](150) NOT NULL,
    [AccessTokenSecret] [varchar](150) NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    [IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_OAuthConnection] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QbmsTransaction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QbmsTransaction](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CustomerId] [int] NOT NULL,
    [Ticket] [varchar](150) NOT NULL,
    [OpId] [varchar](50) NOT NULL,
    [Amount] [money] NOT NULL,
    [OpType] [varchar](50) NULL,
    [Status] [varchar](50) NULL,
    [StatusCode] [varchar](50) NULL,
    [StatusMessage] [varchar](250) NULL,
    [TxnType] [varchar](50) NULL,
    [TxnTimestamp] [varchar](50) NULL,
    [MaskedCCN] [varchar](50) NULL,
    [AuthCode] [varchar](50) NULL,
    [TxnId] [varchar](50) NULL,
 CONSTRAINT [PK_QbmsTransaction] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Session_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Customer]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OAuthConnection_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[OAuthConnection]'))
ALTER TABLE [dbo].[OAuthConnection]  WITH CHECK ADD  CONSTRAINT [FK_OAuthConnection_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[OAuthConnection] CHECK CONSTRAINT [FK_OAuthConnection_Customer]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QbmsTransaction_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[QbmsTransaction]'))
ALTER TABLE [dbo].[QbmsTransaction]  WITH CHECK ADD  CONSTRAINT [FK_QbmsTransaction_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[QbmsTransaction] CHECK CONSTRAINT [FK_QbmsTransaction_Customer]
